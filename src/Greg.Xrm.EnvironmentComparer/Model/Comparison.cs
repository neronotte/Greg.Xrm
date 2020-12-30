using System;
using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentComparer.Model
{
	public class Comparison<T>
		where T : class
	{

		public static Comparison<T> OnlyItem1(string key, T item1)
		{
			return new Comparison<T>(key, item1, null, RecordComparisonResult.RightMissing);
		}
		public static Comparison<T> OnlyItem2(string key, T item2)
		{
			return new Comparison<T>(key, null, item2, RecordComparisonResult.LeftMissing);
		}




		public Comparison(string key, T item1, T item2, RecordComparisonResult result, IReadOnlyCollection<Difference> differentProperties = null)
		{
			this.Key = key;
			this.Item1 = item1;
			this.Item2 = item2;
			this.Result = result;
			this.DifferentProperties = differentProperties ?? Array.Empty<Difference>();
		}

		public string Key { get; }

		public T Item1 { get; }
		public T Item2 { get; }
		public RecordComparisonResult Result { get; }
		public IReadOnlyCollection<Difference> DifferentProperties { get; }

		public Dictionary<string, string> AdditionalInfo { get; }

		public override string ToString()
		{
			return this.Result.ToString();
		}
	}
}
