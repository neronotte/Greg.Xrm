using Greg.Xrm.RoleEditor.Model;

namespace Greg.Xrm.RoleEditor.Views.Common
{
	public class PrivilegeConfigForMiscMemento
	{
		public const string CommandName = "Greg.Xrm.RoleEditor-misc";
		public static PrivilegeConfigForMiscMemento CreateNew()
		{
			return new PrivilegeConfigForMiscMemento
			{
				Name = CommandName
			};
		}

		public string Name { get; set; }
		public Level? Level { get; set; }

		[Newtonsoft.Json.JsonIgnore]
		public bool IsValid
		{
			get
			{
				return CommandName.Equals(this.Name);
			}
		}
	}
}
