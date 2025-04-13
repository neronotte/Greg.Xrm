using Greg.Xrm.RoleEditor.Model;

namespace Greg.Xrm.RoleEditor.Views.UserBrowser
{
    public class RefreshUserRequest
    {
		public RefreshUserRequest(SystemUser user)
		{
			User = user;
		}

		public SystemUser User { get; }
	}
}
