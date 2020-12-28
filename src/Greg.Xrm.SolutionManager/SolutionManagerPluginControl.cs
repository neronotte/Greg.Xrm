using Greg.Xrm.Async;
using Greg.Xrm.SolutionManager.Model;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.SolutionManager
{
	public partial class SolutionManagerPluginControl : PluginControlBase
	{
		private Settings mySettings;
		private readonly IAsyncJobScheduler scheduler;
		private readonly Views.SolutionProgress.SolutionProgressView solutionProgressView;
		private readonly Views.DataTree.DataTreeView dataTreeView;
		private readonly PluginViewModel viewModel = new PluginViewModel();

		public SolutionManagerPluginControl()
		{
			InitializeComponent();

			var importJobRepository = ImportJob.GetRepository();

			this.scheduler = new AsyncJobScheduler(this, this.viewModel);

			this.dataTreeView = new Views.DataTree.DataTreeView(this.viewModel);
			this.dataTreeView.Show(this.dockPanel, DockState.DockRight);

			this.solutionProgressView = new Views.SolutionProgress.SolutionProgressView(this.scheduler, importJobRepository, this.viewModel);
			this.solutionProgressView.Show(this.dockPanel, DockState.Document);


			this.tStartMonitoring.DataBindings.Add(nameof(this.tStartMonitoring.Enabled), this.viewModel, nameof(this.viewModel.CanStartMonitoring));
			this.tStopMonitoring.DataBindings.Add(nameof(this.tStopMonitoring.Enabled), this.viewModel, nameof(this.viewModel.CanStopMonitoring));
			this.tStoppingLabel.DataBindings.Add(nameof(this.tStoppingLabel.Visible), this.viewModel, nameof(this.viewModel.StopMonitoringRequested));
		}

		private void MyPluginControl_Load(object sender, EventArgs e)
		{
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

		private void tsbClose_Click(object sender, EventArgs e)
		{
			CloseTool();
		}

		private void tsbSample_Click(object sender, EventArgs e)
		{
			// The ExecuteMethod method handles connecting to an
			// organization if XrmToolBox is not yet connected
			ExecuteMethod(GetAccounts);
		}

		private void GetAccounts()
		{
			WorkAsync(new WorkAsyncInfo
			{
				Message = "Getting accounts",
				Work = (worker, args) =>
				{
					args.Result = Service.RetrieveMultiple(new QueryExpression("account")
					{
						TopCount = 50
					});
				},
				PostWorkCallBack = (args) =>
				{
					if (args.Error != null)
					{
						MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					var result = args.Result as EntityCollection;
					if (result != null)
					{
						MessageBox.Show($"Found {result.Entities.Count} accounts");
					}
				}
			});
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

		/// <summary>
		/// This event occurs when the connection has been updated in XrmToolBox
		/// </summary>
		public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
		{
			base.UpdateConnection(newService, detail, actionName, parameter);

			if (mySettings != null && detail != null)
			{
				mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
				LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
			}

			this.solutionProgressView.Service = newService;
			this.solutionProgressView.StartAsyncMonitor();
		}

		private void OnStartMonitoringClick(object sender, EventArgs e)
		{
			this.solutionProgressView.StartAsyncMonitor();
		}

		private void OnStopMonitoringClick(object sender, EventArgs e)
		{
			this.viewModel.StopMonitoringRequested = true;
		}
	}
}