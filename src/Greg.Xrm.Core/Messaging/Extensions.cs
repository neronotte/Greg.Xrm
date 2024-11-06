using Greg.Xrm.Messaging;

namespace Greg.Xrm.Core.Messaging
{
	public static class Extensions
	{
		public static void Send<T>(this IMessenger messenger)
			where T : new()
		{
			if (messenger == null) return;

			messenger.Send(new T());
		}
	}
}
