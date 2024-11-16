using Greg.Xrm.RoleEditor.Model;
using System;

namespace Greg.Xrm.RoleEditor.Views.Browser
{
	public class RoleEventArgs : EventArgs
	{
        public RoleEventArgs(Role role)
        {
			Role = role;
		}

        public Role Role { get; }
	}
}
