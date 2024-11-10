using System.ComponentModel;

namespace Greg.Xrm.RoleEditor.Model
{
	public enum Level
	{
		[Description("None")]
		None = 0,

		[Description("User")]
		User = 1,

		[Description("Business Unit")]
		BusinessUnit = 2,

		[Description("Parent-Child Business Unit")]
		ParentChild = 3,

		[Description("Organization")]
		Organization = 4
	}
}
