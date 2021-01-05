using Greg.Xrm.Messaging;
using System;
using System.Linq.Expressions;

namespace Greg.Xrm.Model
{
	public interface IChangeManagerFluent
	{
		IChangeManagerFluent ChangesAlso<TProperty>(Expression<Func<TProperty>> otherProperty);
		void NotifyOthers(IMessenger messenger);
	}
}
