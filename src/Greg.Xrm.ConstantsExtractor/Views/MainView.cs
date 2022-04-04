using Greg.Xrm.ConstantsExtractor.Core;
using Greg.Xrm.ConstantsExtractor.Messaging;
using Greg.Xrm.ConstantsExtractor.Model;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Theming;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Greg.Xrm.ConstantsExtractor.Views
{
	public partial class MainView : PluginControlBase, IGitHubPlugin
	{
		private readonly IMessenger messenger;
		private readonly SettingsView settingsView;
		private readonly OutputView outputView;

		#region IGitHubPlugin implementation
		public string RepositoryName => GitHubPluginConstants.RepositoryName;

		public string UserName => GitHubPluginConstants.UserName;

		#endregion

		public MainView(IThemeProvider themeProvider)
		{
			if (themeProvider == null)
				throw new ArgumentNullException(nameof(themeProvider));


			InitializeComponent();

			this.messenger = new Messenger(this);
			
			this.settingsView = new SettingsView(themeProvider, messenger);
			this.settingsView.Show(this.dockPanel, DockState.DockLeft);


			this.outputView = new OutputView(themeProvider, messenger);
			this.outputView.Show(this.dockPanel, DockState.Document);

			this.messenger.Register<Export>(OnExportRequested);
		}

		private void OnExportRequested(Export msg)
		{
			ExecuteMethod(Export, msg.Settings);
		}


		private void OnCloseToolRequested(object sender, EventArgs e)
		{
			CloseTool();
		}

		/// <summary>
		/// This event occurs when the connection has been updated in XrmToolBox
		/// </summary>
		public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
		{
			base.UpdateConnection(newService, detail, actionName, parameter);

			if (newService == null)
				return;

			WorkAsync(new WorkAsyncInfo
			{
				Message = "Reading solutions",
				Work = (worker, args) =>
				{
					var query = new QueryExpression("solution");
					query.ColumnSet.AddColumns("uniquename", "friendlyname");
					query.Criteria.AddCondition("isvisible", ConditionOperator.Equal, true);
					query.AddOrder("friendlyname", OrderType.Ascending);

					var solutionList = this.Service.RetrieveMultiple(query).Entities.Select(x => new Solution(x)).ToList();
					args.Result = solutionList;
				},
				PostWorkCallBack = (args) =>
				{
					if (args.Error != null)
					{
						MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					if (args.Result is List<Solution> solutionList)
					{
						this.messenger.Send(new LoadSolutionsCompleted(solutionList));
					}
				}
			});
		}


		private void Export(Settings settings)
		{
			WorkAsync(new WorkAsyncInfo
			{
				Message = "Extracting files",
				Work = (worker, args) =>
				{
					var constantsExtractorManager = new ConstantExtractorManager(
						this.outputView,
						this.Service, 
						settings.SolutionName,
						settings.ExtractTypes, 
						settings.ExtractDescriptions, 
						settings.CsFolder, 
						settings.JsFolder, 
						settings.NamespaceCs, 
						settings.NamespaceJs, 
						settings.JsHeaderLines);

					constantsExtractorManager.ExtractConstants();

					var folders = new List<string>();
					if (settings.GetCsConstants)
					{
						var writer = new WriteConstantsToFileCs(this.outputView, constantsExtractorManager);
						writer.WriteConstantsToFile();

						folders.Add(settings.CsFolder);
					}

					if (settings.GetJsConstants)
					{
						var writer = new WriteConstantsToFileJs(this.outputView, constantsExtractorManager);
						writer.WriteConstantsToFile();

						folders.Add(settings.JsFolder);
					}

					args.Result = folders;
				},
				PostWorkCallBack = (args) =>
				{
					if (args.Error != null)
					{
						MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					if (args.Result is List<string> folders)
					{
						MessageBox.Show($"Export completed!");

						foreach (var folder in folders)
						{
							Process.Start("explorer.exe", folder);
						}
					}

					this.messenger.Send<ExportCompleted>();
				}
			});
		}
	}
}