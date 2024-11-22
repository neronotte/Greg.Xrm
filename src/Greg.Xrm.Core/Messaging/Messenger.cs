using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Greg.Xrm.Messaging
{
	public class Messenger : IMessenger
	{
		private readonly Dictionary<Type, List<IRegistration>> registry = new Dictionary<Type, List<IRegistration>>();
		private readonly Control pluginControl;

		public Messenger(Control pluginControl)
		{
			this.pluginControl = pluginControl ?? throw new ArgumentNullException(nameof(pluginControl));
		}



		public Guid Register<T>(Action<T> callback)
		{
			return Register(new MessageHandlerDelegate<T>(callback));
		}

		public Guid Register<T>(IMessageHandler<T> callback)
		{
			var type = typeof(T);

			var registration = new Registration<T>(callback);

			if (!this.registry.ContainsKey(type))
			{
				this.registry[type] = new List<IRegistration>();
			}
			this.registry[type].Add(registration);

			return registration.Id;
		}

		public void Unregister(Guid registrationId)
		{
			foreach (var list in registry.Values)
			{
				var registration = list.Find(x => x.Id == registrationId);
				if (registration != null)
				{
					list.Remove(registration);
					return;
				}
			}
		}

		public void Send<T>() where T : new()
		{
			var message = new T();
			Send(message);
		}

		public void Send<T>(T message)
		{
			var messageType = typeof(T);
			if (!this.registry.TryGetValue(messageType, out List<IRegistration> list)) return;
			if (list.Count == 0) return;

			Action callback = () =>
			{
				foreach (var registration in list)
				{
					registration.Execute(message);
				}
			};

			if (this.pluginControl.InvokeRequired)
			{
				this.pluginControl.Invoke(callback);
			}
			else
			{
				callback();
			}
		}

		public IScopedMessenger CreateScope()
		{
			return new ScopedMessenger(this);
		}
	}
}
