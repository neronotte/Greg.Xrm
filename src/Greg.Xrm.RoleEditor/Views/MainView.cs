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
using Greg.Xrm.RoleEditor.Views.AddUserRoles;
using Greg.Xrm.RoleEditor.Views.BulkEditor;
using Greg.Xrm.RoleEditor.Views.Comparer;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.RoleEditor.Views.RoleBrowser;
using Greg.Xrm.RoleEditor.Views.UsageInspector;
using Greg.Xrm.RoleEditor.Views.UserBrowser;
using Greg.Xrm.Theming;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Media.Animation;
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
		private readonly IDependencyRepository dependencyRepository;
		private readonly IRoleRepository roleRepository;

		private readonly MainViewModel viewModel;

		private readonly OutputView outputView;
		private readonly Dictionary<Role, DockContent> roleViewDict = new Dictionary<Role, DockContent>();
		private readonly ISettingsProvider<Settings> settingsProvider;

		private readonly Dictionary<DataverseEnvironment, UserRolesView> userRolesViewDict = new Dictionary<DataverseEnvironment, UserRolesView>();




		public MainView(ISettingsProvider<Settings> settingsProvider, IThemeProvider themeProvider)
		{
			InitializeComponent();


			this.dockPanel.DocumentStyle = DocumentStyle.DockingWindow;
			this.dockPanel.Theme = new VS2015BlueTheme();
			this.dockPanel.CustomizaFloatWindow(x => x.MakeResizable().AllowAltTab());

			this.messenger = new Messenger(this);
			Register(messenger);
			this.outputView = new OutputView(themeProvider, messenger);


			// initialization of model and services
			this.settingsProvider = settingsProvider;
			var privilegeRepository = new Privilege.Repository();
			this.roleRepository = new Role.Repository(this.outputView, this.messenger);
			this.dependencyRepository = new Dependency.Repository(this.outputView);
			var businessUnitRepository = new BusinessUnit.Repository();
			var systemUserRepository = new SystemUser.Repository(this.outputView);

			var roleTemplateBuilder = new RoleTemplateBuilder(this.outputView, privilegeRepository);
			this.privilegeClassificationProvider = new PrivilegeClassificationProvider(settingsProvider);
			this.privilegeSnippetRepository = new PrivilegeSnippetRepository(outputView, settingsProvider);

			// ui initialization

			this.viewModel = new MainViewModel(
				this.outputView, 
				this.messenger, 
				settingsProvider,
				roleTemplateBuilder, 
				roleRepository, 
				businessUnitRepository,
				systemUserRepository);
			this.scheduler = new AsyncJobScheduler(this, this.viewModel);
			this.messenger.RegisterJobScheduler(scheduler);



			this.outputView.Show(this.dockPanel, DockState.DockBottom);

			var helpContentIndexProvider = new HelpContentIndexProvider();
			var helpContentIndex = helpContentIndexProvider.GetIndex();
			var helpRepository = new HelpRepository(helpContentIndex, GetType().Assembly);
			var helpView = new HelpView(this.messenger, this.outputView, helpRepository, Topics.Home);

			helpView.Show(this.dockPanel, DockState.DockRight);
			helpView.DockState = DockState.DockRightAutoHide;

			var userBrowserView = new UserBrowserView(this.outputView, messenger, roleRepository);
			userBrowserView.Show(this.dockPanel, DockState.DockLeft);


			var roleBrowserView = new RoleBrowserView(this.outputView, messenger, settingsProvider, roleRepository);
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
			this.viewModel.ShowUserRolesView += OnShowUserRolesView;
			this.viewModel.UserRolesViewClosed += OnCloseUserRolesView;


			this.messenger.Register<ShowHelp>(m =>
			{
				helpView.Show();
			});


			this.messenger.Register<OpenUsageInspector>(m =>
			{
				var view = new UsageInspectorView(this.dependencyRepository, m.Role);
				view.Show(this.dockPanel, DockState.Document);
			});

			this.messenger.Register<RoleComparisonResult>(m =>
			{
				var view = new RoleComparerView(this.settingsProvider, m);
				view.Show(this.dockPanel, DockState.Document);
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
				if (e.Roles.Length == 1)
				{
					var editor = new Editor.RoleEditorView(
						this.settingsProvider,
						this.privilegeSnippetRepository,
						this.privilegeClassificationProvider,
						e.Roles[0]);

					foreach (var role in e.Roles)
					{
						this.roleViewDict[role] = editor;
					}

					editor.Show(this.dockPanel, DockState.Document);
				}
				else
				{
					var editor = new BulkEditorView(
						this.settingsProvider,
						this.privilegeSnippetRepository,
						this.privilegeClassificationProvider,
						e.Roles);

					foreach (var role in e.Roles)
					{
						this.roleViewDict[role] = editor;
					}

					editor.Show(this.dockPanel, DockState.Document);
				}			
			}
		}

		private void OnShowUserRolesView(object sender, UserRolesViewOpen e)
		{
			lock(this.syncRoot)
			{
				if (!this.userRolesViewDict.TryGetValue(e.Environment, out var view))
				{
					view = new UserRolesView(e.Environment, this.roleRepository);
					this.userRolesViewDict[e.Environment] = view;
				}

				view.Show(this.dockPanel, DockState.Document);
			}
		}

		private void OnCloseUserRolesView(object sender, UserRolesViewClosed e)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new Action(() => OnCloseUserRolesView(sender, e)));
				return;
			}

			lock (this.syncRoot)
			{
				if (!this.userRolesViewDict.TryGetValue(e.Environment, out var view))
				{
					return;		
				}

				if (!view.IsDisposed)
				{
					view.Dispose();
				}

				this.userRolesViewDict.Remove(e.Environment);
			}
		}

		private void OnShowRoleRequested(object sender, OpenRoleView e)
		{
			lock (this.syncRoot)
			{
				if (!this.roleViewDict.TryGetValue(e.Roles[0], out var editor)) return;
				editor.Show(this.dockPanel, DockState.Document);
			}
		}

		private void OnCloseRoleRequested(object sender, CloseRoleView e)
		{
			lock(this.syncRoot)
			{
				foreach (var role in e.Roles)
				{
					this.roleViewDict.Remove(role);
				}
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
