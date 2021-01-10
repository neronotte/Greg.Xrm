using Microsoft.Xrm.Sdk;

namespace Greg.Xrm.EnvironmentComparer.Engine.Config
{
	public class KeyProviderById : IKeyProvider<Entity>
	{
		public string Name => "Guid";

		public string GetKey(Entity item)
		{
			if (item == null) return null;
			return item.Id.ToString("D").ToLowerInvariant();
		}
	}
}
