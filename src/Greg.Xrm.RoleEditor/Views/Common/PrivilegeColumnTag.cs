using Microsoft.Xrm.Sdk.Metadata;

namespace Greg.Xrm.RoleEditor.Views.Common
{
	/// <summary>
	/// Tag that is "attached" to a privilege column of the editors grid
	/// </summary>
	public class PrivilegeColumnTag
	{
		public PrivilegeColumnTag(PrivilegeType privilegeType)
		{
			PrivilegeType = privilegeType;
		}

		public PrivilegeType PrivilegeType { get; }
	}
}
