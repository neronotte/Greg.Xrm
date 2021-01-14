using Greg.Xrm.EnvironmentComparer.Help;
using Greg.Xrm.EnvironmentComparer.Logging;
using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.Messaging;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.EnvironmentComparer.Views.Help
{
	public partial class HelpView : DockContent
	{
		private readonly HelpViewModel viewModel;
		private readonly IMessenger messenger;

		public HelpView(IMessenger messenger, ILog log, IHelpRepository helpRepository)
		{
			InitializeComponent();

			this.viewModel = new HelpViewModel(messenger, log, helpRepository);
			this.webBrowser1.Bind(_ => _.DocumentText, this.viewModel, _ => _.Content);


			this.webBrowser1.Navigating += OnNavigating;

			this.messenger = messenger;
			this.messenger.Register<ShowHelp>(m => { 
				this.VisibleState = DockState.DockRight;
			});
		}



		private void OnNavigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			var url = e.Url?.ToString();
			if (string.IsNullOrWhiteSpace(url))
			{
				return;
			}
			if (string.Equals("about:blank", url))
			{
				return;
			}

			var topic = url.Replace("about:/", string.Empty);
			if (string.Equals(this.viewModel.CurrentTopic, topic))
			{
				e.Cancel = true;
				return;
			}

			this.messenger.Send(new ShowHelp(topic));
		}
	}
}
