using BrightIdeasSoftware;
using Greg.Xrm.Core.Views.Help;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.RoleEditor.Help;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Browser;
using Greg.Xrm.RoleEditor.Views.Messages;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.RoleEditor.Views.RoleBrowser
{
	public partial class RoleBrowserView : DockContent
	{
		private readonly RoleBrowserViewModel viewModel;


		public RoleBrowserView(
			ILog log,
			IMessenger messenger,
			ISettingsProvider<Settings> settingsProvider,
			IRoleRepository roleRepository)
		{
			this.RegisterHelp(messenger, Topics.Browser);

			this.DockHandler.CloseButton = false;
			this.DockHandler.AllowEndUserDocking = false;
			this.DockHandler.AllowEndUserDocking = false;


			InitializeComponent();
			this.Text = this.TabText = "Role Browser";



			this.roleTree.UseFiltering = true;
			this.roleTree.FullRowSelect = true;
			this.roleTree.HideSelection = false;
			this.roleTree.IsSimpleDragSource = true;
			this.cTreeName.Sortable = false;
			this.tSearchText.Enabled = false;
			this.viewModel = new RoleBrowserViewModel(log, messenger, settingsProvider, roleRepository);


			this.tMoreFilters_HideNotCustomizableRolesToolStripMenuItem.Bind(
				x => x.Checked,
				this.viewModel,
				vm => vm.ShouldHideNotCustomizableRoles);
			this.tMoreFilters_HideNotCustomizableRolesToolStripMenuItem.BindCommand(() => viewModel.ShouldHideNotCustomizableRolesCommand);

			this.tMoreFilters_HideManagedRolesToolStripMenuItem.Bind(
				x => x.Checked,
				this.viewModel,
				vm => vm.ShouldHideManagedRoles);
			this.tMoreFilters_HideManagedRolesToolStripMenuItem.BindCommand(() => viewModel.ShouldHideManagedRolesCommand);

			this.tools.Bind(x => x.Enabled, viewModel, vm => vm.IsEnabled);

			this.roleTree.Bind(x => x.Enabled, viewModel, vm => vm.IsEnabled);
			this.roleTree.Bind(x => x.EmptyListMsg, this.viewModel, vm => vm.EmptyListMessage);

			this.roleTree.CanExpandGetter = x =>
			{
				if (x is DataverseEnvironment e) return e.Count > 0;
				if (x is BusinessUnit b) return b.Children.Count > 0 || b.Roles.Count > 0;
				return false;
			};

			this.roleTree.ChildrenGetter = x =>
			{
				if (x is DataverseEnvironment e) return e;
				if (x is BusinessUnit b) return b.Children.Cast<object>().Union(b.Roles);
				return null;
			};

			this.cTreeName.ImageGetter = x =>
			{
				if (x is DataverseEnvironment) return "env";
				if (x is BusinessUnit) return "bu";
				return "role";
			};






			this.tNew.Bind(x => x.Enabled, this.viewModel, vm => vm.IsEnabled);
			this.tNewFromBlank.BindCommand(() => viewModel.NewRoleCommand, () => this.viewModel.GetSelectedRoleEnvironmentOrDefault());
			this.tNewFromAppOpener.BindCommand(() => viewModel.NewRoleFromAppOpenerCommand);
			this.tNewFromBasicUser.BindCommand(() => viewModel.NewRoleFromBasicUserCommand);
			this.tNewCloneCurrent.BindCommand(() => viewModel.NewRoleFromCurrentCommand);
			this.tNewCloneCurrent2.BindCommand(() => viewModel.NewRoleFromCurrentCommand);

			this.viewModel.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(RoleBrowserViewModel.Environments))
				{
					this.roleTree.Roots = this.viewModel.Environments;
					this.roleTree.ExpandAll();
				}
				if (e.PropertyName == nameof(RoleBrowserViewModel.ShouldHideNotCustomizableRoles) ||
					e.PropertyName == nameof(RoleBrowserViewModel.ShouldHideManagedRoles))
				{
					RefreshModelFilter();
				}
				if (e.PropertyName == nameof(RoleBrowserViewModel.IsEnabled))
				{
					this.tSearchText.Enabled = this.viewModel.IsEnabled;
				}
			};

			this.roleTree.FormatRow += (s, e) =>
			{
				if (!(e.Model is Role role)) return;

				if (!role.iscustomizable)
				{
					e.Item.ForeColor = Color.Gray;
				}
			};

			this.roleTree.SelectedIndexChanged += (s, e) =>
			{
				this.viewModel.SelectedRole = this.roleTree.SelectedObject as Role;
			};
			this.roleTree.MouseDoubleClick += (s, e) =>
			{
				if (this.viewModel.SelectedRole == null) return;
				this.viewModel.OpenRoleCommand.Execute(this.viewModel.SelectedRole);
			};
			this.roleTree.CellRightClick += (s, e) =>
			{
				var selectedRoles = this.roleTree.SelectedObjects.OfType<Role>().ToArray();
				var environmentList = this.viewModel.GetRoleEnvironment(selectedRoles);

				// the multiple edit is available only if roles to edit are in the same environment
				this.tEditMultiple.Visible = selectedRoles.Length > 1
					&& selectedRoles.Length <= 15
					&& environmentList.Length == 1;
				this.tNewCloneCurrent2.Visible = selectedRoles.Length == 1;
				this.tInspectUsage.Visible = selectedRoles.Length == 1;
				this.tCompareRoles.Visible = selectedRoles.Length == 2;

				var selectedEnvironment = this.roleTree.SelectedObjects.OfType<DataverseEnvironment>().ToArray();
				this.tAddUserRoles.Visible = selectedEnvironment.Length == 1;
				this.tAddUserRoles.Tag = selectedEnvironment.FirstOrDefault();

				this.contextMenu.Show(this.roleTree, e.Location);
			};
			this.tEditMultiple.Click += (s, e) =>
			{
				var roles = this.roleTree.SelectedObjects.OfType<Role>().ToArray();
				if (roles.Length == 0) return;
				if (roles.Length == 1)
				{
					this.viewModel.OpenRoleCommand.Execute(this.viewModel.SelectedRole);
					return;
				}

				this.viewModel.OpenMultipleRolesCommand.Execute(roles);
			};
			this.tInspectUsage.Click += (s, e) =>
			{
				var roles = this.roleTree.SelectedObjects.OfType<Role>().ToArray();
				if (roles.Length == 0) return;
				this.viewModel.OpenUsageInspectorCommand.Execute(roles[0]);
			};
			this.tCompareRoles.Click += (s, e) =>
			{
				var roles = this.roleTree.SelectedObjects.OfType<Role>().ToArray();
				if (roles.Length != 2) return;
				this.viewModel.CompareRolesCommand.Execute((roles[0], roles[1]));
			};
			this.tAddUserRoles.Click += (s, e) =>
			{
				var environment = this.tAddUserRoles.Tag as DataverseEnvironment;
				if (environment == null) return;
				this.viewModel.OpenAddUserRoleViewCommand.Execute(environment);
			};

			this.tSearchText.KeyUp += (s, e) =>
			{
				RefreshModelFilter();
				e.Handled = true;
			};

			RefreshModelFilter();


			var textOverlay = this.roleTree.EmptyListMsgOverlay as TextOverlay;
			textOverlay.TextColor = Color.Gray;
			textOverlay.BackColor = Color.White;
			textOverlay.BorderColor = Color.White;
			textOverlay.BorderWidth = 1f;
			textOverlay.Font = new Font(this.Font.FontFamily, 12f);


			this.tSearchRoleByPrivilege.BindCommand(() => this.viewModel.SearchByPrivilegeCommand, () => this);
			this.tSearchRoleBySolution.BindCommand(() => this.viewModel.SearchBySolutionCommand, () => this);
			messenger.Register<SearchRoleCompleted>(m =>
			{
				if (m.Roles.Length == 0)
				{
					MessageBox.Show(this, "No roles found.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				this.tools.Visible = false;
				this.roleTree.Visible = false;

				var control = new SearchResultView();
				control.SearchDescription = m.SearchDescription;
				control.Roles = m.Roles;
				control.Dock = DockStyle.Fill;
				control.SelectedRolesChanged += (s, e) =>
				{
					this.viewModel.SelectedRole = control.SelectedRoles.FirstOrDefault();
				};
				control.RoleDoubleClicked += (s, e) =>
				{
					this.viewModel.OpenRoleCommand.Execute(e.Role);
				};
				this.Controls.Add(control);

				control.BackClicked += (s, e) =>
				{
					this.tools.Visible = true;
					this.roleTree.Visible = true;
					this.Controls.Remove(control);
					control.Dispose();
				};

				control.CloneSelectedRoleClicked += (s, e) =>
				{
					this.viewModel.SelectedRole = e.Role;
					this.viewModel.NewRoleFromCurrentCommand.Execute(null);
				};
			});
		}





		private void RefreshModelFilter()
		{
			var searchText = this.tSearchText.Text?.ToLowerInvariant();

			var modelFilter = new ModelFilter(model =>
			{
				if (model is BusinessUnit businessUnit) return businessUnit.HasAnyRole();
				if (!(model is Role role)) return true;
				if (!string.IsNullOrWhiteSpace(searchText) && !role.name.ToLowerInvariant().Contains(searchText)) return false;
				if (this.viewModel.ShouldHideNotCustomizableRoles && !role.iscustomizable) return false;
				if (this.viewModel.ShouldHideManagedRoles && role.ismanaged) return false;

				return true;
			});

			this.roleTree.ModelFilter = modelFilter;
		}
	}
}
