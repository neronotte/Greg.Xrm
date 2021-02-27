using Greg.Xrm.EnvironmentComparer.Actions;
using Greg.Xrm.EnvironmentComparer.Engine;
using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	public class DeleteRowCommand : CommandBase
	{
		private readonly IMessenger messenger;
		private readonly Func<IReadOnlyCollection<ObjectComparison<Entity>>, bool> additionalCriteriaOnSelectedResults;
		private string env1;
		private string env2;

		public DeleteRowCommand(IMessenger messenger, Func<IReadOnlyCollection<ObjectComparison<Entity>>, bool> additionalCriteriaOnSelectedResults)
		{
			this.messenger = messenger;
			this.additionalCriteriaOnSelectedResults = additionalCriteriaOnSelectedResults;
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
			get => this.Get<IReadOnlyCollection<ObjectComparison<Entity>>>();
			set => Set(value);
		}



		protected override void ExecuteInternal(object arg)
		{
			var index = (int)arg;

			if (this.SelectedResults.Count == 0) return;

			var envName = index == 1 ? this.env1 : this.env2;

			var confirm = MessageBox.Show($"Do you really want do delete the selected record from {envName}?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (confirm != DialogResult.Yes) return;


			var actionList = new List<IAction>();
			foreach (var result in this.SelectedResults.Where(_ => !_.IsActioned()))
			{
				var entity = index == 1 ? result.Item1 : result.Item2;

				var action = new ActionDeleteRecord(result, entity, index, envName);
				actionList.Add(action);

				result.SetActioned();
				this.messenger.Send(new ResultUpdatedMessage(result));
			}

			if (actionList.Count > 0)
				this.messenger.Send(new SubmitActionMessage(actionList));
		}
	}
}
