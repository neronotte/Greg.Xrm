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

		class SkipNothing : ISkipAttributeCriteria
		{
			public bool ShouldSkip(string attributeName)
			{
				return false;
			}
		}

		class SkipAttributes : ISkipAttributeCriteria
		{
			private readonly string[] attributesToSkip;

			public SkipAttributes(string[] attributesToSkip)
			{
				this.attributesToSkip = attributesToSkip;
			}

			public bool ShouldSkip(string attributeName)
			{
				return this.attributesToSkip.Any(_ => string.Equals(_, attributeName, System.StringComparison.OrdinalIgnoreCase));
			}
		}
	}
}
