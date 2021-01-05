using Greg.Xrm.EnvironmentComparer.Actions;
using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.EnvironmentComparer.Model;
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
		private IReadOnlyCollection<Model.Comparison<Entity>> results;
		private readonly IThemeProvider themeProvider;
		private readonly IMessenger messenger;
		private string env1 = "ENV1", env2 = "ENV2";

		public ResultGridView(IThemeProvider themeProvider, IMessenger messenger)
		{
			InitializeComponent();
			
			this.cmiCopyToEnv1.Enabled = false;
			this.cmiCopyToEnv2.Enabled = false;
			this.themeProvider = themeProvider ?? throw new ArgumentNullException(nameof(themeProvider));
			this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
			this.ApplyTheme();


			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				.ChangesProperty(_ => _.ConnectionName1)
				.Execute(e =>
				{
					var env1name = e.GetNewValue<string>();
					this.env1 = (string.IsNullOrWhiteSpace(env1name) ? "ENV1" : env1name);
					this.cEnv1.Text = this.env1 + " values";
					this.cmiCopyToEnv1.Text = "Copy to " + this.env1;
				});
			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				 .ChangesProperty(_ => _.ConnectionName2)
				 .Execute(e =>
				 {
					 var env2name = e.GetNewValue<string>();
					 this.env2 = (string.IsNullOrWhiteSpace(env2name) ? "ENV2" : env2name);
					 this.cEnv2.Text = this.env2 + " values";
					 this.cmiCopyToEnv2.Text = "Copy to " + this.env2;
				 });
		}


		private void ApplyTheme()
		{
			var theme = this.themeProvider.GetCurrentTheme();

			this.listView1.BackColor = theme.PanelBackgroundColor;
			this.listView1.Font = theme.PanelFont;
		}


		public IReadOnlyCollection<Model.Comparison<Entity>> Results
		{
			get => this.results;
			set
			{
				this.results = value;
				this.OnResultsChanged();
			}
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

				var item = this.listView1.Items.Add(result.Key);
				item.BackColor = color;
				item.Tag = result;

				var subItem = item.SubItems.Add(result.Result.ToString());
				subItem.BackColor = color;
				subItem.Tag = result;

				subItem = item.SubItems.Add(result.DifferentProperties.OrderBy(_ => _.FieldName).Select(_ => _.FieldName).Join(" | "));
				subItem.BackColor = color;
				subItem.Tag = result;

				subItem = item.SubItems.Add(result.DifferentProperties.OrderBy(_ => _.FieldName).Select(_ => _.FormattedValue1).Join(" | "));
				subItem.BackColor = color;
				subItem.Tag = result;

				subItem = item.SubItems.Add(result.DifferentProperties.OrderBy(_ => _.FieldName).Select(_ => _.FormattedValue2).Join(" | "));
				subItem.BackColor = color;
				subItem.Tag = result;
			}



			this.listView1.EndUpdate();
		}




		

		private void OnRecordSelectionChanged(object sender, EventArgs e)
		{
			this.cmiCopyToEnv1.Enabled = false;
			this.cmiCopyToEnv2.Enabled = false;
			this.cmiCompare.Enabled = this.listView1.SelectedItems.Count == 1;

			if (this.listView1.SelectedItems.Count == 0) return;

			var item = this.listView1.SelectedItems[0];
			var result = (Model.Comparison<Entity>)item.Tag;
			this.messenger.Send(new CompareResultRecordSelected(result));

			this.cmiCopyToEnv1.Enabled = this.listView1.SelectedItems
				.Cast<ListViewItem>()
				.Select(x => x.Tag)
				.Cast<Model.Comparison<Entity>>()
				.All(x => x.Result == RecordComparisonResult.LeftMissing || x.Result == RecordComparisonResult.MatchingButDifferent);
			
			this.cmiCopyToEnv2.Enabled = this.listView1.SelectedItems
				.Cast<ListViewItem>()
				.Select(x => x.Tag)
				.Cast<Model.Comparison<Entity>>()
				.All(x => x.Result == RecordComparisonResult.RightMissing || x.Result == RecordComparisonResult.MatchingButDifferent); 
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

		private void OnCopyToEnv2Click(object sender, EventArgs e)
		{
			CopySelectedRowTo(this.env2, 1);
		}

		private void OnCopyToEnv1Click(object sender, EventArgs e)
		{
			CopySelectedRowTo(this.env1, 2);
		}

		private void CopySelectedRowTo(string envName, int from)
		{
			if (this.listView1.SelectedItems.Count == 0) return;

			var actionList = new List<IAction>();
			foreach (ListViewItem item in this.listView1.SelectedItems)
			{
				var result = item.Tag as Model.Comparison<Entity>;
				if (result == null) return;

				var entity = from == 1 ? result.Item1 : result.Item2;

				var action = new ActionCopyEntity
				{
					EntityKey = result.Key,
					EnvironmentName = envName,
					EntityName = entity.LogicalName,
					Entity = entity
				};
				actionList.Add(action);
			}
			this.messenger.Send(new SubmitActionMessage(actionList));
		}
	}
}
