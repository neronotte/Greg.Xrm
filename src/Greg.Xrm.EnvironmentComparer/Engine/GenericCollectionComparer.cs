using Greg.Xrm.EnvironmentComparer.Engine.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentComparer.Engine
{
	/// <summary>
	/// Instances of this type are able to compare two distinct, generic, collection of objects, using a specialized
	/// <see cref="IExtendedEqualityComparer{T}"/> to deep compare collection values.
	/// </summary>
	/// <typeparam name="T">The type of objects in the collection.</typeparam>
	public class GenericCollectionComparer<T>
		where T : class
	{
		private readonly IKeyProvider<T> keyProvider;
		private readonly IExtendedEqualityComparer<T> equalityComparer;

		public GenericCollectionComparer(IKeyProvider<T> keyProvider, IExtendedEqualityComparer<T> equalityComparer, string name)
		{
			this.keyProvider = keyProvider ?? throw new ArgumentNullException(nameof(keyProvider));
			this.equalityComparer = equalityComparer ?? throw new ArgumentNullException(nameof(equalityComparer));
			this.Name = name;
		}


		/// <summary>
		/// Gets the name of the current comparer.
		/// </summary>
		public string Name { get; }


		/// <summary>
		/// Compares two collections of objects of type <typeparamref name="T"/>.
		/// </summary>
		/// <param name="list1">The first collection</param>
		/// <param name="list2">The second collection</param>
		/// <returns>A list of <see cref="ObjectComparison{T}"/> records representing the results of the comparison of the two collections</returns>
		public IReadOnlyCollection<ObjectComparison<T>> Compare(IReadOnlyCollection<T> list1, IReadOnlyCollection<T> list2)
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

			var resultList = new List<ObjectComparison<T>>();

			foreach (var kvp in dict1)
			{
				var item1 = kvp.Value;

				ObjectComparison<T> comparison;
				if (!dict2.TryGetValue(kvp.Key, out T item2))
				{
					this.equalityComparer.Equals(item1, null, out List<Difference> differentProperties);

					comparison = ObjectComparison<T>.OnlyItem1(kvp.Key, item1, differentProperties);
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

				this.equalityComparer.Equals(null, item2, out List<Difference> differentProperties);

				var comparison = ObjectComparison<T>.OnlyItem2(kvp.Key, item2, differentProperties);
				resultList.Add(comparison);
			}

			return resultList;
		}


		/// <summary>
		/// Uses the equality comparer to compare two objects of the collection in order to identify whether they are equal or
		/// different by some property.
		/// </summary>
		/// <param name="key">The objects key</param>
		/// <param name="item1">The first item</param>
		/// <param name="item2">The second item</param>
		/// <returns>An instance of <see cref="ObjectComparison{T}"/> that represents the result of the comparison</returns>
		private ObjectComparison<T> DeepCompare(string key, T item1, T item2)
		{
			var areEqual = this.equalityComparer.Equals(item1, item2, out List<Difference> differentProperties);
			if (areEqual)
			{
				return ObjectComparison<T>.Equals(key, item1, item2);
			}

			return ObjectComparison<T>.MatchingButDifferent(key, item1, item2, differentProperties);
		}
	}
}
