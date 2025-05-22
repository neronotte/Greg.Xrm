using Greg.Xrm.Async;
using Greg.Xrm.Messaging;
using System;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.Core.Async
{
	public class JobMessageHandler : IMessageHandler<WorkAsyncInfo>
	{
		private readonly IAsyncJobScheduler asyncJobScheduler;

		public JobMessageHandler(IAsyncJobScheduler asyncJobScheduler)
		{
			this.asyncJobScheduler = asyncJobScheduler ?? throw new ArgumentNullException(nameof(asyncJobScheduler));
		}


		public void Handle(WorkAsyncInfo message)
		{
			if (message == null)
				throw new ArgumentNullException(nameof(message), "The async job to process cannot be null.");

			this.asyncJobScheduler.Enqueue(message);
		}
	}
}
