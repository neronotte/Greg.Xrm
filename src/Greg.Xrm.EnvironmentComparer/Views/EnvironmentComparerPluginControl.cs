using Greg.Xrm.Async;
using Greg.Xrm.EnvironmentComparer.Help;
using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.EnvironmentComparer.Views.Actions;
using Greg.Xrm.EnvironmentComparer.Views.Configurator;
using Greg.Xrm.EnvironmentComparer.Views.Help;
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
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace Greg.Xrm.EnvironmentComparer.Views
{
	public partial class EnvironmentComparerPluginControl : MultipleConnectionsPluginControlBase, IGitHubPlugin, IStatusBarMessenger
	{
		const string ConnectToEnvironment1String = "1. Connect to environment 1";
		const string ConnectToEnvironment2String = "2. Connect to environment 2";


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


			this.outputView = new OutputView(themeProvider, messenger);
			this.outputView.Show(this.dockPanel, DockState.DockBottomAutoHide);

			this.viewModel = new EnvironmentComparerViewModel(this.messenger);
			var scheduler = new AsyncJobScheduler(this, viewModel);

			this.resultTreeView = new ResultTreeView(themeProvider, scheduler, this.messenger, this.outputView);
			this.resultTreeView.Show(this.dockPanel, DockState.DockLeft);

			this.configuratorView = new ConfiguratorView(themeProvider, scheduler, this.messenger, this.outputView);
			this.configuratorView.Show(this.dockPanel, DockState.DockLeft);

			this.resultGridView = new ResultGridView(themeProvider, this.messenger);
			this.resultGridView.Show(this.dockPanel, DockState.Document);

			this.resultRecordView = new ResultRecordView(themeProvider, this.messenger);
			this.resultRecordView.Show(this.dockPanel, DockState.Document);

			this.actionsView = new ActionsView(themeProvider, scheduler, this.messenger, this.outputView);
			this.actionsView.Show(this.dockPanel, DockState.Document);

			var helpContentIndexProvider = new HelpContentIndexProvider();
			var helpContentIndex = helpContentIndexProvider.GetIndex();
			var helpRepository = new HelpRepository(helpContentIndex);
			var helpView = new HelpView(this.messenger, this.outputView, helpRepository);
			helpView.Show(this.dockPanel, DockState.DockRightAutoHide);

			this.resultGridView.Show();
			this.configuratorView.Show();

			this.tEquals.Bind(_ => _.Visible, this.viewModel, _ => _.AreEqual);


			this.messenger.Register<HighlightResultRecord>(m =>
			{
				this.resultRecordView.Show();
			});
			this.messenger.Register<LoadEntitiesRequest>(m =>
			{
				ExecuteMethod(LoadEntities);
			});

			this.messenger.Register<StatusBarMessageEventArgs>(m =>
			{
				this.SendMessageToStatusBar?.Invoke(this, m);
			});
		}


		#region IGitHubPlugin implementation

		public string RepositoryName => GitHubPluginConstants.RepositoryName;

		public string UserName => GitHubPluginConstants.UserName;

		#endregion

		#region IStatusBarMessenger implementation

		public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

		#endregion


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
			if (this.viewModel.Env2 != null)
			{
				var answer = MessageBox.Show("Do you really want to change the connection to ENV2?", ConnectToEnvironment2String, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (answer != DialogResult.Yes) return;

				this.RemoveAdditionalOrganization(this.viewModel.Env2);
				this.viewModel.Env2 = null;
				this.messenger.Send<ResetEntityList>();
			}

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
		}

		private void OnCloseToolClicked(object sender, EventArgs e)
		{
			CloseTool();
		}



		void SetConnectionNames(string env1name, string env2name)
		{
			this.tEnv1Name.Text = string.IsNullOrWhiteSpace(env1name) ? ConnectToEnvironment1String : "ENV1: " + env1name;
			this.tConnectToEnv2.Text = string.IsNullOrWhiteSpace(env2name) ? ConnectToEnvironment2String : "- ENV2: " + env2name + " (click to change)";
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