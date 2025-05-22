using System;
using System.Collections.Generic;

namespace Greg.Xrm.Messaging
{
	public class ScopedMessenger : IScopedMessenger
	{
		private readonly List<Guid> registrations = new List<Guid>();
		private readonly IMessenger messenger;
		private bool disposedValue;

		public ScopedMessenger(IMessenger messenger)
		{
			this.messenger = messenger;
		}

		public Guid Register<T>(Action<T> callback)
		{
			if (disposedValue) throw new ObjectDisposedException(nameof(ScopedMessenger));

			var id = this.messenger.Register(callback);
			this.registrations.Add(id);
			return id;
		}

		public Guid Register<T>(IMessageHandler<T> callback)
		{
			if (disposedValue) throw new ObjectDisposedException(nameof(ScopedMessenger));

			var id = this.messenger.Register(callback);
			this.registrations.Add(id);
			return id;
		}

		public void Send<T>(T message)
		{
			if (disposedValue) throw new ObjectDisposedException(nameof(ScopedMessenger));

			this.messenger.Send(message);
		}

		public void Send<T>() where T : new()
		{
			if (disposedValue) throw new ObjectDisposedException(nameof(ScopedMessenger));

			this.messenger.Send<T>();
		}

		public void Unregister(Guid registrationId)
		{
			if (disposedValue) throw new ObjectDisposedException(nameof(ScopedMessenger));

			this.registrations.Remove(registrationId);
			this.messenger.Unregister(registrationId);
		}

		public IScopedMessenger CreateScope()
		{
			return new ScopedMessenger(this.messenger);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					foreach (var id in this.registrations)
					{
						this.messenger.Unregister(id);
					}
					this.registrations.Clear();
				}

				disposedValue = true;
			}
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
