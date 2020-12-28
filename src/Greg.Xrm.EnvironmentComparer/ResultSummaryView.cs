using Greg.Xrm.EnvironmentComparer.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.EnvironmentComparer
{
	public partial class ResultSummaryView : DockContent
	{
		private readonly Action<IReadOnlyCollection<Model.Comparison<Entity>>> onResultSelected;
		private CompareResult compareResult;

		public ResultSummaryView(Action<IReadOnlyCollection<Model.Comparison<Entity>>> onResultSelected)
		{
			InitializeComponent();

			base.TabText = "Result Summary";
			this.onResultSelected = onResultSelected;
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
					childNode.NodeFont = new Font(resultTree.Font, FontStyle.Bold);
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

			}

			this.resultTree.ExpandAll();

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
