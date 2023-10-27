using Greg.Xrm.ModernThemeBuilder.Model;
using System.Collections.Generic;

namespace Greg.Xrm.ModernThemeBuilder.Views.Messages
{
	public class SolutionComponentLoaded
	{
		public List<SolutionComponent> SolutionComponents { get; }

		public SolutionComponentLoaded(List<SolutionComponent> solutionComponents)
		{
			this.SolutionComponents = solutionComponents ?? new List<SolutionComponent>();
		}
	}
}