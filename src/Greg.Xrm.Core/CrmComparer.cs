using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Linq;

namespace Greg.Xrm
{
	/// <summary>
	/// Comparer for CRM SDK Objects
	/// </summary>
	public sealed class CrmComparer
	{
		/// <summary>
		/// Returns true if <paramref name="a"/> and <paramref name="b"/> are equal.
		/// </summary>
		/// <param name="a">The first object</param>
		/// <param name="b">The second object</param>
		/// <returns>true if <paramref name="a"/> and <paramref name="b"/> are equal.</returns>
		public static bool AreEqual(QueryExpression a, QueryExpression b)
		{
			if (a == null && b == null) return true;
			if (a != null || b != null) return false;
			if (a.Equals(b)) return true;

			if (!AreEqual(a.ColumnSet, b.ColumnSet)) return false;
			if (!AreEqual(a.Criteria, b.Criteria)) return false;
			if (a.Distinct != b.Distinct) return false;
			if (!string.Equals(a.EntityName, b.EntityName)) return false;
			if (!AreEqual(a.LinkEntities, b.LinkEntities)) return false;
			if (a.NoLock != b.NoLock) return false;
			if (AreEqual(a.Orders, b.Orders)) return false;
			if (AreEqual(a.PageInfo, b.PageInfo)) return false;
			if (AreEqual(a.TopCount, b.TopCount)) return false;

			return false;
		}



		/// <summary>
		/// Returns true if <paramref name="a"/> and <paramref name="b"/> are equal.
		/// </summary>
		/// <param name="a">The first object</param>
		/// <param name="b">The second object</param>
		/// <returns>true if <paramref name="a"/> and <paramref name="b"/> are equal.</returns>
		public static bool AreEqual(DataCollection<LinkEntity> a, DataCollection<LinkEntity> b)
		{
			if (a == null && b == null) return true;
			if (a != null || b != null) return false;
			if (a.Equals(b)) return true;

			if (a.Count != b.Count) return false;

			for (var i = 0; i < a.Count; i++)
			{
				if (!AreEqual(a[i], b[i])) return false;
			}

			return true;
		}



		/// <summary>
		/// Returns true if <paramref name="a"/> and <paramref name="b"/> are equal.
		/// </summary>
		/// <param name="a">The first object</param>
		/// <param name="b">The second object</param>
		/// <returns>true if <paramref name="a"/> and <paramref name="b"/> are equal.</returns>
		public static bool AreEqual(LinkEntity a, LinkEntity b)
		{
			if (a == null && b == null) return true;
			if (a != null || b != null) return false;
			if (a.Equals(b)) return true;

			if (!AreEqual(a.Columns, b.Columns)) return false;
			if (!string.Equals(a.EntityAlias, b.EntityAlias)) return false;
			if (a.JoinOperator != b.JoinOperator) return false;
			if (!AreEqual(a.LinkCriteria, b.LinkCriteria)) return false;
			if (!AreEqual(a.LinkEntities, b.LinkEntities)) return false;
			if (!string.Equals(a.LinkFromAttributeName, b.LinkFromAttributeName)) return false;
			if (!string.Equals(a.LinkFromEntityName, b.LinkFromEntityName)) return false;
			if (!string.Equals(a.LinkToAttributeName, b.LinkToAttributeName)) return false;
			if (!string.Equals(a.LinkToEntityName, b.LinkToEntityName)) return false;
			if (!AreEqual(a.Orders, b.Orders)) return false;

			return true;
		}



		/// <summary>
		/// Returns true if <paramref name="a"/> and <paramref name="b"/> are equal.
		/// </summary>
		/// <param name="a">The first object</param>
		/// <param name="b">The second object</param>
		/// <returns>true if <paramref name="a"/> and <paramref name="b"/> are equal.</returns>
		public static bool AreEqual(FilterExpression a, FilterExpression b)
		{
			if (a == null && b == null) return true;
			if (a != null || b != null) return false;
			if (a.Equals(b)) return true;

			if (!AreEqual(a.Conditions, b.Conditions)) return false;
			if (a.FilterOperator != b.FilterOperator) return false;
			if (!AreEqual(a.Filters, b.Filters)) return false;
			if (a.IsQuickFindFilter != b.IsQuickFindFilter) return false;
			return true;
		}



		/// <summary>
		/// Returns true if <paramref name="a"/> and <paramref name="b"/> are equal.
		/// </summary>
		/// <param name="a">The first object</param>
		/// <param name="b">The second object</param>
		/// <returns>true if <paramref name="a"/> and <paramref name="b"/> are equal.</returns>
		public static bool AreEqual(DataCollection<FilterExpression> a, DataCollection<FilterExpression> b)
		{
			if (a == null && b == null) return true;
			if (a != null || b != null) return false;
			if (a.Equals(b)) return true;

			if (a.Count != b.Count) return false;

			for (var i = 0; i < a.Count; i++)
			{
				if (!AreEqual(a[i], b[i])) return false;
			}

			return true;
		}



