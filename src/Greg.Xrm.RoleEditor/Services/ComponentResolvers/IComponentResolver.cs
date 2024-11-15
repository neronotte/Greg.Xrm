using System;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Services.ComponentResolvers
{
	public interface IComponentResolver
	{
		Dictionary<Guid, string> GetNames(IReadOnlyList<Guid> componentIdSet);
	}
}
