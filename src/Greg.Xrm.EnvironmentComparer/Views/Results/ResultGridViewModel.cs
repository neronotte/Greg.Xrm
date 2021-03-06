﻿using Greg.Xrm.EnvironmentComparer.Actions;
using Greg.Xrm.EnvironmentComparer.Engine;
using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Microsoft.Xrm.Sdk;
using System;
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

			this.CopyToEnv1Command = new CopyRowCommand(messenger, o => o.AreAllLeftMissingOrDifferentAndNotActioned());
			this.CopyToEnv2Command = new CopyRowCommand(messenger, o => o.AreAllRightMissingOrDifferentAndNotActioned());
			this.DeleteFromEnv1Command = new DeleteRowCommand(messenger, o => o.AreAllRightMissingOrDifferentAndNotActioned());
			this.DeleteFromEnv2Command = new DeleteRowCommand(messenger, o => o.AreAllLeftMissingOrDifferentAndNotActioned());


			this.WhenChanges(() => SelectedResults)
				.ChangesAlso(() => IsCompareEnabled)
				.Execute(_ => this.CopyToEnv1Command.SelectedResults = this.SelectedResults)
				.Execute(_ => this.CopyToEnv2Command.SelectedResults = this.SelectedResults)
				.Execute(_ => this.DeleteFromEnv1Command.SelectedResults = this.SelectedResults)
				.Execute(_ => this.DeleteFromEnv2Command.SelectedResults = this.SelectedResults);

			messenger.Register<CompareResultGroupSelected>(m =>
			{
				this.Results = m.Results;
				this.SelectedResults = null;
			});

			this.messenger.Register<ActionRemoved>(m =>
			{
				var result = m.Action.Result;
				result.ClearActioned();

				this.messenger.Send(new ResultUpdatedMessage( m.Action.Result));
			});

			this.messenger.Register<ResultUpdatedMessage>(o =>
			{
				this.CopyToEnv1Command.RefreshCanExecute();
				this.CopyToEnv2Command.RefreshCanExecute();
				this.DeleteFromEnv1Command.RefreshCanExecute();
				this.DeleteFromEnv2Command.RefreshCanExecute();
			});


			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				.ChangesProperty(_ => _.ConnectionName1)
				.Execute(e =>
				{
					this.env1 = e.GetNewValue<string>();
					this.Results = Array.Empty<ObjectComparison<Entity>>();
				});

			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				.ChangesProperty(_ => _.ConnectionName2)
				.Execute(e =>
				{
					this.env2 = e.GetNewValue<string>();
					this.Results = Array.Empty<ObjectComparison<Entity>>();
				});
		}

		public CopyRowCommand CopyToEnv1Command { get; }

		public CopyRowCommand CopyToEnv2Command { get; }

		public DeleteRowCommand DeleteFromEnv1Command { get; }
			   
		public DeleteRowCommand DeleteFromEnv2Command { get; }


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
