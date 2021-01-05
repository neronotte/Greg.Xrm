using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.EnvironmentComparer.Model;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	public class ResultGridViewModel : ViewModel
	{
		public ResultGridViewModel(IMessenger messenger)
		{
			this.WhenChanges(() => SelectedResults)
				.ChangesAlso(() => IsCompareEnabled);

			messenger.Register<CompareResultGroupSelected>(m =>
			{
				this.Results = m.Results;
				this.SelectedResults = null;
			});
		}

		public IReadOnlyCollection<Model.Comparison<Entity>> Results
		{
			get => Get<IReadOnlyCollection<Model.Comparison<Entity>>>();
			private set => Set(value);
		}

		public IReadOnlyCollection<Model.Comparison<Entity>> SelectedResults
		{
			get => Get<IReadOnlyCollection<Model.Comparison<Entity>>>();
			set => Set(value);
		}

		public bool IsCompareEnabled
		{
			get => this.SelectedResults != null && this.SelectedResults.Count == 1;
		}

		public bool IsCopyToEnv1Enabled
		{
			get => this.SelectedResults?
				.All(x => x.Result == RecordComparisonResult.LeftMissing || x.Result == RecordComparisonResult.MatchingButDifferent) ?? false;
		}

		public bool IsCopyToEnv2Enabled
		{
			get => this.SelectedResults?
				.All(x => x.Result == RecordComparisonResult.RightMissing || x.Result == RecordComparisonResult.MatchingButDifferent) ?? false;
		}
	}
}
