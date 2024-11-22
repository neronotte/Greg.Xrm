using Greg.Xrm.RoleEditor.Model;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Views.Comparer
{
	public class RoleComparisonResult : List<TreeNode>
	{
        public RoleComparisonResult(Role role1, Role role2)
        {
			Role1 = role1 ?? throw new System.ArgumentNullException(nameof(role1));
			Role2 = role2 ?? throw new System.ArgumentNullException(nameof(role2));
		}
        public Role Role1 { get; }
		public Role Role2 { get; }

	}
}
