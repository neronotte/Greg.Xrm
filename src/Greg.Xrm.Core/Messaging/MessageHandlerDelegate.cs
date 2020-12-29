using System;

namespace Greg.Xrm.Messaging
{
	public class MessageHandlerDelegate<T> : IMessageHandler<T>
	{
		private readonly Action<T> callback;

		public MessageHandlerDelegate(Action<T> callback)
		{
			this.callback = callback ?? throw new ArgumentNullException(nameof(callback));
		}

		public void Handle(T message)
		{
			this.callback(message);
		}
	}
}
