using Greg.Xrm.Async;
using Greg.Xrm.Logging;
using Greg.Xrm.ModernThemeBuilder.Model;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Greg.Xrm.ModernThemeBuilder.Views
{
	public partial class SolutionDialog : Form
	{
		private readonly ILog log;

		public SolutionDialog(IOrganizationService crm, IAsyncJobScheduler scheduler, ILog log)
		{
			InitializeComponent();
			this.Load += this.OnDialogLoad;
			this.Crm = crm;
			this.Scheduler = scheduler;
			this.log = log;

			this.grid.AutoGenerateColumns = false;
			this.grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.grid.MultiSelect = false;
		}

		public IOrganizationService Crm { get; }
		public IAsyncJobScheduler Scheduler { get; }

		private void OnDialogLoad(object sender, System.EventArgs e)
		{
			this.Scheduler.Enqueue(new XrmToolBox.Extensibility.WorkAsyncInfo
			{
				Work = LoadSolutions,
				Message = "Loading solutions...",
				PostWorkCallBack = OnSolutionsLoaded,
			});
		}

		private void LoadSolutions(BackgroundWorker worker, DoWorkEventArgs args)
		{
			this.log.Debug("Retriving the list of unmanaged solutions from the current environment...");
			var solutionRepository = Solution.GetRepository(this.Crm);
			var solutionList = solutionRepository.GetUnmanagedSolutions();
			this.log.Debug($"Found {solutionList.Count} unmanaged solutions.");

			args.Result = solutionList;
		}

		private void OnSolutionsLoaded(RunWorkerCompletedEventArgs args)
		{
			if (args.Error != null)
			{
				this.log.Error(args.Error.Message, args.Error);
				MessageBox.Show(args.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var solutionList = args.Result as List<Solution>;
			this.grid.DataSource = solutionList;

			this.txtSearch.Enabled = true;
			this.btnSearch.Enabled = true;
		}





		public Solution SelectedSolution
		{
			get
			{
				if (this.grid.SelectedRows.Count == 0) return null;
				var selectedRow = this.grid.SelectedRows[0];
				return selectedRow.DataBoundItem as Solution;
			}
		}

		private void OnCancelClick(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void OnOkClick(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void OnCellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (this.SelectedSolution == null) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void OnSelectionChanged(object sender, System.EventArgs e)
		{
			this.btnOk.Enabled = this.SelectedSolution != null;
			this.btnSearch.Enabled = this.SelectedSolution != null;
			this.txtSearch.Enabled = this.SelectedSolution != null;
		}

		private void OnSearchClick(object sender, System.EventArgs e)
		{
			this.grid.ClearSelection();

			if (string.IsNullOrWhiteSpace(this.txtSearch.Text))
			{
				return;
			}

			var solutionName = this.txtSearch.Text.Trim().ToLowerInvariant();
			try
			{
				foreach (DataGridViewRow row in this.grid.Rows)
				{
					var solution = row.DataBoundItem as Solution;
					if (solution == null) continue;

					if (solution.friendlyname.IndexOf(solutionName, System.StringComparison.OrdinalIgnoreCase) > -1)
					{
						row.Selected = true;
						return;
					}
				}
			}
			finally
			{
				this.txtSearch.Focus();
			}
		}

		private void OnSearchKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				this.OnSearchClick(sender, e);
			}
		}
	}
}
