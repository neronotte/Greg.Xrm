using Greg.Xrm.Logging;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public class SolutionComponentGridBuilder
	{
		private readonly ILog log;

		public SolutionComponentGridBuilder(ILog log)
		{
			this.log = log ?? throw new ArgumentNullException(nameof(log));
		}




		public SolutionComponentGrid Create(IReadOnlyCollection<ConnectionModel> connections, SolutionRow solutionRow)
		{
			var grid = new SolutionComponentGrid();
			foreach (var env in connections)
			{
				var solution = solutionRow[env.Detail.ConnectionName];
				if (solution == null) continue;


				var resultList = GetSolutionComponentsFromSolutionAndEnvironment(solution, env.Crm, env.Detail.ConnectionName);
				grid.AddComponents(env, resultList);
			}

			FillCustomControls(grid, connections);
			FillDisplayStrings(grid, connections);
			FillEmailTemplates(grid, connections);
			FillEntities(grid, connections);
			FillFieldSecurityProfiles(grid, connections);
			FillPluginAssemblies(grid, connections);
			FillRibbonCustomizations(grid, connections);
			FillRoles(grid, connections);
			FillSdkMessageProcessingSteps(grid, connections);
			FillSavedQuery(grid, connections);
			FillSystemForms(grid, connections);
			FillWebResources(grid, connections);
			FillWorkflows(grid, connections);

			return grid;
		}

		private List<SolutionComponent> GetSolutionComponentsFromSolutionAndEnvironment(Solution solution, IOrganizationService crm, string environmentName)
		{
			using (log.Track($"Getting components from solution <{solution.friendlyname}> and environment <{environmentName}>"))
			{
				var query = new QueryExpression("solutioncomponent");
				query.ColumnSet.AddColumns("componenttype", "rootsolutioncomponentid", "solutionid", "objectid", "ismetadata");
				query.Criteria.AddCondition("solutionid", ConditionOperator.Equal, solution.Id);

				var resultList = crm.RetrieveAll(query, x => new SolutionComponent(x));
				return resultList;
			}
		}



		private void FillCustomControls(SolutionComponentGrid grid, IReadOnlyCollection<ConnectionModel> connections)
		{
			using (this.log.Track("Fetching CustomControl definitions"))
			{
				var group = grid.FirstOrDefault(_ => _.ComponentTypeCode == (int)SolutionComponentType.CustomControl);
				if (group == null) return;

				group.SetAnalyzed();

				var idList = group
					.OfType<SolutionComponentLeaf>()
					.Select(_ => _.ObjectId)
					.ToList();

				foreach (var env in connections)
				{
					if (idList.Count == 0) return;

					var query = new QueryExpression("customcontrol");
					query.ColumnSet.AddColumns("name");
					query.Criteria.AddCondition("customcontrolid", ConditionOperator.In, idList.Cast<object>().ToArray());
					query.NoLock = true;

					var result = env.Crm.RetrieveMultiple(query);

					var entityList = result.Entities;

					foreach (var entity in entityList)
					{
						var entityId = entity.Id;
						idList.Remove(entityId);

						var leaf = group.OfType<SolutionComponentLeaf>().First(_ => _.ObjectId == entityId);
						leaf.SetLabelFromCustomControl(entity);
					}
				}
			}
		}

		private void FillDisplayStrings(SolutionComponentGrid grid, IReadOnlyCollection<ConnectionModel> connections)
		{
			using (this.log.Track("Fetching DisplayString definitions"))
			{
				var group = grid.FirstOrDefault(_ => _.ComponentTypeCode == (int)SolutionComponentType.DisplayString);
				if (group == null) return;

				group.SetAnalyzed();

				var idList = group
					.OfType<SolutionComponentLeaf>()
					.Select(_ => _.ObjectId)
					.ToList();

				foreach (var env in connections)
				{
					if (idList.Count == 0) return;

					var query = new QueryExpression("displaystring");
					query.ColumnSet.AddColumns("displaystringkey");
					query.Criteria.AddCondition("displaystringid", ConditionOperator.In, idList.Cast<object>().ToArray());
					query.NoLock = true;

					var result = env.Crm.RetrieveMultiple(query);

					var entityList = result.Entities;

					foreach (var entity in entityList)
					{
						var entityId = entity.Id;
						idList.Remove(entityId);

						var leaf = group.OfType<SolutionComponentLeaf>().First(_ => _.ObjectId == entityId);
						leaf.SetLabelFromDisplayString(entity);
					}
				}
			}
		}

		private void FillEmailTemplates(SolutionComponentGrid grid, IReadOnlyCollection<ConnectionModel> connections)
		{
			using (this.log.Track("Fetching email templates definitions"))
			{
				var group = grid.FirstOrDefault(_ => _.ComponentTypeCode == (int)SolutionComponentType.EmailTemplate);
				if (group == null) return;

				group.SetAnalyzed();

				var idList = group
					.OfType<SolutionComponentLeaf>()
					.Select(_ => _.ObjectId)
					.ToList();

				foreach (var env in connections)
				{
					if (idList.Count == 0) return;

					var query = new QueryExpression("template");
					query.ColumnSet.AddColumns("title");
					query.Criteria.AddCondition("templateid", ConditionOperator.In, idList.Cast<object>().ToArray());
					query.NoLock = true;

					var result = env.Crm.RetrieveMultiple(query);

					var entityList = result.Entities;

					foreach (var entity in entityList)
					{
						var entityId = entity.Id;
						idList.Remove(entityId);

						var leaf = group.OfType<SolutionComponentLeaf>().First(_ => _.ObjectId == entityId);
						leaf.SetLabelFromEmailTemplate(entity);
					}
				}
			}
		}

		private void FillEntities(SolutionComponentGrid grid, IReadOnlyCollection<ConnectionModel> connections)
		{
			using(this.log.Track("Fetching entity definitions"))
			{
				var group = grid.FirstOrDefault(_ => _.ComponentTypeCode == (int)SolutionComponentType.Entity);
				if (group == null) return;

				group.SetAnalyzed();

				var idList = group
					.OfType<SolutionComponentLeaf>()
					.Select(_ => _.ObjectId)
					.ToList();

				foreach (var env in connections)
				{
					if (idList.Count == 0) return;

					var query = new EntityQueryExpression
					{
						AttributeQuery = new AttributeQueryExpression
						{
							Properties = new MetadataPropertiesExpression("MetadataId", "SchemaName", "LogicalName", "DisplayName")
						}
					};
					query.Criteria.Conditions.Add(new MetadataConditionExpression("MetadataId", MetadataConditionOperator.In, idList.ToArray()));

					var request = new RetrieveMetadataChangesRequest
					{
						Query = query
					};

					var result = (RetrieveMetadataChangesResponse)env.Crm.Execute(request);

					var entityMetadataList = result.EntityMetadata;

					foreach (var entityMetadata in entityMetadataList)
					{
						var entityId = entityMetadata.MetadataId.Value;
						idList.Remove(entityId);

						var leaf = group.OfType<SolutionComponentLeaf>().First(_ => _.ObjectId == entityId);
						leaf.SetLabelFromEntityMetadata(entityMetadata);
					}
				}
			}
		}

		private void FillFieldSecurityProfiles(SolutionComponentGrid grid, IReadOnlyCollection<ConnectionModel> connections)
		{
			using (this.log.Track("Fetching Field Security Profile definitions"))
			{
				var group = grid.FirstOrDefault(_ => _.ComponentTypeCode == (int)SolutionComponentType.FieldSecurityProfile);
				if (group == null) return;

				group.SetAnalyzed();

				var idList = group
					.OfType<SolutionComponentLeaf>()
					.Select(_ => _.ObjectId)
					.ToList();

				foreach (var env in connections)
				{
					if (idList.Count == 0) return;

					var query = new QueryExpression("fieldsecurityprofile");
					query.ColumnSet.AddColumns("name");
					query.Criteria.AddCondition("fieldsecurityprofileid", ConditionOperator.In, idList.Cast<object>().ToArray());
					query.NoLock = true;

					var result = env.Crm.RetrieveMultiple(query);

					var entityList = result.Entities;

					foreach (var entity in entityList)
					{
						var entityId = entity.Id;
						idList.Remove(entityId);

						var leaf = group.OfType<SolutionComponentLeaf>().First(_ => _.ObjectId == entityId);
						leaf.SetLabelFromFieldSecurityProfile(entity);
					}
				}
			}
		}

		private void FillPluginAssemblies(SolutionComponentGrid grid, IReadOnlyCollection<ConnectionModel> connections)
		{
			using (this.log.Track("Fetching Plugin Assembly definitions"))
			{
				var group = grid.FirstOrDefault(_ => _.ComponentTypeCode == (int)SolutionComponentType.PluginAssembly);
				if (group == null) return;

				group.SetAnalyzed();

				var idList = group
					.OfType<SolutionComponentLeaf>()
					.Select(_ => _.ObjectId)
					.ToList();

				foreach (var env in connections)
				{
					if (idList.Count == 0) return;

					var query = new QueryExpression("pluginassembly");
					query.ColumnSet.AddColumns("name");
					query.Criteria.AddCondition("pluginassemblyid", ConditionOperator.In, idList.Cast<object>().ToArray());
					query.NoLock = true;

					var result = env.Crm.RetrieveMultiple(query);

					var entityList = result.Entities;

					foreach (var entity in entityList)
					{
						var entityId = entity.Id;
						idList.Remove(entityId);

						var leaf = group.OfType<SolutionComponentLeaf>().First(_ => _.ObjectId == entityId);
						leaf.SetLabelFromPluginAssembly(entity);
					}
				}
			}
		}

		private void FillRibbonCustomizations(SolutionComponentGrid grid, IReadOnlyCollection<ConnectionModel> connections)
		{
			using (this.log.Track("Fetching RibbonCustomizations definitions"))
			{
				var group = grid.FirstOrDefault(_ => _.ComponentTypeCode == (int)SolutionComponentType.RibbonCustomization);
				if (group == null) return;

				group.SetAnalyzed();

				var idList = group
					.OfType<SolutionComponentLeaf>()
					.Select(_ => _.ObjectId)
					.ToList();

				foreach (var env in connections)
				{
					if (idList.Count == 0) return;

					var query = new QueryExpression("ribboncustomization");
					query.ColumnSet.AddColumns("entity");
					query.Criteria.AddCondition("ribboncustomizationid", ConditionOperator.In, idList.Cast<object>().ToArray());
					query.NoLock = true;

					var result = env.Crm.RetrieveMultiple(query);

					var entityList = result.Entities;

					foreach (var entity in entityList)
					{
						var entityId = entity.Id;
						idList.Remove(entityId);

						var leaf = group.OfType<SolutionComponentLeaf>().First(_ => _.ObjectId == entityId);
						leaf.SetLabelFromRibbonCustomization(entity);
					}
				}
			}
		}

		private void FillRoles(SolutionComponentGrid grid, IReadOnlyCollection<ConnectionModel> connections)
		{
			using (this.log.Track("Fetching Roles definitions"))
			{
				var group = grid.FirstOrDefault(_ => _.ComponentTypeCode == (int)SolutionComponentType.Role);
				if (group == null) return;

				group.SetAnalyzed();

				var idList = group
					.OfType<SolutionComponentLeaf>()
					.Select(_ => _.ObjectId)
					.ToList();

				foreach (var env in connections)
				{
					if (idList.Count == 0) return;

					var query = new QueryExpression("role");
					query.ColumnSet.AddColumns("name");
					query.Criteria.AddCondition("roleid", ConditionOperator.In, idList.Cast<object>().ToArray());
					query.NoLock = true;

					var result = env.Crm.RetrieveMultiple(query);

					var entityList = result.Entities;

					foreach (var entity in entityList)
					{
						var entityId = entity.Id;
						idList.Remove(entityId);

						var leaf = group.OfType<SolutionComponentLeaf>().First(_ => _.ObjectId == entityId);
						leaf.SetLabelFromRole(entity);
					}
				}
			}
		}

		private void FillSdkMessageProcessingSteps(SolutionComponentGrid grid, IReadOnlyCollection<ConnectionModel> connections)
		{
			using (this.log.Track("Fetching SDK message processing step definitions"))
			{
				var group = grid.FirstOrDefault(_ => _.ComponentTypeCode == (int)SolutionComponentType.SDKMessageProcessingStep);
				if (group == null) return;

				group.SetAnalyzed();

				var idList = group
					.OfType<SolutionComponentLeaf>()
					.Select(_ => _.ObjectId)
					.ToList();

				foreach (var env in connections)
				{
					if (idList.Count == 0) return;

					var query = new QueryExpression("sdkmessageprocessingstep");
					query.ColumnSet.AddColumns("sdkmessageprocessingstepid", "name");
					query.Criteria.AddCondition("sdkmessageprocessingstepid", ConditionOperator.In, idList.Cast<object>().ToArray());
					query.NoLock = true;

					var result = env.Crm.RetrieveMultiple(query);

					var entityList = result.Entities;

					foreach (var entity in entityList)
					{
						var entityId = entity.Id;
						idList.Remove(entityId);

						var leaf = group.OfType<SolutionComponentLeaf>().First(_ => _.ObjectId == entityId);
						leaf.SetLabelFromSdkMessageProcessingStep(entity);
					}
				}
			}
		}

		private void FillSavedQuery(SolutionComponentGrid grid, IReadOnlyCollection<ConnectionModel> connections)
		{
			using (this.log.Track("Fetching SavedQuery definitions"))
			{
				var group = grid.FirstOrDefault(_ => _.ComponentTypeCode == (int)SolutionComponentType.SavedQuery);
				if (group == null) return;

				group.SetAnalyzed();

				var idList = group
					.OfType<SolutionComponentLeaf>()
					.Select(_ => _.ObjectId)
					.ToList();

				foreach (var env in connections)
				{
					if (idList.Count == 0) return;

					var query = new QueryExpression("savedquery");
					query.ColumnSet.AddColumns("name", "returnedtypecode");
					query.Criteria.AddCondition("savedqueryid", ConditionOperator.In, idList.Cast<object>().ToArray());
					query.NoLock = true;

					var result = env.Crm.RetrieveMultiple(query);

					var entityList = result.Entities;

					foreach (var entity in entityList)
					{
						var entityId = entity.Id;
						idList.Remove(entityId);

						var leaf = group.OfType<SolutionComponentLeaf>().First(_ => _.ObjectId == entityId);
						leaf.SetLabelFromSavedQuery(entity);
					}
				}
			}
		}

		private void FillSystemForms(SolutionComponentGrid grid, IReadOnlyCollection<ConnectionModel> connections)
		{
			using (this.log.Track("Fetching SystemForm definitions"))
			{
				var group = grid.FirstOrDefault(_ => _.ComponentTypeCode == (int)SolutionComponentType.SystemForm);
				if (group == null) return;

				group.SetAnalyzed();

				var idList = group
					.OfType<SolutionComponentLeaf>()
					.Select(_ => _.ObjectId)
					.ToList();

				foreach (var env in connections)
				{
					if (idList.Count == 0) return;

					var query = new QueryExpression("systemform");
					query.ColumnSet.AddColumns("objecttypecode", "type", "name", "description");
					query.Criteria.AddCondition("formid", ConditionOperator.In, idList.Cast<object>().ToArray());
					query.NoLock = true;

					var result = env.Crm.RetrieveMultiple(query);

					var entityList = result.Entities;

					foreach (var entity in entityList)
					{
						var entityId = entity.Id;
						idList.Remove(entityId);

						var leaf = group.OfType<SolutionComponentLeaf>().First(_ => _.ObjectId == entityId);
						leaf.SetLabelFromSystemForm(entity);
					}
				}
			}
		}

		private void FillWebResources(SolutionComponentGrid grid, IReadOnlyCollection<ConnectionModel> connections)
		{
			using (this.log.Track("Fetching webresource definitions"))
			{
				var group = grid.FirstOrDefault(_ => _.ComponentTypeCode == (int)SolutionComponentType.WebResource);
				if (group == null) return;

				group.SetAnalyzed();

				var idList = group
					.OfType<SolutionComponentLeaf>()
					.Select(_ => _.ObjectId)
					.ToList();

				foreach (var env in connections)
				{
					if (idList.Count == 0) return;

					var query = new QueryExpression("webresource");
					query.ColumnSet.AddColumns("displayname", "name");
					query.Criteria.AddCondition("webresourceid", ConditionOperator.In, idList.Cast<object>().ToArray());
					query.NoLock = true;

					var result = env.Crm.RetrieveMultiple(query);

					var entityList = result.Entities;

					foreach (var entity in entityList)
					{
						var entityId = entity.Id;
						idList.Remove(entityId);

						var leaf = group.OfType<SolutionComponentLeaf>().First(_ => _.ObjectId == entityId);
						leaf.SetLabelFromWebResource(entity);
					}
				}
			}
		}

		private void FillWorkflows(SolutionComponentGrid grid, IReadOnlyCollection<ConnectionModel> connections)
		{
			using (this.log.Track("Fetching workflow definitions"))
			{
				var group = grid.FirstOrDefault(_ => _.ComponentTypeCode == (int)SolutionComponentType.Workflow);
				if (group == null) return;

				group.SetAnalyzed();

				var idList = group
					.OfType<SolutionComponentLeaf>()
					.Select(_ => _.ObjectId)
					.ToList();

				foreach (var env in connections)
				{
					if (idList.Count == 0) return;

					var query = new QueryExpression("workflow");
					query.ColumnSet.AddColumns("uniquename", "name");
					query.Criteria.AddCondition("workflowid", ConditionOperator.In, idList.Cast<object>().ToArray());
					query.NoLock = true;

					var result = env.Crm.RetrieveMultiple(query);

					var entityList = result.Entities;

					foreach (var entity in entityList)
					{
						var entityId = entity.Id;
						idList.Remove(entityId);

						var leaf = group.OfType<SolutionComponentLeaf>().First(_ => _.ObjectId == entityId);
						leaf.SetLabelFromWorkflow(entity);
					}
				}
			}
		}
	}
}
