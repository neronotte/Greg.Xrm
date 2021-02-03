using McTools.Xrm.Connection;
using System;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Messaging
{
	public class RemoveConnectionMessage
	{
		public RemoveConnectionMessage(ConnectionDetail detail)
		{
			this.Detail = detail ?? throw new ArgumentNullException(nameof(detail));
		}

		public ConnectionDetail Detail { get; }
	}
}
