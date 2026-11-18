using Greg.Xrm.Logging;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Metadata.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution2
{
	public class ResolverForEntitiesAndAttributes : IComponentResolver
	{
		private readonly ILog log;

		public ResolverForEntitiesAndAttributes(ILog log)
		{
			this.log = log;
		}

		public void Resolve(IReadOnlyCollection<SolutionComponent> componentList, ConnectionModel env)
		{
			var entityGroup = componentList.Where(_ => _.componenttype.Value == (int)SolutionComponentType.Entity).ToList();
			if (entityGroup.Count == 0) return;

			using (this.log.Track("Fetching entity definitions"))
			{
				var attributeGroup = componentList.Where(_ => _.componenttype.Value == (int)SolutionComponentType.Attribute).ToList();
				



				var entityIdList = entityGroup
					.Select(_ => _.objectid)
					.ToList();

				var attributeIdList = attributeGroup?
					.Select(_ => _.objectid)
					.ToList() ?? new List<Guid>();

				var query = new EntityQueryExpression
				{
					AttributeQuery = new AttributeQueryExpression
					{
						Properties = new MetadataPropertiesExpression("MetadataId", "SchemaName", "LogicalName", "DisplayName")
					},
					Properties = new MetadataPropertiesExpression
					{
						AllProperties = true
					}
				};
				query.Criteria.Conditions.Add(new MetadataConditionExpression("MetadataId", MetadataConditionOperator.In, entityIdList.ToArray()));

				var request = new RetrieveMetadataChangesRequest
				{
					Query = query
				};

				var result = (RetrieveMetadataChangesResponse)env.Crm.Execute(request);

				var entityMetadataList = result.EntityMetadata;

				foreach (var entityMetadata in entityMetadataList)
				{
					var entityId = entityMetadata.MetadataId.Value;
					entityIdList.Remove(entityId);

					var solutionComponent = entityGroup.First(_ => _.objectid == entityId);
					SetLabelFromEntityMetadata(solutionComponent, entityMetadata);

					if (attributeGroup != null)
					{
						foreach (var attributeMetadata in entityMetadata.Attributes)
						{
							var attributeId = attributeMetadata.MetadataId.Value;
							attributeIdList.Remove(attributeId);

							solutionComponent = attributeGroup.FirstOrDefault(_ => _.objectid == attributeId);
							SetLabelFromAttributeMetadata(solutionComponent, entityMetadata, attributeMetadata);
						}
					}
				}
			}

		}

		private static void SetLabelFromEntityMetadata(SolutionComponent solutionComponent, EntityMetadata entityMetadata)
		{
			solutionComponent.Label = $"{entityMetadata.DisplayName?.UserLocalizedLabel?.Label} ({entityMetadata.LogicalName})"
				.Replace("()", string.Empty)
				.Trim();
		}
		private static void SetLabelFromAttributeMetadata(SolutionComponent solutionComponent, EntityMetadata entityMetadata, AttributeMetadata attributeMetadata)
		{
			if (solutionComponent == null) return;
			solutionComponent.Label = $"{entityMetadata.LogicalName}.{attributeMetadata.LogicalName}";
		}
	}
}
