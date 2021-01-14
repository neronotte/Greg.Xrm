using Greg.Xrm.EnvironmentComparer.Actions;
using Greg.Xrm.EnvironmentComparer.Engine;
using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using System.Windows.Forms;
using static Greg.Xrm.Extensions;

namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	public class ResultGridViewModel : ViewModel
	{
		private readonly IMessenger messenger;
		private string env1 = "ENV1", env2 = "ENV2";


		public ResultGridViewModel(IMessenger messenger)
		{
			this.messenger = messenger;

			this.WhenChanges(() => SelectedResults)
				.ChangesAlso(() => IsCompareEnabled)
				.ChangesAlso(() => IsCopyToEnv1Enabled)
				.ChangesAlso(() => IsCopyToEnv2Enabled)
				.ChangesAlso(() => IsDeleteFromEnv1Enabled)
				.ChangesAlso(() => IsDeleteFromEnv2Enabled);

			messenger.Register<CompareResultGroupSelected>(m =>
			{
				this.Results = m.Results;
				this.SelectedResults = null;
			});

			this.messenger.Register<ActionRemoved>(m =>
			{
				var result = m.Action.Result;
				result.ClearActioned();

				this.OnResultUpdated(m.Action.Result);
			});


			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				.ChangesProperty(_ => _.ConnectionName1)
				.Execute(e =>
				{
					this.env1 = e.GetNewValue<string>();
				});

			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				.ChangesProperty(_ => _.ConnectionName2)
				.Execute(e =>
				{
					this.env2 = e.GetNewValue<string>();
				});
		}

		#region ResultUpdated event

		public event ResultUpdatedEventHandler ResultUpdated;

		protected void OnResultUpdated(ObjectComparison<Entity> result)
		{
			this.ResultUpdated?.Invoke(this, new ResultUpdatedEventArgs(result));
		}

		#endregion


		public IReadOnlyCollection<ObjectComparison<Entity>> Results
		{
			get => Get<IReadOnlyCollection<ObjectComparison<Entity>>>();
			private set => Set(value);
		}

		public IReadOnlyCollection<ObjectComparison<Entity>> SelectedResults
		{
			get => Get<IReadOnlyCollection<ObjectComparison<Entity>>>();
			set => Set(value);
		}

		public bool IsCompareEnabled
		{
			get => this.SelectedResults != null && this.SelectedResults.Count == 1;
		}

		public bool IsCopyToEnv1Enabled
		{
			get => this.SelectedResults.AreAllLeftMissingOrDifferentAndNotActioned();
		}

		public bool IsCopyToEnv2Enabled
		{
			get => this.SelectedResults.AreAllRightMissingOrDifferentAndNotActioned();
		}

		public bool IsDeleteFromEnv1Enabled
		{
			get => this.SelectedResults.AreAllRightMissingOrDifferentAndNotActioned();
		}

		public bool IsDeleteFromEnv2Enabled
		{
			get => this.SelectedResults.AreAllLeftMissingOrDifferentAndNotActioned();
		}




		public void CopySelectedRowTo(int index)
		{
			if (this.SelectedResults.Count == 0) return;

			var envName = index == 1 ? this.env1 : this.env2;

			var actionList = new List<IAction>();
			foreach (var result in this.SelectedResults)
			{
				var entity = index == 2 ? result.Item1 : result.Item2;

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
				actionList.Add(action);

				result.SetActioned();
				OnResultUpdated(result);
			}
			this.messenger.Send(new SubmitActionMessage(actionList));
		}


		public void DeleteSelectedRowFrom(int index)
		{
			if (this.SelectedResults.Count == 0) return;

			var envName = index == 1 ? this.env1 : this.env2;

			var confirm = MessageBox.Show($"Do you really want do delete the selected record from {envName}?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (confirm != DialogResult.Yes) return;


			var actionList = new List<IAction>();
			foreach (var result in this.SelectedResults)
			{
				var entity = index == 1 ? result.Item1 : result.Item2;

				var action = new ActionDeleteRecord(result, entity, index, envName);
				actionList.Add(action);

				result.SetActioned();
				OnResultUpdated(result);
			}
			this.messenger.Send(new SubmitActionMessage(actionList));
		}
	}
}
