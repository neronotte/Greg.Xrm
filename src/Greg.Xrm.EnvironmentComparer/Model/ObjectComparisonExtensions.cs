﻿using Greg.Xrm.EnvironmentComparer.Engine;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentComparer
{
	public static class ObjectComparisonExtensions
	{
		const string Actioned = "Actioned";



		public static bool IsActioned<T>(this ObjectComparison<T> x)
			where T : class
		{
			return x.Contains(Actioned);
		}


		public static void SetActioned<T>(this ObjectComparison<T> recordComparison)
			where T : class
		{
			if (recordComparison == null) return;

			recordComparison[Actioned] = Actioned;
		}

		public static void ClearActioned<T>(this ObjectComparison<T> recordComparison)
			where T : class
		{
			if (recordComparison == null) return;
			recordComparison.Remove(Actioned);
		}





		public static bool IsLeftMissingOrDifferent<T>(this ObjectComparison<T> x)
			where T : class
		{
			return x.Result == ObjectComparisonResult.LeftMissing
				|| x.Result == ObjectComparisonResult.MatchingButDifferent;
		}
		public static bool IsRightMissingOrDifferent<T>(this ObjectComparison<T> x)
			where T : class
		{
			return x.Result == ObjectComparisonResult.RightMissing
				|| x.Result == ObjectComparisonResult.MatchingButDifferent;
		}

		public static bool AreAllLeftMissingOrDifferentAndNotActioned<T>(this IEnumerable<ObjectComparison<T>> items)
			where T : class
		{
			if (items == null) return false;

			return items.All(_ => _.IsLeftMissingOrDifferent() && !_.IsActioned());
		}

		public static bool AreAllRightMissingOrDifferentAndNotActioned<T>(this IEnumerable<ObjectComparison<T>> items)
			where T : class
		{
			if (items == null) return false;

			return items.All(_ => _.IsRightMissingOrDifferent() && !_.IsActioned());
		}
	}
}
