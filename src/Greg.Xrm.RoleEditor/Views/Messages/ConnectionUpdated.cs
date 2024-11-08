using Greg.Xrm.Core;

namespace Greg.Xrm.RoleEditor.Views.Messages
{
	public class ConnectionUpdated
	{

		public ConnectionUpdated(IXrmToolboxPluginContext context)
		{
			Context = context;
		}

		public IXrmToolboxPluginContext Context { get; }
	}
}