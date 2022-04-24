using System;
using System.IO;
using System.Reflection;

namespace Greg.Xrm.Core.Help
{
	public class HelpRepository : IHelpRepository
	{
		private readonly IHelpContentIndex index;
		private readonly Assembly resourceAssembly;

		public HelpRepository(IHelpContentIndex index, Assembly resourceAssembly)
		{
			this.index = index ?? throw new ArgumentNullException(nameof(index));
			this.resourceAssembly = resourceAssembly ?? throw new ArgumentNullException(nameof(resourceAssembly));
		}

		public string GetContentByTopic(string topic)
		{
			if (!this.index.TryGetResourceNameByTopic(topic, out string resourceName))
			{
				throw new HelpTopicNotFoundException($"Topic <{topic}> not found!");
			}

			return GetResourceString(resourceName);
		}


		private string GetResourceString(string resourceName)
		{
			using (var stream = this.resourceAssembly.GetManifestResourceStream(resourceName))
			{
				if (stream == null)
				{
					throw new ArgumentException($"Resource not found: <{resourceName}>");
				}

				using (var reader = new StreamReader(stream))
				{
					var text = reader.ReadToEnd();
					return text;
				}
			}
		}
	}
}
