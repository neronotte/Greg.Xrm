using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentComparer.Engine
{
	public interface ICompareEngine
	{
		CompareResultSet CompareAll();

		CompareResultSet CompareAll(CompareResultSet previousResults, IReadOnlyCollection<string> entitiesToCompare);
	}
}
