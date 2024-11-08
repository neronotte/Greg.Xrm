using BrightIdeasSoftware;
using Greg.Xrm.Core.Views.Help;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.RoleEditor.Help;
using Greg.Xrm.RoleEditor.Model;
using System.Drawing;
using System.Linq;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.RoleEditor.Views.RoleBrowser
{
	public partial class RoleBrowserView : DockContent
	{
		private readonly RoleBrowserViewModel viewModel;


		public RoleBrowserView(ILog log, IMessenger messenger)
		{
			this.RegisterHelp(messenger, Topics.Browser);

			this.DockHandler.CloseButton = false;
			this.DockHandler.AllowEndUserDocking = false;
			this.DockHandler.AllowEndUserDocking = false;

			InitializeComponent();
			this.Text = this.TabText = "Role Browser";

			this.roleTree.UseFiltering = true;
			this.roleTree.FullRowSelect = true;
			this.cTreeName.Sortable = false;
			this.tSearchText.Enabled = false;
			this.viewModel = new RoleBrowserViewModel(log, messenger);


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
