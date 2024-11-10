using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Model
{
	[DebuggerDisplay("{Name}")]
	public class TemplateForGenericPrivilege
	{
		private readonly Privilege privilege;

		public TemplateForGenericPrivilege(Privilege privilege)
		{
			var name = privilege.name.Substring(3);
			this.Name = name.SplitCamelCase();
			this.privilege = privilege;
		}


		public string Name { get; protected set; }

		public string PrivilegeName => this.privilege.name;
		public Guid PrivilegeId => this.privilege.Id;


		public IReadOnlyList<Level> AllowedValues => privilege.AllowedValues;

		public bool IsValidLevel(Level level)
		{
			return this.AllowedValues.Any(x => x == level);
		}
	}
}
