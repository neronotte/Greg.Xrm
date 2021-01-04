using Microsoft.Xrm.Sdk;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentComparer.Model
{
	public class CompareResultForEntity : IReadOnlyCollection<Comparison<Entity>>
	{
		private readonly List<Comparison<Entity>> recordComparisonList = new List<Comparison<Entity>>();
		private readonly Dictionary<string, string> additionalDataDict = new Dictionary<string, string>();

		public CompareResultForEntity(string entityName, IReadOnlyCollection<Comparison<Entity>> innerCollection)
		{
			this.EntityName = entityName;
			this.recordComparisonList.AddRange(innerCollection);
		}



		public string EntityName { get; }


		public Comparison<Entity> this[int index]
		{
			get => this.recordComparisonList[index];
		}


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

		internal void Remove(string additionalDataKey)
		{
			this.additionalDataDict.Remove(additionalDataKey);
		}




		public int Count => this.recordComparisonList.Count;

		public IEnumerator<Comparison<Entity>> GetEnumerator()
		{
			return this.recordComparisonList.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
