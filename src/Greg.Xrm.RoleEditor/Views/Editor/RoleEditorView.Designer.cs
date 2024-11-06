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
			this.tools = new System.Windows.Forms.ToolStrip();
			this.tSave = new Xrm.Views.ToolStripBindableButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.tExportExcel = new System.Windows.Forms.ToolStripButton();
			this.tExportMarkdown = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
			this.tShowOnlyAssignedPrivileges = new Greg.Xrm.Views.ToolStripBindableButton();
			this.tShowAllPrivileges = new Greg.Xrm.Views.ToolStripBindableButton();
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
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.tools2 = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
			this.tSearchTableText = new System.Windows.Forms.ToolStripTextBox();
			this.tabMisc = new System.Windows.Forms.TabPage();
			this.tools3 = new System.Windows.Forms.ToolStrip();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.tabChangeSummary = new System.Windows.Forms.TabPage();
			this.olvChangeSummary = new BrightIdeasSoftware.ObjectListView();
			this.cSummaryText = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.treeMisc = new BrightIdeasSoftware.TreeListView();
			this.cMiscName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cMiscValue = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cMiscTooltip = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.tools.SuspendLayout();
			this.tabs.SuspendLayout();
			this.tabTables.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeTables)).BeginInit();
			this.tools2.SuspendLayout();
			this.tabMisc.SuspendLayout();
			this.tabChangeSummary.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.olvChangeSummary)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.treeMisc)).BeginInit();
			this.SuspendLayout();
			// 
			// tools
			// 
			this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSave,
            this.toolStripSeparator2,
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tExportExcel,
            this.tExportMarkdown,
            this.toolStripSeparator3,
            this.toolStripLabel2,
            this.tShowOnlyAssignedPrivileges,
            this.tShowAllPrivileges});
			this.tools.Location = new System.Drawing.Point(0, 0);
			this.tools.Name = "tools";
			this.tools.Size = new System.Drawing.Size(870, 25);
			this.tools.TabIndex = 0;
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
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton1.Text = "toolStripButton1";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(44, 22);
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
			this.tabs.Size = new System.Drawing.Size(870, 503);
			this.tabs.TabIndex = 1;
			this.tabs.SelectedIndexChanged += new System.EventHandler(this.OnTabChanged);
			// 
			// tabTables
			// 
			this.tabTables.Controls.Add(this.treeTables);
			this.tabTables.Controls.Add(this.tools2);
			this.tabTables.Location = new System.Drawing.Point(4, 22);
			this.tabTables.Name = "tabTables";
			this.tabTables.Padding = new System.Windows.Forms.Padding(3);
			this.tabTables.Size = new System.Drawing.Size(862, 477);
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
			this.treeTables.Size = new System.Drawing.Size(856, 446);
			this.treeTables.SmallImageList = this.imageList;
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
			this.cName.Text = "Name";
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
			this.cLogicalName.Text = "Logical Name";
			this.cLogicalName.Width = 200;
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "none");
			this.imageList.Images.SetKeyName(1, "user");
			this.imageList.Images.SetKeyName(2, "businessunit");
			this.imageList.Images.SetKeyName(3, "parentchild");
			this.imageList.Images.SetKeyName(4, "organization");
			this.imageList.Images.SetKeyName(5, "add");
			this.imageList.Images.SetKeyName(6, "remove");
			this.imageList.Images.SetKeyName(7, "replace");
			// 
			// tools2
			// 
			this.tools2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.tSearchTableText});
			this.tools2.Location = new System.Drawing.Point(3, 3);
			this.tools2.Name = "tools2";
			this.tools2.Size = new System.Drawing.Size(856, 25);
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
			this.tabMisc.Size = new System.Drawing.Size(862, 477);
			this.tabMisc.TabIndex = 1;
			this.tabMisc.Text = "Miscellaneous Privileges";
			this.tabMisc.UseVisualStyleBackColor = true;
			// 
			// tools3
			// 
			this.tools3.Location = new System.Drawing.Point(3, 3);
			this.tools3.Name = "tools3";
			this.tools3.Size = new System.Drawing.Size(856, 25);
			this.tools3.TabIndex = 0;
			this.tools3.Text = "toolStrip1";
			// 
			// tabGeneral
			// 
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(862, 477);
			this.tabGeneral.TabIndex = 2;
			this.tabGeneral.Text = "General information";
			this.tabGeneral.UseVisualStyleBackColor = true;
			// 
			// tabChangeSummary
			// 
			this.tabChangeSummary.Controls.Add(this.olvChangeSummary);
			this.tabChangeSummary.Location = new System.Drawing.Point(4, 22);
			this.tabChangeSummary.Name = "tabChangeSummary";
			this.tabChangeSummary.Padding = new System.Windows.Forms.Padding(3);
			this.tabChangeSummary.Size = new System.Drawing.Size(862, 477);
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
			this.olvChangeSummary.Size = new System.Drawing.Size(856, 471);
			this.olvChangeSummary.SmallImageList = this.imageList;
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
			this.treeMisc.Size = new System.Drawing.Size(856, 446);
			this.treeMisc.SmallImageList = this.imageList;
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
			// RoleEditorView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(870, 528);
			this.Controls.Add(this.tabs);
			this.Controls.Add(this.tools);
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
			this.tabChangeSummary.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.olvChangeSummary)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.treeMisc)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip tools;
		private Xrm.Views.ToolStripBindableButton tSave;
		private System.Windows.Forms.TabControl tabs;
		private System.Windows.Forms.TabPage tabTables;
		private System.Windows.Forms.TabPage tabMisc;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.ToolStrip tools2;
		private System.Windows.Forms.ToolStrip tools3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripButton tExportExcel;
		private System.Windows.Forms.ToolStripButton tExportMarkdown;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private BrightIdeasSoftware.TreeListView treeTables;
		private System.Windows.Forms.ImageList imageList;
		private BrightIdeasSoftware.OLVColumn cName;
		private BrightIdeasSoftware.OLVColumn cCreate;
		private BrightIdeasSoftware.OLVColumn cRead;
		private BrightIdeasSoftware.OLVColumn cWrite;
		private BrightIdeasSoftware.OLVColumn cDelete;
		private BrightIdeasSoftware.OLVColumn cAppend;
		private BrightIdeasSoftware.OLVColumn cAppendTo;
		private BrightIdeasSoftware.OLVColumn cAssign;
		private BrightIdeasSoftware.OLVColumn cShare;
		private Xrm.Views.ToolStripBindableButton tShowOnlyAssignedPrivileges;
		private Xrm.Views.ToolStripBindableButton tShowAllPrivileges;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		private System.Windows.Forms.ToolStripLabel toolStripLabel3;
		private System.Windows.Forms.ToolStripTextBox tSearchTableText;
		private BrightIdeasSoftware.OLVColumn cLogicalName;
		private System.Windows.Forms.TabPage tabChangeSummary;
		private BrightIdeasSoftware.ObjectListView olvChangeSummary;
		private BrightIdeasSoftware.OLVColumn cSummaryText;
		private BrightIdeasSoftware.TreeListView treeMisc;
		private BrightIdeasSoftware.OLVColumn cMiscName;
		private BrightIdeasSoftware.OLVColumn cMiscValue;
		private BrightIdeasSoftware.OLVColumn cMiscTooltip;
	}
}
