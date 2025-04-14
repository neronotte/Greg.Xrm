using Greg.Xrm.Core;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Model
{
	public interface IRoleRepository
	{
		IReadOnlyList<Role> GetParentRoles(IXrmToolboxPluginContext executionContext, TemplateForRole template);

		IReadOnlyList<Role> GetRolesByPrivilege(IXrmToolboxPluginContext executionContext, string privilegeName, TemplateForRole template);

		IReadOnlyList<Role> GetRolesBySolution(IXrmToolboxPluginContext executionContext, EntityReference solutionRef, TemplateForRole template);

		IReadOnlyList<Role> GetRolesByUser(IXrmToolboxPluginContext executionContext, EntityReference userRef, TemplateForRole template);

		IReadOnlyList<Role> GetRolesByNamesAndBusinessUnit(IXrmToolboxPluginContext executionContext, List<Tuple<string, EntityReference>> tuplesToFetch, TemplateForRole template);
	}
}
