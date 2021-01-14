namespace Greg.Xrm.EnvironmentComparer.Help
{
	public interface IHelpContentIndex
	{
		bool TryGetResourceNameByTopic(string topic, out string resourceName);
	}
}
