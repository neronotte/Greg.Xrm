using Greg.Xrm.EnvironmentComparer.Actions;
using Greg.Xrm.EnvironmentComparer.Engine;
using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using static Greg.Xrm.Extensions;

namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	public class CopyRowCommand : CommandBase
	{
		private readonly IMessenger messenger;
		private readonly Func<IReadOnlyCollection<ObjectComparison<Entity>>, bool> additionalCriteriaOnSelectedResults;
		private readonly ILog log;
		private string env1;
		private string env2;

		public CopyRowCommand(IMessenger messenger, Func<IReadOnlyCollection<ObjectComparison<Entity>>, bool> additionalCriteriaOnSelectedResults, ILog log)
		{
			this.messenger = messenger;
			this.additionalCriteriaOnSelectedResults = additionalCriteriaOnSelectedResults;
			this.log = log;
			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				.ChangesProperty(_ => _.ConnectionName1)
				.Execute(e =>
				{
					this.env1 = e.GetNewValue<string>();
					RecalculateCanExecute();
				});

			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				 .ChangesProperty(_ => _.ConnectionName2)
				 .Execute(e =>
				 {
					 this.env2 = e.GetNewValue<string>();
					 RecalculateCanExecute();
				 });


			this.WhenChanges(() => this.SelectedResults).Execute(RecalculateCanExecute);

		}

		private void RecalculateCanExecute(object obj = null)
		{
			this.CanExecute = SelectedResults != null
				&& !string.IsNullOrWhiteSpace(this.env1)
				&& !string.IsNullOrWhiteSpace(this.env2)
				&& SelectedResults.Any(_ => !_.IsActioned())
				&& additionalCriteriaOnSelectedResults(this.SelectedResults);
		}

		public IReadOnlyCollection<ObjectComparison<Entity>> SelectedResults
		{
			get => Get<IReadOnlyCollection<ObjectComparison<Entity>>>();
			set => Set(value);
		}


		protected override void ExecuteInternal(object arg)
		{
			var index = (int)arg;
			var selectedResults = this.SelectedResults;


			if (selectedResults.Count == 0) return;

			var envName = index == 1 ? this.env1 : this.env2;

			var actionList = new List<IAction>();
			foreach (var result in selectedResults.Where(_ => !_.IsActioned()))
			{
				var entity = index == 2 ? result.Item1 : result.Item2;

				if (result.IsManyToMany())
				{
					this.log.Error("Copy row on many to many relationship table is not supported!");
					continue;
				}
				else
				{
					var action = CreateActionCopyRecord(index, envName, result, ref entity);
					actionList.Add(action);
				}

				result.SetActioned();
				this.messenger.Send(new ResultUpdatedMessage(result));
			}


			if (actionList.Count > 0)
				this.messenger.Send(new SubmitActionMessage(actionList));
		}







		private static ActionCopyRecord CreateActionCopyRecord(int index, string envName, ObjectComparison<Entity> result, ref Entity entity)
		{
			// in case of matching but different, we need to pick the ID of the matching record
			if (result.Result == ObjectComparisonResult.MatchingButDifferent)
			{
				var matchingEntity = index == 1 ? result.Item1 : result.Item2;

				var entityIdAttributeName = entity.LogicalName + "id";

				entity = entity.Clone();
				entity.Attributes.Remove(entityIdAttributeName);
				entity.Id = matchingEntity.Id;

				// set null the attributes that are in the matching entity but not in this one, in order to clear their values
				foreach (var matchingEntityAttribute in matchingEntity.Attributes.Keys)
				{
					if (CloneSettings.IsForbidden(entity, matchingEntityAttribute)) continue;

					if (!entity.Contains(matchingEntityAttribute))
					{
						entity[matchingEntityAttribute] = null;
					}
				}
			}


			var action = new ActionCopyRecord(result, entity, index, envName);
			return action;
		}
	}
}
