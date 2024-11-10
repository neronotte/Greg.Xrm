using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Model
{
	public class TemplateForTableSet : ITemplateForTable
	{
		private readonly EntityMetadata[] tableSet;
		private readonly Dictionary<PrivilegeType, SecurityPrivilegeMetadata> privilegeDict;

		public TemplateForTableSet(string name, EntityMetadata[] tableSet)
		{
			if (tableSet == null || tableSet.Length == 0)
				throw new ArgumentException("There should be at least one table in the set", nameof(tableSet));

			this.Name = name;
			this.tableSet = tableSet;
			this.privilegeDict = this.tableSet[0].Privileges.Where(x => x.PrivilegeType != PrivilegeType.None).ToDictionary(x => x.PrivilegeType);
		}

		public string Name { get; }

		public string LogicalName => string.Join(", ", this.tableSet.OrderBy(x => x.LogicalName).Select(x => x.LogicalName));


		public SecurityPrivilegeMetadata this[PrivilegeType privilegeType]
		{
			get
			{
				if (this.privilegeDict.TryGetValue(privilegeType, out var metadata))
					return metadata;
				return null;
			}
		}

		public IEnumerator<KeyValuePair<PrivilegeType, SecurityPrivilegeMetadata>> GetEnumerator()
		{
			return privilegeDict.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
