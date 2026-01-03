namespace Greg.Xrm.RoleEditor.Views.Editor
{
	partial class RoleEditorView
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoleEditorView));
			this.privilegeImagesOld = new System.Windows.Forms.ImageList(this.components);
			this.tools = new System.Windows.Forms.ToolStrip();
			this.tSave = new Greg.Xrm.Views.ToolStripBindableButton();
			this.tAddToSolution = new Greg.Xrm.Views.ToolStripBindableButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.tExportExcel = new Greg.Xrm.Views.ToolStripBindableButton();
			this.tExportMarkdown = new Greg.Xrm.Views.ToolStripBindableButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tImportExcel = new Greg.Xrm.Views.ToolStripBindableButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
			this.tShowOnlyAssignedPrivileges = new Greg.Xrm.Views.ToolStripBindableButton();
			this.tShowAllPrivileges = new Greg.Xrm.Views.ToolStripBindableButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tCopyAllPrivileges = new Greg.Xrm.Views.ToolStripBindableButton();
			this.tPasteAllPrivileges = new Greg.Xrm.Views.ToolStripBindableButton();
			this.tabs = new System.Windows.Forms.TabControl();
			this.tabTables = new System.Windows.Forms.TabPage();
			this.treeTables = new BrightIdeasSoftware.TreeListView();
			this.cName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cCreate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cRead = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cWrite = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cDelete = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cAppend = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cAppendTo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cAssign = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cShare = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cLogicalName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.privilegeImagesNew = new System.Windows.Forms.ImageList(this.components);
			this.tools2 = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
			this.tSearchTableText = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
			this.tabMisc = new System.Windows.Forms.TabPage();
			this.treeMisc = new BrightIdeasSoftware.TreeListView();
			this.cMiscName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cMiscValue = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cMiscTooltip = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.tools3 = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
			this.txtSearchMisc = new System.Windows.Forms.ToolStripTextBox();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtRoleName = new System.Windows.Forms.TextBox();
			this.txtRoleBusinessUnit = new System.Windows.Forms.TextBox();
			this.txtRoleDescription = new System.Windows.Forms.TextBox();
			this.cmbRoleInheritance = new System.Windows.Forms.ComboBox();
			this.btnRoleBusinessUnitLookup = new System.Windows.Forms.Button();
			this.tabChangeSummary = new System.Windows.Forms.TabPage();
			this.olvChangeSummary = new BrightIdeasSoftware.ObjectListView();
			this.cSummaryText = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.notificationPanel = new Greg.Xrm.Core.Views.NotificationPanel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mSet0 = new System.Windows.Forms.ToolStripMenuItem();
			this.mSet1 = new System.Windows.Forms.ToolStripMenuItem();
			this.mSet2 = new System.Windows.Forms.ToolStripMenuItem();
			this.mSet3 = new System.Windows.Forms.ToolStripMenuItem();
			this.mSet4 = new System.Windows.Forms.ToolStripMenuItem();
			this.tools.SuspendLayout();
			this.tabs.SuspendLayout();
			this.tabTables.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeTables)).BeginInit();
			this.tools2.SuspendLayout();
			this.tabMisc.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeMisc)).BeginInit();
			this.tools3.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabChangeSummary.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.olvChangeSummary)).BeginInit();
			this.contextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// privilegeImagesOld
			// 
			this.privilegeImagesOld.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("privilegeImagesOld.ImageStream")));
			this.privilegeImagesOld.TransparentColor = System.Drawing.Color.Transparent;
			this.privilegeImagesOld.Images.SetKeyName(0, "none");
			this.privilegeImagesOld.Images.SetKeyName(1, "user");
			this.privilegeImagesOld.Images.SetKeyName(2, "businessunit");
			this.privilegeImagesOld.Images.SetKeyName(3, "parentchild");
			this.privilegeImagesOld.Images.SetKeyName(4, "organization");
			this.privilegeImagesOld.Images.SetKeyName(5, "add");
			this.privilegeImagesOld.Images.SetKeyName(6, "remove");
			this.privilegeImagesOld.Images.SetKeyName(7, "replace");
			// 
			// tools
			// 
			this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSave,
            this.tAddToSolution,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.tExportExcel,
            this.tExportMarkdown,
            this.toolStripSeparator3,
            this.tImportExcel,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.tShowOnlyAssignedPrivileges,
            this.tShowAllPrivileges,
            this.toolStripSeparator4,
            this.tCopyAllPrivileges,
            this.tPasteAllPrivileges});
			this.tools.Location = new System.Drawing.Point(0, 0);
			this.tools.Name = "tools";
			this.tools.Size = new System.Drawing.Size(1004, 25);
			this.tools.TabIndex = 3;
			this.tools.Text = "toolStrip1";
			// 
			// tSave
			// 
			this.tSave.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.disk;
			this.tSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tSave.Name = "tSave";
			this.tSave.Size = new System.Drawing.Size(51, 22);
			this.tSave.Text = "Save";
			// 
			// tAddToSolution
			// 
			this.tAddToSolution.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.package_add;
			this.tAddToSolution.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tAddToSolution.Name = "tAddToSolution";
			this.tAddToSolution.Size = new System.Drawing.Size(141, 22);
			this.tAddToSolution.Text = "Add role to solution...";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(43, 22);
			this.toolStripLabel1.Text = "Export:";
			// 
			// tExportExcel
			// 
			this.tExportExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tExportExcel.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.page_white_excel;
			this.tExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tExportExcel.Name = "tExportExcel";
			this.tExportExcel.Size = new System.Drawing.Size(23, 22);
			this.tExportExcel.Text = "Excel";
			// 
			// tExportMarkdown
			// 
			this.tExportMarkdown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tExportMarkdown.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.page_white_text;
			this.tExportMarkdown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tExportMarkdown.Name = "tExportMarkdown";
			this.tExportMarkdown.Size = new System.Drawing.Size(23, 22);
			this.tExportMarkdown.Text = "Markdown";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// tImportExcel
			// 
			this.tImportExcel.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.page_white_excel;
			this.tImportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tImportExcel.Name = "tImportExcel";
			this.tImportExcel.Size = new System.Drawing.Size(63, 22);
			this.tImportExcel.Text = "Import";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel2
			// 
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new System.Drawing.Size(36, 22);
			this.toolStripLabel2.Text = "Filter:";
			// 
			// tShowOnlyAssignedPrivileges
			// 
			this.tShowOnlyAssignedPrivileges.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tShowOnlyAssignedPrivileges.Image = ((System.Drawing.Image)(resources.GetObject("tShowOnlyAssignedPrivileges.Image")));
			this.tShowOnlyAssignedPrivileges.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tShowOnlyAssignedPrivileges.Name = "tShowOnlyAssignedPrivileges";
			this.tShowOnlyAssignedPrivileges.Size = new System.Drawing.Size(168, 22);
			this.tShowOnlyAssignedPrivileges.Text = "Show only assigned privileges";
			// 
			// tShowAllPrivileges
			// 
			this.tShowAllPrivileges.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tShowAllPrivileges.Image = ((System.Drawing.Image)(resources.GetObject("tShowAllPrivileges.Image")));
			this.tShowAllPrivileges.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tShowAllPrivileges.Name = "tShowAllPrivileges";
			this.tShowAllPrivileges.Size = new System.Drawing.Size(108, 22);
			this.tShowAllPrivileges.Text = "Show all privileges";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// tCopyAllPrivileges
			// 
			this.tCopyAllPrivileges.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.page_white_copy;
			this.tCopyAllPrivileges.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tCopyAllPrivileges.Name = "tCopyAllPrivileges";
			this.tCopyAllPrivileges.Size = new System.Drawing.Size(123, 22);
			this.tCopyAllPrivileges.Text = "Copy all privileges";
			// 
			// tPasteAllPrivileges
			// 
			this.tPasteAllPrivileges.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.page_white_paste;
			this.tPasteAllPrivileges.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tPasteAllPrivileges.Name = "tPasteAllPrivileges";
			this.tPasteAllPrivileges.Size = new System.Drawing.Size(123, 22);
			this.tPasteAllPrivileges.Text = "Paste all privileges";
			// 
			// tabs
			// 
			this.tabs.Controls.Add(this.tabTables);
			this.tabs.Controls.Add(this.tabMisc);
			this.tabs.Controls.Add(this.tabGeneral);
			this.tabs.Controls.Add(this.tabChangeSummary);
			this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabs.Location = new System.Drawing.Point(0, 25);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(1004, 503);
			this.tabs.TabIndex = 4;
			// 
			// tabTables
			// 
			this.tabTables.Controls.Add(this.treeTables);
			this.tabTables.Controls.Add(this.tools2);
			this.tabTables.Location = new System.Drawing.Point(4, 22);
			this.tabTables.Name = "tabTables";
			this.tabTables.Padding = new System.Windows.Forms.Padding(3);
			this.tabTables.Size = new System.Drawing.Size(996, 477);
			this.tabTables.TabIndex = 0;
			this.tabTables.Text = "Table related privileges";
			this.tabTables.UseVisualStyleBackColor = true;
			// 
			// treeTables
			// 
			this.treeTables.AllColumns.Add(this.cName);
			this.treeTables.AllColumns.Add(this.cCreate);
			this.treeTables.AllColumns.Add(this.cRead);
			this.treeTables.AllColumns.Add(this.cWrite);
			this.treeTables.AllColumns.Add(this.cDelete);
			this.treeTables.AllColumns.Add(this.cAppend);
			this.treeTables.AllColumns.Add(this.cAppendTo);
			this.treeTables.AllColumns.Add(this.cAssign);
			this.treeTables.AllColumns.Add(this.cShare);
			this.treeTables.AllColumns.Add(this.cLogicalName);
			this.treeTables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cName,
            this.cCreate,
            this.cRead,
            this.cWrite,
            this.cDelete,
            this.cAppend,
            this.cAppendTo,
            this.cAssign,
            this.cShare,
            this.cLogicalName});
			this.treeTables.CopySelectionOnControlC = false;
			this.treeTables.CopySelectionOnControlCUsesDragSource = false;
			this.treeTables.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeTables.FullRowSelect = true;
			this.treeTables.GridLines = true;
			this.treeTables.HideSelection = false;
			this.treeTables.Location = new System.Drawing.Point(3, 28);
			this.treeTables.Name = "treeTables";
			this.treeTables.OwnerDraw = true;
			this.treeTables.ShowGroups = false;
			this.treeTables.Size = new System.Drawing.Size(990, 446);
			this.treeTables.SmallImageList = this.privilegeImagesNew;
			this.treeTables.TabIndex = 1;
			this.treeTables.UseCompatibleStateImageBehavior = false;
			this.treeTables.View = System.Windows.Forms.View.Details;
			this.treeTables.VirtualMode = true;
			// 
			// cName
			// 
			this.cName.AspectName = "Name";
			this.cName.CellPadding = null;
			this.cName.IsEditable = false;
			this.cName.Text = "Table Display Name";
			this.cName.Width = 200;
			// 
			// cCreate
			// 
			this.cCreate.CellPadding = null;
			this.cCreate.Text = "Create";
			// 
			// cRead
			// 
			this.cRead.CellPadding = null;
			this.cRead.Text = "Read";
			// 
			// cWrite
			// 
			this.cWrite.CellPadding = null;
			this.cWrite.Text = "Write";
			// 
			// cDelete
			// 
			this.cDelete.CellPadding = null;
			this.cDelete.Text = "Delete";
			// 
			// cAppend
			// 
			this.cAppend.CellPadding = null;
			this.cAppend.Text = "Append";
			// 
			// cAppendTo
			// 
			this.cAppendTo.CellPadding = null;
			this.cAppendTo.Text = "Append To";
			// 
			// cAssign
			// 
			this.cAssign.CellPadding = null;
			this.cAssign.Text = "Assign";
			// 
			// cShare
			// 
			this.cShare.CellPadding = null;
			this.cShare.Text = "Share";
			// 
			// cLogicalName
			// 
			this.cLogicalName.AspectName = "Tooltip";
			this.cLogicalName.CellPadding = null;
			this.cLogicalName.FillsFreeSpace = true;
			this.cLogicalName.Text = "Table Logical Name";
			this.cLogicalName.Width = 200;
			// 
			// privilegeImagesNew
			// 
			this.privilegeImagesNew.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("privilegeImagesNew.ImageStream")));
			this.privilegeImagesNew.TransparentColor = System.Drawing.Color.Transparent;
			this.privilegeImagesNew.Images.SetKeyName(0, "none");
			this.privilegeImagesNew.Images.SetKeyName(1, "user");
			this.privilegeImagesNew.Images.SetKeyName(2, "businessunit");
			this.privilegeImagesNew.Images.SetKeyName(3, "parentchild");
			this.privilegeImagesNew.Images.SetKeyName(4, "organization");
			// 
			// tools2
			// 
			this.tools2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.tSearchTableText,
            this.toolStripLabel5});
			this.tools2.Location = new System.Drawing.Point(3, 3);
			this.tools2.Name = "tools2";
			this.tools2.Size = new System.Drawing.Size(990, 25);
			this.tools2.TabIndex = 0;
			this.tools2.Text = "toolStrip1";
			// 
			// toolStripLabel3
			// 
			this.toolStripLabel3.Name = "toolStripLabel3";
			this.toolStripLabel3.Size = new System.Drawing.Size(74, 22);
			this.toolStripLabel3.Text = "Search table:";
			// 
			// tSearchTableText
			// 
			this.tSearchTableText.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.tSearchTableText.Name = "tSearchTableText";
			this.tSearchTableText.Size = new System.Drawing.Size(150, 25);
			// 
			// toolStripLabel5
			// 
			this.toolStripLabel5.ForeColor = System.Drawing.Color.Gray;
			this.toolStripLabel5.Name = "toolStripLabel5";
			this.toolStripLabel5.Size = new System.Drawing.Size(638, 22);
			this.toolStripLabel5.Text = "Type CTRL+0-9 to apply privilege snippets. Type CTRL+SHIFT+5-9 or enter the Setti" +
    "ngs dialog to configure the snippets.";
			// 
			// tabMisc
			// 
			this.tabMisc.Controls.Add(this.treeMisc);
			this.tabMisc.Controls.Add(this.tools3);
			this.tabMisc.Location = new System.Drawing.Point(4, 22);
			this.tabMisc.Name = "tabMisc";
			this.tabMisc.Padding = new System.Windows.Forms.Padding(3);
			this.tabMisc.Size = new System.Drawing.Size(996, 477);
			this.tabMisc.TabIndex = 1;
			this.tabMisc.Text = "Miscellaneous Privileges";
			this.tabMisc.UseVisualStyleBackColor = true;
			// 
			// treeMisc
			// 
			this.treeMisc.AllColumns.Add(this.cMiscName);
			this.treeMisc.AllColumns.Add(this.cMiscValue);
			this.treeMisc.AllColumns.Add(this.cMiscTooltip);
			this.treeMisc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cMiscName,
            this.cMiscValue,
            this.cMiscTooltip});
			this.treeMisc.CopySelectionOnControlC = false;
			this.treeMisc.CopySelectionOnControlCUsesDragSource = false;
			this.treeMisc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeMisc.FullRowSelect = true;
			this.treeMisc.GridLines = true;
			this.treeMisc.HideSelection = false;
			this.treeMisc.Location = new System.Drawing.Point(3, 28);
			this.treeMisc.Name = "treeMisc";
			this.treeMisc.OwnerDraw = true;
			this.treeMisc.ShowGroups = false;
			this.treeMisc.Size = new System.Drawing.Size(990, 446);
			this.treeMisc.SmallImageList = this.privilegeImagesOld;
			this.treeMisc.TabIndex = 2;
			this.treeMisc.UseCompatibleStateImageBehavior = false;
			this.treeMisc.View = System.Windows.Forms.View.Details;
			this.treeMisc.VirtualMode = true;
			// 
			// cMiscName
			// 
			this.cMiscName.AspectName = "Name";
			this.cMiscName.CellPadding = null;
			this.cMiscName.IsEditable = false;
			this.cMiscName.Text = "Name";
			this.cMiscName.Width = 200;
			// 
			// cMiscValue
			// 
			this.cMiscValue.AspectName = "";
			this.cMiscValue.CellPadding = null;
			this.cMiscValue.Text = "Do Action";
			// 
			// cMiscTooltip
			// 
			this.cMiscTooltip.AspectName = "Tooltip";
			this.cMiscTooltip.CellPadding = null;
			this.cMiscTooltip.FillsFreeSpace = true;
			this.cMiscTooltip.Text = "Privilege Name";
			this.cMiscTooltip.Width = 200;
			// 
			// tools3
			// 
			this.tools3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel4,
            this.txtSearchMisc});
			this.tools3.Location = new System.Drawing.Point(3, 3);
			this.tools3.Name = "tools3";
			this.tools3.Size = new System.Drawing.Size(990, 25);
			this.tools3.TabIndex = 0;
			this.tools3.Text = "toolStrip1";
			// 
			// toolStripLabel4
			// 
			this.toolStripLabel4.Name = "toolStripLabel4";
			this.toolStripLabel4.Size = new System.Drawing.Size(93, 22);
			this.toolStripLabel4.Text = "Search privilege:";
			// 
			// txtSearchMisc
			// 
			this.txtSearchMisc.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.txtSearchMisc.Name = "txtSearchMisc";
			this.txtSearchMisc.Size = new System.Drawing.Size(150, 25);
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.tableLayoutPanel1);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Padding = new System.Windows.Forms.Padding(10);
			this.tabGeneral.Size = new System.Drawing.Size(996, 477);
			this.tabGeneral.TabIndex = 2;
			this.tabGeneral.Text = "General information";
			this.tabGeneral.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.txtRoleName, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtRoleBusinessUnit, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.txtRoleDescription, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.cmbRoleInheritance, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.btnRoleBusinessUnitLookup, 2, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(976, 205);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Margin = new System.Windows.Forms.Padding(0);
			this.label1.Name = "label1";
			this.label1.Padding = new System.Windows.Forms.Padding(5);
			this.label1.Size = new System.Drawing.Size(200, 30);
			this.label1.TabIndex = 0;
			this.label1.Text = "Role Name:";
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(0, 30);
			this.label2.Margin = new System.Windows.Forms.Padding(0);
			this.label2.Name = "label2";
			this.label2.Padding = new System.Windows.Forms.Padding(5);
			this.label2.Size = new System.Drawing.Size(200, 30);
			this.label2.TabIndex = 1;
			this.label2.Text = "Business Unit:";
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(0, 60);
			this.label3.Margin = new System.Windows.Forms.Padding(0);
			this.label3.Name = "label3";
			this.label3.Padding = new System.Windows.Forms.Padding(5);
			this.label3.Size = new System.Drawing.Size(200, 30);
			this.label3.TabIndex = 2;
			this.label3.Text = "Description";
			// 
			// label4
			// 
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(0, 90);
			this.label4.Margin = new System.Windows.Forms.Padding(0);
			this.label4.Name = "label4";
			this.label4.Padding = new System.Windows.Forms.Padding(5);
			this.label4.Size = new System.Drawing.Size(200, 30);
			this.label4.TabIndex = 3;
			this.label4.Text = "Member\'s privilege inheritance";
			// 
			// txtRoleName
			// 
			this.txtRoleName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.SetColumnSpan(this.txtRoleName, 2);
			this.txtRoleName.Location = new System.Drawing.Point(203, 3);
			this.txtRoleName.Name = "txtRoleName";
			this.txtRoleName.Size = new System.Drawing.Size(770, 20);
			this.txtRoleName.TabIndex = 4;
			// 
			// txtRoleBusinessUnit
			// 
			this.txtRoleBusinessUnit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtRoleBusinessUnit.Enabled = false;
			this.txtRoleBusinessUnit.Location = new System.Drawing.Point(203, 33);
			this.txtRoleBusinessUnit.Name = "txtRoleBusinessUnit";
			this.txtRoleBusinessUnit.Size = new System.Drawing.Size(730, 20);
			this.txtRoleBusinessUnit.TabIndex = 5;
			// 
			// txtRoleDescription
			// 
			this.txtRoleDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.SetColumnSpan(this.txtRoleDescription, 2);
			this.txtRoleDescription.Location = new System.Drawing.Point(203, 63);
			this.txtRoleDescription.Name = "txtRoleDescription";
			this.txtRoleDescription.Size = new System.Drawing.Size(770, 20);
			this.txtRoleDescription.TabIndex = 6;
			// 
			// cmbRoleInheritance
			// 
			this.cmbRoleInheritance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.SetColumnSpan(this.cmbRoleInheritance, 2);
			this.cmbRoleInheritance.FormattingEnabled = true;
			this.cmbRoleInheritance.Location = new System.Drawing.Point(203, 93);
			this.cmbRoleInheritance.Name = "cmbRoleInheritance";
			this.cmbRoleInheritance.Size = new System.Drawing.Size(770, 21);
			this.cmbRoleInheritance.TabIndex = 7;
			// 
			// btnRoleBusinessUnitLookup
			// 
			this.btnRoleBusinessUnitLookup.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.zoom;
			this.btnRoleBusinessUnitLookup.Location = new System.Drawing.Point(939, 33);
			this.btnRoleBusinessUnitLookup.Name = "btnRoleBusinessUnitLookup";
			this.btnRoleBusinessUnitLookup.Size = new System.Drawing.Size(34, 23);
			this.btnRoleBusinessUnitLookup.TabIndex = 8;
			this.btnRoleBusinessUnitLookup.UseVisualStyleBackColor = true;
			// 
			// tabChangeSummary
			// 
			this.tabChangeSummary.Controls.Add(this.olvChangeSummary);
			this.tabChangeSummary.Location = new System.Drawing.Point(4, 22);
			this.tabChangeSummary.Name = "tabChangeSummary";
			this.tabChangeSummary.Padding = new System.Windows.Forms.Padding(3);
			this.tabChangeSummary.Size = new System.Drawing.Size(996, 477);
			this.tabChangeSummary.TabIndex = 3;
			this.tabChangeSummary.Text = "Change Summary";
			this.tabChangeSummary.UseVisualStyleBackColor = true;
			// 
			// olvChangeSummary
			// 
			this.olvChangeSummary.AllColumns.Add(this.cSummaryText);
			this.olvChangeSummary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cSummaryText});
			this.olvChangeSummary.Dock = System.Windows.Forms.DockStyle.Fill;
			this.olvChangeSummary.HideSelection = false;
			this.olvChangeSummary.Location = new System.Drawing.Point(3, 3);
			this.olvChangeSummary.Name = "olvChangeSummary";
			this.olvChangeSummary.Size = new System.Drawing.Size(990, 471);
			this.olvChangeSummary.SmallImageList = this.privilegeImagesOld;
			this.olvChangeSummary.TabIndex = 0;
			this.olvChangeSummary.UseCompatibleStateImageBehavior = false;
			this.olvChangeSummary.View = System.Windows.Forms.View.Details;
			// 
			// cSummaryText
			// 
			this.cSummaryText.AspectName = "Text";
			this.cSummaryText.CellPadding = null;
			this.cSummaryText.FillsFreeSpace = true;
			this.cSummaryText.Text = "Change details";
			this.cSummaryText.Width = 400;
			// 
			// notificationPanel
			// 
			this.notificationPanel.AutoSize = true;
			this.notificationPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.notificationPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.notificationPanel.Location = new System.Drawing.Point(0, 0);
			this.notificationPanel.Name = "notificationPanel";
			this.notificationPanel.Size = new System.Drawing.Size(1004, 0);
			this.notificationPanel.TabIndex = 2;
			// 
			// contextMenu
			// 
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mSet0,
            this.mSet1,
            this.mSet2,
            this.mSet3,
            this.mSet4});
			this.contextMenu.Name = "contextMenu";
			this.contextMenu.Size = new System.Drawing.Size(215, 114);
			// 
			// mSet0
			// 
			this.mSet0.Name = "mSet0";
			this.mSet0.Size = new System.Drawing.Size(214, 22);
			this.mSet0.Tag = "0";
			this.mSet0.Text = "None";
			// 
			// mSet1
			// 
			this.mSet1.Name = "mSet1";
			this.mSet1.Size = new System.Drawing.Size(214, 22);
			this.mSet1.Tag = "1";
			this.mSet1.Text = "User";
			// 
			// mSet2
			// 
			this.mSet2.Name = "mSet2";
			this.mSet2.Size = new System.Drawing.Size(214, 22);
			this.mSet2.Tag = "2";
			this.mSet2.Text = "Business Unit";
			// 
			// mSet3
			// 
			this.mSet3.Name = "mSet3";
			this.mSet3.Size = new System.Drawing.Size(214, 22);
			this.mSet3.Tag = "3";
			this.mSet3.Text = "Parent-Child Business Unit";
			// 
			// mSet4
			// 
			this.mSet4.Name = "mSet4";
			this.mSet4.Size = new System.Drawing.Size(214, 22);
			this.mSet4.Tag = "4";
			this.mSet4.Text = "Organization";
			// 
			// RoleEditorView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1004, 528);
			this.Controls.Add(this.tabs);
			this.Controls.Add(this.tools);
			this.Controls.Add(this.notificationPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "RoleEditorView";
			this.tools.ResumeLayout(false);
			this.tools.PerformLayout();
			this.tabs.ResumeLayout(false);
			this.tabTables.ResumeLayout(false);
			this.tabTables.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeTables)).EndInit();
			this.tools2.ResumeLayout(false);
			this.tools2.PerformLayout();
			this.tabMisc.ResumeLayout(false);
			this.tabMisc.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeMisc)).EndInit();
			this.tools3.ResumeLayout(false);
			this.tools3.PerformLayout();
			this.tabGeneral.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tabChangeSummary.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.olvChangeSummary)).EndInit();
			this.contextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ImageList privilegeImagesOld;
		private Core.Views.NotificationPanel notificationPanel;
		private System.Windows.Forms.ToolStrip tools;
		private Xrm.Views.ToolStripBindableButton tSave;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private Xrm.Views.ToolStripBindableButton tExportExcel;
		private Xrm.Views.ToolStripBindableButton tExportMarkdown;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		private Xrm.Views.ToolStripBindableButton tShowOnlyAssignedPrivileges;
		private Xrm.Views.ToolStripBindableButton tShowAllPrivileges;
		private System.Windows.Forms.TabControl tabs;
		private System.Windows.Forms.TabPage tabTables;
		private BrightIdeasSoftware.TreeListView treeTables;
		private BrightIdeasSoftware.OLVColumn cName;
		private BrightIdeasSoftware.OLVColumn cCreate;
		private BrightIdeasSoftware.OLVColumn cRead;
		private BrightIdeasSoftware.OLVColumn cWrite;
		private BrightIdeasSoftware.OLVColumn cDelete;
		private BrightIdeasSoftware.OLVColumn cAppend;
		private BrightIdeasSoftware.OLVColumn cAppendTo;
		private BrightIdeasSoftware.OLVColumn cAssign;
		private BrightIdeasSoftware.OLVColumn cShare;
		private BrightIdeasSoftware.OLVColumn cLogicalName;
		private System.Windows.Forms.ToolStrip tools2;
		private System.Windows.Forms.ToolStripLabel toolStripLabel3;
		private System.Windows.Forms.ToolStripTextBox tSearchTableText;
		private System.Windows.Forms.TabPage tabMisc;
		private BrightIdeasSoftware.TreeListView treeMisc;
		private BrightIdeasSoftware.OLVColumn cMiscName;
		private BrightIdeasSoftware.OLVColumn cMiscValue;
		private BrightIdeasSoftware.OLVColumn cMiscTooltip;
		private System.Windows.Forms.ToolStrip tools3;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabPage tabChangeSummary;
		private BrightIdeasSoftware.ObjectListView olvChangeSummary;
		private BrightIdeasSoftware.OLVColumn cSummaryText;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtRoleName;
		private System.Windows.Forms.TextBox txtRoleBusinessUnit;
		private System.Windows.Forms.TextBox txtRoleDescription;
		private System.Windows.Forms.ComboBox cmbRoleInheritance;
		private System.Windows.Forms.Button btnRoleBusinessUnitLookup;
		private System.Windows.Forms.ToolStripLabel toolStripLabel4;
		private System.Windows.Forms.ToolStripTextBox txtSearchMisc;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ImageList privilegeImagesNew;
		private System.Windows.Forms.ContextMenuStrip contextMenu;
		private System.Windows.Forms.ToolStripMenuItem mSet0;
		private System.Windows.Forms.ToolStripMenuItem mSet1;
		private System.Windows.Forms.ToolStripMenuItem mSet2;
		private System.Windows.Forms.ToolStripMenuItem mSet3;
		private System.Windows.Forms.ToolStripMenuItem mSet4;
		private Xrm.Views.ToolStripBindableButton tAddToSolution;
		private Xrm.Views.ToolStripBindableButton tImportExcel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripLabel toolStripLabel5;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private Xrm.Views.ToolStripBindableButton tCopyAllPrivileges;
		private Xrm.Views.ToolStripBindableButton tPasteAllPrivileges;
	}
}
