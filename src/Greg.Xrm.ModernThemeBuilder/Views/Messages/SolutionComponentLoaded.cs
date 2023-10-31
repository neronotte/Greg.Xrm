using Greg.Xrm.ModernThemeBuilder.Model;
using System.Collections.Generic;

namespace Greg.Xrm.ModernThemeBuilder.Views.Messages
{
	public class SolutionComponentLoaded
	{
		public Solution Solution { get; }
		public List<SolutionComponent> SolutionComponents { get; }

		public SolutionComponentLoaded(Solution solution, List<SolutionComponent> solutionComponents)
		{
			this.Solution = solution;
			this.SolutionComponents = solutionComponents ?? new List<SolutionComponent>();
		}
	}
}