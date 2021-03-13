using Greg.Xrm.Logging;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution
{
	public class ResolverForEntities : IResolver
	{
		private readonly ILog log;

		public ResolverForEntities(ILog log)
		{
			this.log = log ?? throw new System.ArgumentNullException(nameof(log));
		}


		public void Resolve(SolutionComponentGrid grid, IReadOnlyCollection<ConnectionModel> connections)
		{
			using (this.log.Track("Fetching entity definitions"))
			{
				var entityGroup = grid.FirstOrDefault(_ => _.ComponentTypeCode == (int)SolutionComponentType.Entity);
				if (entityGroup == null) return;

				entityGroup.SetAnalyzed();


				var attributeGroup = grid.FirstOrDefault(_ => _.ComponentTypeCode == (int)SolutionComponentType.Attribute);
				if (attributeGroup != null)
				{
					attributeGroup.SetAnalyzed();
				}



				var entityIdList = entityGroup
					.OfType<SolutionComponentLeaf>()
					.Select(_ => _.ObjectId)
					.ToList();

				var attributeIdList = attributeGroup?
					.OfType<SolutionComponentLeaf>()
					.Select(_ => _.ObjectId)
					.ToList() ?? new List<Guid>();

				foreach (var env in connections)
				{
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

						var leaf = entityGroup.OfType<SolutionComponentLeaf>().First(_ => _.ObjectId == entityId);
						leaf.SetLabelFromEntityMetadata(entityMetadata);

						if (attributeGroup != null)
						{
							foreach (var attributeMetadata in entityMetadata.Attributes)
							{
								var attributeId = attributeMetadata.MetadataId.Value;
								attributeIdList.Remove(attributeId);

								leaf = attributeGroup.OfType<SolutionComponentLeaf>().FirstOrDefault(_ => _.ObjectId == attributeId);
								leaf?.SetLabelFromAttributeMetadata(entityMetadata, attributeMetadata);
							}
						}
					}
				}
			}
		}
	}
}
