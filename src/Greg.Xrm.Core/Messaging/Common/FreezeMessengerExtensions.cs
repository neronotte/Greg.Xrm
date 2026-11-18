using Greg.Xrm.Messaging;

namespace Greg.Xrm
{
	public static class FreezeMessengerExtensions
	{
		public static void Freeze(this IMessenger messenger)
		{
			messenger.Send<Freeze>();
		}

		public static void Unfreeze(this IMessenger messenger)
		{
			messenger.Send<Unfreeze>();
		}
	}
}
