using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System;

namespace Greg.Xrm.DataModelWikiEditor.Views
{
	public class ConnectionUpdatedMessage
	{
		public ConnectionUpdatedMessage(ConnectionDetail connectionDetail, IOrganizationService crm)
		{
			this.ConnectionDetail = connectionDetail ?? throw new ArgumentNullException(nameof(connectionDetail));
			this.Crm = crm ?? throw new ArgumentNullException(nameof(crm));
		}

		public ConnectionDetail ConnectionDetail { get; private set; }
		public IOrganizationService Crm { get; private set; }
	}
}
