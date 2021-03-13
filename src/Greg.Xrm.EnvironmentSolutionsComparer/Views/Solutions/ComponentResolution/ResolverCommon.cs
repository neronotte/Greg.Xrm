using Greg.Xrm.Logging;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution
{
	public abstract class ResolverCommon : IResolver
	{
		public abstract SolutionComponentType ComponentType { get; }
		public abstract string ComponentName { get; }
		public virtual string ComponentIdField { get => this.ComponentName + "id"; }
		public virtual string ComponentLabelField { get => "name"; }

		private readonly ILog log;


		public Func<Entity, string> LabelExtractor { get; protected set; }

		protected ResolverCommon(ILog log)
		{
			this.log = log ?? throw new System.ArgumentNullException(nameof(log));

			this.LabelExtractor = e => e.GetAttributeValue<string>(ComponentLabelField);
		}



		public virtual void Resolve(SolutionComponentGrid grid, IReadOnlyCollection<ConnectionModel> connections)
		{
			using (this.log.Track($"Fetching {ComponentType} definitions"))
			{
				var group = grid.FirstOrDefault(_ => _.ComponentTypeCode == (int)ComponentType);
				if (group == null) return;

				group.SetAnalyzed();

				var idList = group
					.OfType<SolutionComponentLeaf>()
					.Select(_ => _.ObjectId)
					.ToList();

				foreach (var env in connections)
				{
					if (idList.Count == 0) return;

					var query = new QueryExpression(ComponentName);
					query.ColumnSet.AddColumns(ComponentLabelField);
					query.Criteria.AddCondition(ComponentIdField, ConditionOperator.In, idList.Cast<object>().ToArray());
					query.NoLock = true;

					var result = env.Crm.RetrieveMultiple(query);

					var entityList = result.Entities;

					foreach (var entity in entityList)
					{
						var entityId = entity.Id;
						idList.Remove(entityId);

						var leaf = group.OfType<SolutionComponentLeaf>().First(_ => _.ObjectId == entityId);
						leaf.SetLabel( this.LabelExtractor(entity) );
					}
				}
			}
		}
	}
}
