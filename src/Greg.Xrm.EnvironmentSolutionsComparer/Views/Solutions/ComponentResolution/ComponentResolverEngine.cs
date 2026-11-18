using Greg.Xrm.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution2
{
	public class ComponentResolverEngine : IComponentResolverEngine
	{
		private readonly ILog log;
		private readonly Dictionary<int, Func<IComponentResolver>> resolverStrategyCache = new Dictionary<int, Func<IComponentResolver>>();


		public void ResolveAll(IReadOnlyCollection<SolutionComponent> componentList, ConnectionModel env)
		{
			var groups = componentList
				.GroupBy(_ => _.componenttype.Value)
				.OrderBy(x => x.Key)
				.ToList();

			foreach (var group in groups)
			{
				using (this.log.Track($"Fetching {group.Key} definitions"))
				{
					if (this.resolverStrategyCache.TryGetValue(group.Key, out var resolverFactory))
					{
						var resolver = resolverFactory();
						resolver.Resolve(group.ToArray(), env);
						continue;
					}

					if (group.Key == (int)SolutionComponentType.Entity || group.Key == (int)SolutionComponentType.Attribute)
					{
						// this is handled at the end of the foreach
						continue;
					}

					var sampleComponent = group.First();

					if (string.IsNullOrWhiteSpace(sampleComponent.ComponentTypeName) && !string.IsNullOrEmpty(sampleComponent.SolutionComponentDefinitionPrimaryEntityName))
					{
						var resolver = ByQuery(sampleComponent.SolutionComponentDefinitionPrimaryEntityName, null);
						resolver.Resolve(group.ToArray(), env);
					}
				}
			}

			var entityAndAttributeResolver = new ResolverForEntitiesAndAttributes(log);
			entityAndAttributeResolver.Resolve(componentList, env);
		}






		public ComponentResolverEngine(ILog log)
		{
			this.log = log ?? throw new System.ArgumentNullException(nameof(log));
			// add here a component resolver for each value of the SolutionComponentType enum, sorted by the enum value


#pragma warning disable S125 // Sections of code should not be commented out
			//this.resolverStrategyCache.Add((int)SolutionComponentType.Entity);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.Attribute);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.Relationship);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.AttributePicklistValue);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.AttributeLookupValue);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.ViewAttribute);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.LocalizedLabel);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.RelationshipExtraCondition);
			this.AddStrategy(SolutionComponentType.OptionSet, () => new ResolverForGlobalOptionSets(log));
			this.AddStrategy(SolutionComponentType.EntityRelationship, () => ByQuery("entityrelationship", "schemaname"));
			//this.resolverStrategyCache.Add((int)SolutionComponentType.EntityRelationshipRole);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.EntityRelationshipRelationships, null);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.ManagedProperty);
			this.AddStrategy(SolutionComponentType.EntityKey);
			this.AddStrategy(SolutionComponentType.Privilege);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.PrivilegeObjectTypeCode);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.Index);
			this.AddStrategy(SolutionComponentType.Role);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.RolePrivilege);
			this.AddStrategy(SolutionComponentType.DisplayString, () => ByQuery("displaystring", "displaystringkey"));
			//this.resolverStrategyCache.Add((int)SolutionComponentType.DisplayStringMap);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.Form);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.Organization);
			this.AddStrategy(SolutionComponentType.SavedQuery);
			this.AddStrategy(SolutionComponentType.Workflow, () => new ResolverForWorkflows(log));
			//this.resolverStrategyCache.Add((int)SolutionComponentType.Report);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.ReportEntity);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.ReportCategory);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.ReportVisibility);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.Attachment);
			this.AddStrategy(SolutionComponentType.EmailTemplate, () => ByQuery("template", "title"));
			//this.resolverStrategyCache.Add((int)SolutionComponentType.ContractTemplate);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.KBArticleTemplate);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.MailMergeTemplate);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.DuplicateRule);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.DuplicateRuleCondition);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.EntityMap);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.AttributeMap);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.RibbonCommand);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.RibbonContextGroup);
			this.AddStrategy(SolutionComponentType.RibbonCustomization, () => ByQuery("ribboncustomization", "entity"));
			//this.resolverStrategyCache.Add((int)SolutionComponentType.RibbonRule);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.RibbonTabToCommandMap);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.RibbonDiff);
			this.AddStrategy(SolutionComponentType.SavedQueryVisualization);
			this.AddStrategy(SolutionComponentType.SystemForm, () => new ResolverForSystemForms(log));
			this.AddStrategy(SolutionComponentType.WebResource);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.SiteMap);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.ConnectionRole);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.ComplexControl);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.HierarchyRule);
			this.AddStrategy(SolutionComponentType.CustomControl);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.CustomControlDefaultConfig);
			this.AddStrategy(SolutionComponentType.FieldSecurityProfile);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.FieldPermission);
			this.AddStrategy(SolutionComponentType.AppModule);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.PluginType);
			this.AddStrategy(SolutionComponentType.PluginAssembly);
			this.AddStrategy(SolutionComponentType.SDKMessageProcessingStep);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.SDKMessageProcessingStepImage, null);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.ServiceEndpoint);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.RoutingRule);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.RoutingRuleItem);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.SLA);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.SLAItem);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.ConvertRule);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.ConvertRuleItem);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.MobileOfflineProfile);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.MobileOfflineProfileItem);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.SimilarityRule);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.DataSourceMapping);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.SDKMessage);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.SDKMessageFilter);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.SdkMessagePair);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.SdkMessageRequest);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.SdkMessageRequestField);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.SdkMessageResponse);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.SdkMessageResponseField);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.WebWizard);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.ImportMap);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.CanvasApp);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.Connector);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.Connector2);
			this.AddStrategy(SolutionComponentType.EnvironmentVariableDefinition, () => ByQuery("environmentvariabledefinition", "schemaname", "environmentvariabledefinitionid"));
			this.AddStrategy(SolutionComponentType.EnvironmentVariableValue, () => ByQuery("environmentvariablevalue", "schemaname", "environmentvariablevalueid"));
			//this.resolverStrategyCache.Add((int)SolutionComponentType.AIProjectType);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.AIProject);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.AIConfiguration);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.EntityAnalyticsConfiguration, null);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.AttributeImageConfiguration, null);
			//this.resolverStrategyCache.Add((int)SolutionComponentType.EntityImageConfiguration);
#pragma warning restore S125 // Sections of code should not be commented out

		}

		private void AddStrategy(SolutionComponentType componentType)
		{
			var tableName = Enum.GetName(typeof(SolutionComponentType), componentType)?.ToLowerInvariant();
			if (string.IsNullOrWhiteSpace(tableName))
				throw new InvalidOperationException($"Invalid enum value {componentType}");

			AddStrategy(componentType, () => ByQuery(tableName));
		}

		private void AddStrategy(SolutionComponentType componentType, Func<IComponentResolver> factory)
		{
			this.resolverStrategyCache[(int)componentType] = factory;
		}

		private IComponentResolver ByQuery(string table, string nameColumn = "name", string tableIdColumn = null)
		{
			return new ResolverByQuery(this.log, table, nameColumn, tableIdColumn);
		}
	}
}
