namespace Greg.Xrm.RoleEditor.Views.AddUserRoles
{
	partial class UserRolesView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserRolesView));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tApply = new Greg.Xrm.Views.ToolStripBindableButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tMatrixVisibility = new System.Windows.Forms.ToolStripLabel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.lblDrop = new System.Windows.Forms.Label();
			this.treeListView1 = new BrightIdeasSoftware.TreeListView();
			this.cName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cBusinessUnit = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cDomainName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.images = new System.Windows.Forms.ImageList(this.components);
			this.toolStrip1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tApply,
            this.toolStripSeparator1,
            this.tMatrixVisibility});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(900, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tApply
			// 
			this.tApply.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.lightning;
			this.tApply.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tApply.Name = "tApply";
			this.tApply.Size = new System.Drawing.Size(58, 22);
			this.tApply.Text = "Apply";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tMatrixVisibility
			// 
			this.tMatrixVisibility.BackColor = System.Drawing.SystemColors.Control;
			this.tMatrixVisibility.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tMatrixVisibility.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.information;
			this.tMatrixVisibility.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.tMatrixVisibility.Name = "tMatrixVisibility";
			this.tMatrixVisibility.Size = new System.Drawing.Size(288, 22);
			this.tMatrixVisibility.Text = "Record ownership across Business Units is enabled.";
			this.tMatrixVisibility.Visible = false;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.lblDrop, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.treeListView1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(900, 410);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// lblDrop
			// 
			this.lblDrop.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblDrop.Location = new System.Drawing.Point(3, 0);
			this.lblDrop.Name = "lblDrop";
			this.lblDrop.Size = new System.Drawing.Size(894, 50);
			this.lblDrop.TabIndex = 0;
			this.lblDrop.Text = resources.GetString("lblDrop.Text");
			this.lblDrop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// treeListView1
			// 
			this.treeListView1.AllColumns.Add(this.cName);
			this.treeListView1.AllColumns.Add(this.cBusinessUnit);
			this.treeListView1.AllColumns.Add(this.cDomainName);
			this.treeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cName,
            this.cBusinessUnit,
            this.cDomainName});
			this.treeListView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeListView1.HideSelection = false;
			this.treeListView1.Location = new System.Drawing.Point(3, 53);
			this.treeListView1.Name = "treeListView1";
			this.treeListView1.OwnerDraw = true;
			this.treeListView1.ShowGroups = false;
			this.treeListView1.Size = new System.Drawing.Size(894, 354);
			this.treeListView1.SmallImageList = this.images;
			this.treeListView1.TabIndex = 1;
			this.treeListView1.UseCompatibleStateImageBehavior = false;
			this.treeListView1.View = System.Windows.Forms.View.Details;
			this.treeListView1.VirtualMode = true;
			// 
			// cName
			// 
			this.cName.AspectName = "Name";
			this.cName.CellPadding = null;
			this.cName.Text = "Name";
			this.cName.Width = 200;
			// 
			// cBusinessUnit
			// 
			this.cBusinessUnit.AspectName = "BusinessUnit";
			this.cBusinessUnit.CellPadding = null;
			this.cBusinessUnit.Text = "Business Unit";
			this.cBusinessUnit.Width = 150;
			// 
			// cDomainName
			// 
			this.cDomainName.AspectName = "DomainName";
			this.cDomainName.CellPadding = null;
			this.cDomainName.FillsFreeSpace = true;
			this.cDomainName.Text = "DomainName";
			this.cDomainName.Width = 150;
			// 
			// images
			// 
			this.images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("images.ImageStream")));
			this.images.TransparentColor = System.Drawing.Color.Transparent;
			this.images.Images.SetKeyName(0, "user");
			this.images.Images.SetKeyName(1, "businessunit");
			this.images.Images.SetKeyName(2, "role");
			this.images.Images.SetKeyName(3, "group");
			this.images.Images.SetKeyName(4, "loading");
			this.images.Images.SetKeyName(5, "env");
			this.images.Images.SetKeyName(6, "rolelist");
			// 
			// UserRolesView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(900, 435);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.toolStrip1);
			this.Name = "UserRolesView";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ImageList images;
		private System.Windows.Forms.Label lblDrop;
		private BrightIdeasSoftware.TreeListView treeListView1;
		private BrightIdeasSoftware.OLVColumn cName;
		private BrightIdeasSoftware.OLVColumn cBusinessUnit;
		private BrightIdeasSoftware.OLVColumn cDomainName;
		private System.Windows.Forms.ToolStripLabel tMatrixVisibility;
		private Greg.Xrm.Views.ToolStripBindableButton tApply;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}
