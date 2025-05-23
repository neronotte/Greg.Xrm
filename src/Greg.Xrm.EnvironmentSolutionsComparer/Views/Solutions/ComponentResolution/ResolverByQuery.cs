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
	public class ResolverByQuery : IComponentResolver
	{
		private readonly ILog log;
		private readonly string table;
		private string nameColumn;
		private string tableIdColumn;

		public ResolverByQuery(ILog log, string table, string nameColumn, string tableIdColumn = null)
		{
			this.log = log;
			this.table = table;
			this.nameColumn = nameColumn;
			this.tableIdColumn = tableIdColumn;
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
			if (this.nameColumn == null || this.tableIdColumn == null)
			{
				var request = new RetrieveEntityRequest
				{
					LogicalName = this.table,
					EntityFilters = EntityFilters.Entity
				};

				var response = (RetrieveEntityResponse)crm.Execute(request);

				var metadata = response.EntityMetadata;

				if (this.nameColumn == null)
				{
					this.nameColumn = metadata.PrimaryNameAttribute;
				}
				if (this.tableIdColumn == null)
				{
					this.tableIdColumn = metadata.PrimaryIdAttribute;
				}
			}	



			Dictionary<Guid, string> dict;
			try
			{
				var query = new QueryExpression(this.table);
				query.ColumnSet.AddColumns(this.nameColumn);
				query.Criteria.AddCondition(this.tableIdColumn, ConditionOperator.In, componentIdSet.Cast<object>().ToArray());
				query.NoLock = true;

				var result = crm.RetrieveMultiple(query);

				dict = result.Entities.ToDictionary(x => x.Id, x => x.GetAttributeValue<string>(this.nameColumn));
			}
			catch (FaultException<OrganizationServiceFault> ex)
			{
				this.log.Error($"Error while retrieving {table} records: {ex.Message}", ex);
				dict = new Dictionary<Guid, string>();
			}
			return dict;
		}
	}
}
