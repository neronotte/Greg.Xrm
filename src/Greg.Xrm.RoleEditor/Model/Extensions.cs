using Greg.Xrm.RoleEditor.Model;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace Greg.Xrm.RoleEditor
{
	public static class Extensions
	{
		public static bool[] GetValidLevels(this SecurityPrivilegeMetadata privilegeMetadata)
		{
			var result = new bool[5];
			result[0] = true;
			result[1] = privilegeMetadata.CanBeBasic;
			result[2] = privilegeMetadata.CanBeLocal;
			result[3] = privilegeMetadata.CanBeDeep;
			result[4] = privilegeMetadata.CanBeGlobal;
			return result;
		}

		public static bool IsValidLevel(this SecurityPrivilegeMetadata privilegeMetadata, Level nextValue)
		{
			if (nextValue == Level.None) return true;
			if (nextValue == Level.User && privilegeMetadata.CanBeBasic) return true;
			if (nextValue == Level.BusinessUnit && privilegeMetadata.CanBeLocal) return true;
			if (nextValue == Level.ParentChild && privilegeMetadata.CanBeDeep) return true;
			if (nextValue == Level.Organization && privilegeMetadata.CanBeGlobal) return true;
			return false;
		}


		public static Level GetLevel(this RolePrivilege rolePrivilege)
		{
			if (rolePrivilege == null) return Level.None;

			var depth = rolePrivilege.Depth;
			switch (depth)
			{
				case PrivilegeDepth.Basic:
					return Level.User;
				case PrivilegeDepth.Local:
					return Level.BusinessUnit;
				case PrivilegeDepth.Deep:
					return Level.ParentChild;
				case PrivilegeDepth.Global:
					return Level.Organization;
			}
			return Level.None;
		}
		public static PrivilegeDepth? ToPrivilegeDepth(this Level level)
		{
			switch(level)
			{
				case Level.None:
					return null;
				case Level.User:
					return PrivilegeDepth.Basic;
				case Level.BusinessUnit:
					return PrivilegeDepth.Local;
				case Level.ParentChild:
					return PrivilegeDepth.Deep;
				case Level.Organization:
					return PrivilegeDepth.Global;
				default:
					return null;
			}
		}
	}
}
