using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.DataModelWikiEditor.Views
{
	public partial class MainView : PluginControlBase
	{

		private readonly MainViewModel viewModel;
	
		public MainView(ISettingsProvider<Settings> settingsProvider)
		{
			InitializeComponent();

			this.viewModel = new MainViewModel(settingsProvider);

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
		}

		private void OnCloseClick(object sender, EventArgs e)
		{
			CloseTool();
		}


	}
}