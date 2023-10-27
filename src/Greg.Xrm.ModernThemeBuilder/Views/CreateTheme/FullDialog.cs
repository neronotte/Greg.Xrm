using Greg.Xrm.Async;
using Greg.Xrm.Logging;
using Greg.Xrm.ModernThemeBuilder.Model;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Greg.Xrm.ModernThemeBuilder.Views.CreateTheme
{
	public partial class FullDialog : Form
	{
		private readonly ILog log;

		public FullDialog(IOrganizationService crm, IAsyncJobScheduler scheduler, ILog log)
		{
			InitializeComponent();
			this.Load += this.OnDialogLoad;
			this.Crm = crm;
			this.Scheduler = scheduler;
			this.log = log;

			this.grid.AutoGenerateColumns = false;
			this.grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.grid.MultiSelect = false;


			this.lblPrefix.Text = "_";
			this.txtName.Text = "/themes/";
		}



		public IOrganizationService Crm { get; }
		public IAsyncJobScheduler Scheduler { get; }

		public string ThemeName { get; private set; }

		public Solution SelectedSolution
		{
			get
			{
				if (this.grid.SelectedRows.Count == 0) return null;
				var selectedRow = this.grid.SelectedRows[0];
				return selectedRow.DataBoundItem as Solution;
			}
		}



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

		private void OnSelectionChanged(object sender, System.EventArgs e)
		{
			this.btnSearch.Enabled = this.SelectedSolution != null;
			this.txtSearch.Enabled = this.SelectedSolution != null;
			this.lblPrefix.Text = (this.SelectedSolution?.publisheridprefix ?? string.Empty) + "_";
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





		private void OnCancelClick(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void OnOkClick(object sender, System.EventArgs e)
		{
			this.btnOk.Enabled = false;
			this.txtName.Enabled = false;

			if (this.SelectedSolution == null) {

				errorProvider.SetError(this.grid, "Please select a solution");

				this.btnOk.Enabled = true;
				this.txtName.Enabled = true;
				return;
			}

			var name = this.txtName.Text;
			if (string.IsNullOrWhiteSpace(name))
			{
				errorProvider.SetError(this.txtName, "Please enter a name");

				this.btnOk.Enabled = true;
				this.txtName.Enabled = true;
				return;
			}

			var regex = new Regex("^[a-zA-Z0-9_/.]+[a-zA-Z0-9_]$");
			if (!regex.IsMatch(name))
			{
				errorProvider.SetError(this.txtName, "Name can contain only numbers, letters, underscores or dot, and cannot end with dot.");

				this.btnOk.Enabled = true;
				this.txtName.Enabled = true;
				return;
			}


			var fullName = this.lblPrefix.Text + name + this.lblSuffix.Text;
			this.ThemeName = fullName;

			this.Scheduler.Enqueue(new XrmToolBox.Extensibility.WorkAsyncInfo
			{
				Work = CheckWebResourcePresence,
				Message = "Checking if the given name is a duplicate...",
				AsyncArgument = fullName,
				PostWorkCallBack = OnCheckWebResourcePresenceCompleted,
			});
		}

		private void CheckWebResourcePresence(BackgroundWorker worker, DoWorkEventArgs args)
		{
			var fullName = args.Argument as string;
			var webResourceRepository = WebResource.GetRepository(this.Crm);
			var exists = webResourceRepository.Exists(fullName);
			args.Result = exists;
		}

		private void OnCheckWebResourcePresenceCompleted(RunWorkerCompletedEventArgs args)
		{
			if (args.Error != null)
			{
				MessageBox.Show("An error occurred while checking for duplicates: " + args.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

				this.btnOk.Enabled = true;
				this.txtName.Enabled = true;
				return;
			}

			var exists = (bool)args.Result;
			if (exists)
			{
				errorProvider.SetError(this.txtName, "A webresource with the given name is already in the system.");

				this.btnOk.Enabled = true;
				this.txtName.Enabled = true;
				return;
			}

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

	}
}
