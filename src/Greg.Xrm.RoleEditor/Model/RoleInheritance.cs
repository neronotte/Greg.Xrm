using System.ComponentModel;

namespace Greg.Xrm.RoleEditor.Model
{
	/// <summary>
	/// 0 - Team privileges only
	/// 1 - Direct User (Basic) access level and Team privileges
	/// </summary>
	public enum RoleInheritance
	{
		[Description("Team privileges only")]
		TeamPrivilegesOnly = 0,

		[Description("Direct User (Basic) access level and Team privileges")]
		DirectUserAndTeamPrivileges = 1
	}
}
