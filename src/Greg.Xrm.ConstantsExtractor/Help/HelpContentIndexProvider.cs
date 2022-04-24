using Greg.Xrm.Core.Help;
using System;

namespace Greg.Xrm.ConstantsExtractor.Help
{
	public class HelpContentIndexProvider
	{
		private readonly Lazy<IHelpContentIndex> instance = new Lazy<IHelpContentIndex>(CreateInstance);

		private static IHelpContentIndex CreateInstance()
		{
			var resourcePath = typeof(HelpContentIndexProvider).Namespace + ".Content.";


			return new HelpContentIndex(resourcePath)
			{
				{ Topics.Home, "home.html" }
			};
		}



		public IHelpContentIndex GetIndex()
		{
			return instance.Value;
		}
	}
}
