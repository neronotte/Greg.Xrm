using Greg.Xrm.Async;
using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.EnvironmentComparer.Engine.Memento;
using Greg.Xrm.EnvironmentComparer.Views.Actions;
using Greg.Xrm.Messaging;
using Greg.Xrm.Theming;
using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.Extensibility;
using Greg.Xrm.EnvironmentComparer.Help;
using Greg.Xrm.Model;
using Greg.Xrm.Logging;

namespace Greg.Xrm.EnvironmentComparer.Views.Configurator
{
	public partial class ConfiguratorView : DockContent
	{
		private readonly IAsyncJobScheduler asyncJobScheduler;
		private readonly IThemeProvider themeProvider;
		private readonly IMessenger messenger;
		private readonly ILog log;
		private readonly ConfiguratorViewModel viewModel;

		public ConfiguratorView(
			IThemeProvider themeProvider,
			IAsyncJobScheduler asyncJobScheduler,
			IMessenger messenger,
			ILog log)
		{
			InitializeComponent();

			this.RegisterHelp(messenger, Topics.Configurator);

			this.tAdd.Enabled = false;
			this.viewModel = new ConfiguratorViewModel(log, messenger);
			this.asyncJobScheduler = asyncJobScheduler ?? throw new ArgumentNullException(nameof(asyncJobScheduler));
			this.themeProvider = themeProvider ?? throw new ArgumentNullException(nameof(themeProvider));
			this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
			this.log = log;
			this.mLoadEntities.Bind(_ => _.Enabled, this.viewModel, _ => _.CanLoadEntities);
			this.tLoadEntities.Bind(_ => _.Enabled, this.viewModel, _ => _.CanLoadEntities);

			this.mNew.Bind(_ => _.Enabled, this.viewModel, _ => _.CanNewMemento);
			this.tNew.Bind(_ => _.Enabled, this.viewModel, _ => _.CanNewMemento);
			this.mOpen.Bind(_ => _.Enabled, this.viewModel, _ => _.CanOpenMemento);
			this.tOpen.Bind(_ => _.Enabled, this.viewModel, _ => _.CanOpenMemento);
			this.mSave.Bind(_ => _.Enabled, this.viewModel, _ => _.CanSaveMemento);
			this.tSave.Bind(_ => _.Enabled, this.viewModel, _ => _.CanSaveAsMemento);
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

			// If one or more actions have been applied successfully, reexecute the comparison
			this.messenger.Register<ApplyActionsResult>(m =>
			{
				if (m.SucceededCount == 0) return;

				this.OnExecuteClick(this.tExecute, EventArgs.Empty);
			});


			messenger.WhenObject<EnvironmentComparerViewModel>()
				.ChangesProperty(_ => _.Crm1)
				.Execute(m =>
				{
					this.Show();
				});

			messenger.WhenObject<EnvironmentComparerViewModel>()
				.ChangesProperty(_ => _.Crm2)
				.Execute(m =>
				{
					this.Show();
				});

			this.ApplyTheme();


			this.viewModel.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(this.viewModel.Memento))
				{
					OnMementoChanged();
				}
			};

			this.viewModel.Initialize();
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


			using (var dialog = new ConfiguratorDialog(this.messenger, this.viewModel.EntityMetadataList))
			{
				if (dialog.ShowDialog(this) != DialogResult.OK) return;

				var memento = dialog.Memento;

				if (this.viewModel.Memento == null)
				{
					this.viewModel.Memento = new EngineMemento();
				}

				this.viewModel.Memento.Entities.Add(memento);
				this.viewModel.RefreshMemento();
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
			this.viewModel.RefreshMemento();
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

			using (var dialog = new ConfiguratorDialog(this.messenger, this.viewModel.EntityMetadataList))
			{
				dialog.Memento = entityMemento;

				if (dialog.ShowDialog(this) != DialogResult.OK) return;

				var memento = dialog.Memento;
				var index = this.viewModel.Memento.Entities.IndexOf(entityMemento);
				this.viewModel.Memento.Entities.RemoveAt(index);
				this.viewModel.Memento.Entities.Insert(index, memento);
				this.viewModel.RefreshMemento();
			}
		}


		private void OnOpenClick(object sender, EventArgs e)
		{
			this.viewModel.OpenMemento();
		}

		private void OnSaveAsClick(object sender, EventArgs e)
		{
			this.viewModel.SaveAsMemento();
		}

		private void OnSaveClick(object sender, EventArgs e)
		{
			this.viewModel.SaveMemento();
		}


		private void OnNewClick(object sender, EventArgs e)
		{
			this.viewModel.NewMemento();
		}


		private void OnExecuteClick(object sender, EventArgs e)
		{
			this.asyncJobScheduler.Enqueue(new WorkAsyncInfo
			{
				Message = "Executing comparison, please wait...",
				Work = (w, e1) => {
					this.viewModel.ExecuteComparison();
				},
				PostWorkCallBack = e1 => {
					if (e1.Error != null)
					{
						this.log.Error("Error while executing comparison: " + e1.Error.Message, e1.Error);
						return;
					}


					log.Info("Compare completed");
				}
			});
		}
	}
}
