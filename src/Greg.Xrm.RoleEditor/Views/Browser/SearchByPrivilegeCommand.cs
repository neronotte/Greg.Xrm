using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.RoleBrowser;
using Greg.Xrm.RoleEditor.Views.Search;
using Greg.Xrm.Views;
using System.Windows.Forms;

namespace Greg.Xrm.RoleEditor.Views.Browser
{
	public class SearchByPrivilegeCommand : CommandBase<RoleBrowserView>
	{
		private readonly RoleBrowserViewModel viewModel;
		private readonly IRoleRepository roleRepository;

		public SearchByPrivilegeCommand(RoleBrowserViewModel viewModel, IRoleRepository roleRepository)
        {
			this.viewModel = viewModel;
			this.roleRepository = roleRepository;

			this.viewModel.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(this.viewModel.IsEnabled))
					RefreshCanExecute();
			};
		}

		private void RefreshCanExecute()
		{
			this.CanExecute = this.viewModel.IsEnabled;
		}

		protected override void ExecuteInternal(RoleBrowserView owner)
		{
			var environment = this.viewModel.GetSelectedRoleEnvironmentOrDefault();
			if (environment == null) return;

			using (var dialog = new SearchByPrivilegeDialog(environment, this.roleRepository))
			{
				dialog.StartPosition = FormStartPosition.CenterParent;
				dialog.ShowDialog(owner);
			}
		}
	}
}
