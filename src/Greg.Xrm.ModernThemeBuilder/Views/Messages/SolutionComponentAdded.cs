using Greg.Xrm.ModernThemeBuilder.Model;

namespace Greg.Xrm.ModernThemeBuilder.Views.Messages
{
	internal class SolutionComponentAdded
	{
		public SolutionComponent SolutionComponent { get; set; }

		public SolutionComponentAdded(SolutionComponent solutionComponent)
		{
			this.SolutionComponent = solutionComponent;
		}
	}
}