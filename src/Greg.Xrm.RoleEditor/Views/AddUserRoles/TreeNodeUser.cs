using Greg.Xrm.RoleEditor.Model;

namespace Greg.Xrm.RoleEditor.Views.AddUserRoles
{
	public class TreeNodeUser : TreeNode
	{
		private readonly SystemUser user;

		public TreeNodeUser(SystemUser user)
		{
			this.user = user;
			this.Name = user.fullname;
			this.BusinessUnit = user.businessunitidFormatted;
		}


		public string DomainName => this.user.domainname;

		public override int GetHashCode()
		{
			return this.user.Id.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (!(obj is TreeNodeUser other)) return false;

			return this.user.Id == other.user.Id;
		}

		public SystemUser GetUser()
		{
			return this.user;
		}
	}
}
