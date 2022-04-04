using Greg.Xrm.Async;
using Greg.Xrm.EnvironmentSolutionsComparer.Messaging;
using Greg.Xrm.EnvironmentSolutionsComparer.Views.Environments;
using Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Theming;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Specialized;
using System.Linq;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views
{
	public partial class MainView :  MultipleConnectionsPluginControlBase, IGitHubPlugin
	{
		const string AdditionalOrganization = "AdditionalOrganization";
		private readonly MainViewModel viewModel;

		private readonly OutputView outputView;
		private readonly EnvironmentListView environmentListView;
		private readonly SolutionsView solutionsView;
		private readonly SolutionComponentsView solutionComponentsView;



		#region IGitHubPlugin implementation
		public string RepositoryName => GitHubPluginConstants.RepositoryName;

		public string UserName => GitHubPluginConstants.UserName;


		#endregion 


		public MainView(IThemeProvider themeProvider)
		{
			InitializeComponent();

			var messenger = new Messenger(this);

			this.viewModel = new MainViewModel(messenger);

			var scheduler = new AsyncJobScheduler(this, this.viewModel);

			this.outputView = new OutputView(themeProvider, messenger);
			this.outputView.Show(this.dockPanel, DockState.DockBottom);

			this.environmentListView = new EnvironmentListView(themeProvider, messenger);
			this.environmentListView.Show(this.dockPanel, DockState.DockLeft);

			this.solutionsView = new SolutionsView(this.outputView, themeProvider, messenger, scheduler);
			this.solutionsView.Show(this.dockPanel, DockState.Document);

			this.solutionComponentsView = new SolutionComponentsView(this.outputView, themeProvider, messenger, scheduler);
			this.solutionComponentsView.Show(this.dockPanel, DockState.Document);

			this.solutionsView.Show();


			messenger.Register<AddNewConnectionMessage>(m => this.AddAdditionalOrganization());
			messenger.Register<RemoveConnectionMessage>(m => this.RemoveAdditionalOrganization(m.Detail));
		}



		private void OnCloseClick(object sender, EventArgs e)
		{
			this.CloseTool();
		}

		public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
		{
			base.UpdateConnection(newService, detail, actionName, parameter);

			if (string.Empty.Equals(actionName))
			{
				this.viewModel.AddDefaultEnvironment(detail, newService);
			}
			else if (string.Equals(actionName, AdditionalOrganization, StringComparison.OrdinalIgnoreCase))
			{
				this.viewModel.AddEnvironment(detail, newService);
			}
		}

		protected override void ConnectionDetailsUpdated(NotifyCollectionChangedEventArgs e)
		{
			var oldItems = (e.OldItems ?? Array.Empty<ConnectionDetail>()).OfType<ConnectionDetail>().ToList();
			this.viewModel.RemoveEnvironment(oldItems); // only removal is handled here because add triggers the "UpdateConnection" method
		}
	}
}
