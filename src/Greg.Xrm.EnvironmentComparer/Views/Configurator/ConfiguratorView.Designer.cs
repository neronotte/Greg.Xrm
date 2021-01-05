
namespace Greg.Xrm.EnvironmentComparer.Views.Configurator
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
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tLoadEntities = new Greg.Xrm.Views.ToolStripBindableButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tAdd = new Greg.Xrm.Views.ToolStripBindableButton();
			this.tEdit = new Greg.Xrm.Views.ToolStripBindableButton();
			this.tRemove = new Greg.Xrm.Views.ToolStripBindableButton();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.images = new System.Windows.Forms.ImageList(this.components);
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mOpen = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.mSave = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.mSaveAs = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mLoadEntities = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.mAdd = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.mEdit = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.mRemove = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.compareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mExecute = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tExecute = new Greg.Xrm.Views.ToolStripBindableButton();
			this.toolStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.tLoadEntities,
            this.toolStripSeparator1,
            this.tAdd,
            this.tEdit,
            this.tRemove,
            this.toolStripSeparator3,
            this.tExecute});
			this.toolStrip1.Location = new System.Drawing.Point(0, 24);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(565, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "tools";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tLoadEntities
			// 
			this.tLoadEntities.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tLoadEntities.Enabled = false;
			this.tLoadEntities.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.table_refresh;
			this.tLoadEntities.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tLoadEntities.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tLoadEntities.Name = "tLoadEntities";
			this.tLoadEntities.Size = new System.Drawing.Size(23, 22);
			this.tLoadEntities.Text = "Load entities";
			this.tLoadEntities.Click += new System.EventHandler(this.OnLoadEntitiesClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tAdd
			// 
			this.tAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tAdd.Enabled = false;
			this.tAdd.Image = ((System.Drawing.Image)(resources.GetObject("tAdd.Image")));
			this.tAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tAdd.Name = "tAdd";
			this.tAdd.Size = new System.Drawing.Size(23, 22);
			this.tAdd.Text = "Add entity";
			this.tAdd.Click += new System.EventHandler(this.OnAddClick);
			// 
			// tEdit
			// 
			this.tEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tEdit.Enabled = false;
			this.tEdit.Image = ((System.Drawing.Image)(resources.GetObject("tEdit.Image")));
			this.tEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tEdit.Name = "tEdit";
			this.tEdit.Size = new System.Drawing.Size(23, 22);
			this.tEdit.Text = "Edit";
			this.tEdit.Click += new System.EventHandler(this.OnEditClick);
			// 
			// tRemove
			// 
			this.tRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tRemove.Enabled = false;
			this.tRemove.Image = ((System.Drawing.Image)(resources.GetObject("tRemove.Image")));
			this.tRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tRemove.Name = "tRemove";
			this.tRemove.Size = new System.Drawing.Size(23, 22);
			this.tRemove.Text = "Remove";
			this.tRemove.Click += new System.EventHandler(this.OnRemoveClick);
			// 
			// treeView1
			// 
			this.treeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(46)))), ((int)(((byte)(40)))));
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.treeView1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(203)))), ((int)(((byte)(203)))));
			this.treeView1.ImageIndex = 0;
			this.treeView1.ImageList = this.images;
			this.treeView1.Location = new System.Drawing.Point(0, 49);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = 0;
			this.treeView1.Size = new System.Drawing.Size(565, 461);
			this.treeView1.TabIndex = 1;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnAfterSelectTreeNode);
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
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.actionsToolStripMenuItem,
            this.compareToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(565, 24);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mOpen,
            this.mSave,
            this.mSaveAs});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// mOpen
			// 
			this.mOpen.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.folder;
			this.mOpen.Name = "mOpen";
			this.mOpen.Size = new System.Drawing.Size(121, 22);
			this.mOpen.Text = "Open...";
			this.mOpen.Click += new System.EventHandler(this.OnOpenClick);
			// 
			// mSave
			// 
			this.mSave.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.disk;
			this.mSave.Name = "mSave";
			this.mSave.Size = new System.Drawing.Size(121, 22);
			this.mSave.Text = "Save";
			this.mSave.Click += new System.EventHandler(this.OnSaveClick);
			// 
			// mSaveAs
			// 
			this.mSaveAs.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.disk;
			this.mSaveAs.Name = "mSaveAs";
			this.mSaveAs.Size = new System.Drawing.Size(121, 22);
			this.mSaveAs.Text = "Save as...";
			this.mSaveAs.Click += new System.EventHandler(this.OnSaveAsClick);
			// 
			// actionsToolStripMenuItem
			// 
			this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mLoadEntities,
            this.toolStripMenuItem1,
            this.mAdd,
            this.mEdit,
            this.mRemove});
			this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
			this.actionsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
			this.actionsToolStripMenuItem.Text = "Actions";
			// 
			// mLoadEntities
			// 
			this.mLoadEntities.Enabled = false;
			this.mLoadEntities.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.table_refresh;
			this.mLoadEntities.Name = "mLoadEntities";
			this.mLoadEntities.Size = new System.Drawing.Size(141, 22);
			this.mLoadEntities.Text = "Load entities";
			this.mLoadEntities.Click += new System.EventHandler(this.OnLoadEntitiesClick);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(138, 6);
			// 
			// mAdd
			// 
			this.mAdd.Enabled = false;
			this.mAdd.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.table_add;
			this.mAdd.Name = "mAdd";
			this.mAdd.Size = new System.Drawing.Size(141, 22);
			this.mAdd.Text = "Add entity";
			this.mAdd.Click += new System.EventHandler(this.OnAddClick);
			// 
			// mEdit
			// 
			this.mEdit.Enabled = false;
			this.mEdit.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.table_edit;
			this.mEdit.Name = "mEdit";
			this.mEdit.Size = new System.Drawing.Size(141, 22);
			this.mEdit.Text = "Edit";
			this.mEdit.Click += new System.EventHandler(this.OnEditClick);
			// 
			// mRemove
			// 
			this.mRemove.Enabled = false;
			this.mRemove.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.table_delete;
			this.mRemove.Name = "mRemove";
			this.mRemove.Size = new System.Drawing.Size(141, 22);
			this.mRemove.Text = "Remove";
			this.mRemove.Click += new System.EventHandler(this.OnRemoveClick);
			// 
			// compareToolStripMenuItem
			// 
			this.compareToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mExecute});
			this.compareToolStripMenuItem.Name = "compareToolStripMenuItem";
			this.compareToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
			this.compareToolStripMenuItem.Text = "Compare";
			// 
			// mExecute
			// 
			this.mExecute.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.compare;
			this.mExecute.Name = "mExecute";
			this.mExecute.Size = new System.Drawing.Size(181, 22);
			this.mExecute.Text = "Execute comparison";
			this.mExecute.Click += new System.EventHandler(this.OnExecuteClick);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// tExecute
			// 
			this.tExecute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tExecute.Enabled = false;
			this.tExecute.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.compare;
			this.tExecute.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tExecute.Name = "tExecute";
			this.tExecute.Size = new System.Drawing.Size(23, 22);
			this.tExecute.Text = "Execute comparison";
			this.tExecute.Click += new System.EventHandler(this.OnExecuteClick);
			// 
			// ConfiguratorView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(565, 510);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "ConfiguratorView";
			this.TabText = "Configurator";
			this.Text = "Configurator";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private Greg.Xrm.Views.ToolStripBindableButton tAdd;
		private Greg.Xrm.Views.ToolStripBindableButton tRemove;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ImageList images;
		private Greg.Xrm.Views.ToolStripBindableButton tEdit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
		private Greg.Xrm.Views.ToolStripBindableMenuItem mLoadEntities;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private Greg.Xrm.Views.ToolStripBindableMenuItem mAdd;
		private Greg.Xrm.Views.ToolStripBindableMenuItem mEdit;
		private Greg.Xrm.Views.ToolStripBindableMenuItem mRemove;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private Greg.Xrm.Views.ToolStripBindableMenuItem mOpen;
		private Greg.Xrm.Views.ToolStripBindableMenuItem mSave;
		private Greg.Xrm.Views.ToolStripBindableMenuItem mSaveAs;
		private System.Windows.Forms.ToolStripMenuItem compareToolStripMenuItem;
		private Greg.Xrm.Views.ToolStripBindableMenuItem mExecute;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private Greg.Xrm.Views.ToolStripBindableButton tLoadEntities;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private Greg.Xrm.Views.ToolStripBindableButton tExecute;
	}
}
