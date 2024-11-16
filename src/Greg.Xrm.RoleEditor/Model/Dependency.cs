using Greg.Xrm.Core;
using Greg.Xrm.Logging;
using Greg.Xrm.Model;
using Greg.Xrm.RoleEditor.Services.ComponentResolvers;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Model
{
	public class Dependency : EntityWrapper
	{
		private Dependency(Entity entity) : base(entity)
		{
			DependentComponentLabel = $"{dependentcomponentobjectid} ({DependentComponentTypeFormatted})";
		}

#pragma warning disable IDE1006 // Naming Styles
		public Guid requiredcomponentbasesolutionid => Get<Guid>();
		public Guid requiredcomponentobjectid => Get<Guid>();

		public OptionSetValue dependencytype => Get<OptionSetValue>();
		public Guid requiredcomponentparentid => Get<Guid>();
		public OptionSetValue requiredcomponenttype => Get<OptionSetValue>();

		public EntityReference requiredcomponentnodeid => Get<EntityReference>();
		public OptionSetValue dependentcomponenttype => Get<OptionSetValue>();
		public Guid dependentcomponentparentid => Get<Guid>();
		public Guid dependentcomponentbasesolutionid => Get<Guid>();
		public EntityReference dependentcomponentnodeid => Get<EntityReference>();
		public Guid dependentcomponentobjectid => Get<Guid>();
#pragma warning restore IDE1006 // Naming Styles


		public string DependencyTypeFormatted => GetFormatted(nameof(dependencytype));
		public string RequiredComponentTypeFormatted => GetFormatted(nameof(requiredcomponenttype));
		public string DependentComponentTypeFormatted => GetDependentComponentTypeFormatted();


		private string GetDependentComponentTypeFormatted()
		{
			var text = GetFormatted(nameof(dependentcomponenttype));
			if (string.IsNullOrWhiteSpace(text) && this.dependentcomponenttype?.Value == (int)ComponentType.AppModule)
			{
				text = "App Module";
			}
			return text;
		}

		public string DependentComponentLabel { get; protected set; }


		public override string ToString()
		{
			return $"{DependentComponentTypeFormatted}: {DependentComponentLabel} --- depends on --> {RequiredComponentTypeFormatted}: {requiredcomponentobjectid}";
		}


		public class Repository : IDependencyRepository
		{
			private readonly ILog log;

			public Repository(ILog log)
			{
				this.log = log;
			}

			public DependencyList GetRoleDependencies(IOrganizationService crm, Role role, bool? forDelete = false)
			{
				EntityCollection entities;

				if (forDelete.GetValueOrDefault())
				{
					var request = new RetrieveDependenciesForDeleteRequest
					{
						ComponentType = (int)ComponentType.Role,
						ObjectId = role.Id
					};
					var response = (RetrieveDependenciesForDeleteResponse)crm.Execute(request);
					entities = response.EntityCollection;
				}
				else
				{
					var request = new RetrieveDependentComponentsRequest
					{
						ComponentType = (int)ComponentType.Role,
						ObjectId = role.Id
					};
					var response = (RetrieveDependentComponentsResponse)crm.Execute(request);
					entities = response.EntityCollection;
				}

				var dependencies = entities.Entities
					.Select(x => new Dependency(x))
					.OrderBy(x => x.DependentComponentTypeFormatted)
					.ThenBy(x => x.dependentcomponentobjectid)
					.ToArray();

				var resolverFactory = new ComponentResolverFactory(crm, log);

				ResolveDependencyNames(dependencies, resolverFactory);

				return new DependencyList(dependencies);
			}

			private static void ResolveDependencyNames(Dependency[] dependencies, ComponentResolverFactory resolverFactory)
			{
				var dependencyGroups = dependencies.GroupBy(x => x.dependentcomponenttype.Value)
				.OrderBy(x => x.First().DependentComponentTypeFormatted)
				.ToArray();

				foreach (var dependencyGroup in dependencyGroups)
				{
					var componentType = dependencyGroup.Key;
					var resolver = resolverFactory.GetComponentResolverFor(componentType);

					if (resolver != null)
					{
						var componentIds = dependencyGroup.Select(x => x.dependentcomponentobjectid).ToArray();
						var componentNames = resolver.GetNames(componentIds);

						foreach (var dependency in dependencyGroup.OrderBy(x => x.dependentcomponentobjectid))
						{
							var componentName = componentNames[dependency.dependentcomponentobjectid];
							dependency.DependentComponentLabel = componentName;
						}
					}
				}
			}
		}
	}
}