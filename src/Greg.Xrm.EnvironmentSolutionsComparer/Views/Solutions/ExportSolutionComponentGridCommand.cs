using Greg.Xrm.Async;
using Greg.Xrm.Logging;
using Greg.Xrm.Views;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public class ExportSolutionComponentGridCommand : CommandBase
	{
		private readonly ILog log;
		private readonly IAsyncJobScheduler scheduler;
		private readonly SolutionComponentsViewModel viewModel;

		public ExportSolutionComponentGridCommand(ILog log, IAsyncJobScheduler scheduler, SolutionComponentsViewModel viewModel)
		{
			this.log = log;
			this.scheduler = scheduler;
			this.viewModel = viewModel;

			this.viewModel.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(viewModel.Grid))
				{
					this.CanExecute = this.viewModel.Grid.Count > 0;
					this.RefreshCanExecute();
				}
			};
		}


		protected override void ExecuteInternal(object arg)
		{
			string fileName;
			using (var dialog =new SaveFileDialog())
			{
				dialog.Filter = "Excel files (*.xlsx)|*.xlsx";
				dialog.DefaultExt = "xlsx";
				dialog.AddExtension = true;
				dialog.Title = "Export Solution Components";
				if (dialog.ShowDialog() != DialogResult.OK) return;

				fileName = dialog.FileName;
			}

			try
			{
				if (File.Exists(fileName))
				{
					File.Delete(fileName);
				}
			}
			catch (IOException ex)
			{
				this.log.Error($"Failed to delete existing file {fileName}", ex);
				return;
			}


			this.scheduler.Enqueue(new WorkAsyncInfo
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
			using (log.Track("Exporting solution components"))
			{
				try
				{
					using (var package = new ExcelPackage())
					{
						var ws = package.Workbook.Worksheets.Add("Solution Components");


						var row = 1;
						var col = 0;

						ws.Cells[row, ++col].Value = "Component type";
						ws.Cells[row, ++col].Value = "Component name";

						foreach (var env in this.viewModel.Connections)
						{
							ws.Cells[row, ++col].Value = env.Detail.ConnectionName;
							ws.Cells[row, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
						}

						ws.Cells[row, ++col].Value = "Notes";


						foreach (var composite in this.viewModel.Grid.OfType<SolutionComponentComposite>())
						{
							foreach (var leaf in composite.OfType<SolutionComponentLeaf>())
							{
								row++;
								col = 0;

								ws.Cells[row, ++col].Value = composite.Label;
								ws.Cells[row, col].Style.Font.Bold = true;

								ws.Cells[row, ++col].Value = leaf.Label;
								ws.Cells[row, col].Style.Font.Bold = true;

								foreach (var env in this.viewModel.Connections)
								{
									if (leaf.Environments.TryGetValue(env, out var solutionComponent))
									{
										ws.Cells[row, ++col].Value = solutionComponent.objectid.ToString();
										ws.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
										ws.Cells[row, col].Style.Fill.PatternType = ExcelFillStyle.Solid;

										if (leaf.Environments.Select(x => x.Value.objectid).Distinct().Count() > 1)
										{
											ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(Constants.WarnBackColor);
											ws.Cells[row, col].Style.Font.Color.SetColor(Constants.WarnForeColor);
										}
										else
										{
											ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(Constants.SuccessBackColor);
											ws.Cells[row, col].Style.Font.Color.SetColor(Constants.SuccessForeColor);
										}

									}
									else
									{
										ws.Cells[row, ++col].Value = string.Empty;
										ws.Cells[row, col].Style.Fill.PatternType = ExcelFillStyle.Solid;
										ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(Constants.ErrorBackColor);
										ws.Cells[row, col].Style.Font.Color.SetColor(Constants.ErrorForeColor);
									}
								}

								ws.Cells[row, ++col].Value = CalculateNotes(composite, leaf);
							}
						}

						var table = ws.Tables.Add(ws.Cells[1, 1, row, col], "SolutionComponents");
						table.TableStyle = OfficeOpenXml.Table.TableStyles.Medium2;

						for (int i = 1; i <= col; i++)
						{
							ws.Column(i).AutoFit();
						}


						using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
						{
							package.SaveAs(stream);
						}
					}

					Process.Start(new ProcessStartInfo
					{
						FileName = fileName,
						UseShellExecute = true
					});
				}
				catch (IOException ex)
				{
					this.log.Error($"Error during export: " + ex.Message, ex);
				}
			}
		}

		private static string CalculateNotes(SolutionComponentComposite composite, SolutionComponentLeaf leaf)
		{
			var sb = new List<string>();

			var haveDistinctIds = leaf.Environments
							.GroupBy(kvp => kvp.Value.objectid)
							.Count() > 1;

			if (haveDistinctIds)
			{
				sb.Add("The unique identifier of the component changes between environments");
			}

			var howManyDistinctComponentTypes = composite
							.OfType<SolutionComponentLeaf>()
							.SelectMany(l => l.Environments.Select(kvp => kvp.Value.componenttype.Value)).Distinct()
							.Count();

			if (howManyDistinctComponentTypes > 1)
			{
				sb.Add($"The component type code differs between the environments");
			}

			return string.Join("; ", sb);
		}
	}
}
