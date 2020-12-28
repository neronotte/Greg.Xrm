using Greg.Xrm.SolutionManager.Model;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.SolutionManager.Views.DataTree
{
	public partial class DataTreeView : DockContent
	{
		public DataTreeView(PluginViewModel viewModel)
		{
			InitializeComponent();
			this.viewModel = viewModel;

			this.viewModel.PropertyChanged += OnViewModelPropertyChanged;
		}

		private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (string.Equals(e.PropertyName,nameof(this.viewModel.CurrentImportJobData)))
			{
				this.Document = this.viewModel.CurrentImportJobData;
			}
		}

		private XDocument document;
		private readonly PluginViewModel viewModel;

		public XDocument Document 
		{
			get => this.document; 
			set
			{
				var currentDocumentText = this.document?.ToString();
				var valueText = value?.ToString();

				if (currentDocumentText == valueText) return;

				this.document = value;
				RefreshTreeView();
			}
		}

		private void RefreshTreeView()
		{
			this.tree.BeginUpdate();
			this.tree.Nodes.Clear();

			if (this.document != null)
			{
				var rootNode = this.tree.Nodes.Add(this.document.Root.Name.ToString());
				rootNode.ImageKey = "node";
				rootNode.SelectedImageKey = "node";
				this.RecourseTree(rootNode, this.document.Root);
			}
			this.tree.EndUpdate();
		}

		private void RecourseTree(TreeNode node, XElement element)
		{
			foreach (var attribute in element.Attributes())
			{
				var childNode = node.Nodes.Add($"{attribute.Name}: {attribute.Value}");
				childNode.ImageKey = "attribute";
				childNode.SelectedImageKey = "attribute";
				childNode.ForeColor = tree.ForeColor;

				if ("result".Equals(attribute.Name.ToString(), StringComparison.OrdinalIgnoreCase)
					&& "warning".Equals(attribute.Value, System.StringComparison.OrdinalIgnoreCase))
				{
					childNode.ForeColor = Color.Orange;
					RecourseReverse(childNode.Parent, n => { 
						if (n.ForeColor == tree.ForeColor)
						{
							n.ForeColor = Color.Orange;
						}
					});
				}

				if ("result".Equals(attribute.Name.ToString(), StringComparison.OrdinalIgnoreCase)
					&& "error".Equals(attribute.Value, System.StringComparison.OrdinalIgnoreCase))
				{
					childNode.ForeColor = Color.Red;
					RecourseReverse(childNode.Parent, n => n.ForeColor = Color.Red);
				}

			}

			foreach (var childElement in element.Elements())
			{
				var childNode = node.Nodes.Add(childElement.Name.ToString());
				childNode.ImageKey = "node";
				childNode.SelectedImageKey = "node";
				childNode.ForeColor = tree.ForeColor;
				RecourseTree(childNode, childElement);
			}
		}

		private void RecourseReverse(TreeNode parent, Action<TreeNode> action)
		{
			if (parent == null) return;
			action(parent);
			RecourseReverse(parent.Parent, action);
		}
	}
}
