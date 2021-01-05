using System;
using System.Linq.Expressions;

namespace Greg.Xrm.Model
{
	public interface IMessengerPropertyChangedFluentInterface<T>
	{
		IMessengerPropertyChangedFluentInterfaceExecuteMethod<T> ChangesProperty<TProperty>(Expression<Func<T, TProperty>> componentProperty);
	}
}
