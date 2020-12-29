using System;

namespace Greg.Xrm.Messaging
{
	public class Registration<T> : IRegistration
	{
		private readonly IMessageHandler<T> handler;

		public Registration(IMessageHandler<T> handler)
		{
			this.Id = Guid.NewGuid();
			this.handler = handler;
		}

		public Guid Id { get; }

		public void Execute(object message)
		{
			this.handler.Handle((T)message);
		}
	}
}
