using Greg.Xrm.Core.Help;
using System;
using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentComparer.Help
{
	public class HelpContentIndexProvider
	{
		private readonly Lazy<IHelpContentIndex> instance = new Lazy<IHelpContentIndex>(CreateInstance);

		private static IHelpContentIndex CreateInstance()
		{
			var resourcePath = typeof(HelpContentIndexProvider).Namespace + ".Content.";


			return new HelpContentIndex(resourcePath)
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
	}
}
