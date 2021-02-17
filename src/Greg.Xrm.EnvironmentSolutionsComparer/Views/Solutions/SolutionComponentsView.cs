using BrightIdeasSoftware;
using Greg.Xrm.Async;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Theming;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public partial class SolutionComponentsView : DockContent
	{
		private readonly SolutionComponentsViewModel viewModel;

		public SolutionComponentsView(ILog log, IThemeProvider themeProvider, IMessenger messenger, IAsyncJobScheduler scheduler)
		{
			InitializeComponent();


			var theme = themeProvider.GetCurrentTheme();
			this.tree.Font = theme.PanelFont;

			this.viewModel = new SolutionComponentsViewModel(scheduler, log, messenger);
			this.tCompareComponents.Bind(_ => _.Enabled, this.viewModel, _ => _.CanExecuteComparison);
			this.tCompareComponents.Click += (s, e) => this.viewModel.CompareAsync();

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
				Width = 400,
				AspectGetter = _ => ((SolutionComponentNode)_).Label + ((_ is SolutionComponentComposite node) ? $" ({node.Count})" : string.Empty)
			};
			this.tree.Columns.Add(col);

			col = new OLVColumn
			{
				Text = "Object Id",
				Width = 200,
				AspectGetter = _ => (_ is SolutionComponentLeaf model) ? model.ObjectId.ToString() : string.Empty
			};
			this.tree.Columns.Add(col);


			foreach (var connection in this.viewModel.Connections)
			{
				col = new OLVColumn
				{
					Text = connection.Detail.ConnectionName,
					Width = 150,
					AspectGetter = _ => (_ is SolutionComponentLeaf model) && model.Environments.Contains(connection) ? "YEP" : string.Empty
				};
				this.tree.Columns.Add(col);
			}

			this.tree.EndUpdate();
		}

		private void RefreshGrid()
		{
			this.tree.BeginUpdate();

			this.tree.Roots = this.viewModel.Grid.OrderBy(_ => _.Label);

			this.tree.EndUpdate();
		}
	}
}
