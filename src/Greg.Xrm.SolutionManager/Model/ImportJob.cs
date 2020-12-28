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


		public string uniquename => GetAliased<string>("s", nameof(uniquename));
		public string friendlyname => GetAliased<string>("s", nameof(friendlyname));
		public string description => GetAliased<string>("s", nameof(description));
		public EntityReference publisherid => GetAliased<EntityReference>("s", nameof(publisherid));
		public string version => GetAliased<string>("s", nameof(version));
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

				var solutionLink = query.AddLink("solution", "solutionid", "solutionid", JoinOperator.LeftOuter);
				solutionLink.EntityAlias = "s";
				solutionLink.Columns.AddColumns(
					nameof(uniquename),
					nameof(friendlyname), 
					nameof(description), 
					nameof(publisherid), 
					nameof(version));

				query.AddOrder(nameof(startedon), OrderType.Descending);

				var importJob = service.RetrieveMultiple(query).Entities.Select(x => new ImportJob(x)).FirstOrDefault();
				return importJob;
			}
		}
	}
}
