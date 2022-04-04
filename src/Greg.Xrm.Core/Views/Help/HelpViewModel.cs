using Greg.Xrm.Core.Help;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using System;

namespace Greg.Xrm.Core.Views.Help
{
	public class HelpViewModel : ViewModel
	{
		private readonly ILog log;
		private readonly IHelpRepository helpRepository;

		public HelpViewModel(IMessenger messenger, ILog log, IHelpRepository helpRepository, string initialTopic)
		{
			messenger.Register<ShowHelp>(OnHelpRequested);
			this.log = log;
			this.helpRepository = helpRepository;
			this.OnHelpRequested(initialTopic);
		}

		private void OnHelpRequested(ShowHelp m)
		{
			OnHelpRequested(m.TopicId);
		}


		private void OnHelpRequested(string topic)
		{
			try
			{
				this.CurrentTopic = topic;

				var content = this.helpRepository.GetContentByTopic(topic);
				this.Content = content;
			}
			catch (HelpTopicNotFoundException ex)
			{
				this.log.Error(ex.Message);
				this.Content = ex.Message;
			}
			catch (ArgumentException ex)
			{
				this.log.Error(ex.Message);
				this.Content = ex.Message;
			}
		}

		public string CurrentTopic
		{
			get => Get<string>();
			private set => Set(value);
		}

		public string Content
		{
			get => Get<string>();
			private set => Set(value);
		}
	}
}
