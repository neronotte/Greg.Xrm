
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
			this.cmiMarkOK = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.cmiUnmarkOK = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.cmiCopyToEnv2 = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.cmiCopyToEnv1 = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.cmiDeleteFromEnv1 = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.cmiDeleteFromEnv2 = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.images = new System.Windows.Forms.ImageList(this.components);
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tDownloadExcelFile = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.resultTree = new System.Windows.Forms.TreeView();
			this.contextMenuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiMarkOK,
            this.cmiUnmarkOK,
            this.cmiCopyToEnv2,
            this.cmiCopyToEnv1,
            this.cmiDeleteFromEnv1,
            this.cmiDeleteFromEnv2});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(181, 158);
			// 
			// mMarkOK
			// 
			this.cmiMarkOK.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.accept;
			this.cmiMarkOK.Name = "mMarkOK";
			this.cmiMarkOK.Size = new System.Drawing.Size(180, 22);
			this.cmiMarkOK.Text = "Mark as good";
			this.cmiMarkOK.Click += new System.EventHandler(this.OnMarkOkClick);
			// 
			// mUnmarkOK
			// 
			this.cmiUnmarkOK.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.delete;
			this.cmiUnmarkOK.Name = "mUnmarkOK";
			this.cmiUnmarkOK.Size = new System.Drawing.Size(180, 22);
			this.cmiUnmarkOK.Text = "Unmark as good";
			this.cmiUnmarkOK.Click += new System.EventHandler(this.OnUnmarkOkClick);
			// 
			// cmiCopyToEnv2
			// 
			this.cmiCopyToEnv2.Enabled = false;
			this.cmiCopyToEnv2.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.arrow_right;
			this.cmiCopyToEnv2.Name = "cmiCopyToEnv2";
			this.cmiCopyToEnv2.Size = new System.Drawing.Size(180, 22);
			this.cmiCopyToEnv2.Text = "Copy to ENV2";
			// 
			// cmiCopyToEnv1
			// 
			this.cmiCopyToEnv1.Enabled = false;
			this.cmiCopyToEnv1.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.arrow_left;
			this.cmiCopyToEnv1.Name = "cmiCopyToEnv1";
			this.cmiCopyToEnv1.Size = new System.Drawing.Size(180, 22);
			this.cmiCopyToEnv1.Text = "Copy to ENV1";
			// 
			// cmiDeleteFromEnv1
			// 
			this.cmiDeleteFromEnv1.Enabled = false;
			this.cmiDeleteFromEnv1.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.delete_left;
			this.cmiDeleteFromEnv1.Name = "cmiDeleteFromEnv1";
			this.cmiDeleteFromEnv1.Size = new System.Drawing.Size(180, 22);
			this.cmiDeleteFromEnv1.Text = "Delete from ENV1";
			// 
			// cmiDeleteFromEnv2
			// 
			this.cmiDeleteFromEnv2.Enabled = false;
			this.cmiDeleteFromEnv2.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.delete_right;
			this.cmiDeleteFromEnv2.Name = "cmiDeleteFromEnv2";
			this.cmiDeleteFromEnv2.Size = new System.Drawing.Size(180, 22);
			this.cmiDeleteFromEnv2.Text = "Delete from ENV2";
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
			this.resultTree.ShowNodeToolTips = true;
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
			this.TabText = "Result Summary";
			this.Text = "Result Summary";
			this.contextMenuStrip1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ImageList images;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private Greg.Xrm.Views.ToolStripBindableMenuItem cmiMarkOK;
		private Greg.Xrm.Views.ToolStripBindableMenuItem cmiUnmarkOK;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.TreeView resultTree;
		private Greg.Xrm.Views.ToolStripBindableMenuItem tDownloadExcelFile;
		private Greg.Xrm.Views.ToolStripBindableMenuItem cmiCopyToEnv2;
		private Greg.Xrm.Views.ToolStripBindableMenuItem cmiCopyToEnv1;
		private Greg.Xrm.Views.ToolStripBindableMenuItem cmiDeleteFromEnv1;
		private Greg.Xrm.Views.ToolStripBindableMenuItem cmiDeleteFromEnv2;
	}
}
