using Greg.Xrm.Async;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Metadata.Query;
using System;
using System.Linq;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.DataModelWikiEditor.Views.EntityTree
{
	public class LoadAttributesCommand : CommandBase
	{
		private readonly ILog log;
		private readonly IMessenger messenger;
		private readonly IAsyncJobScheduler scheduler;

		public LoadAttributesCommand(ILog log, IMessenger messenger, IAsyncJobScheduler scheduler)
		{
			this.log = log ?? throw new ArgumentNullException(nameof(log));
			this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
			this.scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));

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

		protected override void ExecuteInternal(object arg)
		{
			var nodeForEntity = (NodeForEntity)arg;


			this.scheduler.Enqueue(new WorkAsyncInfo
			{
				Message = "Retrieving attribute list",
				Work = (w, e) =>
				{
					var query = new EntityQueryExpression();
					query.Criteria.Conditions.Add(new MetadataConditionExpression("LogicalName", MetadataConditionOperator.Equals, nodeForEntity.LogicalName));
					query.Properties = new MetadataPropertiesExpression("Attributes");

					var request = new RetrieveMetadataChangesRequest
					{
						Query = query
					};

					var response = (RetrieveMetadataChangesResponse)this.Crm.Execute(request);
					e.Result = response.EntityMetadata.Select(_ => _.Attributes).FirstOrDefault();
				},
				PostWorkCallBack = e =>
				{
					if (e.Error != null)
					{
						this.log.Error("Error fetching attribute list: " + e.Error.Message, e.Error);
						return;
					}

					var attributeList = (AttributeMetadata[])e.Result;


					nodeForEntity.AttributeList.Clear();
					nodeForEntity.AttributeList.AddRange(attributeList.Select(x => new NodeForAttribute(x)).OrderBy(_ => _.LogicalName));

					this.messenger.Send(new RefreshEntityMessage(nodeForEntity));
				}
			});
		}
	}
}
