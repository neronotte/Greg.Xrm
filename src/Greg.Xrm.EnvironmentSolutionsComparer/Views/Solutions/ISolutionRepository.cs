using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public interface ISolutionRepository
	{
		IReadOnlyCollection<Solution> GetSolutions();
	}
}
