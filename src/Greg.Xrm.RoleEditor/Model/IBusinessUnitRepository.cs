using Greg.Xrm.Core;
using System;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Model
{
	public interface IBusinessUnitRepository
	{
		(BusinessUnit, Dictionary<Guid, BusinessUnit>) GetTree(IXrmToolboxPluginContext context);
	}
}
