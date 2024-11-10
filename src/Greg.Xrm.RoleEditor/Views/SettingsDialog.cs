using Greg.Xrm.Messaging;
using System;
using System.Windows.Forms;

namespace Greg.Xrm.RoleEditor.Views
{
	public partial class SettingsDialog : Form
	{
		private readonly SettingsViewModel viewModel;
		public SettingsDialog(IMessenger messenger, ISettingsProvider<Settings> settingsProvider)
		{
			InitializeComponent();

			this.viewModel = new SettingsViewModel(messenger, settingsProvider);

			this.chkHide1.Bind(x => x.Checked, this.viewModel, x => x.HideNotCustomizableRoles);
			this.chkHide2.Bind(x => x.Checked, this.viewModel, x => x.HideManagedRoles);
			this.chkUseLegacyIcons.Bind(x => x.Checked, this.viewModel, x => x.UseLegacyIcons);
			this.txtTableGrouping.Bind(x => x.Text, this.viewModel, x => x.PrivilegeClassificationForTable);
			this.btnResetTableGrouping.BindCommand(() => this.viewModel.ResetPrivilegeClassificationForTableCommand);
			this.txtMiscGrouping.Bind(x => x.Text, this.viewModel, x => x.PrivilegeClassificationForMisc);
			this.btnResetMiscGrouping.BindCommand(() => this.viewModel.ResetPrivilegeClassificationForMiscCommand);


			this.btnOk.BindCommand(() => this.viewModel.ConfirmCommand);
			this.btnCancel.Click += (s, e) => {
				this.DialogResult = DialogResult.Cancel;
				this.Close();
			};
			this.notificationPanel1.Bind(this.viewModel);

			this.viewModel.Close += OnConfirmClose;
		}

		private void OnConfirmClose(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
