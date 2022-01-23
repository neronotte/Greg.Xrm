using Greg.Xrm.Logging;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentComparer.Engine
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


		public CompareResultSet CompareAll()
		{
			var result = new CompareResultSet();
			foreach (var comparer in this.comparerList)
			{
				var comparisonResult = Compare(comparer);
				result[comparer.EntityName] = comparisonResult;
			}
			return result;
		}

		public CompareResultSet CompareAll(CompareResultSet previousResults, IReadOnlyCollection<string> entitiesToCompare)
		{
			if (previousResults is null)
			{
				throw new ArgumentNullException(nameof(previousResults));
			}
			if (entitiesToCompare is null)
			{
				throw new ArgumentNullException(nameof(entitiesToCompare));
			}

			var result = previousResults.Clone();
			foreach (var comparer in this.comparerList.Where(c => entitiesToCompare.Contains( c.EntityName )))
			{
				var comparisonResult = Compare(comparer);
				result[comparer.EntityName] = comparisonResult;
			}
			return result;
		}

		private CompareResultForEntity Compare(EntityComparer comparer)
		{
			List<Entity> list1, list2;
			bool isEntityValidForCrm1, isEntityValidForCrm2;
			try
			{
				list1 = comparer.GetEntitiesFrom(this.crm1, this.log, "CRM1");
				isEntityValidForCrm1 = true;
			}
			catch(EntityNotFoundException)
			{
				list1 = new List<Entity>();
				isEntityValidForCrm1 = false;
			}

			try
			{
				list2 = comparer.GetEntitiesFrom(this.crm2, this.log, "CRM2");
				isEntityValidForCrm2 = true;
			}
			catch (EntityNotFoundException)
			{
				list2 = new List<Entity>();
				isEntityValidForCrm2 = false;
			}

			var resultByObjectList = comparer.Compare(list1, list2);

			var r = new CompareResultForEntity(comparer.EntityName, isEntityValidForCrm1, isEntityValidForCrm2, resultByObjectList);
			return r;
		}
	}
}