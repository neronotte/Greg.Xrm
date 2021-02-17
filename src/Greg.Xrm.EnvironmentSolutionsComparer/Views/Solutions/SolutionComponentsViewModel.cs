using Greg.Xrm.Async;
using Greg.Xrm.EnvironmentSolutionsComparer.Messaging;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public class SolutionComponentsViewModel : ViewModel
	{
		private readonly IAsyncJobScheduler scheduler;
		private readonly ILog log;
		private readonly IMessenger messenger;

		public SolutionComponentsViewModel(IAsyncJobScheduler scheduler, ILog log, IMessenger messenger)
		{
			this.scheduler = scheduler ?? throw new System.ArgumentNullException(nameof(scheduler));
			this.log = log ?? throw new System.ArgumentNullException(nameof(log));
			this.messenger = messenger ?? throw new System.ArgumentNullException(nameof(messenger));

			this.Connections = new ObservableCollection<ConnectionModel>();

			this.messenger.WhenObject<MainViewModel>()
				.ChangesProperty(_ => _.AllowRequests)
				.Execute(m => this.AllowRequests = (bool)m.NewValue);


			this.WhenChanges(() => AllowRequests)
				.ChangesAlso(() => CanExecuteComparison);
			this.WhenChanges(() => Solution)
				.ChangesAlso(() => CanExecuteComparison)
				.ChangesAlso(() => SolutionName);

			this.Connections.CollectionChanged += (s, e) => this.OnPropertyChanged(nameof(CanExecuteComparison), this.CanExecuteComparison);

			this.messenger.Register<SolutionSelectedMessage>(OnSolutionSelected);
			this.messenger.Register<ConnectionAddedMessage>(obj => this.Connections.Insert(obj.Index, obj.Model));
			this.messenger.Register<ConnectionRemovedMessage>(obj => this.Connections.Remove(obj.Model));
		}


		private void OnSolutionSelected(SolutionSelectedMessage obj)
		{
			this.Solution = obj.Solution;
		}


		public bool AllowRequests
		{
			get => this.Get<bool>();
			private set => this.Set(value);
		}

		public ObservableCollection<ConnectionModel> Connections { get; }

		public SolutionRow Solution
		{
			get => this.Get<SolutionRow>();
			private set => this.Set(value);
		}

		public string SolutionName
		{
			get => this.Solution?.SolutionUniqueName ?? "Please select a solution...";
		}

		public bool CanExecuteComparison
		{
			get => this.AllowRequests && this.Solution != null && this.Connections.Count > 0;
		}

		public SolutionComponentGrid Grid
		{
			get => this.Get<SolutionComponentGrid>();
			private set => this.Set(value);
		}




		public void CompareAsync()
		{
			this.scheduler.Enqueue(new XrmToolBox.Extensibility.WorkAsyncInfo
			{
				Message = "Fetching solution components",
				Work = (w, e) => {

					var builder = new SolutionComponentGridBuilder(this.log);
					var grid = builder.Create(this.Connections, this.Solution);
					e.Result = grid;
				},
				PostWorkCallBack = e => {

					if (e.Error != null)
					{
						log.Error("Error loading solution components: " + e.Error.Message, e.Error);
						return;
					}

					var grid = (SolutionComponentGrid)e.Result;
					this.Grid = grid;
				}
			});
		}


	}
}
