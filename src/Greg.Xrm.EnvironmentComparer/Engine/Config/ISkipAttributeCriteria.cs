namespace Greg.Xrm.EnvironmentComparer.Engine.Config
{
	public interface ISkipAttributeCriteria
	{
		bool ShouldSkip(string attributeName);
	}
}
