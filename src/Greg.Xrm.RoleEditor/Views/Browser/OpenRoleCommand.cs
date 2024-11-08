using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.Views;
using Greg.Xrm.RoleEditor.Views.Messages;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views.RoleBrowser
{
	public class OpenRoleCommand : CommandBase<Role>
	{
		public OpenRoleCommand()
        {
			this.CanExecute = false;
		}


		protected override void ExecuteInternal(Role role)
		{
			if (role == null) return;

			var messenger = role.ExecutionContext.Messenger;
			var log = role.ExecutionContext.Log;

			if (role.Privileges.Count > 0)
			{
				messenger.Send(new OpenRoleView(role));
				return;
			}


			messenger.Send(new WorkAsyncInfo
			{
				Message = "Reading role details...",
				Work = (worker, args) =>
				{
					messenger.Send<Freeze>();
					role.ReadPrivileges();
				},
				PostWorkCallBack = e =>
				{
					messenger.Send<Unfreeze>();
					if (e.Error != null)
					{
						log.Error("Error retrieving role info: " + e.Error.Message, e.Error);
						return;
					}

					messenger.Send(new OpenRoleView(role));
				}
			});
		}
	}
}
