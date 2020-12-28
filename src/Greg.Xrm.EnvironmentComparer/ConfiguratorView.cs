using Greg.Xrm.EnvironmentComparer.Model.Memento;
using System;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.EnvironmentComparer
{
	public partial class ConfiguratorView : DockContent
	{
		private EngineMemento memento;

		public ConfiguratorView()
		{
			InitializeComponent();
			base.TabText = "Configurator";
		}


		public EngineMemento Memento
		{
			get => this.memento;
			set 
			{
				this.memento = value;
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
	}
}
