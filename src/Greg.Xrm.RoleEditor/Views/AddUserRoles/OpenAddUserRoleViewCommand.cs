using Greg.Xrm.Messaging;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.AddUserRoles;
using Greg.Xrm.Views;

namespace Greg.Xrm.RoleEditor.Views.Browser
{
	public class OpenAddUserRoleViewCommand : CommandBase<DataverseEnvironment>
	{
		private readonly IMessenger messenger;

		public OpenAddUserRoleViewCommand(IMessenger messenger)
		{
			this.messenger = messenger;
		}

		protected override void ExecuteInternal(DataverseEnvironment arg)
		{
			this.messenger.Send(new UserRolesViewOpen(arg));
		}
	}
}
