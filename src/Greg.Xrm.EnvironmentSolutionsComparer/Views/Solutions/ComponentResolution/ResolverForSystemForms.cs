using Greg.Xrm.Logging;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution2
{
	public class ResolverForSystemForms : IComponentResolver
	{
		private readonly ILog log;

		public ResolverForSystemForms(ILog log)
		{
			this.log = log;
		}

		public void Resolve(IReadOnlyCollection<SolutionComponent> componentList, ConnectionModel env)
		{
			if (componentList.Count == 0) return;
			var idList = componentList
				.Select(_ => _.objectid)
				.ToList();

			var dict = GetNames(env.Crm, idList);
			foreach (var id in idList)
			{
				if (!dict.TryGetValue(id, out var name))
				{
					name = $"[Missing: {id}]";
				}
				var leaf = componentList.First(_ => _.objectid == id);
				leaf.Label = name;
			}
		}


		public Dictionary<Guid, string> GetNames(IOrganizationService crm, IReadOnlyList<Guid> componentIdSet)
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
				this.log.Error("Error while retrieving systemform records: " + ex.Message, ex);
				dict = new Dictionary<Guid, string>();
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
