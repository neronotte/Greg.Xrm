using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace Greg.Xrm.Core
{

	/// <summary>
	/// This object stores the details of a specific XrmToolbox connection and provides a way to interact with the CRM service.
	/// It's designed to be carried around by objects that need to be tied with a specific environment information.
	/// </summary>
	public class XrmToolboxPluginContext : IXrmToolboxPluginContext
	{
		private readonly IOrganizationService crm;

		public XrmToolboxPluginContext(ILog log, IMessenger messenger, ConnectionDetail details, IOrganizationService service)
		{
			this.Log = log ?? throw new ArgumentNullException(nameof(log));
			this.Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
			this.Details = details ?? throw new ArgumentNullException(nameof(details));
			this.crm = service ?? throw new ArgumentNullException(nameof(service));
		}

		public ConnectionDetail Details { get; }

		public IMessenger Messenger { get; }

		public ILog Log { get; }


		#region IOrganizationService implementation

		public void Associate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
		{
			this.crm.Associate(entityName, entityId, relationship, relatedEntities);
		}

		public Guid Create(Entity entity)
		{
			return this.crm.Create(entity);
		}

		public void Delete(string entityName, Guid id)
		{
			this.crm.Delete(entityName, id);
		}

		public void Disassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
		{
			this.crm.Disassociate(entityName, entityId, relationship, relatedEntities);
		}

		public OrganizationResponse Execute(OrganizationRequest request)
		{
			return this.crm.Execute(request);
		}

		public Entity Retrieve(string entityName, Guid id, ColumnSet columnSet)
		{
			return this.crm.Retrieve(entityName, id, columnSet);
		}

		public EntityCollection RetrieveMultiple(QueryBase query)
		{
			return this.crm.RetrieveMultiple(query);
		}

		#endregion


		#region IMessenger implementation



		public void Send<T>(T message)
		{
			throw new NotImplementedException();
		}

		public void Send<T>() where T : new()
		{
			throw new NotImplementedException();
		}

		public void Unregister(Guid registrationId)
		{
			throw new NotImplementedException();
		}

		public void Update(Entity entity)
		{
			this.crm.Update(entity);
		}

		#endregion



		public override string ToString()
		{
			return this.Details.ConnectionName;
		}
	}
}
