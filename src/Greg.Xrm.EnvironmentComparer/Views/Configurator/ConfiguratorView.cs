using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.EnvironmentComparer.Model.Memento;
using Greg.Xrm.EnvironmentComparer.Views.Configurator;
using Greg.Xrm.Messaging;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.EnvironmentComparer.Configurator
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

		private void OnAddClick(object sender, EventArgs e)
		{
			if (this.EntityMetadataList.Count == 0) return;


			using (var dialog = new ConfiguratorDialog(this.EntityMetadataList))
			{
				if (dialog.ShowDialog(this) != System.Windows.Forms.DialogResult.OK) return;

				var memento = dialog.Memento;
				this.memento.Entities.Add(memento);
				OnMementoChanged();
			}
		}
	}
}
