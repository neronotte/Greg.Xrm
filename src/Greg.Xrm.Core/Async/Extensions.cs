using Greg.Xrm.Async;
using Greg.Xrm.Messaging;
using System;

namespace Greg.Xrm.Core.Async
{
	public static class Extensions
	{
		public static IMessenger RegisterJobScheduler(this IMessenger messenger, IAsyncJobScheduler scheduler)
		{
			if (messenger == null)
				throw new ArgumentNullException(nameof(messenger), "The messenger cannot be null.");

			var handler = new JobMessageHandler(scheduler);
			messenger.Register(handler);
			return messenger;
		}
	}
}
