
namespace Greg.Xrm.SolutionManager.Views.DataTree
{
	partial class DataTreeView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataTreeView));
			this.tree = new System.Windows.Forms.TreeView();
			this.images = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// tree
			// 
			this.tree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(46)))), ((int)(((byte)(40)))));
			this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tree.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tree.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(203)))), ((int)(((byte)(203)))));
			this.tree.ImageIndex = 0;
			this.tree.ImageList = this.images;
			this.tree.Location = new System.Drawing.Point(0, 0);
			this.tree.Name = "tree";
			this.tree.SelectedImageIndex = 0;
			this.tree.Size = new System.Drawing.Size(284, 261);
			this.tree.TabIndex = 0;
			// 
			// images
			// 
			this.images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("images.ImageStream")));
			this.images.TransparentColor = System.Drawing.Color.Transparent;
			this.images.Images.SetKeyName(0, "node");
			this.images.Images.SetKeyName(1, "attribute");
			// 
			// DataTreeView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.CloseButton = false;
			this.CloseButtonVisible = false;
			this.Controls.Add(this.tree);
			this.Name = "DataTreeView";
			this.TabText = "Output tree";
			this.Text = "Output tree";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView tree;
		private System.Windows.Forms.ImageList images;
	}
}
