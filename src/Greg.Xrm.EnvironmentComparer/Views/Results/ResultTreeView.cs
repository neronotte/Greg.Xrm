using Greg.Xrm.Async;
using Greg.Xrm.Core.Views.Help;
using Greg.Xrm.EnvironmentComparer.Engine;
using Greg.Xrm.EnvironmentComparer.Help;
using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.Theming;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	public partial class ResultTreeView : DockContent
	{
		private readonly IThemeProvider themeProvider;
		private readonly IMessenger messenger;
		private readonly ResultTreeViewModel viewModel;
		private CompareResultSet compareResult;
		private string env2;
		private string env1;

		private readonly Color Green = Color.FromArgb(142, 209, 24);





		public ResultTreeView(
			IThemeProvider themeProvider,
			IAsyncJobScheduler scheduler,
			IMessenger messenger,
			ILog log)
		{
			InitializeComponent();

			this.RegisterHelp(messenger, Topics.ResultTree);

			this.themeProvider = themeProvider ?? throw new ArgumentNullException(nameof(themeProvider));
			this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
			this.viewModel = new ResultTreeViewModel(scheduler, messenger, log);


			this.ApplyTheme();

			this.messenger.Register<CompareResultSetAvailable>(m =>
			{
				this.CompareResult = m.CompareResultSet;
			});

			this.cmiMarkOK.Bind(_ => _.Visible, this.viewModel, _ => _.IsMarkOKEnabled);
			this.cmiUnmarkOK.Bind(_ => _.Visible, this.viewModel, _ => _.IsUnmrkOKEnabled);

			this.cmiMarkOK.Bind(_ => _.Enabled, this.viewModel, _ => _.IsMarkOKEnabled);
			this.cmiUnmarkOK.Bind(_ => _.Enabled, this.viewModel, _ => _.IsUnmrkOKEnabled);

			this.tDownloadExcelFile.BindCommand(() => this.viewModel.DownloadExcelCommand);

			this.cmiCopyToEnv1.BindCommand(() => this.viewModel.CopyToEnv1Command, () => 1, CommandExecuteBehavior.EnabledAndVisible);
			this.cmiCopyToEnv2.BindCommand(() => this.viewModel.CopyToEnv2Command, () => 2, CommandExecuteBehavior.EnabledAndVisible);

			this.cmiDeleteFromEnv1.BindCommand(() => this.viewModel.DeleteFromEnv1Command, () => 1, CommandExecuteBehavior.EnabledAndVisible);
			this.cmiDeleteFromEnv2.BindCommand(() => this.viewModel.DeleteFromEnv2Command, () => 2, CommandExecuteBehavior.EnabledAndVisible);


			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				.ChangesProperty(_ => _.ConnectionName1)
				.Execute(e =>
				{
					this.env1 = e.GetNewValue<string>();
					this.CompareResult = null;
					this.cmiCopyToEnv1.Text = "Copy to " + env1;
					this.cmiDeleteFromEnv1.Text = "Delete from " + env1;
				});

			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				 .ChangesProperty(_ => _.ConnectionName2)
				 .Execute(e =>
				 {
					 this.env2 = e.GetNewValue<string>();
					 this.CompareResult = null;
					 this.cmiCopyToEnv2.Text = "Copy to " + env2;
					 this.cmiDeleteFromEnv2.Text = "Delete from " + env2;
				 });
		}


		private void ApplyTheme()
		{
			var theme = this.themeProvider.GetCurrentTheme();
			theme.ApplyTo(this.resultTree);
		}

		public CompareResultSet CompareResult
		{
			get => this.compareResult;
			set
			{
				this.compareResult = value;
				this.OnCompareResultChanged();
			}
		}

		private void OnCompareResultChanged()
		{
			if (this.InvokeRequired)
			{
				Action d = () => OnCompareResultChanged();
				this.BeginInvoke(d);
				return;
			}

			this.tDownloadExcelFile.Enabled = this.CompareResult != null && this.CompareResult.Count > 0;

			this.resultTree.BeginUpdate();
			this.resultTree.Nodes.Clear();

			if (this.CompareResult == null || this.CompareResult.Count == 0)
			{
				this.resultTree.EndUpdate();
				return;
			}

			foreach (var kvp in this.compareResult)
			{
				var shouldExpand = false;
				var node = this.resultTree.Nodes.Add($"{kvp.Key} ({kvp.Value.Count})");
				node.Name = node.Text;
				node.ImageKey = "entity";
				node.SelectedImageKey = "entity";


				if (!kvp.Value.IsEntityValidForCrm1 || !kvp.Value.IsEntityValidForCrm2)
				{
					string missingEnv;
					if (!kvp.Value.IsEntityValidForCrm1 && !kvp.Value.IsEntityValidForCrm2)
					{
						missingEnv = "both environments";
					}
					else if (!kvp.Value.IsEntityValidForCrm1)
					{
						missingEnv = this.env1;
					}
					else
					{
						missingEnv = this.env2;
					}


					node.ForeColor = Color.Red;
					node.ToolTipText = $"Entity not present on {missingEnv}";
					continue;
				}

				node.Tag = kvp.Value;
				if (kvp.Value.Contains(ResultTreeViewModel.AdditionalMetadataMarkedOk))
				{
					node.ForeColor = Green;
					node.Text = node.Name + "*";
				}

				var subGroupList = kvp.Value.Where(_ => _.Result == ObjectComparisonResult.Equals).ToList();
				var count = subGroupList.Count;
				var childNode = node.Nodes.Add($"Matching: {count}/{kvp.Value.Count}");
				childNode.Name = childNode.Text;
				childNode.ImageKey = "matching";
				childNode.SelectedImageKey = "matching";
				childNode.Tag = subGroupList;
				if (count == 0)
				{
					childNode.ForeColor = Color.Gray;
				}
				else if (count == kvp.Value.Count)
				{
					childNode.ForeColor = Green;
					childNode.Parent.ForeColor = Green;
					kvp.Value[ResultTreeViewModel.AdditionalMetadataIsOk] = ResultTreeViewModel.AdditionalMetadataIsOk;
				}


				subGroupList = kvp.Value.Where(_ => _.Result == ObjectComparisonResult.RightMissing).ToList();
				count = subGroupList.Count;
				childNode = node.Nodes.Add($"Missing on ENV2: {count}/{kvp.Value.Count}");
				childNode.Name = childNode.Text;
				childNode.ImageKey = "missing_right";
				childNode.SelectedImageKey = "missing_right";
				childNode.Tag = subGroupList;
				if (count == 0)
				{
					childNode.ForeColor = Color.Gray;
				}
				else if (count == kvp.Value.Count)
				{
					childNode.NodeFont = new Font(resultTree.Font, FontStyle.Bold);
				}
				else
				{
					shouldExpand = true;
				}

				subGroupList = kvp.Value.Where(_ => _.Result == ObjectComparisonResult.LeftMissing).ToList();
				count = subGroupList.Count;
				childNode = node.Nodes.Add($"Missing on ENV1: {count}/{kvp.Value.Count}");
				childNode.Name = childNode.Text;
				childNode.ImageKey = "missing_left";
				childNode.SelectedImageKey = "missing_left";
				childNode.Tag = subGroupList;
				if (count == 0)
				{
					childNode.ForeColor = Color.Gray;
				}
				else if (count == kvp.Value.Count)
				{
					childNode.NodeFont = new Font(resultTree.Font, FontStyle.Bold);
				}
				else
				{
					shouldExpand = true;
				}

				subGroupList = kvp.Value.Where(_ => _.Result == ObjectComparisonResult.MatchingButDifferent).ToList();
				count = subGroupList.Count;
				childNode = node.Nodes.Add($"With differences: {count}/{kvp.Value.Count}");
				childNode.Name = childNode.Text;
				childNode.ImageKey = "mismatch";
				childNode.SelectedImageKey = "mismatch";
				childNode.Tag = subGroupList;
				if (count == 0)
				{
					childNode.ForeColor = Color.Gray;
				}
				else if (count == kvp.Value.Count)
				{
					childNode.NodeFont = new Font(resultTree.Font, FontStyle.Bold);
				}
				else
				{
					shouldExpand = true;
				}

				if (shouldExpand)
				{
					node.Expand();
				}
			}

			this.resultTree.EndUpdate();
			this.Show();

			this.OnNodeSelected(this.resultTree.Nodes.OfType<TreeNode>().FirstOrDefault());
		}



		private void OnNodeSelected(object sender, TreeNodeMouseClickEventArgs e)
		{
			OnNodeSelected(e.Node);
		}

		TreeNode lastSelectedNode = null;

		private void OnNodeSelected(TreeNode node)
		{
			if (node == this.lastSelectedNode) return;
			this.lastSelectedNode = node;

			this.viewModel.SelectedNode = node;
		}

		private void OnMarkOkClick(object sender, EventArgs e)
		{
			var node = this.resultTree.SelectedNode;
			if (node == null) return;
			if (!(node.Tag is CompareResultForEntity cr)) return;
			if (cr.Contains(ResultTreeViewModel.AdditionalMetadataMarkedOk)) return;

			cr[ResultTreeViewModel.AdditionalMetadataMarkedOk] = ResultTreeViewModel.AdditionalMetadataMarkedOk;
			node.ForeColor = Green;
			if (!node.Name.EndsWith("*"))
			{
				node.Text = node.Name + "*";
			}
		}

		private void OnUnmarkOkClick(object sender, EventArgs e)
		{
			var node = this.resultTree.SelectedNode;
			if (node == null) return;
			if (!(node.Tag is CompareResultForEntity cr)) return;
			if (!cr.Contains(ResultTreeViewModel.AdditionalMetadataMarkedOk)) return;

			cr.Remove(ResultTreeViewModel.AdditionalMetadataMarkedOk);
			node.ForeColor = this.themeProvider.GetCurrentTheme().PanelForeColor;
			node.Text = node.Name;
		}
	}
}
