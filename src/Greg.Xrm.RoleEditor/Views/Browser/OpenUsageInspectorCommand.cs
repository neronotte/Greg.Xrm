using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;

namespace Greg.Xrm.RoleEditor.Views.Browser
{
	public class OpenUsageInspectorCommand : CommandBase<Role>
	{
		protected override void ExecuteInternal(Role role)
		{
			if (role == null) return;

			role.ExecutionContext.Messenger.Send(new OpenUsageInspector(role));
		}
	}
}
