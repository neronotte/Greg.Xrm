using Greg.Xrm.Core.Help;
using System;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Help
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
				{ Topics.Browser, "browser.html" },
				{ Topics.Editor, "editor.html" }
			};
		}



		public IHelpContentIndex GetIndex()
		{
			return instance.Value;
		}
	}
}
