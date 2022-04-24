namespace Greg.Xrm.Core.Help
{
	public interface IHelpContentIndex
	{
		bool TryGetResourceNameByTopic(string topic, out string resourceName);
	}
}
