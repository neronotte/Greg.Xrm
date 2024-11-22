using Greg.Xrm.RoleEditor.Model;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Views.UserBrowser
{
	public class UserGroup : List<SystemUser>
	{
		public UserGroup(string name, bool expandedByDefault = false)
		{
			this.Name = name;
			ExpandedByDefault = expandedByDefault;
		}

		public string Name { get; }

		public bool ExpandedByDefault { get; }

		public bool Match(string searchCriteria)
		{
			if (string.IsNullOrWhiteSpace(searchCriteria)) return true;

			return this.Exists(x => x.Match(searchCriteria));
		}
	}
}
