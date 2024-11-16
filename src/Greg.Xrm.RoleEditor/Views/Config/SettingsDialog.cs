using BrightIdeasSoftware;
using Greg.Xrm.Messaging;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Services.Snippets;
using Greg.Xrm.RoleEditor.Views.Config;
using Greg.Xrm.RoleEditor.Views.Editor;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Greg.Xrm.RoleEditor.Views
{
	public partial class SettingsDialog : Form
	{
		private readonly SettingsViewModel viewModel;
		private readonly ToolStripMenuItem[] contextMenuItems = new ToolStripMenuItem[5];



		public SettingsDialog(
			IMessenger messenger, 
			ISettingsProvider<Settings> settingsProvider,
			IPrivilegeSnippetRepository privilegeSnippetRepository)
		{
			InitializeComponent();

			this.viewModel = new SettingsViewModel(messenger, settingsProvider, privilegeSnippetRepository);


			this.chkHide1.Bind(x => x.Checked, this.viewModel, x => x.HideNotCustomizableRoles);
			this.chkHide2.Bind(x => x.Checked, this.viewModel, x => x.HideManagedRoles);
			this.chkUseLegacyIcons.Bind(x => x.Checked, this.viewModel, x => x.UseLegacyIcons);
			this.chkAutoLoad.Bind(x => x.Checked, this.viewModel, x => x.AutoLoadRolesWhenConnectonChanges);
			this.txtTableGrouping.Bind(x => x.Text, this.viewModel, x => x.PrivilegeClassificationForTable);
			this.btnResetTableGrouping.BindCommand(() => this.viewModel.ResetPrivilegeClassificationForTableCommand);
			this.txtMiscGrouping.Bind(x => x.Text, this.viewModel, x => x.PrivilegeClassificationForMisc);
			this.btnResetMiscGrouping.BindCommand(() => this.viewModel.ResetPrivilegeClassificationForMiscCommand);
			this.chkIsRequestLoggingEnabled.Bind(x => x.Checked, this.viewModel, x => x.IsRequestLoggingEnabled);

			// snippets grid

			this.contextMenuItems[0] = this.mSet0;
			this.contextMenuItems[1] = this.mSet1;
			this.contextMenuItems[2] = this.mSet2;
			this.contextMenuItems[3] = this.mSet3;
			this.contextMenuItems[4] = this.mSet4;
			for (int i = 0; i < this.contextMenuItems.Length; i++)
			{
				this.contextMenuItems[i].Tag = (Level)i;
				this.contextMenuItems[i].Click += OnContextMenuClick;
			}
			this.mSetNull.Click += OnContextMenuClick;

			this.tSnipReset.Click += (s, e) =>
			{
				this.viewModel.ResetSnippets();
				this.privilegeSnippetView.RefreshObjects(this.viewModel.Snippets);
			};

			this.RefreshIcons();
			this.privilegeSnippetView.ShowGroups = false;
			this.privilegeSnippetView.UseCellFormatEvents = true;
			this.privilegeSnippetView.UseCustomSelectionColors = true;
			this.privilegeSnippetView.HighlightBackgroundColor = Color.FromArgb(240, 240, 240);
			this.privilegeSnippetView.HighlightForegroundColor = Color.Black;
			this.privilegeSnippetView.CellRightClick += OnCellRightClick;
			this.privilegeSnippetView.CellClick += OnCellClick;
			this.privilegeSnippetView.FormatCell += (s, e) =>
			{
				if (!(e.Model is PrivilegeSnippetViewModel snippet)) return;
				if (snippet.IsEditable) return;
				
				e.SubItem.BackColor = Color.FromArgb(245, 245, 245);
				e.SubItem.ForeColor = Color.Black;
			};

			this.cSnippetIndex.TextAlign = HorizontalAlignment.Center;
			SetColumn(this.cCreate, PrivilegeType.Create);
			SetColumn(this.cRead, PrivilegeType.Read);
			SetColumn(this.cWrite, PrivilegeType.Write);
			SetColumn(this.cDelete, PrivilegeType.Delete);
			SetColumn(this.cAppend, PrivilegeType.Append);
			SetColumn(this.cAppendTo, PrivilegeType.AppendTo);
			SetColumn(this.cAssign, PrivilegeType.Assign);
			SetColumn(this.cShare, PrivilegeType.Share);

			this.privilegeSnippetView.SetObjects(this.viewModel.Snippets);





			// footer
			this.btnOk.BindCommand(() => this.viewModel.ConfirmCommand);
			this.btnCancel.Click += (s, e) => {
				this.DialogResult = DialogResult.Cancel;
				this.Close();
			};
			this.notificationPanel1.Bind(this.viewModel);

			this.viewModel.Close += OnConfirmClose;

			this.viewModel.PropertyChanged += (s, e) => 
			{	
				if (e.PropertyName == nameof(this.viewModel.UseLegacyIcons))
				{
					RefreshIcons();
				}
			};
		}

		private static void SetColumn(OLVColumn column, PrivilegeType privilegeType)
		{
			column.Width = 60;
			column.TextAlign = HorizontalAlignment.Center;
			column.AspectToStringConverter = x => string.Empty;
			column.Sortable = false;
			column.ImageGetter = delegate (object x)
			{
				if (!(x is PrivilegeSnippetViewModel snippet)) return null;

				return (int?)snippet.Get(privilegeType);
			};
			column.Tag = privilegeType;
		}



		private void RefreshIcons()
		{
			var imageList = this.viewModel.UseLegacyIcons ? this.imagesOld : this.imagesNew;

			this.privilegeSnippetView.SmallImageList = imageList;
			for (int i = 0; i < this.contextMenuItems.Length; i++)
			{
				this.contextMenuItems[i].Image = imageList.Images[i];
			}
		}

		private void OnCellClick(object sender, CellClickEventArgs e)
		{
			if (e.Column == null) return;
			if (!(e.Column.Tag is PrivilegeType privilegeType)) return;
			if (!(e.Model is PrivilegeSnippetViewModel snippet)) return;
			if (!snippet.IsEditable) return;

			snippet.Increase(privilegeType);
			this.privilegeSnippetView.RefreshObject(snippet);
		}


		private void OnCellRightClick(object sender, CellRightClickEventArgs e)
		{
			if (e.Column == null) return;
			if (e.ColumnIndex == 0) return;
			if (e.Column.Tag == null)
			{
				this.contextMenu.Tag = null;
				e.Handled = true;
				return;
			}

			if (!(e.Model is PrivilegeSnippetViewModel snippet)) return;
			if (!snippet.IsEditable) return;

			this.contextMenu.Tag = e;
			this.contextMenu.Show(this.privilegeSnippetView, e.Location);
			e.Handled = true;
		}

		private void OnContextMenuClick(object sender, EventArgs e1)
		{
			var e = this.contextMenu.Tag as CellRightClickEventArgs;
			if (e == null) return;


			var menu = (ToolStripMenuItem)sender;
			var selectedLevel = (Level?)menu.Tag;

			if (!(e.Model is PrivilegeSnippetViewModel snippet)) return;
			
			var column = e.Column;
			if (column == null) return;

			if (!(column.Tag is PrivilegeType privilegeType)) return;

			snippet.Set(privilegeType, selectedLevel);
			this.privilegeSnippetView.RefreshObject(snippet);
			e.Handled = true;
			
		}


		private void OnConfirmClose(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
