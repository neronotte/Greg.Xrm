using BrightIdeasSoftware;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.RoleEditor.Model;
using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.RoleEditor.Views.RoleBrowser
{
	public partial class RoleBrowserView : DockContent
	{
		private readonly RoleBrowserViewModel viewModel;

		public RoleBrowserView(ILog log, IMessenger messenger)
		{
			InitializeComponent();
			this.Text = this.TabText = "Role Browser";

			this.roleList.UseFiltering = true;
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

			this.tNew.Bind(
				x => x.Enabled,
				this.viewModel,
				vm => vm.IsEnabled);

			this.tNewCloneCurrent.Bind(x => x.Enabled, this.viewModel, vm => vm.IsEnabled);
			this.tNewFromAppOpener.Bind(x => x.Enabled, this.viewModel, vm => vm.IsEnabled);
			this.tNewFromBasicUser.Bind(x => x.Enabled, this.viewModel, vm => vm.IsEnabled);
			this.tNewFromBlank.Bind(x => x.Enabled, this.viewModel, vm => vm.IsEnabled);
			this.roleList.Bind(x => x.EmptyListMsg, this.viewModel, vm => vm.EmptyListMessage);

			this.viewModel.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(RoleBrowserViewModel.RoleList))
				{
					this.roleList.SetObjects(this.viewModel.RoleList);
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

			this.roleList.FormatRow += (s, e) =>
			{
				if (!(e.Model is Role role)) return;
				
				if (!role.iscustomizable)
				{
					e.Item.ForeColor = System.Drawing.Color.Gray;
				}
			};

			this.roleList.SelectedIndexChanged += (s, e) =>
			{
				this.viewModel.SelectedRole = this.roleList.SelectedObject as Role;
			};
			this.roleList.MouseDoubleClick += (s, e) =>
			{
				this.viewModel.OpenRoleCommand.Execute(this.viewModel.SelectedRole);
			};

			this.tSearchText.KeyUp += (s, e) =>
			{
				RefreshModelFilter();
				e.Handled = true;
			};

			RefreshModelFilter();

			var textOverlay = this.roleList.EmptyListMsgOverlay as TextOverlay;
			textOverlay.TextColor = Color.Gray;
			textOverlay.BackColor = Color.White;
			textOverlay.BorderColor = Color.White;
			textOverlay.BorderWidth = 1f;
			textOverlay.Font = new Font(this.Font.FontFamily, 12f);


			this.tools.Bind(x => x.Enabled, viewModel, vm => vm.IsEnabled);
			this.roleList.Bind(x => x.Enabled, viewModel, vm => vm.IsEnabled);
		}





		private void RefreshModelFilter()
		{
			var searchText = this.tSearchText.Text;
			this.roleList.ModelFilter = new ModelFilter(model =>
			{
				if (!(model is Role role)) return false;
				if (!string.IsNullOrWhiteSpace(searchText) && !role.name.Contains(searchText)) return false;
				if (this.viewModel.ShouldHideNotCustomizableRoles && !role.iscustomizable) return false;
				if (this.viewModel.ShouldHideManagedRoles && role.ismanaged) return false;

				return true;
			});
		}
	}
}
