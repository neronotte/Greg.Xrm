using Greg.Xrm.Messaging;

namespace Greg.Xrm.RoleEditor.Views.Messages
{
	public class Freeze
	{
	}

	public class Unfreeze
	{
	}

	public static class Extensions
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
