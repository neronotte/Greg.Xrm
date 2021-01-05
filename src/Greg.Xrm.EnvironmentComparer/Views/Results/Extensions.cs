using Greg.Xrm.EnvironmentComparer.Model;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	public static class Extensions
	{
		public static bool IsLeftMissingOrDifferent(this Comparison<Entity> x)
		{
			return x.Result == RecordComparisonResult.LeftMissing
				|| x.Result == RecordComparisonResult.MatchingButDifferent;
		}
		public static bool IsRightMissingOrDifferent(this Comparison<Entity> x)
		{
			return x.Result == RecordComparisonResult.RightMissing
				|| x.Result == RecordComparisonResult.MatchingButDifferent;
		}

		public static bool AreAllLeftMissingOrDifferent(this IEnumerable<Comparison<Entity>> items)
		{
			return items?.All(_ => _.IsLeftMissingOrDifferent()) ?? false;
		}

		public static bool AreAllRightMissingOrDifferent(this IEnumerable<Comparison<Entity>> items)
		{
			return items?.All(_ => _.IsRightMissingOrDifferent()) ?? false;
		}
	}
}
