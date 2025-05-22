using Greg.Xrm.Core.Help;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.Core.Views.Help
{
	public partial class HelpView : DockContent
	{
		private readonly HelpViewModel viewModel;
		private readonly IMessenger messenger;

		public HelpView(IMessenger messenger, ILog log, IHelpRepository helpRepository, string initialTopic)
		{
			InitializeComponent();

			this.viewModel = new HelpViewModel(messenger, log, helpRepository, initialTopic);
			this.webBrowser1.Bind(_ => _.DocumentText, this.viewModel, _ => _.Content);

			this.webBrowser1.Navigating += OnNavigating;

			this.messenger = messenger;
			this.messenger.Register<ShowHelp>(m =>
			{
				this.VisibleState = DockState.DockRight;
			});


			this.Shown += (s, e) =>
			{
				this.viewModel.Initialize();
			};
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
