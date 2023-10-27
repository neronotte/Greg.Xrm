using Greg.Xrm.Async;
using Greg.Xrm.ModernThemeBuilder.Model;
using Microsoft.Xrm.Sdk;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Greg.Xrm.ModernThemeBuilder.Views.CreateTheme
{
	public partial class LightDialog : Form
	{
		public LightDialog(Solution solution, IOrganizationService crm, IAsyncJobScheduler scheduler)
		{
			InitializeComponent();

			this.lblPrefix.Text = solution.publisheridprefix + "_";
			this.txtName.Text = "/themes/";
			this.Crm = crm;
			this.Scheduler = scheduler;
		}

		public IOrganizationService Crm { get; }
		public IAsyncJobScheduler Scheduler { get; }


		public string ThemeName { get; private set; }

		private void OnCancelClick(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void OnOkClick(object sender, System.EventArgs e)
		{
			this.btnOk.Enabled = false;
			this.txtName.Enabled = false;

			var name= this.txtName.Text;
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
