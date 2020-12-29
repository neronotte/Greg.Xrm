
namespace Greg.Xrm.EnvironmentComparer.Configurator
{
	partial class ConfiguratorView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfiguratorView));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tAdd = new System.Windows.Forms.ToolStripButton();
			this.tRemove = new System.Windows.Forms.ToolStripButton();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.images = new System.Windows.Forms.ImageList(this.components);
			this.tEdit = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tAdd,
            this.tEdit,
            this.tRemove});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(565, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "tools";
			// 
			// tAdd
			// 
			this.tAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tAdd.Image = ((System.Drawing.Image)(resources.GetObject("tAdd.Image")));
			this.tAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tAdd.Name = "tAdd";
			this.tAdd.Size = new System.Drawing.Size(23, 22);
			this.tAdd.Text = "toolStripButton1";
			this.tAdd.Click += new System.EventHandler(this.OnAddClick);
			// 
			// tRemove
			// 
			this.tRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tRemove.Image = ((System.Drawing.Image)(resources.GetObject("tRemove.Image")));
			this.tRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tRemove.Name = "tRemove";
			this.tRemove.Size = new System.Drawing.Size(23, 22);
			this.tRemove.Text = "toolStripButton1";
			// 
			// treeView1
			// 
			this.treeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(46)))), ((int)(((byte)(40)))));
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.treeView1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(203)))), ((int)(((byte)(203)))));
			this.treeView1.ImageIndex = 0;
			this.treeView1.ImageList = this.images;
			this.treeView1.Location = new System.Drawing.Point(0, 25);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = 0;
			this.treeView1.Size = new System.Drawing.Size(565, 485);
			this.treeView1.TabIndex = 1;
			// 
			// images
			// 
			this.images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("images.ImageStream")));
			this.images.TransparentColor = System.Drawing.Color.Transparent;
			this.images.Images.SetKeyName(0, "entity");
			this.images.Images.SetKeyName(1, "key");
			this.images.Images.SetKeyName(2, "skip");
			this.images.Images.SetKeyName(3, "active");
			// 
			// tEdit
			// 
			this.tEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tEdit.Image = ((System.Drawing.Image)(resources.GetObject("tEdit.Image")));
			this.tEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tEdit.Name = "tEdit";
			this.tEdit.Size = new System.Drawing.Size(23, 22);
			this.tEdit.Text = "toolStripButton1";
			// 
			// ConfiguratorView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(565, 510);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.toolStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ConfiguratorView";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tAdd;
		private System.Windows.Forms.ToolStripButton tRemove;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ImageList images;
		private System.Windows.Forms.ToolStripButton tEdit;
	}
}
