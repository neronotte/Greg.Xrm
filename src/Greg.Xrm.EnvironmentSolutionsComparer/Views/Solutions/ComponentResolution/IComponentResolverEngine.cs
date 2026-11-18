using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution2
{
	public interface IComponentResolverEngine
	{
		void ResolveAll(IReadOnlyCollection<SolutionComponent> componentList, ConnectionModel env);
	}
}
