namespace Greg.Xrm.EnvironmentSolutionsComparer.Views
{
	public partial class MainView
	{

		private void InitializeComponent()
		{
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.tClose = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tClose,
            this.toolStripSeparator1});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(788, 25);
			this.toolStrip.TabIndex = 0;
			this.toolStrip.Text = "toolStrip1";
			// 
			// tClose
			// 
			this.tClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tClose.Image = global::Greg.Xrm.EnvironmentSolutionsComparer.Properties.Resources.door_out;
			this.tClose.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tClose.Name = "tClose";
			this.tClose.Size = new System.Drawing.Size(23, 22);
			this.tClose.Text = "Close tool";
			this.tClose.Click += new System.EventHandler(this.OnCloseClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// dockPanel
			// 
			this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingSdi;
			this.dockPanel.Location = new System.Drawing.Point(0, 25);
			this.dockPanel.Name = "dockPanel";
			this.dockPanel.Size = new System.Drawing.Size(788, 415);
			this.dockPanel.TabIndex = 1;
			// 
			// MainView
			// 
			this.Controls.Add(this.dockPanel);
			this.Controls.Add(this.toolStrip);
			this.Name = "MainView";
			this.Size = new System.Drawing.Size(788, 440);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private System.Windows.Forms.ToolStrip toolStrip;
		private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
		private System.Windows.Forms.ToolStripButton tClose;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}
