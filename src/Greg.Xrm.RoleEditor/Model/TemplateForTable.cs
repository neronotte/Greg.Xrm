using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Model
{
	[DebuggerDisplay("{Name}")]
	public class TemplateForTable : ITemplateForTable
	{
		private readonly EntityMetadata table;
		private readonly Dictionary<PrivilegeType, SecurityPrivilegeMetadata> privilegeDict;


        public TemplateForTable(EntityMetadata table)
		{
			this.table = table ?? throw new ArgumentNullException(nameof(table));

			this.privilegeDict = this.table.Privileges.Where(x => x.PrivilegeType != PrivilegeType.None).ToDictionary(x => x.PrivilegeType);
		}



		public SecurityPrivilegeMetadata this[PrivilegeType privilegeType]
		{
			get
			{
				if (this.privilegeDict.TryGetValue(privilegeType, out var metadata))
					return metadata;
				return null;
			}
		}


        public string Name => table.DisplayName?.UserLocalizedLabel?.Label ?? LogicalName;


		public string LogicalName => table.LogicalName;

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
