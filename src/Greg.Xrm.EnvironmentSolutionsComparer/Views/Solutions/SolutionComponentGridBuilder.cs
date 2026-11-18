using Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution2;
using Greg.Xrm.Logging;
using System;
using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public class SolutionComponentGridBuilder
	{
		private readonly ILog log;
		private readonly IComponentResolverEngine resolverEngine;

		public SolutionComponentGridBuilder(ILog log)
		{
			this.log = log ?? throw new ArgumentNullException(nameof(log));
			this.resolverEngine = new ComponentResolverEngine(log);
		}


		public SolutionComponentGrid Create(IReadOnlyCollection<ConnectionModel> connections, SolutionRow solutionRow)
		{
			var grid = new SolutionComponentGrid();
			foreach (var env in connections)
			{
				var solution = solutionRow[env.Detail.ConnectionName];
				if (solution == null) continue;


				var resultList = SolutionComponent.GetSolutionComponentsFromSolutionAndEnvironment(log, solution, env.Crm, env.Detail.ConnectionName);

				this.resolverEngine.ResolveAll(resultList, env);
				grid.AddComponents(env, resultList);
			}

			return grid;
		}

	}
}
