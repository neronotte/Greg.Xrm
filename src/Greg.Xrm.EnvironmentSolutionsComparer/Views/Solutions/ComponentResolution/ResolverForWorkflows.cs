using Greg.Xrm.Logging;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution2
{
	public class ResolverForWorkflows : IComponentResolver
	{
		private readonly ILog log;

		public ResolverForWorkflows(ILog log)
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




		private Dictionary<Guid, string> GetNames(IOrganizationService crm, IReadOnlyList<Guid> componentIdSet)
		{

			Dictionary<Guid, string> dict;
			try
			{
				var query = new QueryExpression("workflow");
				query.ColumnSet.AddColumns("uniquename", "name");
				query.Criteria.AddCondition("workflowid", ConditionOperator.In, componentIdSet.Cast<object>().ToArray());
				query.NoLock = true;

				var result = crm.RetrieveMultiple(query);

				dict = result.Entities.ToDictionary(x => x.Id, GetLabelFromWorkflow);
			}
			catch (FaultException<OrganizationServiceFault> ex)
			{
				this.log.Error($"Error while retrieving workflow records: {ex.Message}", ex);
				dict = new Dictionary<Guid, string>();
			}
			return dict;
		}

		private static string GetLabelFromWorkflow(Entity entity)
		{
			return $"{entity.GetAttributeValue<string>("name")} ({entity.GetAttributeValue<string>("uniquename")})"
				.Replace("()", string.Empty)
				.Trim();
		}
	}
}
