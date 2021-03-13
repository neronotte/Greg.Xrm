using Greg.Xrm.Logging;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution
{
	public class ResolverForEntityRelationships : IResolver
	{
		private readonly ILog log;

		public ResolverForEntityRelationships(ILog log)
		{
			this.log = log ?? throw new System.ArgumentNullException(nameof(log));
		}
		public void Resolve(SolutionComponentGrid grid, IReadOnlyCollection<ConnectionModel> connections)
		{
			using (log.Track("Fetching entity relationships"))
			{

				var relationshipGroup = grid.FirstOrDefault(_ => _.ComponentTypeCode == (int)SolutionComponentType.EntityRelationship);
				if (relationshipGroup == null) return;

				relationshipGroup.SetAnalyzed();


				var relationshipIdList = relationshipGroup
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
						},
						RelationshipQuery = new RelationshipQueryExpression
						{
							Criteria = new MetadataFilterExpression
							{
								Conditions = {
									new MetadataConditionExpression ("MetadataId", MetadataConditionOperator.In, relationshipIdList.ToArray())
								}
							}
						}
					};

					var request = new RetrieveMetadataChangesRequest
					{
						Query = query
					};

					var result = (RetrieveMetadataChangesResponse)env.Crm.Execute(request);

					var entityMetadataList = result.EntityMetadata;

					foreach (var entityMetadata in entityMetadataList)
					{

						foreach (var relationshipMetadata in entityMetadata.OneToManyRelationships)
						{
							var relationshipId = relationshipMetadata.MetadataId.Value;
							relationshipIdList.Remove(relationshipId);

							var leaf = relationshipGroup.OfType<SolutionComponentLeaf>().FirstOrDefault(_ => _.ObjectId == relationshipId);
							leaf?.SetLabelFromRelationship(relationshipMetadata);
						}


						foreach (var relationshipMetadata in entityMetadata.ManyToOneRelationships)
						{
							var relationshipId = relationshipMetadata.MetadataId.Value;
							relationshipIdList.Remove(relationshipId);

							var leaf = relationshipGroup.OfType<SolutionComponentLeaf>().FirstOrDefault(_ => _.ObjectId == relationshipId);
							leaf?.SetLabelFromRelationship(relationshipMetadata);
						}


						foreach (var relationshipMetadata in entityMetadata.ManyToManyRelationships)
						{
							var relationshipId = relationshipMetadata.MetadataId.Value;
							relationshipIdList.Remove(relationshipId);

							var leaf = relationshipGroup.OfType<SolutionComponentLeaf>().FirstOrDefault(_ => _.ObjectId == relationshipId);
							leaf?.SetLabelFromRelationship(relationshipMetadata);
						}
					}
				}
			}
		}
	}
}
