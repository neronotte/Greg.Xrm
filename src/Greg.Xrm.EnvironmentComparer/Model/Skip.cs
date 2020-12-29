using System.Linq;

namespace Greg.Xrm.EnvironmentComparer.Model
{
	public sealed class Skip
	{
		public static ISkipAttributeCriteria Nothing { get; } = new SkipNothing();

		public static ISkipAttributeCriteria Attributes(params string[] attributes)
		{
			return new SkipAttributes(attributes);
		}



	}
}
