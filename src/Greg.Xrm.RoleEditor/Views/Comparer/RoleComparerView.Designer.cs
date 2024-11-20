namespace Greg.Xrm.RoleEditor.Views.Comparer
{
	partial class RoleComparerView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoleComparerView));
			this.tools = new System.Windows.Forms.ToolStrip();
			this.tExportExcel = new Greg.Xrm.Views.ToolStripBindableButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tree = new BrightIdeasSoftware.TreeListView();
			this.cName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cRole1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cRole2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.privilegeImagesNew = new System.Windows.Forms.ImageList(this.components);
			this.privilegeImagesOld = new System.Windows.Forms.ImageList(this.components);
			this.tools.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tree)).BeginInit();
			this.SuspendLayout();
			// 
			// tools
			// 
			this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tExportExcel,
            this.toolStripSeparator1});
			this.tools.Location = new System.Drawing.Point(0, 0);
			this.tools.Name = "tools";
			this.tools.Size = new System.Drawing.Size(832, 25);
			this.tools.TabIndex = 0;
			this.tools.Text = "toolStrip1";
			// 
			// tExportExcel
			// 
			this.tExportExcel.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.page_white_excel;
			this.tExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tExportExcel.Name = "tExportExcel";
			this.tExportExcel.Size = new System.Drawing.Size(105, 22);
			this.tExportExcel.Text = "Export to Excel";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tree
			// 
			this.tree.AllColumns.Add(this.cName);
			this.tree.AllColumns.Add(this.cRole1);
			this.tree.AllColumns.Add(this.cRole2);
			this.tree.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cName,
            this.cRole1,
            this.cRole2});
			this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tree.FullRowSelect = true;
			this.tree.GridLines = true;
			this.tree.HideSelection = false;
			this.tree.Location = new System.Drawing.Point(0, 25);
			this.tree.Name = "tree";
			this.tree.OwnerDraw = true;
			this.tree.ShowGroups = false;
			this.tree.Size = new System.Drawing.Size(832, 495);
			this.tree.TabIndex = 1;
			this.tree.UseCompatibleStateImageBehavior = false;
			this.tree.View = System.Windows.Forms.View.Details;
			this.tree.VirtualMode = true;
			// 
			// cName
			// 
			this.cName.AspectName = "PrivilegeName";
			this.cName.CellPadding = null;
			this.cName.Sortable = false;
			this.cName.Text = "Group/Privilege Name";
			this.cName.Width = 300;
			// 
			// cRole1
			// 
			this.cRole1.AspectName = "Level1";
			this.cRole1.CellPadding = null;
			this.cRole1.Sortable = false;
			this.cRole1.Text = "Role 1";
			this.cRole1.Width = 200;
			// 
			// cRole2
			// 
			this.cRole2.AspectName = "Level2";
			this.cRole2.CellPadding = null;
			this.cRole2.Sortable = false;
			this.cRole2.Text = "Role 2";
			this.cRole2.Width = 200;
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
			// RoleComparerView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(832, 520);
			this.Controls.Add(this.tree);
			this.Controls.Add(this.tools);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "RoleComparerView";
			this.tools.ResumeLayout(false);
			this.tools.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tree)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip tools;
		private BrightIdeasSoftware.TreeListView tree;
		private BrightIdeasSoftware.OLVColumn cName;
		private BrightIdeasSoftware.OLVColumn cRole1;
		private BrightIdeasSoftware.OLVColumn cRole2;
		private System.Windows.Forms.ImageList privilegeImagesNew;
		private System.Windows.Forms.ImageList privilegeImagesOld;
		private Xrm.Views.ToolStripBindableButton tExportExcel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}
