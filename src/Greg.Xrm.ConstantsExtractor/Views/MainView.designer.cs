
namespace Greg.Xrm.ConstantsExtractor.Views
{
	partial class MainView
	{
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur de composants

		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.toolStripMenu = new System.Windows.Forms.ToolStrip();
			this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.tsbClose = new System.Windows.Forms.ToolStripButton();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripMenu
			// 
			this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tssSeparator1,
            this.toolStripLabel1});
			this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
			this.toolStripMenu.Name = "toolStripMenu";
			this.toolStripMenu.Size = new System.Drawing.Size(1408, 25);
			this.toolStripMenu.TabIndex = 4;
			this.toolStripMenu.Text = "toolStrip1";
			// 
			// tssSeparator1
			// 
			this.tssSeparator1.Name = "tssSeparator1";
			this.tssSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// dockPanel
			// 
			this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingSdi;
			this.dockPanel.Location = new System.Drawing.Point(0, 25);
			this.dockPanel.Name = "dockPanel";
			this.dockPanel.Size = new System.Drawing.Size(1408, 974);
			this.dockPanel.TabIndex = 5;
			// 
			// tsbClose
			// 
			this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbClose.Image = global::Greg.Xrm.ConstantsExtractor.Properties.Resources.door_out;
			this.tsbClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tsbClose.Name = "tsbClose";
			this.tsbClose.Size = new System.Drawing.Size(23, 22);
			this.tsbClose.Text = "Close this tool";
			this.tsbClose.Click += new System.EventHandler(this.OnCloseToolRequested);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.IsLink = true;
			this.toolStripLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(144, 22);
			this.toolStripLabel1.Text = "Credits to: Gianluca Tocco";
			// 
			// ConstantsExtractorPluginControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dockPanel);
			this.Controls.Add(this.toolStripMenu);
			this.Name = "ConstantsExtractorPluginControl";
			this.Size = new System.Drawing.Size(1408, 999);
			this.toolStripMenu.ResumeLayout(false);
			this.toolStripMenu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolStrip toolStripMenu;
		private System.Windows.Forms.ToolStripButton tsbClose;
		private System.Windows.Forms.ToolStripSeparator tssSeparator1;
		private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
	}
}
