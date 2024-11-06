using Microsoft.Xrm.Sdk;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Model
{
	public interface IRoleRepository
	{
		IReadOnlyList<Role> GetParentRoles(IOrganizationService crm);
	}
}
