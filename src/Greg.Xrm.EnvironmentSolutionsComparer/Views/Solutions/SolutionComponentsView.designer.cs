namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public partial class SolutionComponentsView
	{
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolutionComponentsView));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.tree = new BrightIdeasSoftware.TreeListView();
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.tSolutionName = new Greg.Xrm.Views.ToolStripBindableLabel();
			this.tCompareComponents = new Greg.Xrm.Views.ToolStripBindableButton();
			this.tExport = new Greg.Xrm.Views.ToolStripBindableButton();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tree)).BeginInit();
			this.contextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSolutionName,
            this.toolStripSeparator1,
            this.tCompareComponents,
            this.tExport,
            this.toolStripSeparator2,
            this.toolStripLabel1});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(693, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.toolStripLabel1.ForeColor = System.Drawing.Color.Red;
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(243, 22);
			this.toolStripLabel1.Text = "This is a preview feature... work in progress!";
			// 
			// tree
			// 
			this.tree.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.tree.Cursor = System.Windows.Forms.Cursors.Default;
			this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tree.FullRowSelect = true;
			this.tree.GridLines = true;
			this.tree.HideSelection = false;
			this.tree.Location = new System.Drawing.Point(0, 25);
			this.tree.Name = "tree";
			this.tree.OwnerDraw = true;
			this.tree.SelectAllOnControlA = false;
			this.tree.ShowGroups = false;
			this.tree.Size = new System.Drawing.Size(693, 424);
			this.tree.TabIndex = 1;
			this.tree.UseCompatibleStateImageBehavior = false;
			this.tree.View = System.Windows.Forms.View.Details;
			this.tree.VirtualMode = true;
			// 
			// contextMenu
			// 
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mCopy});
			this.contextMenu.Name = "contextMenuStrip1";
			this.contextMenu.Size = new System.Drawing.Size(103, 26);
			// 
			// mCopy
			// 
			this.mCopy.Image = global::Greg.Xrm.EnvironmentSolutionsComparer.Properties.Resources.page_white_copy;
			this.mCopy.Name = "mCopy";
			this.mCopy.Size = new System.Drawing.Size(102, 22);
			this.mCopy.Text = "Copy";
			this.mCopy.Click += new System.EventHandler(this.OnCopyClick);
			// 
			// tSolutionName
			// 
			this.tSolutionName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tSolutionName.Name = "tSolutionName";
			this.tSolutionName.Size = new System.Drawing.Size(85, 22);
			this.tSolutionName.Text = "Solution name";
			// 
			// tCompareComponents
			// 
			this.tCompareComponents.Image = global::Greg.Xrm.EnvironmentSolutionsComparer.Properties.Resources.folder_explore;
			this.tCompareComponents.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tCompareComponents.Name = "tCompareComponents";
			this.tCompareComponents.Size = new System.Drawing.Size(192, 22);
			this.tCompareComponents.Text = "Compare solution components";
			// 
			// tExport
			// 
			this.tExport.Image = global::Greg.Xrm.EnvironmentSolutionsComparer.Properties.Resources.page_white_excel;
			this.tExport.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tExport.Name = "tExport";
			this.tExport.Size = new System.Drawing.Size(69, 22);
			this.tExport.Text = "Export...";
			// 
			// SolutionComponentsView
			// 
			this.ClientSize = new System.Drawing.Size(693, 449);
			this.CloseButton = false;
			this.CloseButtonVisible = false;
			this.Controls.Add(this.tree);
			this.Controls.Add(this.toolStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SolutionComponentsView";
			this.TabText = "Solution components";
			this.Text = "Solution components";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tree)).EndInit();
			this.contextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private System.Windows.Forms.ToolStrip toolStrip1;
		private Greg.Xrm.Views.ToolStripBindableButton tCompareComponents;
		private System.ComponentModel.IContainer components;
		private BrightIdeasSoftware.TreeListView tree;
		private Greg.Xrm.Views.ToolStripBindableLabel tSolutionName;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ContextMenuStrip contextMenu;
		private System.Windows.Forms.ToolStripMenuItem mCopy;
		private Greg.Xrm.Views.ToolStripBindableButton tExport;
	}
}
