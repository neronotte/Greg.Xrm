using Greg.Xrm.RoleEditor.Model;
using System;

namespace Greg.Xrm.RoleEditor.Views.AddUserRoles
{
	public class UserRolesViewOpen
	{

		public UserRolesViewOpen(DataverseEnvironment env)
		{
			this.Environment = env ?? throw new ArgumentNullException(nameof(env));
		}

		public DataverseEnvironment Environment { get; }
	}
}
