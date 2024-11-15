using Greg.Xrm.Core;
using Greg.Xrm.Logging;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Services.ComponentResolvers
{
	public class ComponentResolverFactory : IComponentResolverFactory
	{
		private readonly IOrganizationService crm;
		private readonly ILog log;

		public ComponentResolverFactory(IOrganizationService crm, ILog log)
		{
			this.crm = crm;
			this.log = log;


			this.AddStrategy(ComponentType.SavedQuery);
			this.AddStrategy(ComponentType.AppModule);
			this.AddStrategy(ComponentType.CustomControl);
			this.AddStrategy(ComponentType.DisplayString, () => ByQuery("displaystring", "displaystringkey"));
			this.AddStrategy(ComponentType.EmailTemplate, () => ByQuery("template", "title"));
			this.AddStrategy(ComponentType.FieldSecurityProfile);
			this.AddStrategy(ComponentType.PluginAssembly);
			this.AddStrategy(ComponentType.RibbonCustomization, () => ByQuery("ribboncustomization", "entity"));
			this.AddStrategy(ComponentType.Role);
			this.AddStrategy(ComponentType.SDKMessageProcessingStep);
			this.AddStrategy(ComponentType.SystemForm, () => new ResolverForSystemForms(crm, log));
			this.AddStrategy(ComponentType.WebResource);
			this.AddStrategy(ComponentType.Workflow, () => ByQuery("workflow", "uniquename"));
			this.AddStrategy(ComponentType.EntityRelationship, () => ByQuery("entityrelationship", "schemaname"));
			this.AddStrategy(ComponentType.SavedQueryVisualization);

		}

		#region Strategy Management

		private readonly Dictionary<int, Func<IComponentResolver>> resolverStrategyCache = new Dictionary<int, Func<IComponentResolver>>();
		private void AddStrategy(ComponentType componentType)
		{
			var tableName = Enum.GetName(typeof(ComponentType), componentType)?.ToLowerInvariant();
			if (string.IsNullOrWhiteSpace(tableName))
				throw new InvalidOperationException($"Invalid enum value {componentType}");

			AddStrategy(componentType, () => ByQuery(tableName));
		}

		private void AddStrategy(ComponentType componentType, Func<IComponentResolver> factory)
		{
			resolverStrategyCache[(int)componentType] = factory;
		}

		private IComponentResolver ByQuery(string table, string nameColumn = "name", string tableIdColumn = null)
		{
			return new ResolverByQuery(crm, log, table, nameColumn, tableIdColumn);
		}


		#endregion



		public IComponentResolver GetComponentResolverFor(ComponentType componentType)
		{
			return GetComponentResolverFor((int)componentType);
		}



		public IComponentResolver GetComponentResolverFor(int componentType)
		{
			if (!resolverStrategyCache.TryGetValue(componentType, out var factoryMethod))
			{
				return null;
			}


			return factoryMethod();
		}
	}
}
