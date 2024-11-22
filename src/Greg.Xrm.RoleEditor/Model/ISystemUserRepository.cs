using Greg.Xrm.Core;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Model
{
	public interface ISystemUserRepository
	{
		IReadOnlyList<SystemUser> GetActiveUsers(IXrmToolboxPluginContext executionContext, TemplateForRole template);
	}
}
