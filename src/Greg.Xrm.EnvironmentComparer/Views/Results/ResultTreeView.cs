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
		private CompareResultSet compareResult;

		private readonly Color Green = Color.FromArgb(142, 209, 24);

		private const string AdditionalMetadataIsOk = "isOk";
		private const string AdditionalMetadataMarkedOk = "markedOk";





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

		public CompareResultSet CompareResult
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
				node.Name = node.Text;
				node.ImageKey = "entity";
				node.SelectedImageKey = "entity";
				node.Tag = kvp.Value;
				if (kvp.Value.Contains(AdditionalMetadataMarkedOk))
				{
					node.ForeColor = Green;
					node.Text = node.Name + "*";
				}

				var subGroupList = kvp.Value.Where(_ => _.Result == RecordComparisonResult.Equals).ToList();
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
					kvp.Value[AdditionalMetadataIsOk] = AdditionalMetadataIsOk;
				}


				subGroupList = kvp.Value.Where(_ => _.Result == RecordComparisonResult.RightMissing).ToList();
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

				subGroupList = kvp.Value.Where(_ => _.Result == RecordComparisonResult.LeftMissing).ToList();
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

				subGroupList = kvp.Value.Where(_ => _.Result == RecordComparisonResult.MatchingButDifferent).ToList();
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

			this.OnNodeSelected(this.resultTree.Nodes.OfType<TreeNode>().FirstOrDefault());
		}



		private void OnNodeSelected(object sender, TreeNodeMouseClickEventArgs e)
		{
			HandleMenuItemVisibility(e.Node);
			OnNodeSelected(e.Node);
		}

		private void HandleMenuItemVisibility(TreeNode node)
		{
			this.mMarkOK.Visible = node.Tag is CompareResultForEntity r && !r.ContainsAny(AdditionalMetadataIsOk, AdditionalMetadataMarkedOk);
			this.mUnmarkOK.Visible = node.Tag is CompareResultForEntity r1 && r1.Contains(AdditionalMetadataMarkedOk);
		}

		TreeNode lastSelectedNode = null;

		private void OnNodeSelected(TreeNode node)
		{
			if (node == this.lastSelectedNode) return;
			this.lastSelectedNode = node;

			IReadOnlyCollection<Model.Comparison<Entity>> comparisonResult = Array.Empty<Model.Comparison<Entity>>();
			if (node.Tag is IReadOnlyCollection<Model.Comparison<Entity>> collection)
			{
				comparisonResult = collection;
			}


			this.onResultSelected?.Invoke(comparisonResult);
		}

		private void OnMarkOkClick(object sender, EventArgs e)
		{
			var node = this.resultTree.SelectedNode;
			if (node == null) return;
			if (!(node.Tag is CompareResultForEntity cr)) return;
			if (cr.Contains(AdditionalMetadataMarkedOk)) return;

			cr[AdditionalMetadataMarkedOk] = AdditionalMetadataMarkedOk;
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
			if (!cr.Contains(AdditionalMetadataMarkedOk)) return;

			cr.Remove(AdditionalMetadataMarkedOk);
			node.ForeColor = this.themeProvider.GetCurrentTheme().PanelForeColor;
			node.Text = node.Name;
		}
	}
}
