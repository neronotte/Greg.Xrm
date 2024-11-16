using Greg.Xrm.RoleEditor.Model;
using Microsoft.Xrm.Sdk.Metadata;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Views.Common
{
	public class PrivilegeConfigForTableMemento
	{
		public const string CommandName = "Greg.Xrm.RoleEditor-table";

		public static PrivilegeConfigForTableMemento CreateNew()
		{
			return new PrivilegeConfigForTableMemento
			{
				Name = CommandName,
				Levels = new Dictionary<PrivilegeType, Level?>()
			};
		}

        public string Name { get; set; }
		public Dictionary<PrivilegeType, Level?> Levels { get; set; }


		[JsonIgnore]
		public bool IsValid => CommandName.Equals(this.Name);
	}
}
