using Greg.Xrm.RoleEditor.Model;
using Microsoft.Xrm.Sdk.Metadata;

namespace Greg.Xrm.RoleEditor.Views.Common
{
	public interface IPrivilegeHolder
	{
		Level? this[PrivilegeType privilegeType] { get; set; }
	}
}
