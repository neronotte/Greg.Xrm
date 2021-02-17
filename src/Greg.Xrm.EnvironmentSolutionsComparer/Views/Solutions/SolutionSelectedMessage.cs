namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public class SolutionSelectedMessage
	{
		public SolutionSelectedMessage(SolutionRow solution)
		{
			this.Solution = solution ?? throw new System.ArgumentNullException(nameof(solution));
		}

		public SolutionRow Solution { get; }
	}
}
