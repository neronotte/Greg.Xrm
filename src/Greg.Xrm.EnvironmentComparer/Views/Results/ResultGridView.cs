using Greg.Xrm.EnvironmentComparer.Engine;
using Greg.Xrm.EnvironmentComparer.Help;
using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.Theming;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	public partial class ResultGridView : DockContent
	{
		private IReadOnlyCollection<ObjectComparison<Entity>> results;
		private readonly IThemeProvider themeProvider;
		private readonly IMessenger messenger;

		private readonly ResultGridViewModel viewModel;



		public ResultGridView(IThemeProvider themeProvider, IMessenger messenger)
		{
			InitializeComponent();

			this.RegisterHelp(messenger, Topics.ResultGrid);

			this.themeProvider = themeProvider ?? throw new ArgumentNullException(nameof(themeProvider));
			this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));

			this.viewModel = new ResultGridViewModel(messenger);

			this.cmiCopyToEnv1.BindCommand(() => this.viewModel.CopyToEnv1Command, () => 1, CommandExecuteBehavior.EnabledAndVisible);
			this.cmiCopyToEnv2.BindCommand(() => this.viewModel.CopyToEnv2Command, () => 2, CommandExecuteBehavior.EnabledAndVisible);

			this.cmiDeleteFromEnv1.BindCommand(() => this.viewModel.DeleteFromEnv1Command, () => 1, CommandExecuteBehavior.EnabledAndVisible);
			this.cmiDeleteFromEnv2.BindCommand(() => this.viewModel.DeleteFromEnv2Command, () => 2, CommandExecuteBehavior.EnabledAndVisible);

			this.cmiCompare.Bind(_ => _.Enabled, this.viewModel, _ => _.IsCompareEnabled);

			this.ApplyTheme();


			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				.ChangesProperty(_ => _.ConnectionName1)
				.Execute(e =>
				{
					var env1name = e.GetNewValue<string>();
					var env1 = (string.IsNullOrWhiteSpace(env1name) ? "ENV1" : env1name);
					this.cEnv1.Text = env1 + " values";
					this.lLegend2.Text = "Missing on " + env1;
					this.cmiCopyToEnv1.Text = "Copy to " + env1;
					this.cmiDeleteFromEnv1.Text = "Delete from " + env1;
				});

			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				.ChangesProperty(_ => _.ConnectionName2)
				.Execute(e =>
				{
					var env2name = e.GetNewValue<string>();
					var env2 = (string.IsNullOrWhiteSpace(env2name) ? "ENV2" : env2name);
					this.cEnv2.Text = env2 + " values";
					this.lLegend3.Text = "Missing on " + env2;
					this.cmiCopyToEnv2.Text = "Copy to " + env2;
					this.cmiDeleteFromEnv2.Text = "Delete from " + env2;
				});

			this.viewModel.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(this.viewModel.Results))
				{
					this.results = this.viewModel.Results;
					this.OnResultsChanged();
				}
			};
			this.messenger.Register<ResultUpdatedMessage>(e =>
			{
				var item = this.listView1.Items.Cast<ListViewItem>().FirstOrDefault(_ => _.Tag == e.Result);
				if (item == null)
					return;

				if (e.Result.IsActioned())
				{
					item.BackColor = Constants.BackColorForActioned;
				}
				else
				{
					item.BackColor = e.Result.Result.GetColor();
				}
			});
		}


		private void ApplyTheme()
		{
			var theme = this.themeProvider.GetCurrentTheme();

			this.listView1.BackColor = theme.PanelBackgroundColor;
			this.listView1.Font = theme.PanelFont;

			this.lLegend1.BackColor = Constants.BackColorForEquals;
			this.lLegend1.Font = theme.PanelFont;

			this.lLegend2.BackColor = Constants.BackColorForLeftMissing;
			this.lLegend2.Font = theme.PanelFont;

			this.lLegend3.BackColor = Constants.BackColorForRightMissing;
			this.lLegend3.Font = theme.PanelFont;

			this.lLegend4.BackColor = Constants.BackColorForMatchingButDifferent;
			this.lLegend4.Font = theme.PanelFont;

			this.lLegend5.BackColor = Constants.BackColorForActioned;
			this.lLegend5.Font = theme.PanelFont;
		}

		private void OnResultsChanged()
		{
			if (this.InvokeRequired)
			{
				Action d = OnResultsChanged;
				this.BeginInvoke(d);
				return;
			}

			this.listView1.BeginUpdate();
			this.listView1.Items.Clear();

			if (this.results == null || this.results.Count == 0)
			{
				this.listView1.EndUpdate();
				return;
			}

			foreach (var result in this.results.OrderBy(x => x.Key))
			{
				var color = result.Result.GetColor();

				if (result.IsActioned())
				{
					color = Constants.BackColorForActioned;
				}

				var item = this.listView1.Items.Add(result.Key);
				item.BackColor = color;
				item.Tag = result;

				var subItem = item.SubItems.Add(result.Result.ToString());
				subItem.Tag = result;

				subItem = item.SubItems.Add(result.DifferentProperties.OrderBy(_ => _.FieldName).Select(_ => _.FieldName).Join(" | "));
				subItem.Tag = result;

				subItem = item.SubItems.Add(result.DifferentProperties.OrderBy(_ => _.FieldName).Select(_ => _.FormattedValue1).Join(" | "));
				subItem.Tag = result;

				subItem = item.SubItems.Add(result.DifferentProperties.OrderBy(_ => _.FieldName).Select(_ => _.FormattedValue2).Join(" | "));
				subItem.Tag = result;
			}

			this.listView1.EndUpdate();
			this.Show();
		}




		

		private void OnRecordSelectionChanged(object sender, EventArgs e)
		{
			var selectedResults = this.listView1.SelectedItems
				.Cast<ListViewItem>()
				.Select(x => x.Tag)
				.Cast<ObjectComparison<Entity>>()
				.ToArray();

			this.viewModel.SelectedResults = selectedResults;

			if (selectedResults.Length > 0)
			{
				this.messenger.Send(new CompareResultRecordSelected(selectedResults[0]));
			}
		}

		private void OnResultKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F12 && this.listView1.SelectedItems.Count > 0)
			{
				this.messenger.Send<HighlightResultRecord>();
			}
		}

		private void OnCompareClick(object sender, EventArgs e)
		{
			if (this.listView1.SelectedItems.Count > 0)
			{
				this.messenger.Send<HighlightResultRecord>();
			}
		}
	}
}
