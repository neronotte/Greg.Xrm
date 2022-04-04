namespace Greg.Xrm.Core.Help
{
	[System.Serializable]
	public class HelpTopicNotFoundException : System.Exception
	{
		public HelpTopicNotFoundException() { }
		public HelpTopicNotFoundException(string message) : base(message) { }
		public HelpTopicNotFoundException(string message, System.Exception inner) : base(message, inner) { }
		protected HelpTopicNotFoundException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
