namespace Greg.Xrm.ModernThemeBuilder.Views
{
	partial class SolutionView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolutionView));
			this.tree = new System.Windows.Forms.TreeView();
			this.images = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// tree
			// 
			this.tree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tree.ImageIndex = 0;
			this.tree.ImageList = this.images;
			this.tree.Location = new System.Drawing.Point(12, 12);
			this.tree.Name = "tree";
			this.tree.SelectedImageIndex = 0;
			this.tree.Size = new System.Drawing.Size(376, 634);
			this.tree.TabIndex = 2;
			this.tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnNodeSelected);
			// 
			// images
			// 
			this.images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("images.ImageStream")));
			this.images.TransparentColor = System.Drawing.Color.Transparent;
			this.images.Images.SetKeyName(0, "dataverse.png");
			this.images.Images.SetKeyName(1, "folder.png");
			this.images.Images.SetKeyName(2, "page_white_code.png");
			// 
			// SolutionView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(400, 658);
			this.Controls.Add(this.tree);
			this.Name = "SolutionView";
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TreeView tree;
		private System.Windows.Forms.ImageList images;
	}
}
