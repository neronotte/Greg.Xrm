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
	public partial class EnvironmentComparerPluginControl : MultipleConnectionsPluginControlBase, IEnvironmentComparerView
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

		private readonly EnvironmentComparerPresenter presenter;
		private readonly IMessenger messenger;

		private readonly EnvironmentComparerViewModel viewModel = new EnvironmentComparerViewModel();
		private readonly IThemeProvider themeProvider;

		public EnvironmentComparerPluginControl(IThemeProvider themeProvider)
		{
			this.themeProvider = themeProvider ?? throw new ArgumentNullException(nameof(themeProvider));


			InitializeComponent();

			this.messenger = new Messenger(this);

			this.tEnv1Name.Text = "1. Connect to environment 1";
			this.tConnectToEnv2.Text = ConnectToEnvironment2String;


			this.outputView = new OutputView(themeProvider);
			this.outputView.Show(this.dockPanel, DockState.DockBottomAutoHide);

			this.resultTreeView = new ResultTreeView(themeProvider, r => this.resultGridView.Results = r);
			this.resultTreeView.Show(this.dockPanel, DockState.DockLeft);

			this.configuratorView = new ConfiguratorView(themeProvider, this.messenger);
			this.configuratorView.Show(this.dockPanel, DockState.DockLeft);

			this.actionsView = new ActionsView(themeProvider, this.messenger, this.outputView);
			this.actionsView.Show(this.dockPanel, DockState.DockRightAutoHide);

			this.resultGridView = new ResultGridView(themeProvider, this.messenger);
			this.resultGridView.Show(this.dockPanel, DockState.Document);

			this.resultRecordView = new ResultRecordView(themeProvider, this.messenger);
			this.resultRecordView.Show(this.dockPanel, DockState.Document);

			this.resultGridView.Show();
			this.configuratorView.Show();


			this.presenter = new EnvironmentComparerPresenter(this.outputView, this, this.viewModel);


			this.tLoadEntities.DataBindings.Add(nameof(this.tLoadEntities.Enabled), this.viewModel, nameof(this.viewModel.CanLoadEntities));


			this.messenger.Register<HighlightResultRecord>(m =>
			{
				this.resultRecordView.Show();
			});
		}


		protected override void ConnectionDetailsUpdated(NotifyCollectionChangedEventArgs e)
		{
			this.presenter.SetEnvironments(this.ConnectionDetail, this.AdditionalConnectionDetails.FirstOrDefault());
			this.messenger.Send<ResetEntityList>();
		}

		private void OnConnectToEnvironment2(object sender, EventArgs e)
		{
			AddAdditionalOrganization();
		}



		/// <summary>
		/// This event occurs when the connection has been updated in XrmToolBox
		/// </summary>
		public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
		{
			base.UpdateConnection(newService, detail, actionName, parameter);

			this.presenter.SetEnvironments(this.ConnectionDetail, this.AdditionalConnectionDetails.FirstOrDefault());
			this.messenger.Send<ResetEntityList>();
		}

		private void MyPluginControl_Load(object sender, EventArgs e)
		{
			this.presenter.SetEnvironments(this.ConnectionDetail, this.AdditionalConnectionDetails.FirstOrDefault());

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

		private void OnOpenMemento(object sender, EventArgs e)
		{
			string fileName;
			using(var dialog = new OpenFileDialog())
			{
				dialog.Filter = "JSON (*.json)|*.json";
				dialog.Title = "Open JSON configuration";

				if (dialog.ShowDialog() != DialogResult.OK) return;
				fileName = dialog.FileName;
			}

			this.presenter.OpenMemento(fileName);
		}


		void IEnvironmentComparerView.CanOpenConfig(bool value)
		{
			this.tOpenMemento.Enabled = value;
		}

		void IEnvironmentComparerView.SetConnectionNames(string env1name, string env2name)
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

			this.resultGridView.SetEnvironmentNames(env1name, env2name);
			this.resultRecordView.SetEnvironmentNames(env1name, env2name);
		}

		void IEnvironmentComparerView.ShowMemento(EngineMemento memento)
		{
			if (this.InvokeRequired)
			{
				Action d = () => ((IEnvironmentComparerView)this).ShowMemento(memento);
				this.BeginInvoke(d);
				return;
			}

			this.configuratorView.Memento = memento;
			this.configuratorView.Show();
		}

		void IEnvironmentComparerView.CanExecuteComparison(bool value)
		{
			this.tExecuteComparison.Enabled = value;
		}

		void IEnvironmentComparerView.ShowComparisonResult(CompareResultSet result)
		{
			if (this.InvokeRequired)
			{
				Action d = () => ((IEnvironmentComparerView)this).ShowComparisonResult(result);
				this.BeginInvoke(d);
				return;
			}

			this.resultTreeView.CompareResult = result;
			this.resultTreeView.Show();

			this.tDownloadExcelFile.Enabled = result != null && result.Count > 0;
		}

		private void OnExecuteComparisonClicked(object sender, EventArgs e)
		{
			WorkAsync(new WorkAsyncInfo
			{
				Message = "Executing comparison, please wait...",
				Work = (w, e1) => {
					this.presenter.ExecuteComparison();
				}
			});	
		}

		private void OnDowloadExcelFileClicked(object sender, EventArgs e)
		{
			string fileName;
			using (var dialog = new FolderBrowserDialog())
			{
				dialog.Description = "Output folder";

				if (dialog.ShowDialog() != DialogResult.OK) return;
				fileName = dialog.SelectedPath;
			}

			WorkAsync(new WorkAsyncInfo
			{
				Message = "Executing generating Excel file, please wait...",
				Work = (w, e1) => {
					this.presenter.DownloadComparisonResultAsExcelFile(fileName, this.resultTreeView.CompareResult);
				}
			});
		}


		private void OnLoadEntitiesClick(object sender, EventArgs e)
		{
			ExecuteMethod(LoadEntities);
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
						this.messenger.Send(new EntityListRetrieved(entityMetadataList));
					}
				}
			});
		}
	}
}