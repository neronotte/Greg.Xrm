using Greg.Xrm.ConstantsExtractor.Model;
using System.Collections.Generic;

namespace Greg.Xrm.ConstantsExtractor.Messaging
{
	class LoadSolutionsCompleted
	{
		public LoadSolutionsCompleted(List<Solution> solutionList)
		{
			SolutionList = solutionList;
		}

		public List<Solution> SolutionList { get; }
	}
}
