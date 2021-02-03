using Greg.Xrm.EnvironmentSolutionsComparer.Views;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Messaging
{
	public class ConnectionRemovedMessage
	{
		public ConnectionRemovedMessage(ConnectionModel model)
		{
			this.Model = model;
		}

		public ConnectionModel Model { get; }
	}
}
