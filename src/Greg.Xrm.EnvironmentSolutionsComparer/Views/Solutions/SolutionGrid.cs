using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public class SolutionGrid
	{
		public SolutionGrid()
		{
			this.Environments = new List<ConnectionModel>();
			this.Rows = new List<SolutionRow>();
		}

		public List<ConnectionModel> Environments { get; }

		public List<SolutionRow> Rows { get; }
	}
}
