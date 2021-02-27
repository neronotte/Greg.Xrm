using Greg.Xrm.Async;
using Greg.Xrm.EnvironmentComparer.Engine;
using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.EnvironmentComparer.Model;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.Views;
using System;
using System.IO;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	public class DownloadExcelCommand : CommandBase
	{
		private readonly IAsyncJobScheduler scheduler;
		private readonly IMessenger messenger;
		private readonly ILog log;
		private string env1;
		private string env2;

		public DownloadExcelCommand(IAsyncJobScheduler scheduler, IMessenger messenger, ILog log)
		{
			this.scheduler = scheduler;
			this.messenger = messenger;
			this.log = log;

			this.messenger.Register<CompareResultSetAvailable>(m =>
			{
				this.CompareResult = m.CompareResultSet;
			});
			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				.ChangesProperty(_ => _.ConnectionName1)
				.Execute(e =>
				{
					this.env1 = e.GetNewValue<string>();
				});

			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				 .ChangesProperty(_ => _.ConnectionName2)
				 .Execute(e =>
				 {
					 this.env2 = e.GetNewValue<string>();
				 });
		}

		public CompareResultSet CompareResult { get; private set; }





		protected override void ExecuteInternal(object arg)
		{
			string fileName;
			using (var dialog = new FolderBrowserDialog())
			{
				dialog.Description = "Output folder";

				if (dialog.ShowDialog() != DialogResult.OK) return;
				fileName = dialog.SelectedPath;
			}

			this.scheduler.Enqueue(new WorkAsyncInfo
			{
				Message = "Executing generating Excel file, please wait...",
				Work = (w, e1) =>
				{
					using (log.Track("Exporting comparison result on excel file " + fileName))
					{
						try
						{
							var reportBuilder = new ReportBuilder(new DirectoryInfo(fileName));
							reportBuilder.GenerateReport(CompareResult, this.env1, this.env2);
						}
#pragma warning disable CA1031 // Do not catch general exception types
						catch (Exception ex)
						{
							log.Error("Error during export: " + ex.Message, ex);
						}
#pragma warning restore CA1031 // Do not catch general exception types
					}
				}
			});
		}
	}
}
