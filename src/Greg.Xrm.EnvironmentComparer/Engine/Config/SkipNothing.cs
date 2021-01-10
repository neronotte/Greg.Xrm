namespace Greg.Xrm.EnvironmentComparer.Engine.Config
{
	public class SkipNothing : ISkipAttributeCriteria
	{
		public bool ShouldSkip(string attributeName)
		{
			return false;
		}
	}
}
