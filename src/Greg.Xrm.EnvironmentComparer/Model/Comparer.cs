using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentComparer.Model
{
	public class Comparer<T>
		where T : class
	{
		private readonly IKeyProvider<T> keyProvider;
		private readonly IExtendedEqualityComparer<T> equalityComparer;

		public Comparer(IKeyProvider<T> keyProvider, IExtendedEqualityComparer<T> equalityComparer, string name)
		{
			this.keyProvider = keyProvider ?? throw new ArgumentNullException(nameof(keyProvider));
			this.equalityComparer = equalityComparer ?? throw new ArgumentNullException(nameof(equalityComparer));
			this.Name = name;
		}

		public string Name { get; }

		public IReadOnlyCollection<Comparison<T>> Compare(IReadOnlyCollection<T> list1, IReadOnlyCollection<T> list2)
		{
			IDictionary<string, T> dict1, dict2;

			try
			{
				dict1 = list1.ToDictionary(_ => this.keyProvider.GetKey(_));
			}
			catch (ArgumentException)
			{
				throw new ComparisonException($"Comparer <{this.Name}> by key <{this.keyProvider.Name}>: List1 contains duplicate values by key!");
			}
			try
			{
				dict2 = list2.ToDictionary(_ => this.keyProvider.GetKey(_));
			}
			catch (ArgumentException)
			{
				throw new ComparisonException($"Comparer <{this.Name}> by key <{this.keyProvider.Name}>: List2 contains duplicate values by key!");
			}

			var resultList = new List<Comparison<T>>();

			foreach (var kvp in dict1)
			{
				var item1 = kvp.Value;

				Comparison<T> comparison;
				if (!dict2.TryGetValue(kvp.Key, out T item2))
				{
					comparison = Comparison<T>.OnlyItem1(kvp.Key, item1);
				}
				else
				{
					comparison = DeepCompare(kvp.Key, item1, item2);
				}
				resultList.Add(comparison);
			}

			foreach (var kvp in dict2)
			{
				var item2 = kvp.Value;

				if (dict1.ContainsKey(kvp.Key))
					continue;


				var comparison = Comparison<T>.OnlyItem2(kvp.Key, item2);
				resultList.Add(comparison);
			}

			return resultList;
		}

		private Comparison<T> DeepCompare(string key, T item1, T item2)
		{
			var areEqual = this.equalityComparer.Equals(item1, item2, out List<Difference> differentProperties);
			if (areEqual)
			{
				return Comparison<T>.Equals(key, item1, item2);
			}

			return Comparison<T>.MatchingButDifferent(key, item1, item2, differentProperties);
		}
	}
}
