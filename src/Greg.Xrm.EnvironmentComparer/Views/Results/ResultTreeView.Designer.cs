
namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	partial class ResultTreeView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultTreeView));
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mMarkOK = new System.Windows.Forms.ToolStripMenuItem();
			this.mUnmarkOK = new System.Windows.Forms.ToolStripMenuItem();
			this.images = new System.Windows.Forms.ImageList(this.components);
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tDownloadExcelFile = new System.Windows.Forms.ToolStripButton();
			this.resultTree = new System.Windows.Forms.TreeView();
			this.contextMenuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMarkOK,
            this.mUnmarkOK});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(162, 48);
			// 
			// mMarkOK
			// 
			this.mMarkOK.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.accept;
			this.mMarkOK.Name = "mMarkOK";
			this.mMarkOK.Size = new System.Drawing.Size(161, 22);
			this.mMarkOK.Text = "Mark as good";
			this.mMarkOK.Click += new System.EventHandler(this.OnMarkOkClick);
			// 
			// mUnmarkOK
			// 
			this.mUnmarkOK.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.delete;
			this.mUnmarkOK.Name = "mUnmarkOK";
			this.mUnmarkOK.Size = new System.Drawing.Size(161, 22);
			this.mUnmarkOK.Text = "Unmark as good";
			this.mUnmarkOK.Click += new System.EventHandler(this.OnUnmarkOkClick);
			// 
			// images
			// 
			this.images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("images.ImageStream")));
			this.images.TransparentColor = System.Drawing.Color.Transparent;
			this.images.Images.SetKeyName(0, "entity");
			this.images.Images.SetKeyName(1, "matching");
			this.images.Images.SetKeyName(2, "missing_left");
			this.images.Images.SetKeyName(3, "missing_right");
			this.images.Images.SetKeyName(4, "mismatch");
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tDownloadExcelFile});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(657, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tDownloadExcelFile
			// 
			this.tDownloadExcelFile.Enabled = false;
			this.tDownloadExcelFile.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.page_white_excel;
			this.tDownloadExcelFile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tDownloadExcelFile.Name = "tDownloadExcelFile";
			this.tDownloadExcelFile.Size = new System.Drawing.Size(139, 22);
			this.tDownloadExcelFile.Text = "Download Excel file...";
			this.tDownloadExcelFile.Click += new System.EventHandler(this.OnDownloadExcelFileClick);
			// 
			// resultTree
			// 
			this.resultTree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(46)))), ((int)(((byte)(40)))));
			this.resultTree.ContextMenuStrip = this.contextMenuStrip1;
			this.resultTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.resultTree.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.resultTree.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(203)))), ((int)(((byte)(203)))));
			this.resultTree.ImageIndex = 0;
			this.resultTree.ImageList = this.images;
			this.resultTree.Location = new System.Drawing.Point(0, 25);
			this.resultTree.Name = "resultTree";
			this.resultTree.SelectedImageIndex = 0;
			this.resultTree.Size = new System.Drawing.Size(657, 602);
			this.resultTree.TabIndex = 2;
			this.resultTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OnNodeSelected);
			// 
			// ResultTreeView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(657, 627);
			this.Controls.Add(this.resultTree);
			this.Controls.Add(this.toolStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ResultTreeView";
			this.contextMenuStrip1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ImageList images;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem mMarkOK;
		private System.Windows.Forms.ToolStripMenuItem mUnmarkOK;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.TreeView resultTree;
		private System.Windows.Forms.ToolStripButton tDownloadExcelFile;
	}
}
