using System;
using System.IO;

namespace Greg.Xrm.EnvironmentComparer.Help
{
	public class HelpRepository : IHelpRepository
	{
		private readonly IHelpContentIndex index;

		public HelpRepository(IHelpContentIndex index)
		{
			this.index = index ?? throw new System.ArgumentNullException(nameof(index));
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
			using (var stream = GetType().Assembly.GetManifestResourceStream(resourceName))
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
