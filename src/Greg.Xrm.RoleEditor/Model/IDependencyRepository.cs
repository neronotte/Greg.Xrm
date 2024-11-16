using Microsoft.Xrm.Sdk;

namespace Greg.Xrm.RoleEditor.Model
{
	public interface IDependencyRepository
	{
		DependencyList GetRoleDependencies(IOrganizationService crm, Role role, bool? forDelete = false);
	}
}
