using Greg.Xrm.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;
using System.Text;

namespace Greg.Xrm.ModernThemeBuilder.Model
{
	public class Solution : EntityWrapper
	{
		private Solution(Entity entity) : base(entity)
		{
		}

		public string friendlyname => Get<string>();
		public string uniquename => Get<string>();
		public string version => Get<string>();
		public string description => Get<string>();
		public string publisheridprefix => GetAliased<string>("p.customizationprefix");
		public string publisheridfriendlyname => GetAliased<string>("p.friendlyname");

		public static ISolutionRepository GetRepository(IOrganizationService crm)
		{
			return new SolutionRepository(crm);
		}

		class SolutionRepository : ISolutionRepository
		{
			private readonly IOrganizationService crm;

			public SolutionRepository(IOrganizationService crm)
			{
				this.crm = crm;
			}

			public IReadOnlyList<Solution> GetUnmanagedSolutions()
			{
				var query = new QueryExpression("solution");
				query.ColumnSet.AddColumns("friendlyname", "uniquename", "version", "description");
				var link = query.AddLink("publisher", "publisherid", "publisherid", JoinOperator.Inner);
				link.Columns.AddColumns("customizationprefix", "friendlyname");
				link.EntityAlias = "p";

				query.Criteria.AddCondition("ismanaged", ConditionOperator.Equal, false);
				query.Criteria.AddCondition("isvisible", ConditionOperator.Equal, true);
				query.AddOrder("createdon", OrderType.Descending);

				var solutionList = this.crm.RetrieveAll(query, x => new Solution(x));
				return solutionList;
			}
		}


		public override string ToString()
		{
			return $"{this.friendlyname} v.{this.version} ({this.uniquename})";
		}
	}

	public interface ISolutionRepository
	{
		IReadOnlyList<Solution> GetUnmanagedSolutions();
	}
}
