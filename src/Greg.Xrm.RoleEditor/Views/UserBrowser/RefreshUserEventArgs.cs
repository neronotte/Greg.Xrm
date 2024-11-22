using Greg.Xrm.RoleEditor.Model;
using System;

namespace Greg.Xrm.RoleEditor.Views.UserBrowser
{
	public class RefreshUserEventArgs : EventArgs
	{
		public RefreshUserEventArgs(SystemUser user)
		{
			User = user;
		}

		public SystemUser User { get; }
	}

}
