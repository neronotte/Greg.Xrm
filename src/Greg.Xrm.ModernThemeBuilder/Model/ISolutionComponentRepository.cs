using System;
using System.Collections.Generic;

namespace Greg.Xrm.ModernThemeBuilder.Model
{
	public interface ISolutionComponentRepository
	{
		List<SolutionComponent> GetSolutionComponentBySolutionId(Guid solutionId);

		void Create(SolutionComponent solutionComponent, Solution solution);
	}
}
