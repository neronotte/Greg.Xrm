namespace Greg.Xrm.RoleEditor.Views.RoleBrowser
{
	partial class RoleBrowserView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoleBrowserView));
			this.tools = new System.Windows.Forms.ToolStrip();
			this.tNew = new Greg.Xrm.Views.ToolStripBindableSplitButton();
			this.tNewFromBlank = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.tNewCloneCurrent = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.tNewFromBasicUser = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.tNewFromAppOpener = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.tSearchText = new System.Windows.Forms.ToolStripTextBox();
			this.tMoreFilters = new System.Windows.Forms.ToolStripDropDownButton();
			this.tMoreFilters_HideNotCustomizableRolesToolStripMenuItem = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.tMoreFilters_HideManagedRolesToolStripMenuItem = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.roleList = new BrightIdeasSoftware.ObjectListView();
			this.cName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cBusinessUnit = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.tools.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.roleList)).BeginInit();
			this.SuspendLayout();
			// 
			// tools
			// 
			this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tNew,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tSearchText,
            this.tMoreFilters});
			this.tools.Location = new System.Drawing.Point(0, 0);
			this.tools.Name = "tools";
			this.tools.Size = new System.Drawing.Size(517, 25);
			this.tools.TabIndex = 0;
			this.tools.Text = "toolStrip1";
			// 
			// toolStripSplitButton1
			// 
			this.tNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tNewFromBlank,
            this.tNewCloneCurrent,
            this.tNewFromBasicUser,
            this.tNewFromAppOpener});
			this.tNew.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.page_white;
			this.tNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tNew.Name = "toolStripSplitButton1";
			this.tNew.Size = new System.Drawing.Size(89, 22);
			this.tNew.Text = "New Role";
			// 
			// tNewFromBlank
			// 
			this.tNewFromBlank.Name = "tNewFromBlank";
			this.tNewFromBlank.Size = new System.Drawing.Size(179, 22);
			this.tNewFromBlank.Text = "From blank";
			// 
			// tNewCloneCurrent
			// 
			this.tNewCloneCurrent.Name = "tNewCloneCurrent";
			this.tNewCloneCurrent.Size = new System.Drawing.Size(179, 22);
			this.tNewCloneCurrent.Text = "Clone current";
			// 
			// tNewFromBasicUser
			// 
			this.tNewFromBasicUser.Name = "tNewFromBasicUser";
			this.tNewFromBasicUser.Size = new System.Drawing.Size(179, 22);
			this.tNewFromBasicUser.Text = "From \"Basic User\"";
			// 
			// tNewFromAppOpener
			// 
			this.tNewFromAppOpener.Name = "tNewFromAppOpener";
			this.tNewFromAppOpener.Size = new System.Drawing.Size(179, 22);
			this.tNewFromAppOpener.Text = "From \"App Opener\"";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.zoom;
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(61, 22);
			this.toolStripLabel1.Text = "Search:";
			// 
			// tSearchText
			// 
			this.tSearchText.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.tSearchText.Name = "tSearchText";
			this.tSearchText.Size = new System.Drawing.Size(150, 25);
			// 
			// tMoreFilters
			// 
			this.tMoreFilters.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tMoreFilters.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tMoreFilters_HideNotCustomizableRolesToolStripMenuItem,
            this.tMoreFilters_HideManagedRolesToolStripMenuItem});
			this.tMoreFilters.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.folder_explore;
			this.tMoreFilters.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tMoreFilters.Name = "tMoreFilters";
			this.tMoreFilters.Size = new System.Drawing.Size(29, 22);
			this.tMoreFilters.Text = "Filter";
			this.tMoreFilters.ToolTipText = "More filters";
			// 
			// tMoreFilters_HideNotCustomizableRolesToolStripMenuItem
			// 
			this.tMoreFilters_HideNotCustomizableRolesToolStripMenuItem.Name = "tMoreFilters_HideNotCustomizableRolesToolStripMenuItem";
			this.tMoreFilters_HideNotCustomizableRolesToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
			this.tMoreFilters_HideNotCustomizableRolesToolStripMenuItem.Text = "Hide not customizable roles";
			// 
			// tMoreFilters_HideManagedRolesToolStripMenuItem
			// 
			this.tMoreFilters_HideManagedRolesToolStripMenuItem.Name = "tMoreFilters_HideManagedRolesToolStripMenuItem";
			this.tMoreFilters_HideManagedRolesToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
			this.tMoreFilters_HideManagedRolesToolStripMenuItem.Text = "Hide managed roles";
			// 
			// roleList
			// 
			this.roleList.AllColumns.Add(this.cName);
			this.roleList.AllColumns.Add(this.cBusinessUnit);
			this.roleList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cName,
            this.cBusinessUnit});
			this.roleList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.roleList.FullRowSelect = true;
			this.roleList.GridLines = true;
			this.roleList.HideSelection = false;
			this.roleList.Location = new System.Drawing.Point(0, 25);
			this.roleList.Name = "roleList";
			this.roleList.ShowGroups = false;
			this.roleList.Size = new System.Drawing.Size(517, 505);
			this.roleList.TabIndex = 1;
			this.roleList.UseCellFormatEvents = true;
			this.roleList.UseCompatibleStateImageBehavior = false;
			this.roleList.View = System.Windows.Forms.View.Details;
			// 
			// cName
			// 
			this.cName.AspectName = "name";
			this.cName.CellPadding = null;
			this.cName.Text = "Name";
			this.cName.Width = 200;
			// 
			// cBusinessUnit
			// 
			this.cBusinessUnit.AspectName = "businessunitidFormatted";
			this.cBusinessUnit.CellPadding = null;
			this.cBusinessUnit.Text = "Business Unit";
			this.cBusinessUnit.Width = 150;
			// 
			// RoleBrowserView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(517, 530);
			this.Controls.Add(this.roleList);
			this.Controls.Add(this.tools);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "RoleBrowserView";
			this.tools.ResumeLayout(false);
			this.tools.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.roleList)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip tools;
		private Greg.Xrm.Views.ToolStripBindableSplitButton tNew;
		private Greg.Xrm.Views.ToolStripBindableMenuItem tNewFromBlank;
		private Greg.Xrm.Views.ToolStripBindableMenuItem tNewCloneCurrent;
		private Greg.Xrm.Views.ToolStripBindableMenuItem tNewFromBasicUser;
		private Greg.Xrm.Views.ToolStripBindableMenuItem tNewFromAppOpener;
		private BrightIdeasSoftware.ObjectListView roleList;
		private BrightIdeasSoftware.OLVColumn cName;
		private BrightIdeasSoftware.OLVColumn cBusinessUnit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripTextBox tSearchText;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripDropDownButton tMoreFilters;
		private Greg.Xrm.Views.ToolStripBindableMenuItem tMoreFilters_HideNotCustomizableRolesToolStripMenuItem;
		private Greg.Xrm.Views.ToolStripBindableMenuItem tMoreFilters_HideManagedRolesToolStripMenuItem;
	}
}
