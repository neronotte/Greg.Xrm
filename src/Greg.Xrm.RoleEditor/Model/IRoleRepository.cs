using Greg.Xrm.Core;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Model
{
	public interface IRoleRepository
	{
		IReadOnlyList<Role> GetParentRoles(IXrmToolboxPluginContext executionContext, TemplateForRole template);

		IReadOnlyList<Role> GetRolesByPrivilege(IXrmToolboxPluginContext executionContext, string privilegeName, TemplateForRole template);

		IReadOnlyList<Role> GetRolesBySolution(IXrmToolboxPluginContext executionContext, EntityReference solutionRef, TemplateForRole template);
	}
}
