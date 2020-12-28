using Greg.Xrm.Async;
using Greg.Xrm.SolutionManager.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.SolutionManager.Views.SolutionProgress
{
	public partial class SolutionProgressView : DockContent
	{
		private readonly IAsyncJobScheduler scheduler;
		private readonly IImportJobRepository importJobRepository;
		private readonly PluginViewModel viewModel;

		public SolutionProgressView(IAsyncJobScheduler scheduler, IImportJobRepository importJobRepository, PluginViewModel viewModel)
		{
			InitializeComponent();

			this.scheduler = scheduler ?? throw new System.ArgumentNullException(nameof(scheduler));
			this.importJobRepository = importJobRepository ?? throw new System.ArgumentNullException(nameof(importJobRepository));
			this.viewModel = viewModel;
		}

		public IOrganizationService Service { get; set; }


		public void StartAsyncMonitor()
		{
			if (this.Service == null) return;
			if (!this.viewModel.CanStartMonitoring) return;

			this.RescheduleImportJob();
			this.viewModel.CanStartMonitoring = false;
		}

		private void RescheduleImportJob()
		{
			this.timer.Stop();
			this.scheduler.Enqueue(new WorkAsyncInfo
			{
				Message = "Reading import job table",
				Work = OnDoWork,
				PostWorkCallBack = OnWorkCompleted
			});
		}

		private void OnDoWork(BackgroundWorker arg1, DoWorkEventArgs arg2)
		{
			arg2.Result = this.importJobRepository.GetLatest(this.Service);
		}

		private void OnWorkCompleted(RunWorkerCompletedEventArgs obj)
		{

			if (obj.Error != null)
			{
				// handle error
				return;
			}
			if (obj.Result is ImportJob importJob)
			{
				ShowImportJob(importJob);

				// if someone requested to stop the monitoring, don't restart the timer.
				if (this.viewModel.StopMonitoringRequested)
				{
					this.viewModel.StopMonitoringRequested = false;
					this.viewModel.CanStartMonitoring = true;
				}
				else
				{
					this.timer.Start();
				}
			}
		}

		private void ShowImportJob(ImportJob importJob)
		{
			this.pbCurrentImport.Value = Convert.ToInt32(importJob.progress.GetValueOrDefault());
			this.lCurrentImport.Text = importJob.progress.GetValueOrDefault().ToString("0.00") + "%";
			this.gCurrentImport.Text = "Last import: " + importJob.solutionname;

			var now = DateTime.Now;

			var sb = new StringBuilder();
			sb.Append("Last check      : ").Append(now.ToString("dd/MM/yyyy HH:mm:ss")).AppendLine();

			sb.AppendLine();
			sb.Append("*** SOLUTION INFO ***").AppendLine();
			sb.Append("uniquename  : ").Append(importJob.uniquename).AppendLine();
			sb.Append("friendlyname: ").Append(importJob.friendlyname).AppendLine();
			sb.Append("version     : ").Append(importJob.version).AppendLine();
			sb.Append("publisher   : ").Append(importJob.publisherid?.Name).AppendLine();
			sb.Append("description : ").Append(importJob.description).AppendLine();
			sb.Append("*********************").AppendLine();
			sb.AppendLine();

			if (!importJob.startedon.HasValue)
			{
				sb.AppendLine("NOT STARTED!");
			}
			else if (importJob.startedon.HasValue && !importJob.completedon.HasValue)
			{
				var start = importJob.startedon.Value.ToLocalTime();
				var progress = importJob.progress.GetValueOrDefault();

				var elapsed = ((now - start).TotalMilliseconds / progress) * 100;
				var elapsedTimespan = TimeSpan.FromMilliseconds(elapsed);

				var end = start.Add(elapsedTimespan);

				sb.Append("Started on      : ").Append(start.ToString("dd/MM/yyyy HH:mm:ss")).AppendLine();
				sb.Append("Will complete on: ").Append(end.ToString("dd/MM/yyyy HH:mm:ss")).AppendLine();
				sb.Append("Remaining       : ").Append(elapsedTimespan).AppendLine();
			}
			else if (importJob.startedon.HasValue && importJob.completedon.HasValue)
			{
				sb.Append("Started on      : ").Append(importJob.startedon.Value.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss")).AppendLine();
				sb.Append("Completed on    : ").Append(importJob.completedon.Value.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss")).AppendLine();

				var elapsedTimespan = importJob.completedon.Value - importJob.startedon.Value;
				sb.Append("Elapsed         : ").Append(elapsedTimespan).AppendLine();

				try
				{
					sb.Append("DATA:").AppendLine();
					var doc = XDocument.Parse(importJob.data);
					sb.AppendLine(doc.ToString(SaveOptions.None));

					this.viewModel.CurrentImportJobData = doc;
				}
				catch
				{
					sb.Append("DATA:").AppendLine();
					sb.AppendLine(importJob.data);

					this.viewModel.CurrentImportJobData = null;
				}

			}

			this.txtOutput.Text = sb.ToString();
		}

		private void OnTimerExpired(object sender, EventArgs e)
		{
			this.RescheduleImportJob();
		}

		private void OnTextAreaKeyUp(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.F)
			{
				OpenFindWindow();
			}
			if (e.KeyCode == Keys.F3)
			{
				OpenFindWindow();
			}
		}

		private void OpenFindWindow()
		{
			this.lblOccurrences.Text = string.Empty;
			this.pFind.Visible = true;
			this.txtFind.Focus();
		}

		private void OnFindKeyDown(object sender, KeyEventArgs e)
		{
			
		}

		private void OnFindKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F3)
			{
				if (string.IsNullOrWhiteSpace(this.txtFind.Text)) return;

				Find(this.txtFind.Text, this.txtOutput.SelectionStart + this.txtOutput.SelectionLength);
			}
			else if (e.KeyCode == Keys.Escape)
			{
				this.pFind.Visible = false;
				this.txtFind.Text = string.Empty;
			}
			else
			{
				if (string.IsNullOrWhiteSpace(this.txtFind.Text)) return;

				Find(this.txtFind.Text, 0);
			}
		}


		private void Find(string textToFind, int initialPosition)
		{
			var index = this.txtOutput.Text.IndexOf(textToFind, initialPosition);
			var occurrenceCount = this.txtOutput.Text.CountSubstring(textToFind);
			this.lblOccurrences.Text = occurrenceCount.ToString();

			if (index >= 0)
			{


				this.txtOutput.SelectionStart = index;
				this.txtOutput.SelectionLength = textToFind.Length;
				this.txtOutput.ScrollToCaret();

				var lineNumber = this.txtOutput.GetLineFromCharIndex(index);
				var firstCharIndexOfLine = this.txtOutput.GetFirstCharIndexFromLine(lineNumber);
				var col = index - firstCharIndexOfLine;
				this.sPosition.Text = $"Row: {lineNumber}, Col: {col}";
			}
			else
			{
				if (initialPosition > 0)
				{
					Find(textToFind, 0);
					return;
				}

				this.txtOutput.SelectionStart = 0;
				this.txtOutput.SelectionLength = 0;
				this.txtOutput.ScrollToCaret();
				this.sPosition.Text = "...";
			}
		}
	}
}
