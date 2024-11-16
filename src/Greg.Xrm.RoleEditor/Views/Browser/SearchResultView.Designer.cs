namespace Greg.Xrm.RoleEditor.Views.Browser
{
	partial class SearchResultView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchResultView));
			this.tools = new System.Windows.Forms.ToolStrip();
			this.tBack = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tSearchDescription = new System.Windows.Forms.ToolStripLabel();
			this.roleList = new BrightIdeasSoftware.ObjectListView();
			this.cRoleName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cBusinessUnit = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.images = new System.Windows.Forms.ImageList(this.components);
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tCloneSelected = new System.Windows.Forms.ToolStripMenuItem();
			this.tools.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.roleList)).BeginInit();
			this.contextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// tools
			// 
			this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tBack,
            this.toolStripSeparator1,
            this.tSearchDescription});
			this.tools.Location = new System.Drawing.Point(0, 0);
			this.tools.Name = "tools";
			this.tools.Size = new System.Drawing.Size(368, 25);
			this.tools.TabIndex = 0;
			this.tools.Text = "toolStrip1";
			// 
			// tBack
			// 
			this.tBack.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.arrow_left;
			this.tBack.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tBack.Name = "tBack";
			this.tBack.Size = new System.Drawing.Size(52, 22);
			this.tBack.Text = "Back";
			this.tBack.ToolTipText = "Back to the role tree view";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tSearchDescription
			// 
			this.tSearchDescription.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tSearchDescription.Name = "tSearchDescription";
			this.tSearchDescription.Size = new System.Drawing.Size(80, 22);
			this.tSearchDescription.Text = "Search results";
			// 
			// roleList
			// 
			this.roleList.AllColumns.Add(this.cRoleName);
			this.roleList.AllColumns.Add(this.cBusinessUnit);
			this.roleList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cRoleName,
            this.cBusinessUnit});
			this.roleList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.roleList.FullRowSelect = true;
			this.roleList.GridLines = true;
			this.roleList.HideSelection = false;
			this.roleList.Location = new System.Drawing.Point(0, 25);
			this.roleList.Name = "roleList";
			this.roleList.ShowGroups = false;
			this.roleList.Size = new System.Drawing.Size(368, 432);
			this.roleList.SmallImageList = this.images;
			this.roleList.TabIndex = 1;
			this.roleList.UseCompatibleStateImageBehavior = false;
			this.roleList.View = System.Windows.Forms.View.Details;
			// 
			// cRoleName
			// 
			this.cRoleName.AspectName = "name";
			this.cRoleName.CellPadding = null;
			this.cRoleName.Text = "Name";
			this.cRoleName.Width = 200;
			// 
			// cBusinessUnit
			// 
			this.cBusinessUnit.AspectName = "businessunitidFormatted";
			this.cBusinessUnit.CellPadding = null;
			this.cBusinessUnit.Text = "Business Unit";
			this.cBusinessUnit.Width = 150;
			// 
			// images
			// 
			this.images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("images.ImageStream")));
			this.images.TransparentColor = System.Drawing.Color.Transparent;
			this.images.Images.SetKeyName(0, "env");
			this.images.Images.SetKeyName(1, "bu");
			this.images.Images.SetKeyName(2, "role");
			// 
			// contextMenu
			// 
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tCloneSelected});
			this.contextMenu.Name = "contextMenu";
			this.contextMenu.Size = new System.Drawing.Size(175, 26);
			// 
			// tCloneSelected
			// 
			this.tCloneSelected.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.page_white_stack;
			this.tCloneSelected.Name = "tCloneSelected";
			this.tCloneSelected.Size = new System.Drawing.Size(174, 22);
			this.tCloneSelected.Text = "Clone selected role";
			// 
			// SearchResultView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.roleList);
			this.Controls.Add(this.tools);
			this.Name = "SearchResultView";
			this.Size = new System.Drawing.Size(368, 457);
			this.tools.ResumeLayout(false);
			this.tools.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.roleList)).EndInit();
			this.contextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip tools;
		private System.Windows.Forms.ToolStripButton tBack;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private BrightIdeasSoftware.ObjectListView roleList;
		private System.Windows.Forms.ImageList images;
		private System.Windows.Forms.ToolStripLabel tSearchDescription;
		private BrightIdeasSoftware.OLVColumn cRoleName;
		private BrightIdeasSoftware.OLVColumn cBusinessUnit;
		private System.Windows.Forms.ContextMenuStrip contextMenu;
		private System.Windows.Forms.ToolStripMenuItem tCloneSelected;
	}
}
