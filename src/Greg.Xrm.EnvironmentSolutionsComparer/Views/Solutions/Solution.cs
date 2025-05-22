using Greg.Xrm.Logging;
using Greg.Xrm.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public class Solution : EntityWrapper
	{
		public Solution(Entity entity) : base(entity)
		{
		}

#pragma warning disable IDE1006 // Naming Styles
		public string uniquename => Get<string>();
		public string friendlyname => Get<string>();
		public string description => Get<string>();
		public string version => Get<string>();
		public bool isvisible => Get<bool>();
		public bool ismanaged => Get<bool>();
		public DateTime installedon => Get<DateTime>();
		public EntityReference parentsolutionid => Get<EntityReference>();
		public EntityReference publisherid => Get<EntityReference>();
#pragma warning restore IDE1006 // Naming Styles


		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (ReferenceEquals(obj, this)) return true;
			if (!(obj is Solution other)) return false;

			return other.uniquename.Equals(this.uniquename)
				&& other.version.Equals(this.version)
				&& other.ismanaged.Equals(this.ismanaged)
				&& other.isvisible.Equals(this.isvisible);
		}

		public override int GetHashCode()
		{
			return this.CalculateHashCode(() => this.uniquename, () => this.version, () => this.isvisible, () => this.ismanaged);
		}



		public static ISolutionRepository GetRepository(ILog log, IOrganizationService crm, string environmentName)
		{
			return new Repository(log, crm, environmentName);
		}


		private class Repository : ISolutionRepository
		{
			private readonly ILog log;
			private readonly IOrganizationService crm;
			private readonly string environmentName;

			public Repository(ILog log, IOrganizationService crm, string environmentName)
			{
				this.log = log;
				this.crm = crm;
				this.environmentName = environmentName;
			}

			public IReadOnlyCollection<Solution> GetSolutions()
			{
				using (log.Track("Loading solution list from environment " + environmentName))
				{
					var query = new QueryExpression("solution");
					query.ColumnSet.AddColumns(
						"description",
						"uniquename",
						"friendlyname",
						"version",
						"installedon",
						"parentsolutionid",
						"publisherid",
						"isvisible",
						"ismanaged");

					var entityList = this.crm.RetrieveMultiple(query)
						.Entities
						.Select(_ => new Solution(_))
						.ToList();

					return entityList;
				}
			}
		}
	}
}
