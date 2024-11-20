using System;

namespace Greg.Xrm.Messaging
{
	public interface IMessenger
	{
		void Send<T>(T message);

		void Send<T>() where T : new();

		Guid Register<T>(Action<T> callback);

		Guid Register<T>(IMessageHandler<T> callback);

		void Unregister(Guid registrationId);


		IScopedMessenger CreateScope();
	}
}
