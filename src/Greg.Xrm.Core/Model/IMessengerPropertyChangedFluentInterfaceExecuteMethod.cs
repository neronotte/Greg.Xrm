using System;

namespace Greg.Xrm.Model
{
	public interface IMessengerPropertyChangedFluentInterfaceExecuteMethod<T> : IMessengerPropertyChangedFluentInterface<T>
	{
		Guid Execute(Action<NotifyPropertyChangedMessage> callback);
	}
}
