using Greg.Xrm.Logging;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Greg.Xrm.RoleEditor.Services.ComponentResolvers
{
	public class ResolverByQuery : IComponentResolver
	{
		private readonly IOrganizationService crm;
		private readonly ILog log;
		private readonly string table;
		private readonly string nameColumn;
		private readonly string tableIdColumn;

		public ResolverByQuery(IOrganizationService crm, ILog log, string table, string nameColumn, string tableIdColumn = null)
		{
			this.crm = crm;
			this.log = log;
			this.table = table;
			this.nameColumn = nameColumn;
			this.tableIdColumn = tableIdColumn ?? $"{table}id";
		}

		public Dictionary<Guid, string> GetNames(IReadOnlyList<Guid> componentIdSet)
		{
			Dictionary<Guid, string> dict;
			try
			{
				var query = new QueryExpression(table);
				query.ColumnSet.AddColumns(nameColumn);
				query.Criteria.AddCondition(tableIdColumn, ConditionOperator.In, componentIdSet.Cast<object>().ToArray());
				query.NoLock = true;

				var result = crm.RetrieveMultiple(query);

				dict = result.Entities.ToDictionary(x => x.Id, x => x.GetAttributeValue<string>(nameColumn));
			}
			catch (FaultException<OrganizationServiceFault> ex)
			{
				log.Error($"Error while retrieving {table} records: {ex.Message}", ex);

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
	}
}