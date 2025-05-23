using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution2
{
	public interface IComponentResolver
	{
		void Resolve(IReadOnlyCollection<SolutionComponent> componentList, ConnectionModel env);
	}
}
