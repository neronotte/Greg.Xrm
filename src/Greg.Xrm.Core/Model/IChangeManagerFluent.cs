using Greg.Xrm.Messaging;
using System;
using System.Linq.Expressions;

namespace Greg.Xrm.Model
{
	/// <summary>
	/// Fluent interface that allows declarative configuration of the behavior of the viewmodel
	/// when some property changes.
	/// </summary>
	public interface IChangeManagerFluent
	{
		/// <summary>
		/// Specifies that the change of a property should trigger also the change of another one
		/// </summary>
		/// <typeparam name="TProperty">The type of the "dependent" property</typeparam>
		/// <param name="otherProperty">An expression that represents the property that "depends" from the first one</param>
		/// <returns>The fluent interface</returns>
		IChangeManagerFluent ChangesAlso<TProperty>(Expression<Func<TProperty>> otherProperty);

		/// <summary>
		/// Instructs the viewmodel to execute a specific method when a property changes
		/// </summary>
		/// <param name="callback">The method to call. The argument of the method is the new value of the property</param>
		/// <returns>The fluent interface</returns>
		IChangeManagerFluent Execute(Action<object> callback);
		
		
		/// <summary>
		/// Instructs the viewmodel to send a message when this property changes
		/// </summary>
		/// <param name="messenger">The messenger that will be used to send the message</param>
		void NotifyOthers(IMessenger messenger);
	}
}
