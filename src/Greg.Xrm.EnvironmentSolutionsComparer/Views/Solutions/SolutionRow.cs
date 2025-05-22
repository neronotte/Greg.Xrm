using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public class SolutionRow : IEnumerable<Solution>
	{
		private readonly Dictionary<string, Solution> solutionDict = new Dictionary<string, Solution>();

		public SolutionRow(string solutionUniqueName, string solutionPublisher)
		{
			this.SolutionUniqueName = solutionUniqueName;
			this.SolutionPublisher = solutionPublisher;
		}
		public string SolutionUniqueName { get; }
		public string SolutionPublisher { get; }

		public Solution this[string environmentName]
		{
			get
			{
				if (this.solutionDict.TryGetValue(environmentName, out Solution solution)) return solution;
				return null;
			}
			set
			{
				if (value == null)
					this.solutionDict.Remove(environmentName);
				else
					this.solutionDict[environmentName] = value;
			}
		}


		public bool AllSameVersion
		{
			get
			{
				return this.solutionDict.Values.Select(_ => _.version).Distinct().Count() <= 1;
			}
		}

		public bool IsAnyInvisible
		{
			get
			{
				return this.solutionDict.Values.Any(_ => !_.isvisible);
			}
		}

		public bool IsEmpty { get => this.solutionDict.Count == 0; }

		public IEnumerator<Solution> GetEnumerator()
		{
			return this.solutionDict.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
