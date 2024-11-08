using System;

namespace Greg.Xrm.Core.Views
{
	public interface INotificationProvider
	{
		event EventHandler<NotificationEventArgs> Notify;
	}
}
