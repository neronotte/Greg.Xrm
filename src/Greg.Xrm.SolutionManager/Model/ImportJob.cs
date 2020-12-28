using Greg.Xrm.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;

namespace Greg.Xrm.SolutionManager.Model
{
	public class ImportJob : EntityWrapper
	{
		public ImportJob(Entity entity) : base(entity)
		{
		}


#pragma warning disable IDE1006 // Naming Styles
		public string solutionname => Get<string>();
		public double? progress => Get<double?>();
		public DateTime? startedon => Get<DateTime?>();
		public DateTime? completedon => Get<DateTime?>();
		public DateTime? createdon => Get<DateTime?>();
		public EntityReference createdby => Get<EntityReference>();
		public string data => Get<string>();
#pragma warning restore IDE1006 // Naming Styles


		public static IImportJobRepository GetRepository()
		{
			return new Repository();
		}

		private class Repository : IImportJobRepository
		{
			public ImportJob GetLatest(IOrganizationService service)
			{
				var query = new QueryExpression("importjob")
				{
					TopCount = 1,
					NoLock = true
				};
				query.ColumnSet.AddColumns(
					nameof(solutionname), 
					nameof(progress), 
					nameof(startedon), 
					nameof(completedon), 
					nameof(createdon),
					nameof(createdby),
					nameof(data));
				query.AddOrder(nameof(startedon), OrderType.Descending);

				var importJob = service.RetrieveMultiple(query).Entities.Select(x => new ImportJob(x)).FirstOrDefault();
				return importJob;
			}
		}
	}
}
