using Greg.Xrm.EnvironmentComparer.Help;
using Greg.Xrm.EnvironmentComparer.Logging;
using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using System;

namespace Greg.Xrm.EnvironmentComparer.Views.Help
{
	public class HelpViewModel : ViewModel
	{
		private readonly ILog log;
		private readonly IHelpRepository helpRepository;

		public HelpViewModel(IMessenger messenger, ILog log, IHelpRepository helpRepository)
		{
			messenger.Register<ShowHelp>(OnHelpRequested);
			this.log = log;
			this.helpRepository = helpRepository;
			this.OnHelpRequested(Topics.Home);
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
