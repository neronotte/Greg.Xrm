namespace Greg.Xrm.RoleEditor.Views
{
	partial class SettingsDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
			this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.pnlFooter = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.tableLayout2 = new System.Windows.Forms.TableLayoutPanel();
			this.tabs = new System.Windows.Forms.TabControl();
			this.tabUX = new System.Windows.Forms.TabPage();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.chkHide2 = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.chkHide1 = new System.Windows.Forms.CheckBox();
			this.chkUseLegacyIcons = new System.Windows.Forms.CheckBox();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.btnResetTableGrouping = new System.Windows.Forms.Button();
			this.txtTableGrouping = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.btnResetMiscGrouping = new System.Windows.Forms.Button();
			this.txtMiscGrouping = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tabSnippets = new System.Windows.Forms.TabPage();
			this.privilegeSnippetView = new BrightIdeasSoftware.ObjectListView();
			this.cSnippetIndex = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cCreate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cRead = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cWrite = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cDelete = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cAppend = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cAppendTo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cAssign = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cShare = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.notificationPanel1 = new Greg.Xrm.Core.Views.NotificationPanel();
			this.imagesOld = new System.Windows.Forms.ImageList(this.components);
			this.imagesNew = new System.Windows.Forms.ImageList(this.components);
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mSet0 = new System.Windows.Forms.ToolStripMenuItem();
			this.mSet1 = new System.Windows.Forms.ToolStripMenuItem();
			this.mSet2 = new System.Windows.Forms.ToolStripMenuItem();
			this.mSet3 = new System.Windows.Forms.ToolStripMenuItem();
			this.mSet4 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.mSetNull = new System.Windows.Forms.ToolStripMenuItem();
			this.tSnipReset = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.tableLayout.SuspendLayout();
			this.pnlHeader.SuspendLayout();
			this.pnlFooter.SuspendLayout();
			this.tableLayout2.SuspendLayout();
			this.tabs.SuspendLayout();
			this.tabUX.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabSnippets.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.privilegeSnippetView)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.contextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayout
			// 
			this.tableLayout.ColumnCount = 1;
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayout.Controls.Add(this.pnlHeader, 0, 0);
			this.tableLayout.Controls.Add(this.pnlFooter, 0, 3);
			this.tableLayout.Controls.Add(this.tableLayout2, 0, 2);
			this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayout.Location = new System.Drawing.Point(0, 0);
			this.tableLayout.Name = "tableLayout";
			this.tableLayout.RowCount = 4;
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.tableLayout.Size = new System.Drawing.Size(980, 471);
			this.tableLayout.TabIndex = 0;
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.Color.White;
			this.pnlHeader.Controls.Add(this.label2);
			this.pnlHeader.Controls.Add(this.label1);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Margin = new System.Windows.Forms.Padding(0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(980, 60);
			this.pnlHeader.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(346, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Configure here your preferences about the tool look && feel and behavior.";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(157, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "_n.RoleEditor Settings";
			// 
			// pnlFooter
			// 
			this.pnlFooter.Controls.Add(this.btnCancel);
			this.pnlFooter.Controls.Add(this.btnOk);
			this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlFooter.Location = new System.Drawing.Point(0, 411);
			this.pnlFooter.Margin = new System.Windows.Forms.Padding(0);
			this.pnlFooter.Name = "pnlFooter";
			this.pnlFooter.Size = new System.Drawing.Size(980, 60);
			this.pnlFooter.TabIndex = 1;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(796, 15);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 30);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Location = new System.Drawing.Point(886, 15);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(80, 30);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// tableLayout2
			// 
			this.tableLayout2.ColumnCount = 1;
			this.tableLayout2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayout2.Controls.Add(this.tabs, 0, 1);
			this.tableLayout2.Controls.Add(this.notificationPanel1, 0, 0);
			this.tableLayout2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayout2.Location = new System.Drawing.Point(0, 60);
			this.tableLayout2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayout2.Name = "tableLayout2";
			this.tableLayout2.RowCount = 2;
			this.tableLayout2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayout2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayout2.Size = new System.Drawing.Size(980, 351);
			this.tableLayout2.TabIndex = 2;
			// 
			// tabs
			// 
			this.tabs.Controls.Add(this.tabUX);
			this.tabs.Controls.Add(this.tabPage1);
			this.tabs.Controls.Add(this.tabSnippets);
			this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabs.Location = new System.Drawing.Point(3, 3);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(974, 345);
			this.tabs.TabIndex = 5;
			// 
			// tabUX
			// 
			this.tabUX.AutoScroll = true;
			this.tabUX.Controls.Add(this.label6);
			this.tabUX.Controls.Add(this.label7);
			this.tabUX.Controls.Add(this.chkHide2);
			this.tabUX.Controls.Add(this.label5);
			this.tabUX.Controls.Add(this.label4);
			this.tabUX.Controls.Add(this.chkHide1);
			this.tabUX.Controls.Add(this.chkUseLegacyIcons);
			this.tabUX.Location = new System.Drawing.Point(4, 22);
			this.tabUX.Name = "tabUX";
			this.tabUX.Padding = new System.Windows.Forms.Padding(10);
			this.tabUX.Size = new System.Drawing.Size(966, 319);
			this.tabUX.TabIndex = 1;
			this.tabUX.Text = "Layout preferences";
			this.tabUX.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(14, 148);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(272, 13);
			this.label6.TabIndex = 6;
			this.label6.Text = "Configure the preferences for the role editor panel.";
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(15, 128);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(64, 13);
			this.label7.TabIndex = 5;
			this.label7.Text = "Role Editor";
			// 
			// chkHide2
			// 
			this.chkHide2.AutoSize = true;
			this.chkHide2.Location = new System.Drawing.Point(16, 83);
			this.chkHide2.Name = "chkHide2";
			this.chkHide2.Size = new System.Drawing.Size(120, 17);
			this.chkHide2.TabIndex = 4;
			this.chkHide2.Text = "Hide managed roles";
			this.chkHide2.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(13, 34);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(525, 13);
			this.label5.TabIndex = 3;
			this.label5.Text = "Configure which roles are visible by default. It requires to close and reopen the" +
    " plugin to take effect.";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(14, 14);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(75, 13);
			this.label4.TabIndex = 2;
			this.label4.Text = "Role Browser";
			// 
			// chkHide1
			// 
			this.chkHide1.AutoSize = true;
			this.chkHide1.Location = new System.Drawing.Point(16, 60);
			this.chkHide1.Name = "chkHide1";
			this.chkHide1.Size = new System.Drawing.Size(155, 17);
			this.chkHide1.TabIndex = 1;
			this.chkHide1.Text = "Hide not customizable roles";
			this.chkHide1.UseVisualStyleBackColor = true;
			// 
			// chkUseLegacyIcons
			// 
			this.chkUseLegacyIcons.AutoSize = true;
			this.chkUseLegacyIcons.Location = new System.Drawing.Point(16, 174);
			this.chkUseLegacyIcons.Name = "chkUseLegacyIcons";
			this.chkUseLegacyIcons.Size = new System.Drawing.Size(149, 17);
			this.chkUseLegacyIcons.TabIndex = 0;
			this.chkUseLegacyIcons.Text = "Use legacy privilege icons";
			this.chkUseLegacyIcons.UseVisualStyleBackColor = true;
			// 
			// tabPage1
			// 
			this.tabPage1.AutoScroll = true;
			this.tabPage1.Controls.Add(this.btnResetTableGrouping);
			this.tabPage1.Controls.Add(this.txtTableGrouping);
			this.tabPage1.Controls.Add(this.label8);
			this.tabPage1.Controls.Add(this.btnResetMiscGrouping);
			this.tabPage1.Controls.Add(this.txtMiscGrouping);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(10);
			this.tabPage1.Size = new System.Drawing.Size(966, 319);
			this.tabPage1.TabIndex = 2;
			this.tabPage1.Text = "Privilege grouping";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// btnResetTableGrouping
			// 
			this.btnResetTableGrouping.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnResetTableGrouping.Location = new System.Drawing.Point(805, 451);
			this.btnResetTableGrouping.Name = "btnResetTableGrouping";
			this.btnResetTableGrouping.Size = new System.Drawing.Size(80, 30);
			this.btnResetTableGrouping.TabIndex = 5;
			this.btnResetTableGrouping.Text = "Reset";
			this.btnResetTableGrouping.UseVisualStyleBackColor = true;
			// 
			// txtTableGrouping
			// 
			this.txtTableGrouping.AcceptsTab = true;
			this.txtTableGrouping.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTableGrouping.Font = new System.Drawing.Font("Cascadia Code", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTableGrouping.Location = new System.Drawing.Point(13, 288);
			this.txtTableGrouping.Multiline = true;
			this.txtTableGrouping.Name = "txtTableGrouping";
			this.txtTableGrouping.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtTableGrouping.Size = new System.Drawing.Size(872, 157);
			this.txtTableGrouping.TabIndex = 4;
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label8.AutoEllipsis = true;
			this.label8.Location = new System.Drawing.Point(13, 251);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(872, 34);
			this.label8.TabIndex = 3;
			this.label8.Text = "The JSON dictionary below allows to configure the structure of the table privileg" +
    "es list.\r\nAny table not indicated in the configuration below will be put in a ge" +
    "neric \"General\" group:\r\n";
			// 
			// btnResetMiscGrouping
			// 
			this.btnResetMiscGrouping.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnResetMiscGrouping.Location = new System.Drawing.Point(805, 210);
			this.btnResetMiscGrouping.Name = "btnResetMiscGrouping";
			this.btnResetMiscGrouping.Size = new System.Drawing.Size(80, 30);
			this.btnResetMiscGrouping.TabIndex = 2;
			this.btnResetMiscGrouping.Text = "Reset";
			this.btnResetMiscGrouping.UseVisualStyleBackColor = true;
			// 
			// txtMiscGrouping
			// 
			this.txtMiscGrouping.AcceptsTab = true;
			this.txtMiscGrouping.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMiscGrouping.Font = new System.Drawing.Font("Cascadia Code", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtMiscGrouping.Location = new System.Drawing.Point(13, 47);
			this.txtMiscGrouping.Multiline = true;
			this.txtMiscGrouping.Name = "txtMiscGrouping";
			this.txtMiscGrouping.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtMiscGrouping.Size = new System.Drawing.Size(872, 157);
			this.txtMiscGrouping.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoEllipsis = true;
			this.label3.Location = new System.Drawing.Point(13, 10);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(872, 34);
			this.label3.TabIndex = 0;
			this.label3.Text = resources.GetString("label3.Text");
			// 
			// tabSnippets
			// 
			this.tabSnippets.Controls.Add(this.privilegeSnippetView);
			this.tabSnippets.Controls.Add(this.toolStrip1);
			this.tabSnippets.Location = new System.Drawing.Point(4, 22);
			this.tabSnippets.Name = "tabSnippets";
			this.tabSnippets.Padding = new System.Windows.Forms.Padding(3);
			this.tabSnippets.Size = new System.Drawing.Size(966, 319);
			this.tabSnippets.TabIndex = 3;
			this.tabSnippets.Text = "Privilege snippets";
			this.tabSnippets.UseVisualStyleBackColor = true;
			// 
			// privilegeSnippetView
			// 
			this.privilegeSnippetView.AllColumns.Add(this.cSnippetIndex);
			this.privilegeSnippetView.AllColumns.Add(this.cCreate);
			this.privilegeSnippetView.AllColumns.Add(this.cRead);
			this.privilegeSnippetView.AllColumns.Add(this.cWrite);
			this.privilegeSnippetView.AllColumns.Add(this.cDelete);
			this.privilegeSnippetView.AllColumns.Add(this.cAppend);
			this.privilegeSnippetView.AllColumns.Add(this.cAppendTo);
			this.privilegeSnippetView.AllColumns.Add(this.cAssign);
			this.privilegeSnippetView.AllColumns.Add(this.cShare);
			this.privilegeSnippetView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cSnippetIndex,
            this.cCreate,
            this.cRead,
            this.cWrite,
            this.cDelete,
            this.cAppend,
            this.cAppendTo,
            this.cAssign,
            this.cShare});
			this.privilegeSnippetView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.privilegeSnippetView.GridLines = true;
			this.privilegeSnippetView.HideSelection = false;
			this.privilegeSnippetView.Location = new System.Drawing.Point(3, 28);
			this.privilegeSnippetView.Name = "privilegeSnippetView";
			this.privilegeSnippetView.Size = new System.Drawing.Size(960, 288);
			this.privilegeSnippetView.TabIndex = 1;
			this.privilegeSnippetView.UseCompatibleStateImageBehavior = false;
			this.privilegeSnippetView.View = System.Windows.Forms.View.Details;
			// 
			// cSnippetIndex
			// 
			this.cSnippetIndex.AspectName = "Index";
			this.cSnippetIndex.CellPadding = null;
			this.cSnippetIndex.Text = "Keyboard Shortcut";
			this.cSnippetIndex.Width = 120;
			// 
			// cCreate
			// 
			this.cCreate.AspectName = "Create";
			this.cCreate.CellPadding = null;
			this.cCreate.Text = "Create";
			// 
			// cRead
			// 
			this.cRead.AspectName = "Read";
			this.cRead.CellPadding = null;
			this.cRead.Text = "Read";
			// 
			// cWrite
			// 
			this.cWrite.AspectName = "Write";
			this.cWrite.CellPadding = null;
			this.cWrite.Text = "Write";
			// 
			// cDelete
			// 
			this.cDelete.AspectName = "Delete";
			this.cDelete.CellPadding = null;
			this.cDelete.Text = "Delete";
			// 
			// cAppend
			// 
			this.cAppend.AspectName = "Append";
			this.cAppend.CellPadding = null;
			this.cAppend.Text = "Append";
			// 
			// cAppendTo
			// 
			this.cAppendTo.AspectName = "AppendTo";
			this.cAppendTo.CellPadding = null;
			this.cAppendTo.Text = "Append To";
			// 
			// cAssign
			// 
			this.cAssign.AspectName = "Assign";
			this.cAssign.CellPadding = null;
			this.cAssign.Text = "Assign";
			// 
			// cShare
			// 
			this.cShare.AspectName = "Share";
			this.cShare.CellPadding = null;
			this.cShare.Text = "Share";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSnipReset,
            this.toolStripSeparator1,
            this.toolStripLabel1});
			this.toolStrip1.Location = new System.Drawing.Point(3, 3);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(960, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// notificationPanel1
			// 
			this.notificationPanel1.AutoSize = true;
			this.notificationPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.notificationPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.notificationPanel1.Location = new System.Drawing.Point(0, 0);
			this.notificationPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.notificationPanel1.Name = "notificationPanel1";
			this.notificationPanel1.Size = new System.Drawing.Size(980, 1);
			this.notificationPanel1.TabIndex = 6;
			// 
			// imagesOld
			// 
			this.imagesOld.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagesOld.ImageStream")));
			this.imagesOld.TransparentColor = System.Drawing.Color.Transparent;
			this.imagesOld.Images.SetKeyName(0, "none");
			this.imagesOld.Images.SetKeyName(1, "user");
			this.imagesOld.Images.SetKeyName(2, "businessunit");
			this.imagesOld.Images.SetKeyName(3, "parentchild");
			this.imagesOld.Images.SetKeyName(4, "organization");
			// 
			// imagesNew
			// 
			this.imagesNew.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagesNew.ImageStream")));
			this.imagesNew.TransparentColor = System.Drawing.Color.Transparent;
			this.imagesNew.Images.SetKeyName(0, "none");
			this.imagesNew.Images.SetKeyName(1, "user");
			this.imagesNew.Images.SetKeyName(2, "businessunit");
			this.imagesNew.Images.SetKeyName(3, "parentchild");
			this.imagesNew.Images.SetKeyName(4, "organization");
			// 
			// contextMenu
			// 
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mSet0,
            this.mSet1,
            this.mSet2,
            this.mSet3,
            this.mSet4,
            this.toolStripMenuItem1,
            this.mSetNull});
			this.contextMenu.Name = "contextMenu";
			this.contextMenu.Size = new System.Drawing.Size(230, 142);
			// 
			// mSet0
			// 
			this.mSet0.Name = "mSet0";
			this.mSet0.Size = new System.Drawing.Size(229, 22);
			this.mSet0.Tag = "0";
			this.mSet0.Text = "None";
			// 
			// mSet1
			// 
			this.mSet1.Name = "mSet1";
			this.mSet1.Size = new System.Drawing.Size(229, 22);
			this.mSet1.Tag = "1";
			this.mSet1.Text = "User";
			// 
			// mSet2
			// 
			this.mSet2.Name = "mSet2";
			this.mSet2.Size = new System.Drawing.Size(229, 22);
			this.mSet2.Tag = "2";
			this.mSet2.Text = "Business Unit";
			// 
			// mSet3
			// 
			this.mSet3.Name = "mSet3";
			this.mSet3.Size = new System.Drawing.Size(229, 22);
			this.mSet3.Tag = "3";
			this.mSet3.Text = "Parent-Child Business Unit";
			// 
			// mSet4
			// 
			this.mSet4.Name = "mSet4";
			this.mSet4.Size = new System.Drawing.Size(229, 22);
			this.mSet4.Tag = "4";
			this.mSet4.Text = "Organization";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(226, 6);
			// 
			// mSetNull
			// 
			this.mSetNull.Name = "mSetNull";
			this.mSetNull.Size = new System.Drawing.Size(229, 22);
			this.mSetNull.Text = "Remove privilege assignment";
			// 
			// tSnipReset
			// 
			this.tSnipReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tSnipReset.Image = ((System.Drawing.Image)(resources.GetObject("tSnipReset.Image")));
			this.tSnipReset.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tSnipReset.Name = "tSnipReset";
			this.tSnipReset.Size = new System.Drawing.Size(39, 22);
			this.tSnipReset.Text = "Reset";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.ForeColor = System.Drawing.Color.Gray;
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(867, 22);
			this.toolStripLabel1.Text = "Custom snippets from 4 to 9 can be freely updated. You can also type CTRL+SHIFT+d" +
    "igit in the Role Editor to create a snippet from the selected table configuratio" +
    "n.";
			// 
			// SettingsDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(980, 471);
			this.Controls.Add(this.tableLayout);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsDialog";
			this.Text = "_n.RoleEditor Settings";
			this.tableLayout.ResumeLayout(false);
			this.pnlHeader.ResumeLayout(false);
			this.pnlHeader.PerformLayout();
			this.pnlFooter.ResumeLayout(false);
			this.tableLayout2.ResumeLayout(false);
			this.tableLayout2.PerformLayout();
			this.tabs.ResumeLayout(false);
			this.tabUX.ResumeLayout(false);
			this.tabUX.PerformLayout();
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabSnippets.ResumeLayout(false);
			this.tabSnippets.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.privilegeSnippetView)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.contextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayout;
		private System.Windows.Forms.Panel pnlHeader;
		private System.Windows.Forms.Panel pnlFooter;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TableLayoutPanel tableLayout2;
		private System.Windows.Forms.TabControl tabs;
		private System.Windows.Forms.TabPage tabUX;
		private System.Windows.Forms.CheckBox chkUseLegacyIcons;
		private System.Windows.Forms.TabPage tabPage1;
		private Core.Views.NotificationPanel notificationPanel1;
		private System.Windows.Forms.TextBox txtMiscGrouping;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnResetMiscGrouping;
		private System.Windows.Forms.CheckBox chkHide1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox chkHide2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button btnResetTableGrouping;
		private System.Windows.Forms.TextBox txtTableGrouping;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TabPage tabSnippets;
		private BrightIdeasSoftware.ObjectListView privilegeSnippetView;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private BrightIdeasSoftware.OLVColumn cSnippetIndex;
		private BrightIdeasSoftware.OLVColumn cCreate;
		private BrightIdeasSoftware.OLVColumn cRead;
		private BrightIdeasSoftware.OLVColumn cWrite;
		private BrightIdeasSoftware.OLVColumn cDelete;
		private BrightIdeasSoftware.OLVColumn cAppend;
		private BrightIdeasSoftware.OLVColumn cAppendTo;
		private BrightIdeasSoftware.OLVColumn cAssign;
		private BrightIdeasSoftware.OLVColumn cShare;
		private System.Windows.Forms.ImageList imagesOld;
		private System.Windows.Forms.ImageList imagesNew;
		private System.Windows.Forms.ContextMenuStrip contextMenu;
		private System.Windows.Forms.ToolStripMenuItem mSet0;
		private System.Windows.Forms.ToolStripMenuItem mSet1;
		private System.Windows.Forms.ToolStripMenuItem mSet2;
		private System.Windows.Forms.ToolStripMenuItem mSet3;
		private System.Windows.Forms.ToolStripMenuItem mSet4;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem mSetNull;
		private System.Windows.Forms.ToolStripButton tSnipReset;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
	}
}