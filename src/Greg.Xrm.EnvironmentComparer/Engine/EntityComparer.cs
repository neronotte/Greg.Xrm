using Greg.Xrm.EnvironmentComparer.Engine.Config;
using Greg.Xrm.EnvironmentComparer.Engine.Memento;
using Greg.Xrm.Logging;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Greg.Xrm.EnvironmentComparer.Engine
{
	public class EntityComparer
	{

		private readonly Action<QueryExpression> applyAdditionalFilters;
		private readonly GenericCollectionComparer<Entity> comparer;
		private readonly IReadOnlyCollection<LookupMappingMemento> lookupMappings;

		public EntityComparer(
			string entityName,
			IKeyProvider<Entity> keyProvider,
			bool onlyActiveRecords)
			: this(entityName, keyProvider, Skip.Nothing, onlyActiveRecords)
		{
		}


		public EntityComparer(
			string entityName,
			IKeyProvider<Entity> keyProvider,
			ISkipAttributeCriteria skipAttributeCriteria = null,
			bool onlyActiveRecords = false)
			: this(entityName, keyProvider, skipAttributeCriteria, onlyActiveRecords, null)
		{
		}


		public EntityComparer(
			string entityName,
			IKeyProvider<Entity> keyProvider,
			ISkipAttributeCriteria skipAttributeCriteria,
			bool onlyActiveRecords,
			string fetchXml,
			IReadOnlyCollection<LookupMappingMemento> lookupMappings = null)
			: this(entityName, keyProvider, GetDefaultFilter(onlyActiveRecords), skipAttributeCriteria)
		{
			this.OnlyActiveRecords = onlyActiveRecords;
			this.FetchXml = string.IsNullOrWhiteSpace(fetchXml) ? null : fetchXml;
			this.lookupMappings = lookupMappings ?? Array.Empty<LookupMappingMemento>();
		}


		private static Action<QueryExpression> GetDefaultFilter(bool onlyActiveRecords)
		{
			if (onlyActiveRecords)
			{
				return q =>
				{
					q.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
				};
			}
			else
			{
				return q => { };
			}
		}

		public EntityComparer(
			string entityName,
			IKeyProvider<Entity> keyProvider,
			Action<QueryExpression> filterCriteria = null,
			ISkipAttributeCriteria skipAttributeCriteria = null)
		{
			this.comparer = new GenericCollectionComparer<Entity>(keyProvider, new EntityEqualityComparer(skipAttributeCriteria), entityName);
			this.EntityName = entityName;
			this.KeyProvider = keyProvider;
			this.applyAdditionalFilters = filterCriteria ?? (q => { });
			this.SkipAttributeCriteria = skipAttributeCriteria;
			this.lookupMappings = Array.Empty<LookupMappingMemento>();
		}





		public string EntityName { get; }
		public ISkipAttributeCriteria SkipAttributeCriteria { get; }
		public IKeyProvider<Entity> KeyProvider { get; }
		public bool OnlyActiveRecords { get; }
		public string FetchXml { get; }

		public IReadOnlyCollection<ObjectComparison<Entity>> Compare(List<Entity> list1, List<Entity> list2)
		{
			return this.comparer.Compare(list1, list2);
		}

		public List<Entity> GetEntitiesFrom(IOrganizationService crm, ILog log, string label)
		{
			var result = new List<Entity>();
			using (log.Track($"Getting entities of type {this.EntityName} from {label}"))
			{
				try
				{
					var query = CreateQuery(crm);
					query.ColumnSet.AllColumns = true;
					query.NoLock = true;
					query.PageInfo = new PagingInfo
					{
						PageNumber = 1
					};
					this.applyAdditionalFilters(query);

					int queryNumber = 0;
					EntityCollection entities;
					do
					{
						queryNumber++;
						log.Debug($"Query {queryNumber} on entity {this.EntityName} on environment {label}");

						entities = crm.RetrieveMultiple(query);

						if (entities.MoreRecords)
						{
							query.PageInfo.PageNumber++;
							query.PageInfo.PagingCookie = entities.PagingCookie;
						}

						result.AddRange(entities.Entities);
					}
					while (entities.MoreRecords);

					log.Debug($"Found {result.Count} records on entity {this.EntityName} on environment {label}");
				}
				catch (FaultException<OrganizationServiceFault> ex)
				{
					log.Error($"Error while querying entity {this.EntityName} on {label}: {ex.Message}", ex);

					if (ex.Message.Contains(Constants.WasNotFoundInTheMetadataCache))
					{
						throw new EntityNotFoundException(this.EntityName);
					}

					return result;
				}
			}

			using (log.Track("Pruning unused attributes"))
			{
				foreach (var entity in result)
				{
					foreach (var attribute in Constants.AttributesToIgnore)
					{
						entity.Attributes.Remove(attribute);
					}

					foreach (var attributeName in entity.Attributes.Keys.ToArray())
					{
						if (!this.SkipAttributeCriteria.ShouldSkip(attributeName)) continue;

						entity.Attributes.Remove(attributeName);
					}
				}
			}

			return result;
		}


		private QueryExpression CreateQuery(IOrganizationService crm)
		{
			if (string.IsNullOrWhiteSpace(this.FetchXml))
			{
				return new QueryExpression(this.EntityName);
			}

			var response = (FetchXmlToQueryExpressionResponse)crm.Execute(new FetchXmlToQueryExpressionRequest
			{
				FetchXml = this.FetchXml
			});

			var query = response.Query;
			query.ColumnSet = new ColumnSet(true);

			// the FetchXML is used only to filter the records: columns from linked entities must not be compared
			ClearColumns(query.LinkEntities);

			// wraps the FetchXML criteria, so that the additional filters (e.g. only active records) are always in AND
			var criteria = new FilterExpression(LogicalOperator.And);
			criteria.AddFilter(query.Criteria);
			query.Criteria = criteria;

			return query;
		}

		private static void ClearColumns(IEnumerable<LinkEntity> linkEntities)
		{
			foreach (var linkEntity in linkEntities)
			{
				linkEntity.Columns = new ColumnSet(false);
				ClearColumns(linkEntity.LinkEntities);
			}
		}

		public void MapLookupReferences(
			IOrganizationService crm1,
			IReadOnlyCollection<Entity> entities1,
			IOrganizationService crm2,
			IReadOnlyCollection<Entity> entities2)
		{
			if (this.lookupMappings.Count == 0) return;

			var mapper1 = new LookupReferenceMapper(crm1, "CRM1", this.lookupMappings);
			var mapper2 = new LookupReferenceMapper(crm2, "CRM2", this.lookupMappings);
			mapper1.ValidateCompatibleWith(mapper2);
			mapper1.Apply(entities1);
			mapper2.Apply(entities2);
		}


		public EntityMemento ToEntityMemento()
		{
			var entityMemento = new EntityMemento
			{
				EntityName = this.EntityName,
				KeyUseGuid = this.KeyProvider == AsKey.UseGuid
			};

			if (this.KeyProvider is KeyProviderByAttributeSet kp)
			{
				entityMemento.KeyAttributeNames = kp.AttributeList.ToList();
			}


			if (this.SkipAttributeCriteria is SkipAttributes sa)
			{
				entityMemento.AttributesToSkip = sa.AttributesToSkip.ToList();
			}

			entityMemento.OnlyActive = this.OnlyActiveRecords;
			entityMemento.FetchXml = this.FetchXml;
			entityMemento.LookupMappings = this.lookupMappings
				.Select(x => (LookupMappingMemento)x.Clone())
				.ToList();
			return entityMemento;
		}
	}

	internal class LookupReferenceMapper
	{
		private const int BatchSize = 200;
		internal const string DestinationRootBusinessUnitSentinel = "$destination-root-businessunit";

		private readonly IOrganizationService crm;
		private readonly string environmentName;
		private readonly IReadOnlyCollection<LookupMappingMemento> mappings;
		private readonly Dictionary<string, EntityMetadata> entityMetadataCache = new Dictionary<string, EntityMetadata>(StringComparer.OrdinalIgnoreCase);
		private readonly Dictionary<string, Dictionary<Guid, Entity>> recordCache = new Dictionary<string, Dictionary<Guid, Entity>>(StringComparer.OrdinalIgnoreCase);

		public LookupReferenceMapper(IOrganizationService crm, string environmentName, IReadOnlyCollection<LookupMappingMemento> mappings)
		{
			this.crm = crm ?? throw new ArgumentNullException(nameof(crm));
			this.environmentName = environmentName;
			this.mappings = mappings ?? throw new ArgumentNullException(nameof(mappings));
		}

		public void ValidateCompatibleWith(LookupReferenceMapper other)
		{
			if (other == null) throw new ArgumentNullException(nameof(other));

			foreach (var mapping in this.mappings)
			{
				if (mapping.UseDestinationRootBusinessUnit) continue;

				var attribute1 = GetMatchAttribute(mapping);
				var attribute2 = other.GetMatchAttribute(mapping);
				if (attribute1.AttributeType != attribute2.AttributeType)
				{
					throw new ComparisonException($"Lookup matching field <{mapping.MatchAttributeName}> on table <{mapping.TargetEntityName}> has a different type in CRM1 and CRM2.");
				}
			}
		}

		public void Apply(IReadOnlyCollection<Entity> entities)
		{
			if (entities == null) throw new ArgumentNullException(nameof(entities));
			if (this.mappings.Count == 0) return;

			foreach (var mapping in this.mappings)
			{
				var references = entities
					.Where(x => x.Contains(mapping.LookupAttributeName))
					.Select(x => x[mapping.LookupAttributeName] as EntityReference)
					.Where(x => x != null && string.Equals(x.LogicalName, mapping.TargetEntityName, StringComparison.OrdinalIgnoreCase))
					.ToList();

				if (references.Count == 0) continue;

				if (mapping.UseDestinationRootBusinessUnit)
				{
					foreach (var reference in references)
					{
						var keyAttributes = new KeyAttributeCollection();
						keyAttributes[DestinationRootBusinessUnitSentinel] = DestinationRootBusinessUnitSentinel;
						reference.Id = Guid.Empty;
						reference.KeyAttributes = keyAttributes;
						reference.Name = null;
					}
					continue;
				}

				var metadata = GetEntityMetadata(mapping.TargetEntityName);
				var attribute = GetMatchAttribute(mapping);
				var recordsById = GetCachedRecords(metadata, attribute, references.Select(x => x.Id).Distinct());
				foreach (var reference in references)
				{
					if (!recordsById.TryGetValue(reference.Id, out Entity record))
					{
						throw new ComparisonException($"Record <{reference.LogicalName}:{reference.Id}> required by lookup <{mapping.LookupAttributeName}> was not found in {this.environmentName}.");
					}
					if (!record.Contains(attribute.LogicalName) || record[attribute.LogicalName] == null)
					{
						throw new ComparisonException($"Record <{reference.LogicalName}:{reference.Id}> has no value for lookup matching field <{attribute.LogicalName}> in {this.environmentName}.");
					}

					var keyAttributes = new KeyAttributeCollection();
					keyAttributes[attribute.LogicalName] = record[attribute.LogicalName];
					reference.Id = Guid.Empty;
					reference.KeyAttributes = keyAttributes;
					reference.Name = null;
				}
			}
		}

		private AttributeMetadata GetMatchAttribute(LookupMappingMemento mapping)
		{
			var attribute = GetEntityMetadata(mapping.TargetEntityName).Attributes
				.FirstOrDefault(x => string.Equals(x.LogicalName, mapping.MatchAttributeName, StringComparison.OrdinalIgnoreCase));
			if (attribute == null)
			{
				throw new ComparisonException($"Lookup matching field <{mapping.MatchAttributeName}> was not found on table <{mapping.TargetEntityName}> in {this.environmentName}.");
			}
			if (string.Equals(attribute.LogicalName, GetEntityMetadata(mapping.TargetEntityName).PrimaryIdAttribute, StringComparison.OrdinalIgnoreCase) ||
				!IsSupportedMatchAttribute(attribute))
			{
				throw new ComparisonException($"Lookup matching field <{mapping.MatchAttributeName}> on table <{mapping.TargetEntityName}> cannot be queried in {this.environmentName}.");
			}
			return attribute;
		}

		private EntityMetadata GetEntityMetadata(string entityName)
		{
			if (this.entityMetadataCache.TryGetValue(entityName, out EntityMetadata metadata))
				return metadata;

			var request = new RetrieveEntityRequest
			{
				LogicalName = entityName,
				EntityFilters = EntityFilters.All,
				RetrieveAsIfPublished = true,
			};
			var response = (RetrieveEntityResponse)this.crm.Execute(request);
			metadata = response.EntityMetadata;
			this.entityMetadataCache[entityName] = metadata;
			return metadata;
		}

		private Dictionary<Guid, Entity> GetCachedRecords(EntityMetadata metadata, AttributeMetadata attribute, IEnumerable<Guid> ids)
		{
			var cacheKey = metadata.LogicalName + "|" + attribute.LogicalName;
			if (!this.recordCache.TryGetValue(cacheKey, out Dictionary<Guid, Entity> cache))
			{
				cache = new Dictionary<Guid, Entity>();
				this.recordCache[cacheKey] = cache;
			}

			var missingIds = ids.Distinct().Where(x => !cache.ContainsKey(x)).ToList();
			foreach (var item in RetrieveRecords(metadata, new[] { attribute.LogicalName }, missingIds))
			{
				cache[item.Key] = item.Value;
			}
			return cache;
		}

		internal static bool IsSupportedMatchAttribute(AttributeMetadata attribute)
		{
			if (attribute == null || attribute.IsLogical.GetValueOrDefault() || !string.IsNullOrWhiteSpace(attribute.AttributeOf)) return false;

			switch (attribute.AttributeType)
			{
				case AttributeTypeCode.BigInt:
				case AttributeTypeCode.Boolean:
				case AttributeTypeCode.DateTime:
				case AttributeTypeCode.Decimal:
				case AttributeTypeCode.Double:
				case AttributeTypeCode.Integer:
				case AttributeTypeCode.Memo:
				case AttributeTypeCode.Money:
				case AttributeTypeCode.Picklist:
				case AttributeTypeCode.State:
				case AttributeTypeCode.Status:
				case AttributeTypeCode.String:
				case AttributeTypeCode.Uniqueidentifier:
					return true;
				default:
					return false;
			}
		}

		internal static Entity CloneForCopy(Entity source)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));

			var clone = source.Clone();
			clone.Id = source.Id;
			foreach (var attributeName in source.Attributes.Keys.ToArray())
			{
				if (!clone.Contains(attributeName)) continue;
				if (!(source[attributeName] is EntityReference reference)) continue;

				var referenceClone = new EntityReference(reference.LogicalName, reference.Id)
				{
					Name = reference.Name,
				};
				if (reference.KeyAttributes != null)
				{
					foreach (var keyAttribute in reference.KeyAttributes)
					{
						referenceClone.KeyAttributes[keyAttribute.Key] = keyAttribute.Value;
					}
				}
				clone[attributeName] = referenceClone;
			}
			return clone;
		}

		internal static void ResolveMappedReferences(IOrganizationService crm, Entity entity)
		{
			if (crm == null) throw new ArgumentNullException(nameof(crm));
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			var mappedReferences = entity.Attributes.Values
				.OfType<EntityReference>()
				.Where(x => x.KeyAttributes?.Count == 1)
				.ToList();

			foreach (var group in mappedReferences.GroupBy(x => x.LogicalName + "|" + x.KeyAttributes.First().Key, StringComparer.OrdinalIgnoreCase))
			{
				var first = group.First();
				if (string.Equals(first.KeyAttributes.First().Key, DestinationRootBusinessUnitSentinel, StringComparison.OrdinalIgnoreCase))
				{
					var rootBusinessUnitId = GetRootBusinessUnitId(crm);
					foreach (var reference in group)
					{
						reference.Id = rootBusinessUnitId;
						reference.KeyAttributes.Clear();
					}
					continue;
				}

				var attributeName = first.KeyAttributes.First().Key;
				var referencesByValue = group
					.GroupBy(x => FormatMatchValue(x.KeyAttributes.First().Value), StringComparer.Ordinal)
					.ToList();

				for (var offset = 0; offset < referencesByValue.Count; offset += BatchSize)
				{
					var batch = referencesByValue.Skip(offset).Take(BatchSize).ToList();
					var query = new QueryExpression(first.LogicalName)
					{
						ColumnSet = new ColumnSet(attributeName),
						NoLock = true,
						PageInfo = new PagingInfo { PageNumber = 1 },
					};
					query.Criteria.AddCondition(attributeName, ConditionOperator.In, batch.Select(x => x.First().KeyAttributes.First().Value).Cast<object>().ToArray());

					var records = new List<Entity>();
					EntityCollection page;
					do
					{
						page = crm.RetrieveMultiple(query);
						records.AddRange(page.Entities);
						if (page.MoreRecords)
						{
							query.PageInfo.PageNumber++;
							query.PageInfo.PagingCookie = page.PagingCookie;
						}
					}
					while (page.MoreRecords);

					foreach (var referenceGroup in batch)
					{
						var matches = records
							.Where(x => x.Contains(attributeName) && CrmComparer.AreEqualCrmObjects(x[attributeName], referenceGroup.First().KeyAttributes.First().Value))
							.ToList();
						if (matches.Count == 0)
						{
							throw new ComparisonException($"Lookup target <{first.LogicalName}> with <{attributeName}={referenceGroup.Key}> was not found in the destination environment.");
						}
						if (matches.Count > 1)
						{
							throw new ComparisonException($"Lookup target <{first.LogicalName}> with <{attributeName}={referenceGroup.Key}> is not unique in the destination environment.");
						}

						foreach (var reference in referenceGroup)
						{
							reference.Id = matches[0].Id;
							reference.KeyAttributes.Clear();
						}
					}
				}
			}
		}

		private static Guid GetRootBusinessUnitId(IOrganizationService crm)
		{
			var query = new QueryExpression("businessunit")
			{
				ColumnSet = new ColumnSet("businessunitid"),
				NoLock = true,
				TopCount = 2,
			};
			query.Criteria.AddCondition("parentbusinessunitid", ConditionOperator.Null);

			var rootBusinessUnits = crm.RetrieveMultiple(query).Entities;
			if (rootBusinessUnits.Count == 0)
			{
				throw new ComparisonException("The root business unit was not found in the destination environment.");
			}
			if (rootBusinessUnits.Count > 1)
			{
				throw new ComparisonException("More than one root business unit was found in the destination environment.");
			}
			return rootBusinessUnits[0].Id;
		}

		private static string FormatMatchValue(object value)
		{
			if (value is OptionSetValue option) return "option:" + option.Value;
			if (value is Money money) return "money:" + money.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
			if (value is DateTime dateTime) return "date:" + dateTime.ToUniversalTime().ToString("O");
			return (value ?? string.Empty).ToString();
		}

		private Dictionary<Guid, Entity> RetrieveRecords(EntityMetadata metadata, IReadOnlyCollection<string> keyAttributes, IEnumerable<Guid> ids)
		{
			var result = new Dictionary<Guid, Entity>();
			var idList = ids.ToList();

			for (var offset = 0; offset < idList.Count; offset += BatchSize)
			{
				var batch = idList.Skip(offset).Take(BatchSize).Cast<object>().ToArray();
				var query = new QueryExpression(metadata.LogicalName)
				{
					ColumnSet = new ColumnSet(keyAttributes.ToArray()),
					NoLock = true,
					PageInfo = new PagingInfo { PageNumber = 1 },
				};
				query.Criteria.AddCondition(metadata.PrimaryIdAttribute, ConditionOperator.In, batch);

				EntityCollection page;
				do
				{
					page = this.crm.RetrieveMultiple(query);
					foreach (var entity in page.Entities)
					{
						result[entity.Id] = entity;
					}

					if (page.MoreRecords)
					{
						query.PageInfo.PageNumber++;
						query.PageInfo.PagingCookie = page.PagingCookie;
					}
				}
				while (page.MoreRecords);
			}

			return result;
		}
	}
}
