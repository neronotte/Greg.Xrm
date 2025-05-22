using Greg.Xrm.Async;
using Greg.Xrm.EnvironmentSolutionsComparer.Messaging;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public class SolutionsViewModel : ViewModel
	{
		private readonly ILog log;
		private readonly IMessenger messenger;
		private readonly IAsyncJobScheduler scheduler;

		public SolutionsViewModel(ILog log, IMessenger messenger, IAsyncJobScheduler scheduler)
		{
			this.log = log;
			this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
			this.scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));

			this.Grid = new SolutionGrid();
			this.AllowRequests = true;

			this.messenger.WhenObject<MainViewModel>()
				.ChangesProperty(_ => _.AllowRequests)
				.Execute(m => this.AllowRequests = (bool)m.NewValue);

			this.WhenChanges(() => this.AllowRequests).ChangesAlso(() => CanExport);
			this.WhenChanges(() => this.ShowOnlyVisibleSolutions)
				.ChangesAlso(() => this.ShowOnlyVisibleSolutionsText)
				.Execute(o => this.RefreshGrid?.Invoke(this, EventArgs.Empty));

			this.RefreshGrid += (s, e) => this.OnPropertyChanged(nameof(CanExport), CanExport);


			this.messenger.Register<ConnectionAddedMessage>(OnConnectionAdded);
			this.messenger.Register<ConnectionRemovedMessage>(OnConnectionRemoved);

		}

		public bool AllowRequests
		{
			get => Get<bool>();
			private set => Set<bool>(value);
		}

		public bool ShowOnlyVisibleSolutions
		{
			get => Get<bool>();
			set => Set(value);
		}

		public string ShowOnlyVisibleSolutionsText
		{
			get
			{
				if (this.ShowOnlyVisibleSolutions)
				{
					return "Show invisible solutions";
				}
				return "Hide invisible solutions";
			}
		}

		public SolutionGrid Grid { get; }

		public bool CanExport
		{
			get => this.AllowRequests && this.Grid.Environments.Count > 0 && this.Grid.Rows.Count > 0;
		}


		public event EventHandler RefreshGrid;



		private void OnConnectionAdded(ConnectionAddedMessage obj)
		{
			var repository = Solution.GetRepository(this.log, obj.Model.Crm, obj.Model.Detail.ConnectionName);

			this.scheduler.Enqueue(new XrmToolBox.Extensibility.WorkAsyncInfo
			{
				Message = "Loading solution list from environment " + obj.Model.Detail.ConnectionName,
				Work = (w, e) =>
				{
					var solutionList = repository.GetSolutions();
					AnalyzeSolutions(solutionList, obj.Index, obj.Model);
				},
				PostWorkCallBack = e =>
				{
					if (e.Error != null)
					{
						this.log.Error(e.Error.Message, e.Error);
						return;
					}

					this.RefreshGrid?.Invoke(this, EventArgs.Empty);
				}
			});
		}

		private void AnalyzeSolutions(IReadOnlyCollection<Solution> solutionList, int solutionIndex, ConnectionModel model)
		{
			this.Grid.Environments.Insert(solutionIndex, model);

			foreach (var solution in solutionList)
			{
				var row = this.Grid.Rows.FirstOrDefault(x => string.Equals(x.SolutionUniqueName, solution.uniquename, StringComparison.Ordinal));
				if (row == null)
				{
					row = new SolutionRow(solution.uniquename, solution.publisherid?.Name ?? string.Empty);
					this.Grid.Rows.Add(row);
				}

				row[model.Detail.ConnectionName] = solution;
			}
		}

		private void OnConnectionRemoved(ConnectionRemovedMessage obj)
		{
			var model = obj.Model;
			this.Grid.Environments.Remove(model);

			var rowsToRemove = new List<SolutionRow>();

			foreach (var row in this.Grid.Rows)
			{
				row[model.Detail.ConnectionName] = null;

				if (row.IsEmpty)
				{
					rowsToRemove.Add(row);
				}
			}

			foreach (var row in rowsToRemove)
			{
				this.Grid.Rows.Remove(row);
			}

			this.RefreshGrid?.Invoke(this, EventArgs.Empty);
		}

		internal void Export()
		{
			string fileName;
			using (var dialog = new SaveFileDialog())
			{
				dialog.Filter = "Excel file (*.xlsx)|*.xlsx";
				dialog.Title = "Export";
				dialog.OverwritePrompt = true;

				if (dialog.ShowDialog() != DialogResult.OK) return;

				fileName = dialog.FileName;
			}


			this.scheduler.Enqueue(new XrmToolBox.Extensibility.WorkAsyncInfo
			{
				Message = "Generating excel report, please wait... ",
				Work = (w, e) =>
				{
					GenerateExcelReport(fileName);
				},
				PostWorkCallBack = e =>
				{
					if (e.Error != null)
					{
						this.log.Error(e.Error.Message, e.Error);
						return;
					}

					Process.Start(fileName);
				}
			});

		}

		private void GenerateExcelReport(string fileName)
		{
			using (var package = new ExcelPackage())
			{
				var ws = package.Workbook.Worksheets.Add("Solutions");

				var row = 1;
				var col = 0;

				ws.Cells[row, ++col].Value = "Solution name";
				ws.Cells[row, ++col].Value = "Publisher";

				foreach (var env in this.Grid.Environments)
				{
					ws.Cells[row, ++col].Value = env.Detail.ConnectionName;
				}

				foreach (var gridRow in this.Grid.Rows.OrderBy(_ => _.SolutionUniqueName))
				{
					if (this.ShowOnlyVisibleSolutions && gridRow.Any(_ => !_.isvisible))
						continue;


					row++;
					col = 0;
					ws.Cells[row, ++col].Value = gridRow.SolutionUniqueName;
					ws.Cells[row, ++col].Value = gridRow.SolutionPublisher;

					var backColors = new List<Color>();
					var foreColors = new List<Color>();

					foreach (var env in this.Grid.Environments)
					{
						var solution = gridRow[env.Detail.ConnectionName];
						if (solution == null)
						{
							ws.Cells[row, ++col].Value = "Not present";
							ws.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
							ws.Cells[row, col].Style.Fill.PatternType = ExcelFillStyle.Solid;
							ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(Constants.ErrorBackColor);
							ws.Cells[row, col].Style.Font.Color.SetColor(Constants.ErrorForeColor);
							backColors.Add(Constants.ErrorBackColor);
							foreColors.Add(Constants.ErrorForeColor);
						}
						else
						{
							var text = solution.version + (solution.ismanaged ? " (M)" : " (UM)");
							ws.Cells[row, ++col].Value = text;
							ws.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

							var fillStyle = solution.ismanaged ? ExcelFillStyle.Solid : ExcelFillStyle.LightTrellis;

							if (!solution.isvisible)
							{
								ws.Cells[row, col].Style.Fill.PatternType = fillStyle;
								ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(Constants.GrayBackColor);
								ws.Cells[row, col].Style.Font.Color.SetColor(Constants.GrayForeColor);
								backColors.Add(Constants.GrayBackColor);
								foreColors.Add(Constants.GrayForeColor);
							}
							else if (gridRow.AllSameVersion)
							{
								ws.Cells[row, col].Style.Fill.PatternType = fillStyle;
								ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(Constants.SuccessBackColor);
								ws.Cells[row, col].Style.Font.Color.SetColor(Constants.SuccessForeColor);
								backColors.Add(Constants.SuccessBackColor);
								foreColors.Add(Constants.SuccessForeColor);
							}
							else
							{
								ws.Cells[row, col].Style.Fill.PatternType = fillStyle;
								ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(Constants.WarnBackColor);
								ws.Cells[row, col].Style.Font.Color.SetColor(Constants.WarnForeColor);
								backColors.Add(Constants.WarnBackColor);
								foreColors.Add(Constants.WarnForeColor);
							}
						}
					}

					var backColor = backColors
						.Distinct()
						.ToList();

					if (backColor.Count == 1)
					{
						ws.Cells[row, 1, row, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
						ws.Cells[row, 1, row, 2].Style.Fill.BackgroundColor.SetColor(backColors[0]);
						ws.Cells[row, 1, row, 2].Style.Font.Color.SetColor(foreColors[0]);
					}
					else
					{
						ws.Cells[row, 1, row, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
						ws.Cells[row, 1, row, 2].Style.Fill.BackgroundColor.SetColor(Constants.WarnBackColor);
						ws.Cells[row, 1, row, 2].Style.Font.Color.SetColor(Constants.WarnForeColor);
					}
				}


				var table = ws.Tables.Add(ws.Cells[1, 1, row, col], "Table1");
				table.TableStyle = OfficeOpenXml.Table.TableStyles.Medium6;

				for (int i = 1; i <= col; i++)
				{
					ws.Column(i).AutoFit();
				}



				using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
				{
					package.SaveAs(stream);
				}
			}
		}
	}
}
