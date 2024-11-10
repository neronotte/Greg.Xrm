using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greg.Xrm.Core.Views
{
	public class NotificationEventArgs
	{
        public NotificationEventArgs(NotificationType type, string message)
        {
			Type = type;
			Message = message;
		}

		public NotificationType Type { get; }
		public string Message { get; }
	}
}
