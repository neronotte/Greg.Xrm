using Greg.Xrm.Async;
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
using System.Windows.Forms;
using static Greg.Xrm.Extensions;

namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	public class ResultTreeViewModel : ViewModel
	{
		public const string AdditionalMetadataIsOk = "isOk";
		public const string AdditionalMetadataMarkedOk = "markedOk";

		private readonly IMessenger messenger;

		public ResultTreeViewModel(IAsyncJobScheduler scheduler, IMessenger messenger, ILog log)
		{
			this.messenger = messenger;

			this.DownloadExcelCommand = new DownloadExcelCommand(scheduler, messenger, log);
			this.CopyToEnv1Command = new CopyRowCommand(messenger, o => o.AreAllLeftMissingOrDifferentAndNotActioned(), log);
			this.CopyToEnv2Command = new CopyRowCommand(messenger, o => o.AreAllRightMissingOrDifferentAndNotActioned(), log);
			this.DeleteFromEnv1Command = new DeleteRowCommand(messenger, o => o.AreAllRightMissingOrDifferentAndNotActioned(), log);
			this.DeleteFromEnv2Command = new DeleteRowCommand(messenger, o => o.AreAllLeftMissingOrDifferentAndNotActioned(), log);

			this.WhenChanges(() => SelectedNode)
				.ChangesAlso(() => IsMarkOKEnabled)
				.ChangesAlso(() => IsUnmrkOKEnabled);

			this.WhenChanges(() => this.SelectedResults)
				.Execute(_ => this.messenger.Send(new CompareResultGroupSelected(this.SelectedResults)))
				.Execute(_ => this.CopyToEnv1Command.SelectedResults = this.SelectedResults)
				.Execute(_ => this.CopyToEnv2Command.SelectedResults = this.SelectedResults)
				.Execute(_ => this.DeleteFromEnv1Command.SelectedResults = this.SelectedResults)
				.Execute(_ => this.DeleteFromEnv2Command.SelectedResults = this.SelectedResults);



			this.messenger.Register<ResultUpdatedMessage>(o =>
			{
				this.CopyToEnv1Command.RefreshCanExecute();
				this.CopyToEnv2Command.RefreshCanExecute();
				this.DeleteFromEnv1Command.RefreshCanExecute();
				this.DeleteFromEnv2Command.RefreshCanExecute();
			});
		}


		public ICommand DownloadExcelCommand { get; }

		public CopyRowCommand CopyToEnv1Command { get; }

		public CopyRowCommand CopyToEnv2Command { get; }

		public DeleteRowCommand DeleteFromEnv1Command { get; }

		public DeleteRowCommand DeleteFromEnv2Command { get; }


		public bool IsMarkOKEnabled
		{
			get => this.SelectedNode?.Tag is CompareResultForEntity r && !r.ContainsAny(AdditionalMetadataIsOk, AdditionalMetadataMarkedOk);
		}

		public bool IsUnmrkOKEnabled
		{
			get => this.SelectedNode?.Tag is CompareResultForEntity r1 && r1.Contains(AdditionalMetadataMarkedOk);
		}


		public TreeNode SelectedNode
		{
			get => Get<TreeNode>();
			set
			{
				Set(value);

				IReadOnlyCollection<ObjectComparison<Entity>> comparisonResult = Array.Empty<ObjectComparison<Entity>>();
				if (value?.Tag is IReadOnlyCollection<ObjectComparison<Entity>> collection)
				{
					comparisonResult = collection;
				}

				this.SelectedResults = comparisonResult;
			}
		}

		public IReadOnlyCollection<ObjectComparison<Entity>> SelectedResults
		{
			get => Get<IReadOnlyCollection<ObjectComparison<Entity>>>();
			private set => Set(value);
		}
	}
}
