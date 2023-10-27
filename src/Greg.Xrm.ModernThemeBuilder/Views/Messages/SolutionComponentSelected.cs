using Greg.Xrm.ModernThemeBuilder.Model;

namespace Greg.Xrm.ModernThemeBuilder.Views.Messages
{
	public class SolutionComponentSelected
	{
		public SolutionComponent SolutionComponent { get; }

		public SolutionComponentSelected(SolutionComponent solutionComponent)
		{
			this.SolutionComponent = solutionComponent;
		}
	}
}