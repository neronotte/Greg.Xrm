using Greg.Xrm.Messaging;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.Views;
using Greg.Xrm.RoleEditor.Views.Messages;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views.RoleBrowser
{
	public class OpenRoleCommand : CommandBase<Role>
	{
		private readonly IMessenger messenger;

		public OpenRoleCommand(IMessenger messenger)
        {
			this.messenger = messenger;
		}


		protected override void ExecuteInternal(Role arg)
		{
			if (arg == null) return;
			this.messenger.Send(new OpenRoleView(arg));
		}
	}
}
