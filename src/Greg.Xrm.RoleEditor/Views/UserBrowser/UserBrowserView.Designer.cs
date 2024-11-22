namespace Greg.Xrm.RoleEditor.Views.UserBrowser
{
	partial class UserBrowserView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserBrowserView));
			this.notifications = new Greg.Xrm.Core.Views.NotificationPanel();
			this.tools = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
			this.tSearchText = new Greg.Xrm.Views.ToolStripBindableTextBox();
			this.images = new System.Windows.Forms.ImageList(this.components);
			this.cName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.cBusinessUnit = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.userTree = new BrightIdeasSoftware.TreeListView();
			this.tools.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.userTree)).BeginInit();
			this.SuspendLayout();
			// 
			// notifications
			// 
			this.notifications.AutoSize = true;
			this.notifications.Dock = System.Windows.Forms.DockStyle.Top;
			this.notifications.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.notifications.Location = new System.Drawing.Point(0, 0);
			this.notifications.Name = "notifications";
			this.notifications.Size = new System.Drawing.Size(447, 0);
			this.notifications.TabIndex = 0;
			// 
			// tools
			// 
			this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.tSearchText});
			this.tools.Location = new System.Drawing.Point(0, 0);
			this.tools.Name = "tools";
			this.tools.Size = new System.Drawing.Size(447, 25);
			this.tools.TabIndex = 1;
			this.tools.Text = "toolStrip1";
			// 
			// toolStripLabel2
			// 
			this.toolStripLabel2.AutoSize = false;
			this.toolStripLabel2.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.zoom;
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new System.Drawing.Size(22, 22);
			// 
			// tSearchText
			// 
			this.tSearchText.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.tSearchText.Name = "tSearchText";
			this.tSearchText.Size = new System.Drawing.Size(150, 25);
			this.tSearchText.Watermark = "Search user";
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
			// 
			// cName
			// 
			this.cName.AspectName = "FullName";
			this.cName.CellPadding = null;
			this.cName.Text = "Name";
			this.cName.Width = 300;
			// 
			// cBusinessUnit
			// 
			this.cBusinessUnit.AspectName = "BusinessUnit";
			this.cBusinessUnit.CellPadding = null;
			this.cBusinessUnit.FillsFreeSpace = true;
			this.cBusinessUnit.Text = "Business Unit";
			this.cBusinessUnit.Width = 150;
			// 
			// userTree
			// 
			this.userTree.AllColumns.Add(this.cName);
			this.userTree.AllColumns.Add(this.cBusinessUnit);
			this.userTree.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cName,
            this.cBusinessUnit});
			this.userTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.userTree.FullRowSelect = true;
			this.userTree.GridLines = true;
			this.userTree.HideSelection = false;
			this.userTree.Location = new System.Drawing.Point(0, 25);
			this.userTree.Name = "userTree";
			this.userTree.OwnerDraw = true;
			this.userTree.ShowGroups = false;
			this.userTree.Size = new System.Drawing.Size(447, 464);
			this.userTree.SmallImageList = this.images;
			this.userTree.TabIndex = 2;
			this.userTree.UseCellFormatEvents = true;
			this.userTree.UseCompatibleStateImageBehavior = false;
			this.userTree.UseFiltering = true;
			this.userTree.View = System.Windows.Forms.View.Details;
			this.userTree.VirtualMode = true;
			// 
			// UserBrowserView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(447, 489);
			this.CloseButton = false;
			this.CloseButtonVisible = false;
			this.Controls.Add(this.userTree);
			this.Controls.Add(this.tools);
			this.Controls.Add(this.notifications);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "UserBrowserView";
			this.tools.ResumeLayout(false);
			this.tools.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.userTree)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Core.Views.NotificationPanel notifications;
		private System.Windows.Forms.ToolStrip tools;
		private Xrm.Views.ToolStripBindableTextBox tSearchText;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		private System.Windows.Forms.ImageList images;
		private BrightIdeasSoftware.OLVColumn cName;
		private BrightIdeasSoftware.OLVColumn cBusinessUnit;
		private BrightIdeasSoftware.TreeListView userTree;
	}
}
