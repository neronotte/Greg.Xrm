using Microsoft.Xrm.Sdk;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentComparer.Engine
{
	public class CompareResultForEntity : IReadOnlyCollection<ObjectComparison<Entity>>
	{
		private readonly List<ObjectComparison<Entity>> recordComparisonList = new List<ObjectComparison<Entity>>();
		private readonly Dictionary<string, string> additionalDataDict = new Dictionary<string, string>();

		public CompareResultForEntity(string entityName, bool isEntityValidForCrm1, bool isEntityValidForCrm2, IReadOnlyCollection<ObjectComparison<Entity>> innerCollection)
		{
			this.EntityName = entityName;
			this.IsEntityValidForCrm1 = isEntityValidForCrm1;
			this.IsEntityValidForCrm2 = isEntityValidForCrm2;
			this.recordComparisonList.AddRange(innerCollection);
		}



		public string EntityName { get; }

		public bool IsEntityValidForCrm1 { get; }

		public bool IsEntityValidForCrm2 { get; }


		public ObjectComparison<Entity> this[int index]
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

		public IEnumerator<ObjectComparison<Entity>> GetEnumerator()
		{
			return this.recordComparisonList.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
