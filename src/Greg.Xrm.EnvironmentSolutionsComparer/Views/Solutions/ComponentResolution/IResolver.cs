using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution
{
	public interface IResolver
	{
		void Resolve(SolutionComponentGrid grid, IReadOnlyCollection<ConnectionModel> connections);
	}
}
