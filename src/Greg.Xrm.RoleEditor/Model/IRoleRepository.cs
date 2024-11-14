using Greg.Xrm.Core;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Model
{
	public interface IRoleRepository
	{
		IReadOnlyList<Role> GetParentRoles(IXrmToolboxPluginContext executionContext, TemplateForRole template);

		IReadOnlyList<Role> GetRolesByPrivilege(IXrmToolboxPluginContext executionContext, string privilegeName, TemplateForRole template);
	}
}
