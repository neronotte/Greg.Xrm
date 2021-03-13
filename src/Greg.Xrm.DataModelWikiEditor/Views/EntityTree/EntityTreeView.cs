using Greg.Xrm.Async;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.DataModelWikiEditor.Views.EntityTree
{
	public partial class EntityTreeView : DockContent
	{
		private readonly EntityTreeViewModel viewModel;

		public EntityTreeView(ILog log, IMessenger messenger, IAsyncJobScheduler scheduler)
		{
			this.InitializeComponent();

			this.viewModel = new EntityTreeViewModel(log, messenger, scheduler);
			this.viewModel.PropertyChanged += OnViewModelPropertyChanged;

			this.tLoadEntities.BindCommand(() => this.viewModel.LoadEntitiesCommand);

			messenger.WhenObject<MainViewModel>()
				.ChangesProperty(_ => _.AllowRequests)
				.Execute(m =>
				{
					this.tree.Enabled = m.GetNewValue<bool>();
				});


			messenger.Register<RefreshEntityMessage>(OnRefreshEntity);
		}

		private void OnRefreshEntity(RefreshEntityMessage obj)
		{
			var node = this.tree.Nodes.OfType<TreeNode>().FirstOrDefault(_ => _.Tag == obj.NodeForEntity);
			if (node == null) return;


			node.Nodes.Clear();
			foreach (var attribute in obj.NodeForEntity.AttributeList)
			{
				var attributeNode = node.Nodes.Add(attribute.ToString());
				attributeNode.Tag = attribute;
			}
		}

		private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(EntityTreeViewModel.Entities))
			{
				RefreshTreeView(this.viewModel.Entities);
			}
		}

		private void RefreshTreeView(IReadOnlyCollection<NodeForEntity> entities)
		{
			this.tree.BeginUpdate();
			this.tree.Nodes.Clear();


			foreach (var entity in entities)
			{
				var node = this.tree.Nodes.Add(entity.LogicalName);
				node.Tag = entity;
				
				node.Nodes.Add("Loading...");
			}

			this.tree.EndUpdate();
		}

		private void OnBeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			var parentNode = e.Node;

			if (parentNode.Nodes.Count == 1 && parentNode.Nodes[0].Tag == null)
			{
				var entityNode = (NodeForEntity)parentNode.Tag;

				this.viewModel.Expand(entityNode);
			}
		}
	}
}
