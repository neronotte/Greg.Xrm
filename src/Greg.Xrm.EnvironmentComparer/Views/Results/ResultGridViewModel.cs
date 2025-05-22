using Greg.Xrm.EnvironmentComparer.Engine;
using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	public class ResultGridViewModel : ViewModel
	{
		private readonly IMessenger messenger;


		public ResultGridViewModel(IMessenger messenger, ILog log)
		{
			this.messenger = messenger;

			this.CopyToEnv1Command = new CopyRowCommand(messenger, o => o.AreAllLeftMissingOrDifferentAndNotActioned(), log);
			this.CopyToEnv2Command = new CopyRowCommand(messenger, o => o.AreAllRightMissingOrDifferentAndNotActioned(), log);
			this.DeleteFromEnv1Command = new DeleteRowCommand(messenger, o => o.AreAllRightMissingOrDifferentAndNotActioned(), log);
			this.DeleteFromEnv2Command = new DeleteRowCommand(messenger, o => o.AreAllLeftMissingOrDifferentAndNotActioned(), log);
			this.OpenRecord1Command = new OpenRecordCommand(messenger, o => o.AreAllRightMissingOrDifferentAndNotActioned(), log);
			this.OpenRecord2Command = new OpenRecordCommand(messenger, o => o.AreAllLeftMissingOrDifferentAndNotActioned(), log);

			this.WhenChanges(() => SelectedResults)
				.ChangesAlso(() => IsCompareEnabled)
				.Execute(_ => this.CopyToEnv1Command.SelectedResults = this.SelectedResults)
				.Execute(_ => this.CopyToEnv2Command.SelectedResults = this.SelectedResults)
				.Execute(_ => this.DeleteFromEnv1Command.SelectedResults = this.SelectedResults)
				.Execute(_ => this.DeleteFromEnv2Command.SelectedResults = this.SelectedResults)
				.Execute(_ => this.OpenRecord1Command.SelectedResults = this.SelectedResults)
				.Execute(_ => this.OpenRecord2Command.SelectedResults = this.SelectedResults);

			messenger.Register<CompareResultGroupSelected>(m =>
			{
				this.Results = m.Results;
				this.SelectedResults = null;
			});

			this.messenger.Register<ActionRemoved>(m =>
			{
				var result = m.Action.Result;
				result.ClearActioned();

				this.messenger.Send(new ResultUpdatedMessage(m.Action.Result));
			});

			this.messenger.Register<ResultUpdatedMessage>(o =>
			{
				this.CopyToEnv1Command.RefreshCanExecute();
				this.CopyToEnv2Command.RefreshCanExecute();
				this.DeleteFromEnv1Command.RefreshCanExecute();
				this.DeleteFromEnv2Command.RefreshCanExecute();
				this.OpenRecord1Command.RefreshCanExecute();
				this.OpenRecord2Command.RefreshCanExecute();
			});


			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				.ChangesProperty(_ => _.ConnectionName1)
				.Execute(e =>
				{
					this.Results = Array.Empty<ObjectComparison<Entity>>();
				});

			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				.ChangesProperty(_ => _.ConnectionName2)
				.Execute(e =>
				{
					this.Results = Array.Empty<ObjectComparison<Entity>>();
				});
		}

		public CopyRowCommand CopyToEnv1Command { get; }

		public CopyRowCommand CopyToEnv2Command { get; }

		public DeleteRowCommand DeleteFromEnv1Command { get; }

		public DeleteRowCommand DeleteFromEnv2Command { get; }

		public OpenRecordCommand OpenRecord1Command { get; }

		public OpenRecordCommand OpenRecord2Command { get; }

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
	}
}
