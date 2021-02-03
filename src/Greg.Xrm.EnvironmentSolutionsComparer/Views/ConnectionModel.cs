using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views
{
	public class ConnectionModel
	{
		public ConnectionModel(ConnectionDetail detail, bool isDefault, IOrganizationService crm)
		{
			this.Detail = detail;
			this.IsDefault = isDefault;
			this.Crm = crm ?? detail.GetCrmServiceClient();
		}

		public IOrganizationService Crm { get; }

		public ConnectionDetail Detail { get; }

		public bool IsDefault { get; }


		public override string ToString()
		{
			return this.Detail.ConnectionName;
		}
	}
}
