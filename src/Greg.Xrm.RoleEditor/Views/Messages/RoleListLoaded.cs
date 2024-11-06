using Greg.Xrm.RoleEditor.Model;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Views.Messages
{
	public class RoleListLoaded
	{
		public IReadOnlyList<Role> RoleList { get; set; }
		public TemplateForRole RoleTemplate { get; internal set; }
	}
}
