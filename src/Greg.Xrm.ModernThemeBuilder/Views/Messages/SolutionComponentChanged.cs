using Greg.Xrm.ModernThemeBuilder.Model;

namespace Greg.Xrm.ModernThemeBuilder.Views
{
	public class SolutionComponentChanged
	{
		public SolutionComponent SolutionComponent { get; set; }

		public SolutionComponentChanged(SolutionComponent solutionComponent)
		{
			this.SolutionComponent = solutionComponent;
		}
	}
}