using Greg.Xrm.RoleEditor.Model;
using System;

namespace Greg.Xrm.RoleEditor.Views.AddUserRoles
{
    public class UserRolesViewClosed
	{

		public UserRolesViewClosed(DataverseEnvironment env)
		{
			this.Environment = env ?? throw new ArgumentNullException(nameof(env));
		}

		public DataverseEnvironment Environment { get; }
	}
}
