using Greg.Xrm.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.Async
{
	public class AsyncJobScheduler : IAsyncJobScheduler
	{
		private readonly Queue<Job> queue;
		private readonly PluginViewModelBase pluginViewModel;
		private readonly PluginControlBase pluginControl;

		public AsyncJobScheduler(PluginControlBase pluginControl, PluginViewModelBase pluginViewModel = null)
		{
			this.queue = new Queue<Job>();
			this.pluginControl = pluginControl;
			this.pluginViewModel = pluginViewModel;
		}

		public void Enqueue(WorkAsyncInfo work)
		{
			var job = new Job(this, work);
			if (!queue.Any())
			{
				if (this.pluginViewModel != null) this.pluginViewModel.AllowRequests = false;
				this.pluginControl.WorkAsync(job.Work);
			}
			queue.Enqueue(job);
		}



		private void WorkAsyncEnded()
		{
			queue.Dequeue();
			if (queue.Any())
			{
				this.pluginControl.WorkAsync(queue.Peek().Work);
			}
			else
			{
				if (this.pluginViewModel != null) this.pluginViewModel.AllowRequests = true;
			}
		}

		private class Job
		{
			public WorkAsyncInfo Work { get; private set; }
			private readonly Action<RunWorkerCompletedEventArgs> postWorkCallBack;
			private readonly AsyncJobScheduler queue;

			public Job(AsyncJobScheduler queue, WorkAsyncInfo work)
			{
				this.queue = queue ?? throw new ArgumentNullException(nameof(queue));
				this.Work = work ?? throw new ArgumentNullException(nameof(work));
				this.postWorkCallBack = work.PostWorkCallBack;
				this.Work.PostWorkCallBack = PostWorkCallBack;
			}

			private void PostWorkCallBack(RunWorkerCompletedEventArgs args)
			{
				try
				{
					postWorkCallBack?.Invoke(args);
				}
				finally
				{
					this.queue.WorkAsyncEnded();
				}
			}
		}
	}
}