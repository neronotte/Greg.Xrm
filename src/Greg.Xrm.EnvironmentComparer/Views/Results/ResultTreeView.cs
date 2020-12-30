using Greg.Xrm.EnvironmentComparer.Model;
using Greg.Xrm.Theming;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	public partial class ResultTreeView : DockContent
	{
		private readonly IThemeProvider themeProvider;
		private readonly Action<IReadOnlyCollection<Model.Comparison<Entity>>> onResultSelected;
		private CompareResult compareResult;

		private readonly Color Green = Color.FromArgb(142, 209, 24);

		public ResultTreeView(IThemeProvider themeProvider, Action<IReadOnlyCollection<Model.Comparison<Entity>>> onResultSelected)
		{
			InitializeComponent();

			base.TabText = "Result Summary";
			this.themeProvider = themeProvider ?? throw new ArgumentNullException(nameof(themeProvider));
			this.onResultSelected = onResultSelected ?? throw new ArgumentNullException(nameof(onResultSelected));
			this.ApplyTheme();
		}


		private void ApplyTheme()
		{
			var theme = this.themeProvider.GetCurrentTheme();
			theme.ApplyTo(this.resultTree);
		}

		public CompareResult CompareResult
		{
			get => this.compareResult;
			set {
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


			this.resultTree.BeginUpdate();
			this.resultTree.Nodes.Clear();

			if (this.compareResult == null || this.compareResult.Count == 0)
			{
				this.resultTree.EndUpdate();
				return;
			}

			foreach (var kvp in this.compareResult)
			{
				var shouldExpand = false;
				var node = this.resultTree.Nodes.Add($"{kvp.Key} ({kvp.Value.Count})" );
				node.ImageKey = "entity";
				node.SelectedImageKey = "entity";
				node.Tag = kvp.Value;

				var count = kvp.Value.Count(_ => _.Result == RecordComparisonResult.Equals);
				var childNode = node.Nodes.Add($"Matching: {count}/{kvp.Value.Count}");
				childNode.ImageKey = "matching";
				childNode.SelectedImageKey = "matching";
				if (count == 0)
				{
					childNode.ForeColor = Color.Gray;
				}
				else if (count == kvp.Value.Count)
				{
					childNode.ForeColor = Green;
					childNode.Parent.ForeColor = Green;
				}

				count = kvp.Value.Count(_ => _.Result == RecordComparisonResult.RightMissing);
				childNode = node.Nodes.Add($"Missing on ENV2: {count}/{kvp.Value.Count}");
				childNode.ImageKey = "missing_right";
				childNode.SelectedImageKey = "missing_right";
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

				count = kvp.Value.Count(_ => _.Result == RecordComparisonResult.LeftMissing);
				childNode = node.Nodes.Add($"Missing on ENV1: {count}/{kvp.Value.Count}");
				childNode.ImageKey = "missing_left";
				childNode.SelectedImageKey = "missing_left";
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

				count = kvp.Value.Count(_ => _.Result == RecordComparisonResult.MatchingButDifferent);
				childNode = node.Nodes.Add($"With differences: {count}/{kvp.Value.Count}");
				childNode.ImageKey = "mismatch";
				childNode.SelectedImageKey = "mismatch";
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

			this.OnNodeSelected(this.resultTree.Nodes.OfType<TreeNode>().FirstOrDefault());
		}

		private void OnNodeSelected(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
		{
			OnNodeSelected(e.Node);
		}

		private void OnNodeSelected(TreeNode node)
		{
			IReadOnlyCollection<Model.Comparison<Entity>> comparisonResult;
			if (node == null)
			{
				comparisonResult = Array.Empty<Model.Comparison<Entity>>();
			}
			else if (node.Parent == null)
			{
				comparisonResult = (IReadOnlyCollection<Model.Comparison<Entity>>)node.Tag;
			}
			else
			{
				comparisonResult = (IReadOnlyCollection<Model.Comparison<Entity>>)(node.Parent.Tag);
			}

			this.onResultSelected?.Invoke(comparisonResult);
		}
	}
}
