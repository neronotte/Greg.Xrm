using Greg.Xrm.EnvironmentComparer.Logging;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Greg.Xrm.EnvironmentComparer.Model
{
	public class EntityComparer
	{
		private static readonly string[] attributesToRemove = new[] { 
			"ownerid", 
			"createdby", 
			"createdon", 
			"modifiedby",
			"modifiedon",
			"owninguser",
			"owningbusinessunit",
			"importsequencenumber",
			"modifiedonbehalfby",
			"overriddencreatedon", 
			"timezoneruleversionnumber"
		};
		private readonly Action<QueryExpression> applyAdditionalFilters;
		private readonly Comparer<Entity> comparer;

		public EntityComparer(string entityName, IKeyProvider<Entity> keyProvider, bool onlyActiveRecords)
		{
			this.comparer = new Comparer<Entity>(keyProvider, new EntityEqualityComparer(), entityName);
			this.EntityName = entityName;

			if (onlyActiveRecords)
			{
				applyAdditionalFilters = q =>
				{
					q.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
				};
			}
			else 
			{
				applyAdditionalFilters = q => { };
			}
		}


		public EntityComparer(string entityName, IKeyProvider<Entity> keyProvider, ISkipAttributeCriteria skipAttributeCriteria = null, bool onlyActiveRecords = false)
		{
			this.comparer = new Comparer<Entity>(keyProvider, new EntityEqualityComparer(skipAttributeCriteria), entityName);
			this.EntityName = entityName;

			if (onlyActiveRecords)
			{
				applyAdditionalFilters = q =>
				{
					q.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
				};
			}
			else
			{
				applyAdditionalFilters = q => { };
			}
		}

		public EntityComparer(string entityName, IKeyProvider<Entity> keyProvider, Action<QueryExpression> filterCriteria = null, ISkipAttributeCriteria skipAttributeCriteria = null)
		{
			this.comparer = new Comparer<Entity>(keyProvider, new EntityEqualityComparer(skipAttributeCriteria), entityName);
			this.EntityName = entityName;
			this.applyAdditionalFilters = filterCriteria ?? (q => { });
		}

		public string EntityName { get; }

		public IReadOnlyCollection<Comparison<Entity>> Compare(List<Entity> list1, List<Entity> list2)
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
				catch(FaultException<OrganizationServiceFault> ex)
				{
					log.Error($"Error while querying entity {this.EntityName} on {label}: {ex.Message}", ex);
					return result;
				}
			}

			using (log.Track("Pruning unused attributes"))
			{
				foreach (var entity in result)
				{
					foreach (var attribute in attributesToRemove)
					{
						entity.Attributes.Remove(attribute);
					}
				}
			}
			return result;
		}
	}
}
