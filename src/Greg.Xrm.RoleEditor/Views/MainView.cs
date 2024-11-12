using Greg.Xrm.Async;
using Greg.Xrm.Core.Async;
using Greg.Xrm.Core.Help;
using Greg.Xrm.Core.Views;
using Greg.Xrm.Core.Views.Help;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.RoleEditor.Help;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Services;
using Greg.Xrm.RoleEditor.Services.Snippets;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.RoleEditor.Views.RoleBrowser;
using Greg.Xrm.Theming;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace Greg.Xrm.RoleEditor.Views
{
	public partial class MainView : GregPluginControlBase<RoleEditorPlugin>, IStatusBarMessenger, ISettingsPlugin
	{
		private readonly object syncRoot = new object();

		private readonly IMessenger messenger;
		private readonly IAsyncJobScheduler scheduler;
		private readonly IPrivilegeClassificationProvider privilegeClassificationProvider;
		private readonly IPrivilegeSnippetRepository privilegeSnippetRepository;

		private readonly MainViewModel viewModel;

		private readonly OutputView outputView;
		private readonly Dictionary<Role, Editor.RoleEditorView> roleViewDict = new Dictionary<Role, Editor.RoleEditorView>();
		private readonly ISettingsProvider<Settings> settingsProvider;

		public MainView(ISettingsProvider<Settings> settingsProvider, IThemeProvider themeProvider)
		{
			InitializeComponent();


			this.dockPanel.DocumentStyle = DocumentStyle.DockingWindow;
			this.dockPanel.Theme = new VS2015BlueTheme();
			this.dockPanel.CustomizaFloatWindow(x => x.MakeResizable().AllowAltTab());

			this.messenger = new Messenger(this);
			this.outputView = new OutputView(themeProvider, messenger);


			// initialization of model and services
			this.settingsProvider = settingsProvider;
			var privilegeRepository = new Privilege.Repository();
			var roleRepository = new Role.Repository(this.outputView, this.messenger);
			var roleTemplateBuilder = new RoleTemplateBuilder(this.outputView, privilegeRepository);
			var businessUnitRepository = new BusinessUnit.Repository();
			this.privilegeClassificationProvider = new PrivilegeClassificationProvider(settingsProvider);
			this.privilegeSnippetRepository = new PrivilegeSnippetRepository(outputView, settingsProvider);

			// ui initialization

			this.viewModel = new MainViewModel(this.outputView, this.messenger, roleTemplateBuilder, roleRepository, businessUnitRepository);
			this.scheduler = new AsyncJobScheduler(this, this.viewModel);
			this.messenger.RegisterJobScheduler(scheduler);



			this.outputView.Show(this.dockPanel, DockState.DockBottom);

			var helpContentIndexProvider = new HelpContentIndexProvider();
			var helpContentIndex = helpContentIndexProvider.GetIndex();
			var helpRepository = new HelpRepository(helpContentIndex, GetType().Assembly);
			var helpView = new HelpView(this.messenger, this.outputView, helpRepository, Topics.Home);

			helpView.Show(this.dockPanel, DockState.DockRight);
			helpView.DockState = DockState.DockRightAutoHide;

			var roleBrowserView = new RoleBrowserView(this.outputView, messenger, settingsProvider);
			roleBrowserView.Show(this.dockPanel, DockState.DockLeft);



			// data binding
			this.tInit.Bind(x => x.Text, viewModel, vm => vm.LoadDataButtonText);
			this.tInit.BindCommand(() => this.viewModel.InitCommand);
			this.tSettings.Click += (s, e) => ShowSettings();



			// messaging rgistrations
			// when something sends a message to the statusbar, it it redirected to the default statusbar
			this.messenger.Register<StatusBarMessageEventArgs>(m => this.SendMessageToStatusBar?.Invoke(this, m));

			this.viewModel.OpenRoleRequested += OnOpenRoleRequested;
			this.viewModel.ShowRoleRequested += OnShowRoleRequested;
			this.viewModel.CloseRoleRequested += OnCloseRoleRequested;

			this.messenger.Register<ShowHelp>(m =>
			{
				helpView.Show();
			});
		}


		#region IStatusBarMessenger implementation

		public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

		#endregion



		// this happens when the user changes the main connection.
		// any state must be reset.
		public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
		{
			this.viewModel.Reset(newService, detail);
		}

		private void OnExitClick(object sender, System.EventArgs e)
		{
			CloseTool();
		}



		private void OnOpenRoleRequested(object sender, OpenRoleView e)
		{
			lock(this.syncRoot)
			{
				var editor = new Editor.RoleEditorView(
					this.settingsProvider,
					this.privilegeSnippetRepository,
					this.privilegeClassificationProvider,
					e.Role);
				this.roleViewDict[e.Role] = editor;
				editor.Show(this.dockPanel, DockState.Document);
			}
		}
		private void OnShowRoleRequested(object sender, OpenRoleView e)
		{
			lock (this.syncRoot)
			{
				if (!this.roleViewDict.TryGetValue(e.Role, out var editor)) return;
				editor.Show(this.dockPanel, DockState.Document);
			}
		}

		private void OnCloseRoleRequested(object sender, CloseRoleView e)
		{
			lock(this.syncRoot)
			{
				this.roleViewDict.Remove(e.Role);
			}
		}

		public void ShowSettings()
		{
			using (var dialog = new SettingsDialog(this.messenger, this.settingsProvider, this.privilegeSnippetRepository))
			{
				dialog.StartPosition = FormStartPosition.CenterParent;
				dialog.ShowDialog(this);
			}
		}
	}
}
