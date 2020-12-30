namespace Greg.Xrm.EnvironmentComparer.Model
{
	public class SkipNothing : ISkipAttributeCriteria
	{
		public bool ShouldSkip(string attributeName)
		{
			return false;
		}
	}
}
