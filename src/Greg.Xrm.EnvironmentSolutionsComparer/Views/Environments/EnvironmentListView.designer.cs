namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Environments
{
	public partial class EnvironmentListView
	{
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnvironmentListView));
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.environmentList = new System.Windows.Forms.ListView();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.tAddEnvironment = new Greg.Xrm.Views.ToolStripBindableButton();
			this.tRemoveEnvironment = new Greg.Xrm.Views.ToolStripBindableButton();
			this.cName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tAddEnvironment,
            this.tRemoveEnvironment});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(317, 25);
			this.toolStrip.TabIndex = 0;
			this.toolStrip.Text = "toolStrip1";
			// 
			// environmentList
			// 
			this.environmentList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cName});
			this.environmentList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.environmentList.HideSelection = false;
			this.environmentList.LargeImageList = this.imageList;
			this.environmentList.Location = new System.Drawing.Point(0, 25);
			this.environmentList.MultiSelect = false;
			this.environmentList.Name = "environmentList";
			this.environmentList.Size = new System.Drawing.Size(317, 574);
			this.environmentList.SmallImageList = this.imageList;
			this.environmentList.StateImageList = this.imageList;
			this.environmentList.TabIndex = 1;
			this.environmentList.UseCompatibleStateImageBehavior = false;
			this.environmentList.View = System.Windows.Forms.View.Details;
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "environment");
			// 
			// tAddEnvironment
			// 
			this.tAddEnvironment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tAddEnvironment.Image = global::Greg.Xrm.EnvironmentSolutionsComparer.Properties.Resources.database_add;
			this.tAddEnvironment.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tAddEnvironment.Name = "tAddEnvironment";
			this.tAddEnvironment.Size = new System.Drawing.Size(23, 22);
			this.tAddEnvironment.Text = "Add environment";
			// 
			// tRemoveEnvironment
			// 
			this.tRemoveEnvironment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tRemoveEnvironment.Image = global::Greg.Xrm.EnvironmentSolutionsComparer.Properties.Resources.database_delete;
			this.tRemoveEnvironment.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tRemoveEnvironment.Name = "tRemoveEnvironment";
			this.tRemoveEnvironment.Size = new System.Drawing.Size(23, 22);
			this.tRemoveEnvironment.Text = "Remove environment";
			// 
			// cName
			// 
			this.cName.Text = "Environment";
			this.cName.Width = 200;
			// 
			// EnvironmentListView
			// 
			this.ClientSize = new System.Drawing.Size(317, 599);
			this.CloseButton = false;
			this.CloseButtonVisible = false;
			this.Controls.Add(this.environmentList);
			this.Controls.Add(this.toolStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "EnvironmentListView";
			this.Text = "Environment list";
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ListView environmentList;
		private System.Windows.Forms.ImageList imageList;
		private System.ComponentModel.IContainer components;
		private Greg.Xrm.Views.ToolStripBindableButton tAddEnvironment;
		private Greg.Xrm.Views.ToolStripBindableButton tRemoveEnvironment;
		private System.Windows.Forms.ColumnHeader cName;
	}
}
