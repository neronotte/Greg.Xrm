using Greg.Xrm.Async;
using Greg.Xrm.Messaging;
using Greg.Xrm.ModernThemeBuilder.Model;
using Greg.Xrm.ModernThemeBuilder.Views.Messages;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.ModernThemeBuilder.Views
{
	public partial class SolutionView : DockContent
	{
		private readonly IMessenger messenger;
		private readonly IAsyncJobScheduler scheduler;
		private IOrganizationService crm;
		private string currentThemeName;

		public SolutionView(IMessenger messenger, IAsyncJobScheduler scheduler)
		{
			InitializeComponent();

			this.TabText = this.Text = "Solution browser";
			this.CloseButtonVisible = false;
			this.Icon = Core.Properties.Resources.Icon;

			this.messenger = messenger;
			this.scheduler = scheduler;

			this.messenger.Register<ConnectionUpdated>(OnConnectionUpdated);
			this.messenger.Register<SolutionComponentAdded>(OnSolutionComponentAdded);
			this.messenger.Register<SolutionComponentChanged>(OnSolutionComponentChanged);
			this.messenger.Register<CurrentThemeSelected>(OnCurrentThemeSelected);
			this.messenger.Register<SolutionComponentLoaded>(OnSolutionComponentsLoaded);
		}

		private void OnCurrentThemeSelected(CurrentThemeSelected selected)
		{
			var previousThemeName = this.currentThemeName;

			if (!string.IsNullOrWhiteSpace(previousThemeName))
			{
				var previousTheme = this.tree.Nodes.Find(previousThemeName, true).FirstOrDefault();

				if (previousTheme != null)
					previousTheme.Text = previousTheme.Text.Replace(" (current)", "");
			}

			this.currentThemeName = selected.ThemeName;

			var currentTheme = this.tree.Nodes.Find(this.currentThemeName, true).FirstOrDefault();
			if (currentTheme != null)
			{
				currentTheme.Text = $"{currentTheme.Text} (current)";
			}
		}

		private void OnSolutionComponentChanged(SolutionComponentChanged msg)
		{
			var node = this.tree.Nodes.Find(msg.SolutionComponent.WebResource.name, true).FirstOrDefault();
			if (node == null) return;

			node.NodeFont = msg.SolutionComponent.IsDirty ?
				new System.Drawing.Font(this.tree.Font, System.Drawing.FontStyle.Bold) :
				this.tree.Font;
		}

		private void OnConnectionUpdated(ConnectionUpdated message)
		{
			this.crm = message.Crm;
			this.tree.Nodes.Clear();
		}



		private void OnSolutionComponentsLoaded(SolutionComponentLoaded msg)
		{
			var solution = msg.Solution;
			if (solution == null)
			{
				this.tree.Nodes.Clear();
				this.messenger.Send(new SolutionComponentSelected(null));
				this.TabText = this.Text = "Solution browser";
				return;
			}

			this.TabText = this.Text = $"Solution: {solution}";

			var solutionComponents = msg.SolutionComponents;
			var nodeToSelect = InitializeTree(solutionComponents);

			if (!string.IsNullOrWhiteSpace(this.currentThemeName))
			{
				nodeToSelect = this.tree.Nodes.Find(this.currentThemeName, true).FirstOrDefault() ?? nodeToSelect;
			}

			this.tree.SelectedNode = nodeToSelect;
		}

		private TreeNode InitializeTree(List<SolutionComponent> solutionComponents)
		{
			this.tree.Nodes.Clear();

			TreeNode firstLeaf = null;
			foreach (var solutionComponent in solutionComponents.OrderBy(x => x.WebResource.name))
			{
				var node = ProcessSolutionComponent(solutionComponent);
				if (firstLeaf == null) firstLeaf = node;
			}
			this.tree.ExpandAll();
			return firstLeaf;
		}

		private TreeNode ProcessSolutionComponent(SolutionComponent solutionComponent)
		{
			var parts = solutionComponent.WebResource.SplitNameInParts();

			TreeNode parentNode = null, currentNode = null;
			for (var i = 0; i < parts.Count; i++)
			{
				var part = parts[i];
				var key = (i == parts.Count - 1) ? solutionComponent.WebResource.name : "PART:" + string.Join("/", parts.Take(i + 1));

				var nodeCollection = i == 0 ? this.tree.Nodes : parentNode.Nodes;
				currentNode = nodeCollection.Find(key, false).FirstOrDefault();

				if (currentNode != null)
				{
					parentNode = currentNode;
					continue;
				}

				var imageIndex = 1;
				var name = part;
				if (i == 0) imageIndex = 0;
				if (i == parts.Count - 1)
				{
					name = this.currentThemeName == solutionComponent.WebResource.name ? $"{name} (current)" : name;
					imageIndex = 2;
				}

				currentNode = nodeCollection.Add(key, name, imageIndex, imageIndex);
				currentNode.Tag = (i == parts.Count - 1) ? solutionComponent : null;
				currentNode.ToolTipText = (i == parts.Count - 1) ? solutionComponent.WebResource.name : null;

				if (i == parts.Count - 1 && solutionComponent.IsDirty)
				{
					currentNode.NodeFont = new System.Drawing.Font(this.tree.Font, System.Drawing.FontStyle.Bold);
				}

				parentNode = currentNode;
			}
			return currentNode;
		}

		private void OnSolutionComponentAdded(SolutionComponentAdded msg)
		{
			if (msg.SolutionComponent == null) return;
			var treeNode = ProcessSolutionComponent(msg.SolutionComponent);
			this.tree.SelectedNode = treeNode;
		}


		private void OnNodeSelected(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Tag == null) return;
			var solutionComponent = e.Node.Tag as SolutionComponent;
			if (solutionComponent == null) return;

			this.messenger.Send(new SolutionComponentSelected(solutionComponent));
		}
	}
}
