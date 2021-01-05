using System;

namespace Greg.Xrm.Model
{
	public class NotifyPropertyChangedMessage
	{
		public NotifyPropertyChangedMessage(object sourceObject, string propertyName, object newValue)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ArgumentException($"'{nameof(propertyName)}' cannot be null or empty", nameof(propertyName));
			}

			this.SourceObject = sourceObject ?? throw new ArgumentNullException(nameof(sourceObject));
			this.SourceObjectType = sourceObject.GetType();
			this.PropertyName = propertyName;
			this.NewValue = newValue;
		}

		public Type SourceObjectType { get; }
		public object SourceObject { get; }
		public string PropertyName { get; }
		public object NewValue { get; }

		public T GetNewValue<T>()
		{
			return (T)NewValue;
		}
	}
}
