using Greg.Xrm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Greg.Xrm.Model
{
	public static class Extensions
	{
		public static Guid OnPropertyChanged(
			this IMessenger messenger,
			Type objectType,
			string propertyName,
			Action<NotifyPropertyChangedMessage> callback)
		{
			var registrationId = messenger.Register<NotifyPropertyChangedMessage>(m =>
			{
				if (m.SourceObjectType != objectType) return;
				if (!string.Equals(propertyName, m.PropertyName, StringComparison.OrdinalIgnoreCase)) return;

				callback(m);
			});
			return registrationId;
		}

		public static IMessengerPropertyChangedFluentInterface<T> WhenObject<T>(
			this IMessenger messenger)
		{
			return new PropertyChangedHandler<T>(messenger);
		}


		class PropertyChangedHandler<T> :
			IMessengerPropertyChangedFluentInterface<T>,
			IMessengerPropertyChangedFluentInterfaceExecuteMethod<T>
		{
			private readonly IMessenger messenger;
			private readonly List<string> propertyNames = new List<string>();

			public PropertyChangedHandler(IMessenger messenger)
			{
				this.messenger = messenger;
			}

			public IMessengerPropertyChangedFluentInterfaceExecuteMethod<T> ChangesProperty<TProperty>(Expression<Func<T, TProperty>> componentProperty)
			{
				this.propertyNames.Add(componentProperty.GetMemberName());
				return this;
			}

			public Guid Execute(Action<NotifyPropertyChangedMessage> callback)
			{
				var objectType = typeof(T);

				var registrationId = messenger.Register<NotifyPropertyChangedMessage>(m =>
				{
					if (m.SourceObjectType != objectType) return;
					if (!this.propertyNames.Any(x => string.Equals(x, m.PropertyName, StringComparison.Ordinal)))
						return;

					callback(m);
				});
				return registrationId;
			}
		}
	}
}
