using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Greg.Xrm.EnvironmentComparer.Model
{
	[Serializable]
	public class ExtendedValidationException : Exception
	{
		public string ObjectName { get; }
		public object Item { get; }
		public List<ValidationResult> Errors { get; }

		public ExtendedValidationException(string objectName, object item, List<ValidationResult> errors) : base("Validation error on object "+ objectName)
		{
			if (string.IsNullOrEmpty(objectName))
			{
				throw new ArgumentException($"'{nameof(objectName)}' cannot be null or empty", nameof(objectName));
			}

			this.ObjectName = objectName;
			this.Item = item ?? throw new ArgumentNullException(nameof(item));
			this.Errors = errors ?? throw new ArgumentNullException(nameof(errors));
		}


		public ExtendedValidationException(string message) : base(message)
		{
		}

		public ExtendedValidationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public ExtendedValidationException()
		{
		}
	}
}
