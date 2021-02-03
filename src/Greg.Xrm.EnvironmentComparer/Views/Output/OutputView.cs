using Greg.Xrm.EnvironmentComparer.Help;
using Greg.Xrm.Messaging;
using Greg.Xrm.Theming;

namespace Greg.Xrm.EnvironmentComparer.Views.Output
{
	public class OutputView : Logging.OutputView
	{
		public OutputView(IThemeProvider themeProvider, IMessenger messenger)
			: base(themeProvider, messenger)
		{
			this.RegisterHelp(messenger, Topics.Output);
		}
	}
}
