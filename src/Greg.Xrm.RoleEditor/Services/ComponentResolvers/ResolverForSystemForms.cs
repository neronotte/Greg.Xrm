using Greg.Xrm.Logging;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Greg.Xrm.RoleEditor.Services.ComponentResolvers
{
	public class ResolverForSystemForms : IComponentResolver
	{
		private readonly IOrganizationService crm;
		private readonly ILog log;

		public ResolverForSystemForms(IOrganizationService crm, ILog log)
		{
			this.crm = crm;
			this.log = log;
		}


		public Dictionary<Guid, string> GetNames(IReadOnlyList<Guid> componentIdSet)
		{
			Dictionary<Guid, string> dict;
			try
			{
				var query = new QueryExpression("systemform");
				query.ColumnSet.AddColumns("objecttypecode", "type", "name", "description");
				query.Criteria.AddCondition("formid", ConditionOperator.In, componentIdSet.Cast<object>().ToArray());
				query.NoLock = true;

				var result = crm.RetrieveMultiple(query);

				dict = result.Entities.ToDictionary(x => x.Id, x => GetLabel(x));
			}
			catch (FaultException<OrganizationServiceFault> ex)
			{
				log.Error($"Error while retrieving systemform records: {ex.Message}", ex);
				dict = new Dictionary<Guid, string>();
			}

			foreach (var id in componentIdSet)
			{
				if (!dict.ContainsKey(id))
				{
					dict.Add(id, $"[Missing: {id}]");
				}
			}
			return dict;
		}

		private static string GetLabel(Entity entity)
		{
			var objectTypeCode = entity.GetFormattedValue("objecttypecode");
			var type = entity.GetFormattedValue("type");
			var name = entity.GetAttributeValue<string>("name");

			return $"{objectTypeCode}, {type} form: {name}";
		}
	}
}
