using Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution;
using Greg.Xrm.Logging;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public class SolutionComponentGridBuilder
	{
		private readonly ILog log;
		private readonly ResolverChain resolvers;

		public SolutionComponentGridBuilder(ILog log)
		{
			this.log = log ?? throw new ArgumentNullException(nameof(log));
			this.resolvers = new ResolverChain(log);
		}




		public SolutionComponentGrid Create(IReadOnlyCollection<ConnectionModel> connections, SolutionRow solutionRow)
		{
			var grid = new SolutionComponentGrid();
			foreach (var env in connections)
			{
				var solution = solutionRow[env.Detail.ConnectionName];
				if (solution == null) continue;


				var resultList = GetSolutionComponentsFromSolutionAndEnvironment(solution, env.Crm, env.Detail.ConnectionName);
				grid.AddComponents(env, resultList);
			}

			this.resolvers.ResolveAll(grid, connections);

			return grid;
		}

		private List<SolutionComponent> GetSolutionComponentsFromSolutionAndEnvironment(Solution solution, IOrganizationService crm, string environmentName)
		{
			using (log.Track($"Getting components from solution <{solution.friendlyname}> and environment <{environmentName}>"))
			{
				var query = new QueryExpression("solutioncomponent");
				query.ColumnSet.AddColumns("componenttype", "rootsolutioncomponentid", "solutionid", "objectid", "ismetadata");
				query.Criteria.AddCondition("solutionid", ConditionOperator.Equal, solution.Id);

				var resultList = crm.RetrieveAll(query, x => new SolutionComponent(x));
				return resultList;
			}
		}
	}
}
