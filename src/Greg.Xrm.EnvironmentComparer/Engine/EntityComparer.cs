using Greg.Xrm.EnvironmentComparer.Engine.Config;
using Greg.Xrm.EnvironmentComparer.Engine.Memento;
using Greg.Xrm.Logging;
using Microsoft.Xrm.Sdk;
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
			: this(entityName, keyProvider, GetDefaultFilter(onlyActiveRecords), skipAttributeCriteria)
		{
			this.OnlyActiveRecords = onlyActiveRecords;
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
		}





		public string EntityName { get; }
		public ISkipAttributeCriteria SkipAttributeCriteria { get; }
		public IKeyProvider<Entity> KeyProvider { get; }
		public bool OnlyActiveRecords { get; }

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
					var query = new QueryExpression(this.EntityName);
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
			return entityMemento;
		}
	}
}