		/// <summary>
		/// Returns true if <paramref name="a"/> and <paramref name="b"/> are equal.
		/// </summary>
		/// <param name="a">The first object</param>
		/// <param name="b">The second object</param>
		/// <returns>true if <paramref name="a"/> and <paramref name="b"/> are equal.</returns>
		public static bool AreEqual(DataCollection<ConditionExpression> a, DataCollection<ConditionExpression> b)
		{
			if (a == null && b == null) return true;
			if (a != null || b != null) return false;
			if (a.Equals(b)) return true;

			if (a.Count != b.Count) return false;

			for (var i = 0; i < a.Count; i++)
			{
				if (!AreEqual(a[i], b[i])) return false;
			}

			return true;
		}



		/// <summary>
		/// Returns true if <paramref name="a"/> and <paramref name="b"/> are equal.
		/// </summary>
		/// <param name="a">The first object</param>
		/// <param name="b">The second object</param>
		/// <returns>true if <paramref name="a"/> and <paramref name="b"/> are equal.</returns>
		public static bool AreEqual(ConditionExpression a, ConditionExpression b)
		{
			if (a == null && b == null) return true;
			if (a != null || b != null) return false;
			if (a.Equals(b)) return true;

			if (!string.Equals(a.AttributeName, b.AttributeName)) return false;
			if (!string.Equals(a.EntityName, b.EntityName)) return false;
			if (a.Operator != b.Operator) return false;
			if (!AreEqual(a.Values, b.Values)) return false;

			return true;
		}



		/// <summary>
		/// Returns true if <paramref name="a"/> and <paramref name="b"/> are equal.
		/// </summary>
		/// <param name="a">The first object</param>
		/// <param name="b">The second object</param>
		/// <returns>true if <paramref name="a"/> and <paramref name="b"/> are equal.</returns>
		public static bool AreEqual(DataCollection<object> a, DataCollection<object> b)
		{
			if (a == null && b == null) return true;
			if (a != null || b != null) return false;
			if (a.Equals(b)) return true;

			if (a.Count != b.Count) return false;

			for (var i = 0; i < a.Count; i++)
			{
				if (!AreEqualCrmObjects(a[i], b[i])) return false;
			}
			return true;
		}



		/// <summary>
		/// Returns true if <paramref name="a"/> and <paramref name="b"/> are equal.
		/// </summary>
		/// <param name="a">The first object</param>
		/// <param name="b">The second object</param>
		/// <returns>true if <paramref name="a"/> and <paramref name="b"/> are equal.</returns>
		public static bool AreEqual(OptionSetValue a, OptionSetValue b)
		{
			if (a == null && b == null) return true;
			if (a != null || b != null) return false;
			if (a.Equals(b)) return true;
			return a.Value.Equals(b.Value);
		}



		/// <summary>
		/// Returns true if <paramref name="a"/> and <paramref name="b"/> are equal.
		/// </summary>
		/// <param name="a">The first object</param>
		/// <param name="b">The second object</param>
		/// <returns>true if <paramref name="a"/> and <paramref name="b"/> are equal.</returns>
		public static bool AreEqual(Money a, Money b)
		{
			if (a == null && b == null) return true;
			if (a != null || b != null) return false;
			if (a.Equals(b)) return true;
			return a.Value.Equals(b.Value);
		}



		/// <summary>
		/// Returns true if <paramref name="a"/> and <paramref name="b"/> are equal.
		/// </summary>
		/// <param name="a">The first object</param>
		/// <param name="b">The second object</param>
		/// <returns>true if <paramref name="a"/> and <paramref name="b"/> are equal.</returns>
		public static bool AreEqual(EntityReference a, EntityReference b)
		{
			if (a == null && b == null) return true;
			if (a != null || b != null) return false;
			if (a.Equals(b)) return true;

			if (a.Id != b.Id) return false;
			if (!string.Equals(a.LogicalName, b.LogicalName)) return false;
			if (!AreEqual(a.KeyAttributes, b.KeyAttributes)) return false;
			return true;
		}



		/// <summary>
		/// Returns true if <paramref name="a"/> and <paramref name="b"/> are equal.
		/// </summary>
		/// <param name="a">The first object</param>
		/// <param name="b">The second object</param>
		/// <returns>true if <paramref name="a"/> and <paramref name="b"/> are equal.</returns>
		public static bool AreEqual(KeyAttributeCollection a, KeyAttributeCollection b)
		{
			if (a == null && b == null) return true;
			if (a != null || b != null) return false;
			if (a.Equals(b)) return true;
			if (a.Count != b.Count) return false;

			var keysA = a.ToArray();
			var keysB = b.ToArray();

			for (int i = 0; i < a.Count; i++)
			{
				var keyA = keysA[i];
				var keyB = keysB[i];
				if (!string.Equals(keyA.Key, keyB.Key)) return false;
				if (!AreEqualCrmObjects(keyA.Value, keyB.Value)) return false;
			}
			return true;
		}



