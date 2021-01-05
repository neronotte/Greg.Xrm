using Greg.Xrm.Async;
using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.EnvironmentComparer.Model;
using Greg.Xrm.EnvironmentComparer.Model.Memento;
using Greg.Xrm.EnvironmentComparer.Views.Actions;
using Greg.Xrm.EnvironmentComparer.Views.Configurator;
using Greg.Xrm.EnvironmentComparer.Views.Output;
using Greg.Xrm.EnvironmentComparer.Views.Results;
using Greg.Xrm.Messaging;
using Greg.Xrm.Theming;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.EnvironmentComparer.Views
{
	public partial class EnvironmentComparerPluginControl : MultipleConnectionsPluginControlBase
	{
		const string ConnectToEnvironment1String = "1. Connect to environment 1";
		const string ConnectToEnvironment2String = "2. Connect to environment 2";


		private Settings mySettings;
		private readonly ConfiguratorView configuratorView;
		private readonly OutputView outputView;
		private readonly ResultTreeView resultTreeView;
		private readonly ActionsView actionsView;
		private readonly ResultGridView resultGridView;
		private readonly ResultRecordView resultRecordView;

		private readonly IMessenger messenger;

		private readonly EnvironmentComparerViewModel viewModel;

		public EnvironmentComparerPluginControl(IThemeProvider themeProvider)
		{
			if (themeProvider == null) 
				throw new ArgumentNullException(nameof(themeProvider));


			InitializeComponent();

			this.messenger = new Messenger(this);

			this.tEnv1Name.Text = "1. Connect to environment 1";
			this.tConnectToEnv2.Text = ConnectToEnvironment2String;


			this.outputView = new OutputView(themeProvider);
			this.outputView.Show(this.dockPanel, DockState.DockBottomAutoHide);

			this.viewModel = new EnvironmentComparerViewModel(this.outputView, this.messenger);
			var scheduler = new AsyncJobScheduler(this, viewModel);

			this.resultTreeView = new ResultTreeView(themeProvider, scheduler, this.messenger, this.outputView);
			this.resultTreeView.Show(this.dockPanel, DockState.DockLeft);

			this.configuratorView = new ConfiguratorView(themeProvider, scheduler, this.messenger, this.outputView);
			this.configuratorView.Show(this.dockPanel, DockState.DockLeft);

			this.actionsView = new ActionsView(themeProvider, this.messenger, this.outputView);
			this.actionsView.Show(this.dockPanel, DockState.DockRightAutoHide);

			this.resultGridView = new ResultGridView(themeProvider, this.messenger);
			this.resultGridView.Show(this.dockPanel, DockState.Document);

			this.resultRecordView = new ResultRecordView(themeProvider, this.messenger);
			this.resultRecordView.Show(this.dockPanel, DockState.Document);

			this.resultGridView.Show();
			this.configuratorView.Show();


			this.messenger.Register<HighlightResultRecord>(m =>
			{
				this.resultRecordView.Show();
			});
			this.messenger.Register<LoadEntitiesRequest>(m =>
			{
				ExecuteMethod(LoadEntities);
			});
		}


		protected override void ConnectionDetailsUpdated(NotifyCollectionChangedEventArgs e)
		{
			SetEnvironments();
		}


		/// <summary>
		/// This event occurs when the connection has been updated in XrmToolBox
		/// </summary>
		public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
		{
			base.UpdateConnection(newService, detail, actionName, parameter);
			SetEnvironments();
		}

		private void OnConnectToEnvironment2(object sender, EventArgs e)
		{
			AddAdditionalOrganization();
		}


		private void SetEnvironments()
		{
			this.viewModel.Env1 = this.ConnectionDetail;
			this.viewModel.Env2 = this.AdditionalConnectionDetails.FirstOrDefault();
			this.SetConnectionNames(this.viewModel.Env1?.ConnectionName, this.viewModel.Env2?.ConnectionName);
			this.messenger.Send<ResetEntityList>();
		}

		private void MyPluginControl_Load(object sender, EventArgs e)
		{
			SetEnvironments();

			// Loads or creates the settings for the plugin
			if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
			{
				mySettings = new Settings();

				LogWarning("Settings not found => a new settings file has been created!");
			}
			else
			{
				LogInfo("Settings found and loaded");
			}
		}

		private void OnCloseToolClicked(object sender, EventArgs e)
		{
			CloseTool();
		}


		/// <summary>
		/// This event occurs when the plugin is closed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
		{
			// Before leaving, save the settings
			SettingsManager.Instance.Save(GetType(), mySettings);
		}



		void SetConnectionNames(string env1name, string env2name)
		{
			this.tEnv1Name.Text = string.IsNullOrWhiteSpace(env1name) ? ConnectToEnvironment1String : "ENV1: " + env1name;
			this.tEnv2Name.Text = string.IsNullOrWhiteSpace(env2name) ? ConnectToEnvironment2String : "- ENV2: " + env2name;

			if (string.IsNullOrWhiteSpace(env1name))
			{
				this.tEnv1Name.Visible = true;
				this.tEnv2Name.Visible = false;
				this.tConnectToEnv2.Visible = false;
			}
			else if (string.IsNullOrWhiteSpace(env2name))
			{
				this.tEnv1Name.Visible = true;
				this.tEnv2Name.Visible = false;
				this.tConnectToEnv2.Visible = true;
			}
			else
			{
				this.tEnv1Name.Visible = true;
				this.tEnv2Name.Visible = true;
				this.tConnectToEnv2.Visible = false;
			}
		}

		private void LoadEntities()
		{
			this.WorkAsync(new WorkAsyncInfo
			{
				Message = "Loading entities, please wait...",
				Work = (bw, e1) => {
					var request = new RetrieveAllEntitiesRequest
					{
						EntityFilters = EntityFilters.Attributes
					};

					var response = (RetrieveAllEntitiesResponse)this.Service.Execute(request);
					e1.Result = response.EntityMetadata;
				},
				PostWorkCallBack = e1 => {
					if (e1.Error != null)
					{
						return;
					}

					if (e1.Result is EntityMetadata[] entityMetadataList)
					{
						this.messenger.Send(new LoadEntitiesResponse(entityMetadataList));
					}
				}
			});
		}
	}
}