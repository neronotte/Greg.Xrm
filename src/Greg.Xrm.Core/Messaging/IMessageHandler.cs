namespace Greg.Xrm.Messaging
{
	public interface IMessageHandler<T>
	{
		void Handle(T message);
	}
}
