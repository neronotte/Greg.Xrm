using Greg.Xrm.Core.Help;
using Greg.Xrm.Messaging;
using System;
using System.Windows.Forms;

namespace Greg.Xrm.Core.Views.Help
{
	public static class HelpExtensions
	{
		public static void RegisterHelp(this Form form, IMessenger messenger, string helpTopic)
		{
			if (form is null)
				throw new ArgumentNullException(nameof(form));
			if (messenger is null)
				throw new ArgumentNullException(nameof(messenger));
			if (string.IsNullOrWhiteSpace(helpTopic))
				throw new ArgumentNullException(nameof(helpTopic));


			form.KeyPreview = true;
			form.KeyUp += (s, e) =>
			{
				if (e.KeyCode == Keys.F1)
				{
					messenger.Send(new ShowHelp(helpTopic));
				}
			};
		}
	}
}
