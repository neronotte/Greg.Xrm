using BrightIdeasSoftware;
using Greg.Xrm.Async;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Theming;
using McTools.Xrm.Connection;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public partial class SolutionComponentsView : DockContent
	{
		private readonly ILog log;
		private readonly SolutionComponentsViewModel viewModel;

		public SolutionComponentsView(
			ILog log, 
			IThemeProvider themeProvider, 
			IMessenger messenger, 
			IAsyncJobScheduler scheduler)
		{
			InitializeComponent();
			this.log = log;

			var theme = themeProvider.GetCurrentTheme();
			this.tree.Font = theme.PanelFont;
			this.tree.FullRowSelect = false;

			this.viewModel = new SolutionComponentsViewModel(scheduler, log, messenger);
			this.tCompareComponents.Bind(_ => _.Enabled, this.viewModel, _ => _.CanExecuteComparison);
			this.tCompareComponents.Click += (s, e) => this.viewModel.CompareAsync();
			this.tExport.BindCommand(() => this.viewModel.ExportCommand);

			this.tSolutionName.Bind(_ => _.Text, this.viewModel, _ => _.SolutionName);

			this.viewModel.Connections.CollectionChanged += OnConnectionListChanged;

			this.viewModel.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(viewModel.Grid))
				{
					RefreshGrid();
				}
			};

			this.tree.ParentGetter = _ => ((SolutionComponentNode)_).Parent;
			this.tree.CanExpandGetter = _ => (_ is SolutionComponentComposite c) && c.Count > 0;
			this.tree.ChildrenGetter = _ => (_ is SolutionComponentComposite c) ? c : (IReadOnlyCollection<SolutionComponentNode>)Array.Empty<SolutionComponentNode>();

			this.tree.UseCellFormatEvents = true;
			this.tree.FormatRow += OnTreeFormatRow;
			this.tree.FormatCell += OnTreeFormatCell;
			this.tree.CellRightClick += OnTreeCellRightClick;


			this.ResetGridColumns();
		}

		private void OnConnectionListChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			ResetGridColumns();
		}

		private void ResetGridColumns()
		{
			this.tree.BeginUpdate();

			this.tree.Roots = null;
			this.tree.Columns.Clear();



			var col = new OLVColumn
			{
				Text = "Component name",
				Width = 500,
				AspectGetter = _ => ((SolutionComponentNode)_).Label + ((_ is SolutionComponentComposite node) ? $" ({node.Count})" : string.Empty)
			};
			this.tree.Columns.Add(col);

			foreach (var connection in this.viewModel.Connections)
			{
				col = new OLVColumn
				{
					Text = connection.Detail.ConnectionName,
					Tag = connection,
					Width = 150,
					TextAlign = System.Windows.Forms.HorizontalAlignment.Center,
					AspectGetter = _ =>
					{
						if (_ is SolutionComponentLeaf leaf)
						{
							if (leaf.Environments.TryGetValue(connection, out var solutionComponent))
							{
								return solutionComponent.objectid.ToString();
							}

							return string.Empty;
						}

						if (_ is SolutionComponentComposite composite)
						{
							var count = composite.OfType<SolutionComponentLeaf>().Count(x => x.Environments.ContainsKey(connection));
							var total = composite.Count;
							var percent = (count * 100d / total);
							var percentString = percent.ToString("0.##");

							return $"{count}/{total} ({percentString}%)";
						}

						return string.Empty;
					}
				};
				this.tree.Columns.Add(col);
			}

			col = new OLVColumn
			{
				Text = "Notes",
				Width = 500,
				AspectGetter = x =>
				{
					if (x is SolutionComponentLeaf leaf)
					{
						var haveDistinctIds = leaf.Environments
							.GroupBy(kvp => kvp.Value.objectid)
							.Count() > 1;

						if (haveDistinctIds)
						{
							return "The unique identifier of the component changes between environments.";
						}
					}


					if (x is SolutionComponentComposite composite)
					{
						var howManyDistinctComponentTypes = composite
							.OfType<SolutionComponentLeaf>()
							.SelectMany(l => l.Environments.Select(kvp => kvp.Value.componenttype.Value)).Distinct()
							.Count();

						if (howManyDistinctComponentTypes > 1)
						{
							return $"The component type code differs between the environments.";
						}
					}


					return string.Empty;
				},
			};
			this.tree.Columns.Add(col);

			this.tree.EndUpdate();
		}



		private void RefreshGrid()
		{
			this.tree.BeginUpdate();

			this.tree.Roots = this.viewModel.Grid.OrderBy(_ => _.Label);

			this.tree.EndUpdate();
		}



		private void OnTreeCellRightClick(object sender, CellRightClickEventArgs e)
		{
			if (e.RowIndex <= 0) return;

			var text = e.SubItem?.ModelValue?.ToString() ?? e.Model?.ToString();
			if (string.IsNullOrWhiteSpace(text)) return;

			this.mCopy.Tag = text;
			this.contextMenu.Show(tree, e.Location);
		}

		private void OnTreeFormatRow(object sender, FormatRowEventArgs e)
		{
			if (e.Model == null) return;
			if (e.Model is SolutionComponentComposite)
			{
				e.Item.Font = new Font(e.Item.Font, System.Drawing.FontStyle.Bold);
			}
		}

		private void OnTreeFormatCell(object sender, FormatCellEventArgs e)
		{
			var backOk = Constants.SuccessBackColor;
			var backWa = Constants.WarnBackColor;
			var backKo = Constants.ErrorBackColor;
			var foreOk = Constants.SuccessForeColor;
			var foreWa = Constants.WarnForeColor;
			var foreKo = Constants.ErrorForeColor;



			if (e.Column == null) return;
			if (e.Model == null) return;
			if (!(e.Model is SolutionComponentLeaf leaf)) return;

			if (e.Column.Tag is ConnectionModel) 
			{
				if (string.IsNullOrWhiteSpace(e.CellValue?.ToString()))
				{
					e.SubItem.BackColor = backKo;
					e.SubItem.ForeColor = foreKo;
				}
				else if (leaf.Environments.Select(x => x.Value.objectid).Distinct().Count() > 1)
				{
					e.SubItem.BackColor = backWa;
					e.SubItem.ForeColor = foreWa;
				}
				else 
				{
					e.SubItem.BackColor = backOk;
					e.SubItem.ForeColor = foreOk;
				}
			}
		}

		private void OnCopyClick(object sender, EventArgs e)
		{
			var text = mCopy.Tag?.ToString();
			System.Windows.Clipboard.SetText(text);

			this.log.Debug($"Copied to clipboard: {text}");
		}
	}
}
