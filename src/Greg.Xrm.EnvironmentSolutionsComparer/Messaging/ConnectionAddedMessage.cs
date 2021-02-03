using Greg.Xrm.EnvironmentSolutionsComparer.Views;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Messaging
{
	public class ConnectionAddedMessage
	{
		public ConnectionAddedMessage(int index, ConnectionModel model)
		{
			this.Index = index;
			this.Model = model;
		}


		public int Index { get; }
		public ConnectionModel Model { get; }
	}
}
