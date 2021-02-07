using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentComparer.Engine
{
	public class ObjectComparison<T>
		where T : class
	{

		#region Static factory

		public static ObjectComparison<T> OnlyItem1(string key, T item1, IReadOnlyCollection<Difference> differentProperties = null)
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentNullException(nameof(key));
			if (item1 == default(T))
				throw new ArgumentNullException(nameof(item1));


			return new ObjectComparison<T>(key, item1, null, ObjectComparisonResult.RightMissing, differentProperties);
		}


		public static ObjectComparison<T> OnlyItem2(string key, T item2, IReadOnlyCollection<Difference> differentProperties = null)
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentNullException(nameof(key));
			if (item2 == default(T))
				throw new ArgumentNullException(nameof(item2));

			return new ObjectComparison<T>(key, null, item2, ObjectComparisonResult.LeftMissing, differentProperties);
		}


		public static ObjectComparison<T> Equals(string key, T item1, T item2)
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentNullException(nameof(key));
			if (item1 == default(T))
				throw new ArgumentNullException(nameof(item1));
			if (item2 == default(T))
				throw new ArgumentNullException(nameof(item2));


			return new ObjectComparison<T>(key, item1, item2, ObjectComparisonResult.Equals);
		}


		public static ObjectComparison<T> MatchingButDifferent(string key, T item1, T item2, IReadOnlyCollection<Difference> differentProperties)
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentNullException(nameof(key));
			if (item1 == default(T))
				throw new ArgumentNullException(nameof(item1));
			if (item2 == default(T))
				throw new ArgumentNullException(nameof(item2));
			if (differentProperties == null)
				throw new ArgumentNullException(nameof(differentProperties));
			if (differentProperties.Count == 0)
				throw new ArgumentException("differentProperties array must have at least one element!", nameof(differentProperties));


			return new ObjectComparison<T>(key, item1, item2, ObjectComparisonResult.MatchingButDifferent, differentProperties);
		}

		#endregion


		protected ObjectComparison(string key, T item1, T item2, ObjectComparisonResult result, IReadOnlyCollection<Difference> differentProperties = null)
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
		public ObjectComparisonResult Result { get; }
		public IReadOnlyCollection<Difference> DifferentProperties { get; }


		private readonly Dictionary<string, string> additionalDataDict = new Dictionary<string, string>();


		public string this[string additionalDataKey]
		{
			get
			{
				if (this.additionalDataDict.TryGetValue(additionalDataKey, out string value)) return value;
				return null;
			}
			set
			{
				this.additionalDataDict[additionalDataKey] = value;
			}
		}

		public bool Contains(string additionalDataKey)
		{
			return this.additionalDataDict.ContainsKey(additionalDataKey);
		}
		public bool ContainsAny(params string[] additionalDataKeyList)
		{
			return additionalDataKeyList.Any(x => this.additionalDataDict.ContainsKey(x));
		}

		public void Remove(string additionalDataKey)
		{
			this.additionalDataDict.Remove(additionalDataKey);
		}



		public override string ToString()
		{
			return this.Result.ToString();
		}
	}
}
