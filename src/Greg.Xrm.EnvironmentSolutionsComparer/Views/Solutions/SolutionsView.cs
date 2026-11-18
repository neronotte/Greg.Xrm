using Greg.Xrm.Async;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Theming;
using System;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using HorizontalAlignment = System.Windows.Forms.HorizontalAlignment;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public partial class SolutionsView : DockContent
	{
		private readonly SolutionsViewModel viewModel;

		public SolutionsView(ILog log, IThemeProvider themeProvider, IMessenger messenger, IAsyncJobScheduler scheduler)
		{
			InitializeComponent();


			var theme = themeProvider.GetCurrentTheme();
			theme.ApplyTo(this.listView);

			this.lSuccess.BackColor = Constants.SuccessBackColor;
			this.lSuccess.ForeColor = Constants.SuccessForeColor;
			this.lSuccess.Font = theme.PanelFont;
			this.lSuccess.Text = "All envs, same version";

			this.lWarn.BackColor = Constants.WarnBackColor;
			this.lWarn.ForeColor = Constants.WarnForeColor;
			this.lWarn.Font = theme.PanelFont;
			this.lWarn.Text = "Differences between envs";

			this.lError.BackColor = Constants.ErrorBackColor;
			this.lError.ForeColor = Constants.ErrorForeColor;
			this.lError.Font = theme.PanelFont;
			this.lError.Text = "Missing from env";

			this.lGray.BackColor = Constants.GrayBackColor;
			this.lGray.ForeColor = Constants.GrayForeColor;
			this.lGray.Font = theme.PanelFont;
			this.lGray.Text = "Present but not visible";


			this.viewModel = new SolutionsViewModel(log, messenger, scheduler);
			this.viewModel.RefreshGrid += OnRefreshGrid;

			this.tExport.Bind(_ => _.Enabled, this.viewModel, _ => _.CanExport);
			this.tExport.Click += (s, e) => this.viewModel.Export();

			this.tShowOnlyVisible.Bind(_ => _.Text, this.viewModel, _ => _.ShowOnlyVisibleSolutionsText);
			this.tShowOnlyVisible.Click += (s, e) => this.viewModel.ShowOnlyVisibleSolutions = !this.viewModel.ShowOnlyVisibleSolutions;

			this.tRefreshSolutionList.BindCommand(() => this.viewModel.RefreshCommand);

			this.listView.SelectedIndexChanged += (s, e) =>
			{
				if (this.listView.SelectedItems.Count == 0) return;

				var item = (SolutionRow)this.listView.SelectedItems[0].Tag;
				messenger.Send(new SolutionSelectedMessage(item));
			};
		}

		private void OnRefreshGrid(object sender, EventArgs e)
		{
			var grid = this.viewModel.Grid;

			this.listView.BeginUpdate();

			this.listView.Columns.Clear();
			this.listView.Columns.Add($"Solutions ({grid.Rows.Count})", 300, HorizontalAlignment.Left);
			this.listView.Columns.Add($"Publisher", 300, HorizontalAlignment.Left);
			foreach (var env in grid.Environments)
			{
				this.listView.Columns.Add(env.Detail.ConnectionName, 150, HorizontalAlignment.Center);
			}



			this.listView.Items.Clear();
			foreach (var gridRow in grid.Rows.OrderBy(_ => _.SolutionUniqueName))
			{
				if (this.viewModel.ShowOnlyVisibleSolutions && gridRow.Any(_ => !_.isvisible))
					continue;


				var item = this.listView.Items.Add(gridRow.SolutionUniqueName);
				item.UseItemStyleForSubItems = false;
				item.SubItems.Add(gridRow.SolutionPublisher);
				item.Tag = gridRow;

				foreach (var env in grid.Environments)
				{
					var solution = gridRow[env.Detail.ConnectionName];

					if (solution == null)
					{
						var subitem = item.SubItems.Add("Not present");
						subitem.BackColor = Constants.ErrorBackColor;
						subitem.ForeColor = Constants.ErrorForeColor;
					}
					else
					{
						var text = solution.version + (solution.ismanaged ? " (M)" : " (UM)");
						var subitem = item.SubItems.Add(text);
						subitem.Tag = solution;

						if (!solution.isvisible)
						{
							subitem.BackColor = Constants.GrayBackColor;
							subitem.ForeColor = Constants.GrayForeColor;
						}
						else if (gridRow.AllSameVersion)
						{
							subitem.BackColor = Constants.SuccessBackColor;
							subitem.ForeColor = Constants.SuccessForeColor;
						}
						else
						{
							subitem.BackColor = Constants.WarnBackColor;
							subitem.ForeColor = Constants.WarnForeColor;
						}
					}
				}


				var backColor = item.SubItems
					.OfType<ListViewItem.ListViewSubItem>()
					.Skip(2)
					.Select(_ => _.BackColor)
					.Distinct()
					.ToList();

				if (backColor.Count == 1)
				{
					item.BackColor = backColor[0];
					item.ForeColor = item.SubItems[2].ForeColor;
					item.SubItems[1].BackColor = backColor[0];
					item.SubItems[1].ForeColor = item.SubItems[2].ForeColor;
				}
				else
				{
					item.BackColor = Constants.WarnBackColor;
					item.ForeColor = Constants.WarnForeColor;
					item.SubItems[1].BackColor = Constants.WarnBackColor;
					item.SubItems[1].ForeColor = Constants.WarnForeColor;
				}
			}


			this.listView.EndUpdate();
		}

		private void OnFindKeyUp(object sender, KeyEventArgs e)
		{
			var search = this.tFind.Text;
			if (string.IsNullOrWhiteSpace(search)) return;

			var contains = search.StartsWith("*");
			search = search.Trim('*');

			var offset = 0;
			if (e.KeyCode == Keys.F3)
			{
				offset = this.listView.SelectedIndices.Count > 0 ? this.listView.SelectedIndices[0] + 1 : offset;
			}

			for (var i = 0; i < this.listView.Items.Count; i++)
			{
				var index = (i + offset) % this.listView.Items.Count;

				var item = this.listView.Items[index];
				if (contains)
				{
					if (item.Text.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
					{
						this.listView.SelectedIndices.Clear();
						this.listView.SelectedIndices.Add(item.Index);
						item.EnsureVisible();
						return;
					}
				}
				else
				{
					if (item.Text.IndexOf(search, StringComparison.OrdinalIgnoreCase) == 0)
					{
						this.listView.SelectedIndices.Clear();
						this.listView.SelectedIndices.Add(item.Index);
						item.EnsureVisible();
						return;
					}
				}
			}
		}
	}
}
