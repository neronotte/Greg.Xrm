
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
			this.resultTree = new System.Windows.Forms.TreeView();
			this.images = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// resultTree
			// 
			this.resultTree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(46)))), ((int)(((byte)(40)))));
			this.resultTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.resultTree.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.resultTree.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(203)))), ((int)(((byte)(203)))));
			this.resultTree.ImageIndex = 0;
			this.resultTree.ImageList = this.images;
			this.resultTree.Location = new System.Drawing.Point(0, 0);
			this.resultTree.Name = "resultTree";
			this.resultTree.SelectedImageIndex = 0;
			this.resultTree.Size = new System.Drawing.Size(657, 627);
			this.resultTree.TabIndex = 0;
			this.resultTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OnNodeSelected);
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
			// ResultSummaryView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(657, 627);
			this.Controls.Add(this.resultTree);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ResultSummaryView";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView resultTree;
		private System.Windows.Forms.ImageList images;
	}
}
