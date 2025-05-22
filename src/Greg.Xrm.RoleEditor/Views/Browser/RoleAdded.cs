using Greg.Xrm.RoleEditor.Model;

namespace Greg.Xrm.RoleEditor.Views.Browser
{
	public class RoleAdded
	{
		public RoleAdded(Role role)
		{
			Role = role;
		}

		public Role Role { get; }
	}
}
