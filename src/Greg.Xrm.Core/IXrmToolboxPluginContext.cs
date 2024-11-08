using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;

namespace Greg.Xrm.Core
{
	/// <summary>
	/// This object stores the details of a specific XrmToolbox connection and provides a way to interact with the CRM service.
	/// It's designed to be carried around by objects that need to be tied with a specific environment connection.
	/// </summary>
	public interface IXrmToolboxPluginContext : IOrganizationService
	{
		/// <summary>
		/// Details of the current connection.
		/// </summary>
		ConnectionDetail Details { get; }

		/// <summary>
		/// Messenger that can be used to communicate with other application components.
		/// </summary>
		IMessenger Messenger { get; }


		/// <summary>
		/// Logger to be used to log messages.
		/// </summary>
		ILog Log { get; }
	}
}
