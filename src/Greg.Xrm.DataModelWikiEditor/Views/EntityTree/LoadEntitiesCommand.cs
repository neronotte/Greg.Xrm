using Greg.Xrm.Async;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.DataModelWikiEditor.Views.EntityTree
{
	public class LoadEntitiesCommand : CommandBase
	{
		private readonly ILog log;
		private readonly IMessenger messenger;
		private readonly IAsyncJobScheduler scheduler;

		public LoadEntitiesCommand(ILog log, IMessenger messenger, IAsyncJobScheduler scheduler)
		{
			this.log = log ?? throw new System.ArgumentNullException(nameof(log));
			this.messenger = messenger ?? throw new System.ArgumentNullException(nameof(messenger));
			this.scheduler = scheduler ?? throw new System.ArgumentNullException(nameof(scheduler));

			this.AllowRequests = true;

			this.WhenChanges(() => this.Crm)
				.ChangesAlso(() => CanExecute);

			this.WhenChanges(() => this.AllowRequests)
				.ChangesAlso(() => CanExecute);

			this.messenger.Register<ConnectionUpdatedMessage>(OnConnectionUpdated);
			this.messenger.WhenObject<MainViewModel>()
				.ChangesProperty(_ => _.AllowRequests)
				.Execute(x => this.AllowRequests = (bool)x.NewValue);
		}

		private void OnConnectionUpdated(ConnectionUpdatedMessage m)
		{
			this.Crm = m.Crm;
		}

		public IOrganizationService Crm
		{
			get => this.Get<IOrganizationService>();
			private set => this.Set(value);
		}

		public bool AllowRequests
		{
			get => this.Get<bool>();
			private set => this.Set(value);
		}

		public override bool CanExecute 
		{
			get => this.Crm != null && this.AllowRequests;
		}



		protected override void ExecuteInternal(object arg)
		{
			this.scheduler.Enqueue(new WorkAsyncInfo
			{
				Message = "Retrieving entity list",
				Work = (w, e) => {
					var request = new RetrieveAllEntitiesRequest
					{
						EntityFilters = EntityFilters.Entity,
						RetrieveAsIfPublished = false
					};


					var response = (RetrieveAllEntitiesResponse)this.Crm.Execute(request);
					e.Result = response;
				},
				PostWorkCallBack = e => {
					if (e.Error != null)
					{
						this.log.Error("Error fetching entity list: " + e.Error.Message, e.Error);
						return;
					}

					var response = (RetrieveAllEntitiesResponse)e.Result;
					this.messenger.Send(new EntitiesLoadedMessage(response.EntityMetadata));
				}
			});
		}
	}
}