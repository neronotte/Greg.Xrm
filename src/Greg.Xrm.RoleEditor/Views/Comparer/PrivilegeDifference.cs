using Greg.Xrm.RoleEditor.Model;
using Microsoft.Crm.Sdk.Messages;

namespace Greg.Xrm.RoleEditor.Views.Comparer
{
	public class PrivilegeDifference : TreeNode
	{
		public PrivilegeDifference(string privilegeName, PrivilegeDepth? level1, PrivilegeDepth? level2, string tooltip = null)
			: base(privilegeName, tooltip: tooltip)
		{
			PrivilegeName = privilegeName;
			Level1 = level1.GetLevel();
			Level2 = level2.GetLevel();
		}

		public string PrivilegeName { get; set; }

		public Level Level1 { get; set; }
		public Level Level2 { get; set; }
	}
}
