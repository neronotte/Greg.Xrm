using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.EnvironmentComparer.Model.Memento;
using Greg.Xrm.Messaging;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.EnvironmentComparer.Views.Configurator
{
	public partial class ConfiguratorView : DockContent
	{
		private readonly IMessenger messenger;
		private EngineMemento memento;

		private IReadOnlyCollection<EntityMetadata> entityMetadataList;

		public ConfiguratorView(IMessenger messenger)
		{
			InitializeComponent();

			this.tAdd.Enabled = false;
			this.entityMetadataList = Array.Empty<EntityMetadata>();

			this.messenger = messenger;
			this.messenger.Register<ResetEntityList>(OnResetEntityList);
			this.messenger.Register<EntityListRetrieved>(OnEntityListRetrieved);
			this.memento = new EngineMemento();
		}

		private void OnResetEntityList(ResetEntityList obj)
		{
			this.EntityMetadataList = Array.Empty<EntityMetadata>();
		}

		private void OnEntityListRetrieved(EntityListRetrieved obj)
		{
			this.EntityMetadataList = obj.Entities;
		}


		public IReadOnlyCollection<EntityMetadata> EntityMetadataList
		{
			get => this.entityMetadataList;
			set 
			{
				if (this.entityMetadataList == value) return;
				this.entityMetadataList = value ?? Array.Empty<EntityMetadata>();

				this.tAdd.Enabled = this.entityMetadataList.Count > 0;
			}
		}


		public EngineMemento Memento
		{
			get => this.memento;
			set 
			{
				this.memento = value ?? new EngineMemento();
				this.OnMementoChanged();
			}
		}

		protected virtual void OnMementoChanged()
		{
			if (this.InvokeRequired)
			{
				Action d = () => OnMementoChanged();
				this.Invoke(d);
				return;
			}

			this.treeView1.BeginUpdate();
			this.treeView1.Nodes.Clear();

			if (this.memento == null)
			{
				this.treeView1.EndUpdate();
				return;
			}

			foreach (var entity in this.memento.Entities)
			{
				var key = entity.KeyUseGuid ? "GUID" : entity.KeyAttributeNames.Join(", ");
				var skip = entity.AttributesToSkip?.Count > 0 ? entity.AttributesToSkip.Join(", ") : "-";
				var onlyActive = entity.OnlyActive ? "Y" : string.Empty;

				var entityItem = this.treeView1.Nodes.Add(entity.EntityName);
				entityItem.ImageKey = "entity";
				entityItem.SelectedImageKey = "entity";
				entityItem.Tag = entity;

				var childItem = entityItem.Nodes.Add("Key on: " + key);
				childItem.ImageKey = "key";
				childItem.SelectedImageKey = "key";


				childItem = entityItem.Nodes.Add("Attributes to ignore: " + skip);
				childItem.ImageKey = "skip";
				childItem.SelectedImageKey = "skip";


				if (entity.OnlyActive)
				{
					childItem = entityItem.Nodes.Add("Only active");
					childItem.ImageKey = "active";
					childItem.SelectedImageKey = "active";
				}
			}
			this.treeView1.ExpandAll();
			this.treeView1.EndUpdate();
		}

		private void OnAfterSelectTreeNode(object sender, TreeViewEventArgs e)
		{
			var node = e.Node;

			if (node != null && node.Tag == null)
			{
				node = node.Parent;
			}

			this.tEdit.Enabled = node != null;
			this.tEdit.Text = $"Edit {node.Text}".Trim();
			this.tEdit.Tag = node;
			this.tRemove.Enabled = node != null;
			this.tRemove.Text = $"Remove {node.Text}".Trim();
			this.tRemove.Tag = node;
		}

		private void OnAddClick(object sender, EventArgs e)
		{
			if (this.EntityMetadataList.Count == 0) return;


			using (var dialog = new ConfiguratorDialog(this.EntityMetadataList))
			{
				if (dialog.ShowDialog(this) != DialogResult.OK) return;

				var memento = dialog.Memento;
				this.memento.Entities.Add(memento);
				OnMementoChanged();
			}
		}

		private void OnRemoveClick(object sender, EventArgs e)
		{
			var btn = (ToolStripButton)sender;
			if (!(btn.Tag is TreeNode node))
			{
				MessageBox.Show("No node selected!");
				return;
			}

			var confirm = MessageBox.Show($"Do you really want to remove entity <{node.Text}>?", $"Remove {node.Text}", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (confirm != DialogResult.Yes) return;


			var entityMemento = (EntityMemento)node.Tag;
			this.Memento.Entities.Remove(entityMemento);
			this.OnMementoChanged();
			this.OnAfterSelectTreeNode(sender, new TreeViewEventArgs(null));
		}

		private void OnEditClick(object sender, EventArgs e)
		{
			var btn = (ToolStripButton)sender;
			if (!(btn.Tag is TreeNode node))
			{
				MessageBox.Show("No node selected!");
				return;
			}

			var entityMemento = (EntityMemento)node.Tag;

			using (var dialog = new ConfiguratorDialog(this.EntityMetadataList))
			{
				dialog.Memento = entityMemento;

				if (dialog.ShowDialog(this) != DialogResult.OK) return;

				var memento = dialog.Memento;
				this.memento.Entities.Add(memento);
				OnMementoChanged();
			}
		}
	}
}
