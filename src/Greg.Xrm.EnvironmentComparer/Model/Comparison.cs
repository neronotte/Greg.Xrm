using System;
using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentComparer.Model
{
	public class Comparison<T>
		where T : class
	{

		#region Static factory

		public static Comparison<T> OnlyItem1(string key, T item1)
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentNullException(nameof(key));
			if (item1 == default(T))
				throw new ArgumentNullException(nameof(item1));


			return new Comparison<T>(key, item1, null, RecordComparisonResult.RightMissing);
		}


		public static Comparison<T> OnlyItem2(string key, T item2)
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentNullException(nameof(key));
			if (item2 == default(T))
				throw new ArgumentNullException(nameof(item2));

			return new Comparison<T>(key, null, item2, RecordComparisonResult.LeftMissing);
		}


		public static Comparison<T> Equals(string key, T item1, T item2)
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentNullException(nameof(key));
			if (item1 == default(T))
				throw new ArgumentNullException(nameof(item1));
			if (item2 == default(T))
				throw new ArgumentNullException(nameof(item2));


			return new Comparison<T>(key, item1, item2, RecordComparisonResult.Equals);
		}


		public static Comparison<T> MatchingButDifferent(string key, T item1, T item2, IReadOnlyCollection<Difference> differentProperties)
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


			return new Comparison<T>(key, item1, item2, RecordComparisonResult.MatchingButDifferent, differentProperties);
		}

		#endregion


		protected Comparison(string key, T item1, T item2, RecordComparisonResult result, IReadOnlyCollection<Difference> differentProperties = null)
		{
			this.Key = key;
			this.Item1 = item1;
			this.Item2 = item2;
			this.Result = result;
			this.DifferentProperties = differentProperties ?? Array.Empty<Difference>();
			this.AdditionalInfo = new Dictionary<string, string>();
		}


		public string Key { get; }
		public T Item1 { get; }
		public T Item2 { get; }
		public RecordComparisonResult Result { get; }
		public IReadOnlyCollection<Difference> DifferentProperties { get; }




		public string this[string additionalInfoKey]
		{
			get { return this.AdditionalInfo[additionalInfoKey]; }
		}

		public bool ContainsKey(string additionalInfoKey)
		{
			return this.AdditionalInfo.ContainsKey(additionalInfoKey);
		}

		public Dictionary<string, string> AdditionalInfo { get; }



		public override string ToString()
		{
			return this.Result.ToString();
		}
	}
}
