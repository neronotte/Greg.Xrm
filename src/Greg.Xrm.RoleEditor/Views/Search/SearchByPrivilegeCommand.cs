using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using System;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views.Search
{
	public class SearchByPrivilegeCommand : CommandBase<SearchByPrivilegeDialog>
	{
		private readonly SearchByPrivilegeViewModel viewModel;
		private readonly DataverseEnvironment environment;
		private readonly IRoleRepository roleRepository;

		public SearchByPrivilegeCommand(
			SearchByPrivilegeViewModel viewModel,
			DataverseEnvironment environment,
			IRoleRepository roleRepository)
		{
			this.viewModel = viewModel;
			this.environment = environment;
			this.roleRepository = roleRepository;
		}

		protected override void ExecuteInternal(SearchByPrivilegeDialog owner)
		{
			var messenger = this.environment.Context.Messenger;
			var crm = this.environment.Context;
			var log = this.environment.Context.Log;
			this.CanExecute = false;


			messenger.Send(new WorkAsyncInfo
			{
				Message = "Searching, please wait...",
				Work = (worker, e) =>
				{
					messenger.Send<Freeze>();
					string searchDescription = "Search by privilege";
					try
					{
						var privilegeName = GetPrivilegeName();
						if (string.IsNullOrWhiteSpace(privilegeName))
						{
							MessageBox.Show("Please select the privilege to look for", "Search role by privilege", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							return;
						}

						searchDescription = $"Roles having \"{privilegeName}\" privilege";

						var roles = this.roleRepository.GetRolesByPrivilege(crm, privilegeName, this.environment.Template);
						var foundRoleIds = roles.Select(x => x.Id).ToList();

						var environmentRoles = this.environment.GetAllRoles();
						var result = environmentRoles.Where(role => foundRoleIds.Contains(role.Id)).ToArray();
						e.Result = new SearchRoleCompleted(result, searchDescription);
					}
					catch (Exception ex)
					{
						log.Error("Error while searching for roles: " + ex.Message, ex);
						e.Result = new SearchRoleCompleted(new Role[0], searchDescription);
					}
					finally
					{
						messenger.Send<Unfreeze>();
					}
				},
				PostWorkCallBack = e =>
				{

					this.CanExecute = true;
					var result = (e.Result as SearchRoleCompleted);
					if (result == null) return;
					messenger.Send(result);

					owner.DialogResult = DialogResult.OK;
					owner.Close();
				}
			});
		}

		private string GetPrivilegeName()
		{
			if (this.viewModel.IsSearchByName)
			{
				return this.viewModel.SelectedPrivilegeName;
			}


			if (this.viewModel.SelecteMiscPrivilege != null)
			{
				var miscTemplate = this.viewModel.Misc.FirstOrDefault(x => x.Name == this.viewModel.SelecteMiscPrivilege.Name);
				return miscTemplate?.PrivilegeName;
			}

			if (this.viewModel.SelectedTable != null)
			{
				var tableTemplate = this.viewModel.SelectedTable.Value;
				var privilegeType = this.viewModel.SelectedPrivilegeType;
				if (tableTemplate == null || privilegeType == null)
				{
					return null;
				}

				return tableTemplate.GetAllPrivileges().FirstOrDefault(x => x.PrivilegeType == privilegeType)?.Name;
			}

			return null;
		}
	}
}
