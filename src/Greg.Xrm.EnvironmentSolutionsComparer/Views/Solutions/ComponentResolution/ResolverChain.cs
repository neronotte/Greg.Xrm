using Greg.Xrm.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution
{
	public class ResolverChain
	{
		private readonly IReadOnlyCollection<IResolver> resolverList;

		public ResolverChain(ILog log)
		{
			var resolverType = typeof(IResolver);
			this.resolverList = GetType().Assembly.GetTypes()
				.Where(_ => resolverType.IsAssignableFrom(_) && !_.IsAbstract && _.GetConstructors().Length > 0)
				.OrderBy(_ => _.Name)
				.Select(_ => (IResolver)Activator.CreateInstance(_, log))
				.ToList();
		}



		public void ResolveAll(SolutionComponentGrid grid, IReadOnlyCollection<ConnectionModel> connections)
		{
			foreach (var resolver in this.resolverList)
			{
				resolver.Resolve(grid, connections);
			}
		}
	}
}
