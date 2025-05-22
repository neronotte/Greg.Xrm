using Greg.Xrm.RoleEditor.Model;

namespace Greg.Xrm.RoleEditor.Views.Messages
{
	public class OpenRoleView
	{
		public OpenRoleView(params Role[] roles)
		{
			if (roles == null || roles.Length == 0)
				throw new System.ArgumentNullException(nameof(roles));

			Roles = roles;
		}

		public Role[] Roles { get; }
	}


	public class CloseRoleView
	{
		public CloseRoleView(params Role[] roles)
		{
			if (roles == null || roles.Length == 0)
				throw new System.ArgumentNullException(nameof(roles));

			Roles = roles;
		}

		public Role[] Roles { get; }
	}
}
