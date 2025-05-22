using Greg.Xrm.RoleEditor.Model;

namespace Greg.Xrm.RoleEditor.Views.Messages
{
	public class RefreshRoleBrowser
	{
		public RefreshRoleBrowser(Role role)
		{
			Role = role;
		}

		public Role Role { get; }
	}
}
