using System;
using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentComparer.Help
{
	public class HelpContentIndexProvider
	{
		private readonly Lazy<IHelpContentIndex> instance = new Lazy<IHelpContentIndex>(CreateInstance);

		private static IHelpContentIndex CreateInstance()
		{
			return new HelpContentIndex
			{
				{ Topics.Home, "home.html" },
				{ Topics.Actions, "actions.html" },
				{ Topics.Configurator, "configurator.html" },
				{ Topics.ConfiguratorDialog, "configurator-dialog.html" },
				{ Topics.Output, "output.html" },
				{ Topics.ResultGrid, "result-grid.html" },
				{ Topics.ResultRecord, "result-record.html" },
				{ Topics.ResultTree, "result-tree.html" }
			};
		}



		public IHelpContentIndex GetIndex()
		{
			return instance.Value;
		}


		private class HelpContentIndex : Dictionary<string, string>, IHelpContentIndex 
		{
			public bool TryGetResourceNameByTopic(string topic, out string resourceName)
			{
				if (string.IsNullOrWhiteSpace(topic))
					throw new ArgumentNullException(nameof(topic));


				topic = topic.ToLowerInvariant();

				var result = this.TryGetValue(topic, out resourceName);
				if (result == false) return false;

				var resourceRoot = GetType().Namespace;

				resourceName = resourceRoot + ".Content." + resourceName;
				return true;
			}
		}
	}
}
