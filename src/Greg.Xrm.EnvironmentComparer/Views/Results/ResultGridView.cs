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
using static Greg.Xrm.Extensions;

namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	public partial class ResultGridView : DockContent
	{
		private IReadOnlyCollection<Model.Comparison<Entity>> results;
		private readonly IThemeProvider themeProvider;
		private readonly IMessenger messenger;
		private string env1 = "ENV1", env2 = "ENV2";

		private readonly ResultGridViewModel viewModel;

		public ResultGridView(IThemeProvider themeProvider, IMessenger messenger)
		{
			InitializeComponent();

			this.themeProvider = themeProvider ?? throw new ArgumentNullException(nameof(themeProvider));
			this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));

			this.viewModel = new ResultGridViewModel(messenger);
			
			this.cmiCopyToEnv1.Bind(_ => _.Enabled, this.viewModel, _ => _.IsCopyToEnv1Enabled);
			this.cmiCopyToEnv2.Bind(_ => _.Enabled, this.viewModel, _ => _.IsCopyToEnv2Enabled);
			this.cmiCompare.Bind(_ => _.Enabled, this.viewModel, _ => _.IsCompareEnabled);

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

			this.viewModel.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(this.viewModel.Results))
				{
					this.results = this.viewModel.Results;
					this.OnResultsChanged();
				}
			};
		}


		private void ApplyTheme()
		{
			var theme = this.themeProvider.GetCurrentTheme();

			this.listView1.BackColor = theme.PanelBackgroundColor;
			this.listView1.Font = theme.PanelFont;
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
			this.Show();
		}




		

		private void OnRecordSelectionChanged(object sender, EventArgs e)
		{
			var selectedResults = this.listView1.SelectedItems
				.Cast<ListViewItem>()
				.Select(x => x.Tag)
				.Cast<Model.Comparison<Entity>>()
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

		private void OnCopyToEnv2Click(object sender, EventArgs e)
		{
			CopySelectedRowTo(2);
		}

		private void OnCopyToEnv1Click(object sender, EventArgs e)
		{
			CopySelectedRowTo(1);
		}

		private void CopySelectedRowTo(int index)
		{
			if (this.listView1.SelectedItems.Count == 0) return;

			var envName = index == 1 ? this.env1 : this.env2;

			var actionList = new List<IAction>();
			foreach (var result in this.listView1.SelectedItems
				.Cast<ListViewItem>()
				.Select(_ => _.Tag)
				.OfType<Model.Comparison<Entity>>())
			{
				var entity = index == 2 ? result.Item1 : result.Item2;

				// in case of matching but different, we need to pick the ID of the matching record
				if (result.Result == RecordComparisonResult.MatchingButDifferent)
				{
					var matchingEntity = index == 1 ? result.Item1 : result.Item2;

					var entityIdAttributeName = entity.LogicalName + "id";

					entity = entity.Clone();
					entity.Attributes.Remove(entityIdAttributeName);
					entity.Id = matchingEntity.Id;

					// set null the attributes that are in the matching entity but not in this one, in order to clear their values
					foreach (var matchingEntityAttribute in matchingEntity.Attributes.Keys)
					{
						if (CloneSettings.IsForbidden(entity, matchingEntityAttribute)) continue;

						if (!entity.Contains(matchingEntityAttribute))
						{
							entity[matchingEntityAttribute] = null;
						}
					}
				}


				var action = new ActionCopyEntity(entity, result.Key, index, envName);
				actionList.Add(action);
			}
			this.messenger.Send(new SubmitActionMessage(actionList));
		}
	}
}
