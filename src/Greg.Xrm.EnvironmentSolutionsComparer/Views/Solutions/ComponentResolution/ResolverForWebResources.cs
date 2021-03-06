﻿using Greg.Xrm.Logging;
using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution
{
	public class ResolverForWebResources : IResolver
	{
		private readonly ILog log;

		public ResolverForWebResources(ILog log)
		{
			this.log = log ?? throw new System.ArgumentNullException(nameof(log));
		}

		public void Resolve(SolutionComponentGrid grid, IReadOnlyCollection<ConnectionModel> connections)
		{
			using (this.log.Track("Fetching webresource definitions"))
			{
				var group = grid.FirstOrDefault(_ => _.ComponentTypeCode == (int)SolutionComponentType.WebResource);
				if (group == null) return;

				group.SetAnalyzed();

				var idList = group
					.OfType<SolutionComponentLeaf>()
					.Select(_ => _.ObjectId)
					.ToList();

				foreach (var env in connections)
				{
					if (idList.Count == 0) return;

					var query = new QueryExpression("webresource");
					query.ColumnSet.AddColumns("displayname", "name");
					query.Criteria.AddCondition("webresourceid", ConditionOperator.In, idList.Cast<object>().ToArray());
					query.NoLock = true;

					var result = env.Crm.RetrieveMultiple(query);

					var entityList = result.Entities;

					foreach (var entity in entityList)
					{
						var entityId = entity.Id;
						idList.Remove(entityId);

						var leaf = group.OfType<SolutionComponentLeaf>().First(_ => _.ObjectId == entityId);
						leaf.SetLabelFromWebResource(entity);
					}
				}
			}
		}
	}
}
