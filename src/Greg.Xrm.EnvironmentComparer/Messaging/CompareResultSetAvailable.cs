using Greg.Xrm.EnvironmentComparer.Engine;

namespace Greg.Xrm.EnvironmentComparer.Messaging
{
	public class CompareResultSetAvailable
	{

		public CompareResultSetAvailable(CompareResultSet compareResultSet)
		{
			this.CompareResultSet = compareResultSet ?? throw new System.ArgumentNullException(nameof(compareResultSet));
		}
		public CompareResultSet CompareResultSet { get; }
	}
}
