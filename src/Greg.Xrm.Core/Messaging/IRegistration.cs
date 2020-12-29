using System;

namespace Greg.Xrm.Messaging
{
	public interface IRegistration
	{
		Guid Id { get; }
		void Execute(object message);
	}
}
