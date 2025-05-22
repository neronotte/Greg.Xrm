using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using System;
using System.Linq;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views.RoleBrowser
{
	public class OpenMultipleRolesCommand : CommandBase<Role[]>
	{
		public OpenMultipleRolesCommand()
		{
			this.CanExecute = false;
		}


		protected override void ExecuteInternal(Role[] roles)
		{
			if (roles == null || roles.Length == 0) return;

			var messenger = roles[0].ExecutionContext.Messenger;
			var log = roles[0].ExecutionContext.Log;

			if (Array.TrueForAll(roles, x => x.Privileges.Count > 0))
			{
				messenger.Send(new OpenRoleView(roles));
				return;
			}


			messenger.Send(new WorkAsyncInfo
			{
				Message = "Reading role details...",
				Work = (worker, args) =>
				{
					messenger.Send<Freeze>();
					foreach (var role in roles.Where(x => x.Privileges.Count == 0))
					{
						role.ReadPrivileges();
					}
				},
				PostWorkCallBack = e =>
				{
					messenger.Send<Unfreeze>();
					if (e.Error != null)
					{
						log.Error("Error retrieving role info: " + e.Error.Message, e.Error);
						return;
					}

					messenger.Send(new OpenRoleView(roles));
				}
			});
		}
	}
}
