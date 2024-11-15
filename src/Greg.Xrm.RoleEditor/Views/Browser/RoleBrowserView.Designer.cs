﻿namespace Greg.Xrm.RoleEditor.Views.RoleBrowser
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoleBrowserView));
			this.tools = new System.Windows.Forms.ToolStrip();
			this.tNew = new Greg.Xrm.Views.ToolStripBindableDropDownButton();
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
			this.roleTree = new BrightIdeasSoftware.TreeListView();
			this.cTreeName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.tools.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.roleTree)).BeginInit();
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
			this.tools.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.tools.Location = new System.Drawing.Point(0, 0);
			this.tools.Name = "tools";
			this.tools.Size = new System.Drawing.Size(517, 25);
			this.tools.TabIndex = 0;
			this.tools.Text = "toolStrip1";
			// 
			// tNew
			// 
			this.tNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tNewFromBlank,
            this.tNewCloneCurrent,
            this.tNewFromBasicUser,
            this.tNewFromAppOpener});
			this.tNew.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.page_white_add;
			this.tNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tNew.Name = "tNew";
			this.tNew.Size = new System.Drawing.Size(86, 22);
			this.tNew.Text = "New Role";
			// 
			// tNewFromBlank
			// 
			this.tNewFromBlank.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.page_white;
			this.tNewFromBlank.Name = "tNewFromBlank";
			this.tNewFromBlank.Size = new System.Drawing.Size(203, 22);
			this.tNewFromBlank.Text = "Start from an empty role";
			// 
			// tNewCloneCurrent
			// 
			this.tNewCloneCurrent.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.page_white_stack;
			this.tNewCloneCurrent.Name = "tNewCloneCurrent";
			this.tNewCloneCurrent.Size = new System.Drawing.Size(203, 22);
			this.tNewCloneCurrent.Text = "Clone selected role";
			// 
			// tNewFromBasicUser
			// 
			this.tNewFromBasicUser.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.page_white_code;
			this.tNewFromBasicUser.Name = "tNewFromBasicUser";
			this.tNewFromBasicUser.Size = new System.Drawing.Size(203, 22);
			this.tNewFromBasicUser.Text = "From \"Basic User\"";
			// 
			// tNewFromAppOpener
			// 
			this.tNewFromAppOpener.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.page_white_code_red;
			this.tNewFromAppOpener.Name = "tNewFromAppOpener";
			this.tNewFromAppOpener.Size = new System.Drawing.Size(203, 22);
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
			// roleTree
			// 
			this.roleTree.AllColumns.Add(this.cTreeName);
			this.roleTree.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cTreeName});
			this.roleTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.roleTree.GridLines = true;
			this.roleTree.HideSelection = false;
			this.roleTree.Location = new System.Drawing.Point(0, 25);
			this.roleTree.Name = "roleTree";
			this.roleTree.OwnerDraw = true;
			this.roleTree.ShowGroups = false;
			this.roleTree.Size = new System.Drawing.Size(517, 505);
			this.roleTree.SmallImageList = this.imageList1;
			this.roleTree.TabIndex = 2;
			this.roleTree.UseCompatibleStateImageBehavior = false;
			this.roleTree.View = System.Windows.Forms.View.Details;
			this.roleTree.VirtualMode = true;
			// 
			// cTreeName
			// 
			this.cTreeName.AspectName = "name";
			this.cTreeName.CellPadding = null;
			this.cTreeName.FillsFreeSpace = true;
			this.cTreeName.Text = "Name";
			this.cTreeName.Width = 300;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "env");
			this.imageList1.Images.SetKeyName(1, "bu");
			this.imageList1.Images.SetKeyName(2, "role");
			// 
			// RoleBrowserView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(517, 530);
			this.CloseButton = false;
			this.CloseButtonVisible = false;
			this.Controls.Add(this.roleTree);
			this.Controls.Add(this.tools);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "RoleBrowserView";
			this.tools.ResumeLayout(false);
			this.tools.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.roleTree)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip tools;
		private Greg.Xrm.Views.ToolStripBindableDropDownButton tNew;
		private Greg.Xrm.Views.ToolStripBindableMenuItem tNewFromBlank;
		private Greg.Xrm.Views.ToolStripBindableMenuItem tNewCloneCurrent;
		private Greg.Xrm.Views.ToolStripBindableMenuItem tNewFromBasicUser;
		private Greg.Xrm.Views.ToolStripBindableMenuItem tNewFromAppOpener;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripTextBox tSearchText;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripDropDownButton tMoreFilters;
		private Greg.Xrm.Views.ToolStripBindableMenuItem tMoreFilters_HideNotCustomizableRolesToolStripMenuItem;
		private Greg.Xrm.Views.ToolStripBindableMenuItem tMoreFilters_HideManagedRolesToolStripMenuItem;
		private BrightIdeasSoftware.TreeListView roleTree;
		private BrightIdeasSoftware.OLVColumn cTreeName;
		private System.Windows.Forms.ImageList imageList1;
	}
}
