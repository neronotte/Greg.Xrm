using Greg.Xrm.Async;
using Greg.Xrm.EnvironmentComparer.Logging;
using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.EnvironmentComparer.Model.Memento;
using Greg.Xrm.Messaging;
using Greg.Xrm.Theming;
using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.EnvironmentComparer.Views.Configurator
{
	public partial class ConfiguratorView : DockContent
	{
		private readonly IAsyncJobScheduler asyncJobScheduler;
		private readonly IThemeProvider themeProvider;
		private readonly IMessenger messenger;
		private readonly ILog log;
		//private readonly EnvironmentComparerViewModel parentViewModel;
		private readonly ConfiguratorViewModel viewModel;

		public ConfiguratorView(
			IAsyncJobScheduler asyncJobScheduler,
			IThemeProvider themeProvider, 
			IMessenger messenger,
			ILog log)
		{
			InitializeComponent();

			this.tAdd.Enabled = false;
			this.viewModel = new ConfiguratorViewModel(log, messenger);
			this.asyncJobScheduler = asyncJobScheduler ?? throw new ArgumentNullException(nameof(asyncJobScheduler));
			this.themeProvider = themeProvider ?? throw new ArgumentNullException(nameof(themeProvider));
			this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
			this.log = log;

			this.mLoadEntities.Bind(_ => _.Enabled, this.viewModel, _ => _.CanLoadEntities);
			this.tLoadEntities.Bind(_ => _.Enabled, this.viewModel, _ => _.CanLoadEntities);

			this.mOpen.Bind(_ => _.Enabled, this.viewModel, _ => _.CanOpenMemento);
			this.mSave.Bind(_ => _.Enabled, this.viewModel, _ => _.CanSaveMemento);
			this.mSaveAs.Bind(_ => _.Enabled, this.viewModel, _ => _.CanSaveAsMemento);

			this.mAdd.Bind(_ => _.Enabled, this.viewModel, _ => _.CanAdd);
			this.mEdit.Bind(_ => _.Enabled, this.viewModel, _ => _.CanEdit);
			this.mEdit.Bind(_ => _.Text, this.viewModel, _ => _.EditLabel);
			this.mRemove.Bind(_ => _.Enabled, this.viewModel, _ => _.CanRemove);
			this.mRemove.Bind(_ => _.Text, this.viewModel, _ => _.RemoveLabel);
			this.tAdd.Bind(_ => _.Enabled, this.viewModel, _ => _.CanAdd);
			this.tEdit.Bind(_ => _.Enabled, this.viewModel, _ => _.CanEdit);
			this.tEdit.Bind(_ => _.ToolTipText, this.viewModel, _ => _.EditLabel);
			this.tRemove.Bind(_ => _.Enabled, this.viewModel, _ => _.CanRemove);
			this.tRemove.Bind(_ => _.ToolTipText, this.viewModel, _ => _.RemoveLabel);

			this.mExecute.Bind(_ => _.Enabled, this.viewModel, _ => _.CanExecuteComparison);
			this.tExecute.Bind(_ => _.Enabled, this.viewModel, _ => _.CanExecuteComparison);

			this.messenger.Register<ResetEntityList>(m => this.viewModel.EntityMetadataList = null);
			this.messenger.Register<LoadEntitiesResponse>(m => this.viewModel.EntityMetadataList = m.Entities);

			this.ApplyTheme();


			this.viewModel.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(this.viewModel.Memento))
				{
					OnMementoChanged();
				}
			};
		}




		private void ApplyTheme()
		{
			var theme = this.themeProvider.GetCurrentTheme();
			theme.ApplyTo(this.treeView1);
		}

		private void OnLoadEntitiesClick(object sender, EventArgs e)
		{
			this.messenger.Send<LoadEntitiesRequest>();
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

			if (this.viewModel.Memento == null)
			{
				this.treeView1.EndUpdate();
				return;
			}

			foreach (var entity in this.viewModel.Memento.Entities)
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
			this.Show();
		}

		private void OnAfterSelectTreeNode(object sender, TreeViewEventArgs e)
		{
			var node = e.Node;
			if (node != null && node.Tag == null)
			{
				node = node.Parent;
			}

			this.viewModel.SelectedNode = node;
		}

		private void OnAddClick(object sender, EventArgs e)
		{
			if (this.viewModel.EntityMetadataList.Count == 0) return;


			using (var dialog = new ConfiguratorDialog(this.viewModel.EntityMetadataList))
			{
				if (dialog.ShowDialog(this) != DialogResult.OK) return;

				var memento = dialog.Memento;

				if (this.viewModel.Memento == null)
				{
					this.viewModel.Memento = new EngineMemento();
				}

				this.viewModel.Memento.Entities.Add(memento);
				OnMementoChanged();
			}
		}

		private void OnRemoveClick(object sender, EventArgs e)
		{
			var node = this.viewModel.SelectedNode;
			if (node == null)
			{
				MessageBox.Show("No node selected!");
				return;
			}

			var confirm = MessageBox.Show($"Do you really want to remove entity <{node.Text}>?", $"Remove {node.Text}", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (confirm != DialogResult.Yes) return;


			var entityMemento = (EntityMemento)node.Tag;
			this.viewModel.Memento.Entities.Remove(entityMemento);
			this.OnMementoChanged();
			this.OnAfterSelectTreeNode(sender, new TreeViewEventArgs(null));
		}



		private void OnEditClick(object sender, EventArgs e)
		{
			var node = this.viewModel.SelectedNode;
			if (node == null)
			{
				MessageBox.Show("No node selected!");
				return;
			}

			var entityMemento = (EntityMemento)node.Tag;

			using (var dialog = new ConfiguratorDialog(this.viewModel.EntityMetadataList))
			{
				dialog.Memento = entityMemento;

				if (dialog.ShowDialog(this) != DialogResult.OK) return;

				var memento = dialog.Memento;
				this.viewModel.Memento.Entities.Remove(entityMemento);
				this.viewModel.Memento.Entities.Add(memento);
				OnMementoChanged();
			}
		}


		private void OnOpenClick(object sender, EventArgs e)
		{
			this.viewModel.OpenMemento(this.log);
		}

		private void OnSaveAsClick(object sender, EventArgs e)
		{

		}

		private void OnSaveClick(object sender, EventArgs e)
		{

		}

		private void OnExecuteClick(object sender, EventArgs e)
		{
			this.asyncJobScheduler.Enqueue(new WorkAsyncInfo
			{
				Message = "Executing comparison, please wait...",
				Work = (w, e1) => {
					this.viewModel.ExecuteComparison();
				}
			});
		}
	}
}
