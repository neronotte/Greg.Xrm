using System;

namespace Greg.Xrm.EnvironmentComparer.Model
{
	[Serializable]
	public class ComparisonException : Exception
	{
		public ComparisonException() { }
		public ComparisonException(string message) : base(message) { }
		public ComparisonException(string message, Exception inner) : base(message, inner) { }
		protected ComparisonException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