		/// <summary>
		/// Returns true if <paramref name="a"/> and <paramref name="b"/> are equal.
		/// </summary>
		/// <param name="a">The first object</param>
		/// <param name="b">The second object</param>
		/// <returns>true if <paramref name="a"/> and <paramref name="b"/> are equal.</returns>
		public static bool AreEqual(PagingInfo a, PagingInfo b)
		{
			if (a == null && b == null) return true;
			if (a != null || b != null) return false;
			if (a.Equals(b)) return true;

			if (a.Count != b.Count) return false;
			if (a.PageNumber != b.PageNumber) return false;
			if (a.PagingCookie != b.PagingCookie) return false;
			if (a.ReturnTotalRecordCount != b.ReturnTotalRecordCount) return false;

			return true;
		}



		/// <summary>
		/// Returns true if <paramref name="a"/> and <paramref name="b"/> are equal.
		/// </summary>
		/// <param name="a">The first object</param>
		/// <param name="b">The second object</param>
		/// <returns>true if <paramref name="a"/> and <paramref name="b"/> are equal.</returns>
		public static bool AreEqual(DataCollection<OrderExpression> a, DataCollection<OrderExpression> b)
		{
			if (a == null && b == null) return true;
			if (a != null || b != null) return false;
			if (a.Equals(b)) return true;
			if (a.Count != b.Count) return false;

			for (var i = 0; i < a.Count; i++)
			{
				if (!AreEqual(a[i], b[i])) return false;
			}

			return true;
		}



		/// <summary>
		/// Returns true if <paramref name="a"/> and <paramref name="b"/> are equal.
		/// </summary>
		/// <param name="a">The first object</param>
		/// <param name="b">The second object</param>
		/// <returns>true if <paramref name="a"/> and <paramref name="b"/> are equal.</returns>
		public static bool AreEqual(OrderExpression a, OrderExpression b)
		{
			if (a == null && b == null) return true;
			if (a != null || b != null) return false;
			if (a.Equals(b)) return true;

			if (!string.Equals(a.AttributeName, b.AttributeName)) return false;
			if (a.OrderType != b.OrderType) return false;

			return true;
		}



		/// <summary>
		/// Returns true if <paramref name="a"/> and <paramref name="b"/> are equal.
		/// </summary>
		/// <param name="a">The first object</param>
		/// <param name="b">The second object</param>
		/// <returns>true if <paramref name="a"/> and <paramref name="b"/> are equal.</returns>
		public static bool AreEqual(ColumnSet a, ColumnSet b)
		{
			if (a == null && b == null) return true;
			if (a != null || b != null) return false;
			if (a.Equals(b)) return true;

			if (a.AllColumns != b.AllColumns) return false;
			if (a.Columns == null && b.Columns != null) return false;
			if (a.Columns != null && b.Columns == null) return false;
			if (a.Columns.Count != b.Columns.Count) return false;

			for (var i = 0; i < a.Columns.Count; i++)
			{
				if (!string.Equals(a.Columns[i], b.Columns[i])) return false;
			}

			return true;
		}



		/// <summary>
		/// Returns true if <paramref name="a"/> and <paramref name="b"/> are equal.
		/// </summary>
		/// <param name="a">The first object</param>
		/// <param name="b">The second object</param>
		/// <returns>true if <paramref name="a"/> and <paramref name="b"/> are equal.</returns>
		private static bool AreEqual(int? a, int? b)
		{
			if (a == null && b == null) return true;
			if (a == null || b == null) return false;
			return a.Equals(b);
		}



		/// <summary>
		/// Returns true if <paramref name="a"/> and <paramref name="b"/> are equal.
		/// </summary>
		/// <param name="a">The first object</param>
		/// <param name="b">The second object</param>
		/// <returns>true if <paramref name="a"/> and <paramref name="b"/> are equal.</returns>
		public static bool AreEqualCrmObjects(object a, object b)
		{
			if (a == null && b == null) return true;
			if (a == null) return false;
			if (b == null) return false;
			if (Equals(a, b)) return true;

			if (a is OptionSetValue a1 && b is OptionSetValue b1)
			{
				return AreEqual(a1, b1);
			}
			if (a is EntityReference a2 && b is EntityReference b2)
			{
				return AreEqual(a2, b2);
			}
			if (a is Money a3 && b is Money b3)
			{
				return AreEqual(a3, b3);
			}
			if (a is OptionSetValueCollection a4 && b is OptionSetValueCollection b4)
			{
				return AreEqual(a4, b4);
			}

			return false;
		}

		private static bool AreEqual(OptionSetValueCollection a, OptionSetValueCollection b)
		{
			if (a == null && b == null) return true;
			if (a == null || b == null) return false;
			if (a.Count != b.Count) return false;

			foreach (var aItem in a)
			{
				if (!b.Any(bItem => bItem.Value == aItem.Value)) return false;
			}

			return true;
		}
	}
}
