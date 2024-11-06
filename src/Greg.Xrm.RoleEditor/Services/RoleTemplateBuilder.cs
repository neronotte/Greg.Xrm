using Greg.Xrm.Logging;
using Greg.Xrm.RoleEditor.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;

namespace Greg.Xrm.RoleEditor.Services
{
	public class RoleTemplateBuilder
	{
		private readonly ILog log;
		private readonly IPrivilegeRepository privilegeRepository;




		public RoleTemplateBuilder(
			ILog log,
			IPrivilegeRepository privilegeRepository)
		{
			this.log = log;
			this.privilegeRepository = privilegeRepository;
		}


		public TemplateForRole CreateTemplate(IOrganizationService crm)
		{
			IReadOnlyList<Privilege> privileges;
			using (this.log.Track("Loading privileges..."))
			{
				privileges = this.privilegeRepository.GetAll(crm);
				this.log.Info($"Found {privileges.Count} privileges");
			}

			IReadOnlyList<EntityMetadata> tables;

			using (this.log.Track("Loading entity metadata..."))
			{
				var request = new RetrieveAllEntitiesRequest();
				request.EntityFilters = EntityFilters.Entity | EntityFilters.Privileges;

				var response = (RetrieveAllEntitiesResponse)crm.Execute(request);

				tables = response.EntityMetadata;

				this.log.Info($"Found {tables.Count} tables");

				var entityPrivileges = tables
					.SelectMany(x => x.Privileges)
					.Where(x => x.PrivilegeType != PrivilegeType.None)
					.Select(x => x.Name)
					.ToList();
				var privilegesToRemove = privileges.Where(x => entityPrivileges.Contains(x.name)).ToArray();

				privileges = privileges.Except(privilegesToRemove).ToList();
			}

			var filteredTables = tables
				.Where(x => x.Privileges.Length > 0 && !ShouldIgnore(x))
				.ToArray();


			DoubleCheck(filteredTables);

			var templatesForTables = filteredTables
				.Select(x => new TemplateForTable(x))
				.Cast<ITemplateForTable>()
				.ToList();

			var activityTables = tables
				.Where(x => x.Privileges.Length > 0 && x.IsActivity.GetValueOrDefault())
				.ToArray();
			if (activityTables.Length > 0)
			{
				templatesForTables.Add(new TemplateForTableSet("Activity", activityTables));
			}

			var customizationTables = tables
				.Where(x => x.Privileges.Length > 0 && IsCustomization(x))
				.ToArray();
			if (customizationTables.Length > 0)
			{
				templatesForTables.Add(new TemplateForTableSet("Customization", customizationTables));
			}

			var privilegesNotTableRelated = privileges.Select(x => new TemplateForGenericPrivilege(x)).ToList();

			var templateForRole = new TemplateForRole(templatesForTables, privilegesNotTableRelated);
			return templateForRole;
		}

		private void DoubleCheck(EntityMetadata[] filteredTables)
		{
			var duplicatedStuff = filteredTables
				.SelectMany(x => x.Privileges.Select(y => new
				{
					Table = x,
					Privilege = y.Name
				}))
				.GroupBy(x => x.Privilege)
				.Where(x => x.Count() > 1)
				.OrderBy(x => x.Key)
				.ToArray();


			foreach (var privilege in duplicatedStuff)
			{
				this.log.Warn("Duplicated privilege: " + privilege.Key);
				foreach (var item in privilege)
				{
					this.log.Warn(" - " + item.Table.LogicalName);
				}
			}
		}



		/// <summary>
		/// This filter is used to determine if a given table must be considered for the role viewer.
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
		private static bool ShouldIgnore(EntityMetadata metadata)
		{
			// this tables use the same privileges of existing tables
			var tablesToIgnore = new[] {
"sdkmessagefilter",
"sdkmessagepair",
"sdkmessagerequest",
"sdkmessagerequestfield",
"sdkmessageresponse",
"sdkmessageresponsefield",
"plugintypestatistic",
"customeraddress",
"activitypointer",
"recurrencerule",
"salesprocessinstance",
"entitlementchannel",
"entitlementtemplatechannel",
"importdata",
"importlog",
"discounttype",
"pricelevel",
"uomschedule",
"publisheraddress",
"resource",
"solutioncomponent",
"customeraddress",
"topichistory",
"topicmodelconfiguration",
"topicmodelexecutionhistory",
"entityrelationship",
"constraintbasedgroup",
"resourcegroup",
"resourcespec",
"orginsightsmetric",
"orginsightsnotification"
			};

			if (metadata.IsActivity.GetValueOrDefault()) return true;
			if (IsCustomization(metadata)) return true;

			return tablesToIgnore.Contains(metadata.LogicalName.ToLowerInvariant());
		}


		private static bool IsCustomization(EntityMetadata metadata)
		{
			var tables = new[] {
"displaystring",
"isvconfig",
"ribboncommand",
"ribboncontextgroup",
"ribboncustomization",
"ribbondiff",
"ribbonrule",
"ribbontabtocommandmap",
"sitemap",
"teamtemplate",
			};

			return tables.Contains(metadata.LogicalName.ToLowerInvariant());
		}

	}

}
