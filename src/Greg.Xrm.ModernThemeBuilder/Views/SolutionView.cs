using Greg.Xrm.Async;
using Greg.Xrm.Messaging;
using Greg.Xrm.ModernThemeBuilder.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

		public SolutionView(IMessenger messenger, IAsyncJobScheduler scheduler)
		{
			InitializeComponent();

			this.TabText = this.Text = "Solution browser";
			this.CloseButtonVisible = false;
			this.Icon = Core.Properties.Resources.Icon;
			
			this.messenger = messenger;
			this.scheduler = scheduler;

			this.messenger.Register<ConnectionUpdatedMessage>(OnConnectionUpdated);
			this.messenger.Register<SolutionComponentAdded>(OnSolutionComponentAdded);
			this.messenger.Register<SolutionComponentChanged>(OnSolutionComponentChanged);
			this.messenger.Register<SolutionSelected>(msg => Initialize(msg.Solution));
		}

		private void OnSolutionComponentChanged(SolutionComponentChanged msg)
		{
			var node = this.tree.Nodes.Find(msg.SolutionComponent.WebResource.name, true).FirstOrDefault();
			if (node == null) return;

			node.NodeFont = msg.SolutionComponent.IsDirty ? 
				new System.Drawing.Font(this.tree.Font, System.Drawing.FontStyle.Bold) : 
				this.tree.Font;
		}

		private void OnConnectionUpdated(ConnectionUpdatedMessage message)
		{
			this.crm = message.Crm;
			this.tree.Nodes.Clear();
		}

		public void Initialize(Solution solution)
		{
			if (solution == null) 
			{
				this.tree.Nodes.Clear();
				this.messenger.Send(new SolutionComponentSelected(null));
				this.TabText = this.Text = "Solution browser";
				return;
			}

			this.TabText = this.Text = $"Solution: {solution}";

			scheduler.Enqueue(new XrmToolBox.Extensibility.WorkAsyncInfo
			{
				Work = LoadSolutionComponents,
				Message = "Loading solution components...",
				PostWorkCallBack = OnSolutionComponentsLoaded,
				AsyncArgument = solution
			});
		}
		private void LoadSolutionComponents(BackgroundWorker worker, DoWorkEventArgs args)
		{
			var solution = args.Argument as Solution;
			if (solution == null)
			{
				args.Result = new List<SolutionComponent>();
				return;
			}

			var solutionComponentRepository = SolutionComponent.GetRepository(this.crm);
			var solutionComponents = solutionComponentRepository.GetSolutionComponentBySolutionId(solution.Id);
			args.Result = solutionComponents;
		}

		private void OnSolutionComponentsLoaded(RunWorkerCompletedEventArgs args)
		{
			var solutionComponents = args.Result as List<SolutionComponent>;
			var firstLeaf = InitializeTree(solutionComponents);
			this.messenger.Send(new SolutionComponentLoaded(solutionComponents));
			this.tree.SelectedNode = firstLeaf;
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

				var nodeCollection = i== 0 ? this.tree.Nodes : parentNode.Nodes;
				currentNode = nodeCollection.Find(key, false).FirstOrDefault();

				if (currentNode != null)
				{
					parentNode = currentNode;
					continue;
				}

				var imageIndex = 1;
				if (i == 0) imageIndex = 0;
				if (i == parts.Count - 1) imageIndex = 2;

				currentNode = nodeCollection.Add(key, part, imageIndex, imageIndex);
				currentNode.Tag = (i == parts.Count - 1) ? solutionComponent : null;
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
