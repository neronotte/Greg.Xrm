using Greg.Xrm.Async;
using Greg.Xrm.DataModelWikiEditor.Views.EntityTree;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Theming;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.DataModelWikiEditor.Views
{
	public partial class MainView : GregPluginControlBase<DataModelWikiEditorPlugin>
	{

		private readonly MainViewModel viewModel;
		private readonly IMessenger messenger;

		public MainView(ISettingsProvider<Settings> settingsProvider, IThemeProvider themeProvider)
		{
			InitializeComponent();


			this.messenger = new Messenger(this);

			this.viewModel = new MainViewModel(settingsProvider);
			var scheduler = new AsyncJobScheduler(this, viewModel);

			var output = new OutputView(themeProvider, messenger);
			output.Show(this.dockPanel1, DockState.DockBottom);


			var entityTreeView = new EntityTreeView(output, messenger, scheduler);
			entityTreeView.Show(this.dockPanel1, DockState.DockLeft);



			this.tConnectToEnvironment.Bind(_ => _.Visible, this.viewModel, _ => _.IsConnectToEnvirnmentVisible);
			this.tEnvironmentName.Bind(_ => _.Visible, this.viewModel, _ => _.IsEnvironmentNameVisible);
			this.tEnvironmentName.Bind(_ => _.Text, this.viewModel, _ => _.ConnectionName);

			this.tOpenFolder.BindCommand(() => this.viewModel.OpenWikiFolder);
		}

		private void AfterLoad(object sender, EventArgs e)
		{
			this.viewModel.Env = this.ConnectionDetail;
		}
		/// <summary>
		/// This event occurs when the connection has been updated in XrmToolBox
		/// </summary>
		public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
		{
			base.UpdateConnection(newService, detail, actionName, parameter);
			this.viewModel.Env = this.ConnectionDetail;

			this.messenger.Send(new ConnectionUpdatedMessage(detail, newService));
		}

		private void OnCloseClick(object sender, EventArgs e)
		{
			CloseTool();
		}


	}
}