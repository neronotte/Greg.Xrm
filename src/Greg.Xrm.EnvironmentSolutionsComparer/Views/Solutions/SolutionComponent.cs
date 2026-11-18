using Greg.Xrm.Logging;
using Greg.Xrm.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public class SolutionComponent : EntityWrapper
	{
		public SolutionComponent(Entity entity) : base(entity)
		{
			this.TypeLabel = this.ComponentTypeName ?? this.SolutionComponentDefinitionPrimaryEntityName ?? componenttype.Value.ToString();
			this.Label = $"{this.TypeLabel}: {this.objectid}";
		}


		public string ComponentTypeName => this.GetFormatted(nameof(componenttype));

#pragma warning disable IDE1006 // Naming Styles
		public OptionSetValue componenttype => this.Get<OptionSetValue>();

		public EntityReference solutionid => this.Get<EntityReference>();

		public Guid objectid => this.Get<Guid>();

		public bool ismetadata => this.Get<bool>();

#pragma warning restore IDE1006 // Naming Styles
		
		public string SolutionComponentDefinitionName => this.GetAliased<string>("scd", "name");
		public string SolutionComponentDefinitionPrimaryEntityName => this.GetAliased<string>("scd", "primaryentityname");
		public int SolutionComponentDefinitionObjectTypeCode => this.GetAliased<int>("scd", "objecttypecode");


		public string Label { get; set; }

		public string TypeLabel { get; set; }


		public override string ToString()
		{
			return $"{ComponentTypeName} {objectid}";
		}


		public string BuildUrl(ConnectionModel conn)
		{
			var webApplicationUrl = conn.Detail.WebApplicationUrl.TrimEnd('/');

			var sb = new StringBuilder();
			sb.Append(webApplicationUrl);
			sb.Append("/main.aspx?");
			if (!string.IsNullOrWhiteSpace(this.SolutionComponentDefinitionPrimaryEntityName))
			{
				sb.Append("etn=");
				sb.Append(this.SolutionComponentDefinitionPrimaryEntityName);
			}
			else
			{
				sb.Append("etc=");
				sb.Append(this.componenttype.Value);
			}
			sb.Append("&pagetype=entityrecord&id=");
			sb.Append(this.objectid.ToString("N"));

			return sb.ToString();
		}



		public static List<SolutionComponent> GetSolutionComponentsFromSolutionAndEnvironment(ILog log, Solution solution, IOrganizationService crm, string environmentName)
		{
			using (log.Track($"Getting components from solution <{solution.friendlyname}> and environment <{environmentName}>"))
			{
				var query = new QueryExpression("solutioncomponent");
				query.ColumnSet.AddColumns("componenttype", "rootsolutioncomponentid", "solutionid", "objectid", "ismetadata");
				query.Criteria.AddCondition("solutionid", ConditionOperator.Equal, solution.Id);
				var link = query.AddLink("solutioncomponentdefinition", "componenttype", "solutioncomponenttype", JoinOperator.LeftOuter);
				link.EntityAlias = "scd";
				link.Columns.AddColumns("name", "primaryentityname", "objecttypecode");

				var resultList = crm.RetrieveAll(query, x => new SolutionComponent(x));
				return resultList;
			}
		}
	}
}
