namespace Greg.Xrm.Core.Views
{
	public class NotificationEventArgs
	{

		public NotificationEventArgs(NotificationType type, string message, int? timerInSeconds = null)
		{
			this.Type = type;
			this.Message = message;
			this.TimerInSeconds = timerInSeconds;
		}

		public NotificationType Type { get; }
		public string Message { get; }
		public int? TimerInSeconds { get; }
	}
}
