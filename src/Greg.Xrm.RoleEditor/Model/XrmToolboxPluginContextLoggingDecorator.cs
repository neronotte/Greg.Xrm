using Greg.Xrm.Core;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.RoleEditor.Services;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace Greg.Xrm.RoleEditor.Model
{
	public class XrmToolboxPluginContextLoggingDecorator : IXrmToolboxPluginContext
	{
		private readonly IXrmToolboxPluginContext inner;

		public XrmToolboxPluginContextLoggingDecorator(IXrmToolboxPluginContext inner)
		{
			this.inner = inner;
		}

		public ConnectionDetail Details => inner.Details;

		public IMessenger Messenger => inner.Messenger;

		public ILog Log => inner.Log;

		public void Associate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
		{
			inner.Associate(entityName, entityId, relationship, relatedEntities);
		}

		public Guid Create(Entity entity)
		{
			return inner.Create(entity);
		}

		public void Delete(string entityName, Guid id)
		{
			inner.Delete(entityName, id);
		}

		public void Disassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
		{
			inner.Disassociate(entityName, entityId, relationship, relatedEntities);
		}

		public OrganizationResponse Execute(OrganizationRequest request)
		{
			RequestLogger.Log(request);
			return inner.Execute(request);
		}

		public Entity Retrieve(string entityName, Guid id, ColumnSet columnSet)
		{
			return inner.Retrieve(entityName, id, columnSet);
		}

		public EntityCollection RetrieveMultiple(QueryBase query)
		{
			return inner.RetrieveMultiple(query);
		}

		public void Update(Entity entity)
		{
			inner.Update(entity);
		}



		public static bool AreEqual(IXrmToolboxPluginContext a, IXrmToolboxPluginContext b)
		{
			if (a == null && b == null)
			{
				return true;
			}

			if (a == null || b == null)
			{
				return false;
			}

			return a.Details == b.Details &&
				a.Messenger == b.Messenger &&
				a.Log == b.Log;
		}
	}
}
