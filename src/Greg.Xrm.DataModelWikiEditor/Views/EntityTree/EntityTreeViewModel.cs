using Greg.Xrm.Async;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.DataModelWikiEditor.Views.EntityTree
{
	public class EntityTreeViewModel : ViewModel
	{
		private readonly ILog log;
		private readonly IAsyncJobScheduler scheduler;


		public EntityTreeViewModel(ILog log, IMessenger messenger, IAsyncJobScheduler scheduler)
		{
			this.log = log ?? throw new ArgumentNullException(nameof(log));
			this.scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));

			this.LoadEntitiesCommand = new LoadEntitiesCommand(log, messenger, scheduler);
			this.LoadAttributesCommand = new LoadAttributesCommand(log, messenger, scheduler);

			messenger.Register<EntitiesLoadedMessage>(OnEntitiesLoaded);
		}

		private void OnEntitiesLoaded(EntitiesLoadedMessage obj)
		{
			this.Entities = obj.EntityMetadataList
				.Select(_ => new NodeForEntity(_))
				.OrderBy(_ => _.LogicalName)
				.ToList();	
		}


		public IReadOnlyCollection<NodeForEntity> Entities
		{
			get => this.Get<IReadOnlyCollection<NodeForEntity>>();
			private set => this.Set(value);
		}

		public ICommand LoadEntitiesCommand { get; }

		public ICommand LoadAttributesCommand { get; set; }



		public void Expand(NodeForEntity entityNode)
		{
			this.LoadAttributesCommand.Execute(entityNode);
		}
	}
}
