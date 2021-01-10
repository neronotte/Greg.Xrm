namespace Greg.Xrm.EnvironmentComparer.Engine.Config
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
