using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Model
{
	public interface ITemplateForTable : IEnumerable<KeyValuePair<PrivilegeType, SecurityPrivilegeMetadata>>
	{
		SecurityPrivilegeMetadata this[PrivilegeType privilegeType] { get; }

		string Name { get;  }

		string LogicalName { get; }
	}
}
