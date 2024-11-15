namespace Greg.Xrm.RoleEditor.Views.BulkEditor
{
	partial class BulkEditorView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BulkEditorView));
			this.privilegeImagesNew = new System.Windows.Forms.ImageList(this.components);
			this.privilegeImagesOld = new System.Windows.Forms.ImageList(this.components);
			this.tools = new System.Windows.Forms.ToolStrip();
			this.tSave = new Greg.Xrm.Views.ToolStripBindableButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.tShowOnlyAssigned = new Greg.Xrm.Views.ToolStripBindableButton();
			this.tShowAll = new Greg.Xrm.Views.ToolStripBindableButton();
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
			this.tools2 = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
			this.tSearchTableText = new System.Windows.Forms.ToolStripTextBox();
			this.tabMisc = new System.Windows.Forms.TabPage();
			this.treeMisc = new BrightIdeasSoftware.TreeListView();
			this.cMiscName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cMiscValue = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cMiscTooltip = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.tools3 = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
			this.txtSearchMisc = new System.Windows.Forms.ToolStripTextBox();
			this.tabChangeSummary = new System.Windows.Forms.TabPage();
			this.treeChangeSummary = new BrightIdeasSoftware.TreeListView();
			this.cChangeSummaryName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mSet0 = new System.Windows.Forms.ToolStripMenuItem();
			this.mSet1 = new System.Windows.Forms.ToolStripMenuItem();
			this.mSet2 = new System.Windows.Forms.ToolStripMenuItem();
			this.mSet3 = new System.Windows.Forms.ToolStripMenuItem();
			this.mSet4 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.mSetNull = new System.Windows.Forms.ToolStripMenuItem();
			this.notificationPanel = new Greg.Xrm.Core.Views.NotificationPanel();
			this.tools.SuspendLayout();
			this.tabs.SuspendLayout();
			this.tabTables.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeTables)).BeginInit();
			this.tools2.SuspendLayout();
			this.tabMisc.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeMisc)).BeginInit();
			this.tools3.SuspendLayout();
			this.tabChangeSummary.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeChangeSummary)).BeginInit();
			this.contextMenu.SuspendLayout();
			this.SuspendLayout();
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
			this.privilegeImagesOld.Images.SetKeyName(8, "role");
			// 
			// tools
			// 
			this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSave,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tShowOnlyAssigned,
            this.tShowAll});
			this.tools.Location = new System.Drawing.Point(0, 0);
			this.tools.Name = "tools";
			this.tools.Size = new System.Drawing.Size(612, 25);
			this.tools.TabIndex = 2;
			this.tools.Text = "toolStrip1";
			// 
			// tSave
			// 
			this.tSave.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.disk;
			this.tSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tSave.Name = "tSave";
			this.tSave.Size = new System.Drawing.Size(51, 22);
			this.tSave.Text = "Save";
			this.tSave.ToolTipText = "Save roles";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(36, 22);
			this.toolStripLabel1.Text = "Filter:";
			// 
			// tShowOnlyAssigned
			// 
			this.tShowOnlyAssigned.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tShowOnlyAssigned.Image = ((System.Drawing.Image)(resources.GetObject("tShowOnlyAssigned.Image")));
			this.tShowOnlyAssigned.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tShowOnlyAssigned.Name = "tShowOnlyAssigned";
			this.tShowOnlyAssigned.Size = new System.Drawing.Size(168, 22);
			this.tShowOnlyAssigned.Text = "Show only assigned privileges";
			// 
			// tShowAll
			// 
			this.tShowAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tShowAll.Image = ((System.Drawing.Image)(resources.GetObject("tShowAll.Image")));
			this.tShowAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tShowAll.Name = "tShowAll";
			this.tShowAll.Size = new System.Drawing.Size(108, 22);
			this.tShowAll.Text = "Show all privileges";
			// 
			// tabs
			// 
			this.tabs.Controls.Add(this.tabTables);
			this.tabs.Controls.Add(this.tabMisc);
			this.tabs.Controls.Add(this.tabChangeSummary);
			this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabs.Location = new System.Drawing.Point(0, 25);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(612, 518);
			this.tabs.TabIndex = 5;
			// 
			// tabTables
			// 
			this.tabTables.Controls.Add(this.treeTables);
			this.tabTables.Controls.Add(this.tools2);
			this.tabTables.Location = new System.Drawing.Point(4, 22);
			this.tabTables.Name = "tabTables";
			this.tabTables.Padding = new System.Windows.Forms.Padding(3);
			this.tabTables.Size = new System.Drawing.Size(604, 492);
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
			this.treeTables.Size = new System.Drawing.Size(598, 461);
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
			// tools2
			// 
			this.tools2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.tSearchTableText});
			this.tools2.Location = new System.Drawing.Point(3, 3);
			this.tools2.Name = "tools2";
			this.tools2.Size = new System.Drawing.Size(598, 25);
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
			// tabMisc
			// 
			this.tabMisc.Controls.Add(this.treeMisc);
			this.tabMisc.Controls.Add(this.tools3);
			this.tabMisc.Location = new System.Drawing.Point(4, 22);
			this.tabMisc.Name = "tabMisc";
			this.tabMisc.Padding = new System.Windows.Forms.Padding(3);
			this.tabMisc.Size = new System.Drawing.Size(604, 492);
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
			this.treeMisc.Size = new System.Drawing.Size(598, 461);
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
			this.tools3.Size = new System.Drawing.Size(598, 25);
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
			// tabChangeSummary
			// 
			this.tabChangeSummary.Controls.Add(this.treeChangeSummary);
			this.tabChangeSummary.Location = new System.Drawing.Point(4, 22);
			this.tabChangeSummary.Name = "tabChangeSummary";
			this.tabChangeSummary.Padding = new System.Windows.Forms.Padding(3);
			this.tabChangeSummary.Size = new System.Drawing.Size(604, 492);
			this.tabChangeSummary.TabIndex = 3;
			this.tabChangeSummary.Text = "Change Summary";
			this.tabChangeSummary.UseVisualStyleBackColor = true;
			// 
			// treeChangeSummary
			// 
			this.treeChangeSummary.AllColumns.Add(this.cChangeSummaryName);
			this.treeChangeSummary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cChangeSummaryName});
			this.treeChangeSummary.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeChangeSummary.FullRowSelect = true;
			this.treeChangeSummary.HideSelection = false;
			this.treeChangeSummary.Location = new System.Drawing.Point(3, 3);
			this.treeChangeSummary.Name = "treeChangeSummary";
			this.treeChangeSummary.OwnerDraw = true;
			this.treeChangeSummary.ShowGroups = false;
			this.treeChangeSummary.Size = new System.Drawing.Size(598, 486);
			this.treeChangeSummary.SmallImageList = this.privilegeImagesOld;
			this.treeChangeSummary.TabIndex = 0;
			this.treeChangeSummary.UseCompatibleStateImageBehavior = false;
			this.treeChangeSummary.View = System.Windows.Forms.View.Details;
			this.treeChangeSummary.VirtualMode = true;
			// 
			// cChangeSummaryName
			// 
			this.cChangeSummaryName.AspectName = "Name";
			this.cChangeSummaryName.CellPadding = null;
			this.cChangeSummaryName.FillsFreeSpace = true;
			this.cChangeSummaryName.Sortable = false;
			this.cChangeSummaryName.Text = "Action";
			this.cChangeSummaryName.Width = 300;
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
			this.contextMenu.Size = new System.Drawing.Size(215, 142);
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
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(211, 6);
			// 
			// mSetNull
			// 
			this.mSetNull.Name = "mSetNull";
			this.mSetNull.Size = new System.Drawing.Size(214, 22);
			this.mSetNull.Text = "Clear changes";
			// 
			// notificationPanel
			// 
			this.notificationPanel.AutoSize = true;
			this.notificationPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.notificationPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.notificationPanel.Location = new System.Drawing.Point(0, 0);
			this.notificationPanel.Name = "notificationPanel";
			this.notificationPanel.Size = new System.Drawing.Size(612, 0);
			this.notificationPanel.TabIndex = 1;
			// 
			// BulkEditorView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(612, 543);
			this.Controls.Add(this.tabs);
			this.Controls.Add(this.tools);
			this.Controls.Add(this.notificationPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "BulkEditorView";
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
			this.tabChangeSummary.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.treeChangeSummary)).EndInit();
			this.contextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ImageList privilegeImagesNew;
		private System.Windows.Forms.ImageList privilegeImagesOld;
		private Core.Views.NotificationPanel notificationPanel;
		private System.Windows.Forms.ToolStrip tools;
		private Xrm.Views.ToolStripBindableButton tSave;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private Xrm.Views.ToolStripBindableButton tShowOnlyAssigned;
		private Xrm.Views.ToolStripBindableButton tShowAll;
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
		private System.Windows.Forms.ToolStripLabel toolStripLabel4;
		private System.Windows.Forms.ToolStripTextBox txtSearchMisc;
		private System.Windows.Forms.TabPage tabChangeSummary;
		private System.Windows.Forms.ContextMenuStrip contextMenu;
		private System.Windows.Forms.ToolStripMenuItem mSet0;
		private System.Windows.Forms.ToolStripMenuItem mSet1;
		private System.Windows.Forms.ToolStripMenuItem mSet2;
		private System.Windows.Forms.ToolStripMenuItem mSet3;
		private System.Windows.Forms.ToolStripMenuItem mSet4;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem mSetNull;
		private BrightIdeasSoftware.TreeListView treeChangeSummary;
		private BrightIdeasSoftware.OLVColumn cChangeSummaryName;
	}
}
