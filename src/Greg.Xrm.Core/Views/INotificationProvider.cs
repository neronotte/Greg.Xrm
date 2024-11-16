using System;

namespace Greg.Xrm.Core.Views
{
	public interface INotificationProvider
	{
		void SendNotification(NotificationType type, string message, int? timerInSeconds = null);

		event EventHandler<NotificationEventArgs> Notify;
	}
}
