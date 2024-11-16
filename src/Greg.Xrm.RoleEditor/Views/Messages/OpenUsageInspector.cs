using Greg.Xrm.RoleEditor.Model;

namespace Greg.Xrm.RoleEditor.Views.Messages
{
	public class OpenUsageInspector
	{
        public OpenUsageInspector(Role role)
        {
			this.Role = role;
		}

		public Role Role { get; }
	}
}
