using Greg.Xrm.EnvironmentComparer.Engine;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.Views;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	public class OpenRecordCommand : CommandBase
	{

		private IMessenger messenger;
		private readonly Func<IReadOnlyCollection<ObjectComparison<Entity>>, bool> additionalCriteriaOnSelectedResults;
		private ILog log;
		private ConnectionDetail env1;
		private ConnectionDetail env2;

		public OpenRecordCommand(IMessenger messenger, Func<IReadOnlyCollection<ObjectComparison<Entity>>, bool> additionalCriteriaOnSelectedResults, ILog log)
		{
			this.messenger = messenger;
			this.additionalCriteriaOnSelectedResults = additionalCriteriaOnSelectedResults;
			this.log = log;
			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				 .ChangesProperty(_ => _.Env1)
				 .Execute(e =>
				 {
					 this.env1 = e.GetNewValue<ConnectionDetail>();
					 RecalculateCanExecute();
				 });

			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				 .ChangesProperty(_ => _.Env2)
				 .Execute(e =>
				 {
					 this.env2 = e.GetNewValue<ConnectionDetail>();
					 RecalculateCanExecute();
				 });


			this.WhenChanges(() => this.SelectedResults).Execute(RecalculateCanExecute);

		}

		private void RecalculateCanExecute(object obj = null)
		{
			this.CanExecute = SelectedResults != null
				&& this.env1 != null
				&& this.env2 != null
				&& SelectedResults.Any(_ => !_.IsActioned())
				&& additionalCriteriaOnSelectedResults(this.SelectedResults);
		}

		public IReadOnlyCollection<ObjectComparison<Entity>> SelectedResults
		{
			get => this.Get<IReadOnlyCollection<ObjectComparison<Entity>>>();
			set => Set(value);
		}

		protected override void ExecuteInternal(object arg)
		{
			var index = (int)arg;
			var selectedResults = this.SelectedResults;


			if (selectedResults.Count == 0) return;

			var environment = index == 1 ? this.env1 : this.env2;

			foreach (var result in selectedResults)
			{
				var entity = index == 1 ? result.Item1 : result.Item2;

				if (result.IsManyToMany())
				{
					this.log.Error("Open record on many to many relationship table is not supported!");
					continue;
				}
				else
				{
					var webApplicationUrl = environment.WebApplicationUrl.TrimEnd('/');
					environment.OpenUrlWithBrowserProfile(new Uri($"{webApplicationUrl}/main.aspx?etn={entity.LogicalName}&pagetype=entityrecord&id=%7B{entity.Id}%7D"));
				}
				this.messenger.Send(new ResultUpdatedMessage(result));
			}
		}

	}
}
