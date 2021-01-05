﻿using Greg.Xrm.Async;
using Greg.Xrm.EnvironmentComparer.Logging;
using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.EnvironmentComparer.Model;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.Theming;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	public partial class ResultTreeView : DockContent
	{
		private readonly IThemeProvider themeProvider;
		private readonly IAsyncJobScheduler scheduler;
		private readonly IMessenger messenger;
		private readonly ILog log;
		private CompareResultSet compareResult;
		private string env2;
		private string env1;

		private readonly Color Green = Color.FromArgb(142, 209, 24);

		private const string AdditionalMetadataIsOk = "isOk";
		private const string AdditionalMetadataMarkedOk = "markedOk";





		public ResultTreeView(
			IThemeProvider themeProvider, 
			IAsyncJobScheduler scheduler,
			IMessenger messenger, 
			ILog log)
		{
			InitializeComponent();

			base.TabText = "Result Summary";
			this.themeProvider = themeProvider ?? throw new ArgumentNullException(nameof(themeProvider));
			this.scheduler = scheduler;
			this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
			this.log = log ?? throw new ArgumentNullException(nameof(log));
			this.ApplyTheme();

			this.messenger.Register<CompareResultSetAvailable>(m =>
			{
				this.CompareResult = m.CompareResultSet;
			});

			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				.ChangesProperty(_ => _.ConnectionName1)
				.Execute(e =>
				{
					this.env1 = e.GetNewValue<string>();
				});
			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				 .ChangesProperty(_ => _.ConnectionName2)
				 .Execute(e =>
				 {
					 this.env2 = e.GetNewValue<string>();
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
			this.Show();

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


			this.messenger.Send(new CompareResultGroupSelected(comparisonResult));
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



		private void OnDownloadExcelFileClick(object sender, EventArgs e)
		{
			string fileName;
			using (var dialog = new FolderBrowserDialog())
			{
				dialog.Description = "Output folder";

				if (dialog.ShowDialog() != DialogResult.OK) return;
				fileName = dialog.SelectedPath;
			}

			this.scheduler.Enqueue(new WorkAsyncInfo
			{
				Message = "Executing generating Excel file, please wait...",
				Work = (w, e1) => {
					using (log.Track("Exporting comparison result on excel file " + fileName))
					{
						try
						{
							var reportBuilder = new ReportBuilder(new DirectoryInfo(fileName));
							reportBuilder.GenerateReport(compareResult, this.env1, this.env2);
						}
						catch (Exception ex)
						{
							log.Error("Error during export: " + ex.Message, ex);
						}
					}
				}
			});
		}
	}
}
