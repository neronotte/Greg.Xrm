using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Model
{
	public class TemplateForRole
	{
		public TemplateForRole(IReadOnlyList<ITemplateForTable> tables, IReadOnlyList<TemplateForGenericPrivilege> miscellaneousPrivileges)
		{
			this.Tables = tables.OrderBy(x => x.Name).ToDictionary(x => x.LogicalName);
			this.Misc = miscellaneousPrivileges.OrderBy(x => x.Name).ToDictionary(x => x.PrivilegeName);
		}



		public IReadOnlyDictionary<string, ITemplateForTable> Tables { get; }

		public IReadOnlyDictionary<string, TemplateForGenericPrivilege> Misc { get; }
	}
}
