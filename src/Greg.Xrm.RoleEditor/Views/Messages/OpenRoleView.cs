using Greg.Xrm.RoleEditor.Model;

namespace Greg.Xrm.RoleEditor.Views.Messages
{
	public class OpenRoleView
	{
		public OpenRoleView(Role role)
		{
			Role = role;
		}

		public Role Role { get; }
	}
	public class CloseRoleView
	{
		public CloseRoleView(Role role)
		{
			Role = role;
		}

		public Role Role { get; }
	}
}
