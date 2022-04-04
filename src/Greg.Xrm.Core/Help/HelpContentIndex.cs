using System;
using System.Collections.Generic;

namespace Greg.Xrm.Core.Help
{
	public class HelpContentIndex : Dictionary<string, string>, IHelpContentIndex
	{
		private readonly string resourcePath;

		public HelpContentIndex(string resourcePath)
		{
			this.resourcePath = resourcePath ?? string.Empty;
		}


		public bool TryGetResourceNameByTopic(string topic, out string resourceName)
		{
			if (string.IsNullOrWhiteSpace(topic))
				throw new ArgumentNullException(nameof(topic));


			topic = topic.ToLowerInvariant();

			var result = this.TryGetValue(topic, out resourceName);
			if (result == false) return false;

			var resourceRoot = GetType().Namespace;

			resourceName = resourcePath + resourceName;
			return true;
		}
	}
}
