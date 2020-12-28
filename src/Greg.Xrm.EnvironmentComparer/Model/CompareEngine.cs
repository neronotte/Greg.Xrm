using Greg.Xrm.EnvironmentComparer.Logging;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentComparer.Model
{
	internal class CompareEngine : ICompareEngine
	{
		private readonly ILog log;
		private readonly IOrganizationService crm1;
		private readonly IOrganizationService crm2;
		private readonly IReadOnlyCollection<EntityComparer> comparerList;

		public CompareEngine(
			ILog log,
			IOrganizationService crm1, 
			IOrganizationService crm2, 
			IReadOnlyCollection<EntityComparer> comparerList)
		{
			this.log = log ?? throw new ArgumentNullException(nameof(log));
			this.crm1 = crm1 ?? throw new ArgumentNullException(nameof(crm1));
			this.crm2 = crm2 ?? throw new ArgumentNullException(nameof(crm2));
			this.comparerList = comparerList ?? throw new ArgumentNullException(nameof(comparerList));
		}


		public CompareResult CompareAll()
		{
			var result = new CompareResult();
			foreach (var comparer in this.comparerList)
			{
				var comparisonResult = Compare(comparer);
				result[comparer.EntityName] = comparisonResult;
			}
			return result;
		}

		private IReadOnlyCollection<Comparison<Entity>> Compare(EntityComparer comparer)
		{
			var list1 = comparer.GetEntitiesFrom(this.crm1, this.log, "CRM1");
			var list2 = comparer.GetEntitiesFrom(this.crm2, this.log, "CRM2");

			var compareResult = comparer.Compare(list1, list2);
			return compareResult;
		}
	}
}