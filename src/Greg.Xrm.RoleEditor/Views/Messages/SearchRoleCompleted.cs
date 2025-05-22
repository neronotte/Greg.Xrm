using Greg.Xrm.RoleEditor.Model;

namespace Greg.Xrm.RoleEditor.Views.Messages
{
	public class SearchRoleCompleted
	{
		public SearchRoleCompleted(Role[] roles, string searchDescription)
		{
			Roles = roles;
			SearchDescription = searchDescription;
		}

		/// <summary>
		/// A meaningful description of what the user was searching for.
		/// </summary>
		public string SearchDescription { get; }

		/// <summary>
		/// The list of roles that matched the search criteria.
		/// </summary>
		public Role[] Roles { get; }
	}
}
