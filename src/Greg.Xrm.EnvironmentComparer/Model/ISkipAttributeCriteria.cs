namespace Greg.Xrm.EnvironmentComparer.Model
{
	public interface ISkipAttributeCriteria
	{
		bool ShouldSkip(string attributeName);
	}
}
