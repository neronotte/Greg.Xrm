
namespace Greg.Xrm.SolutionManager
{
	partial class SolutionManagerPluginControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolutionManagerPluginControl));
			this.toolStripMenu = new System.Windows.Forms.ToolStrip();
			this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.tsbClose = new Greg.Xrm.Views.ToolStripBindableButton();
			this.tStartMonitoring = new Greg.Xrm.Views.ToolStripBindableButton();
			this.tStopMonitoring = new Greg.Xrm.Views.ToolStripBindableButton();
			this.tStoppingLabel = new Greg.Xrm.Views.ToolStripBindableLabel();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripMenu
			// 
			this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tssSeparator1,
            this.tStartMonitoring,
            this.tStopMonitoring,
            this.tStoppingLabel,
            this.toolStripSeparator1});
			this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
			this.toolStripMenu.Name = "toolStripMenu";
			this.toolStripMenu.Size = new System.Drawing.Size(956, 25);
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
			this.dockPanel.Size = new System.Drawing.Size(956, 477);
			this.dockPanel.TabIndex = 5;
			// 
			// tsbClose
			// 
			this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
			this.tsbClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tsbClose.Name = "tsbClose";
			this.tsbClose.Size = new System.Drawing.Size(23, 22);
			this.tsbClose.Text = "Close this tool";
			this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
			// 
			// tStartMonitoring
			// 
			this.tStartMonitoring.Image = ((System.Drawing.Image)(resources.GetObject("tStartMonitoring.Image")));
			this.tStartMonitoring.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tStartMonitoring.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tStartMonitoring.Name = "tStartMonitoring";
			this.tStartMonitoring.Size = new System.Drawing.Size(114, 22);
			this.tStartMonitoring.Text = "Start monitoring";
			this.tStartMonitoring.Click += new System.EventHandler(this.OnStartMonitoringClick);
			// 
			// tStopMonitoring
			// 
			this.tStopMonitoring.Image = ((System.Drawing.Image)(resources.GetObject("tStopMonitoring.Image")));
			this.tStopMonitoring.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tStopMonitoring.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tStopMonitoring.Name = "tStopMonitoring";
			this.tStopMonitoring.Size = new System.Drawing.Size(114, 22);
			this.tStopMonitoring.Text = "Stop monitoring";
			this.tStopMonitoring.Click += new System.EventHandler(this.OnStopMonitoringClick);
			// 
			// tStoppingLabel
			// 
			this.tStoppingLabel.Name = "tStoppingLabel";
			this.tStoppingLabel.Size = new System.Drawing.Size(128, 22);
			this.tStoppingLabel.Text = "Stopping... please wait!";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// SolutionManagerPluginControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dockPanel);
			this.Controls.Add(this.toolStripMenu);
			this.Name = "SolutionManagerPluginControl";
			this.Size = new System.Drawing.Size(956, 502);
			this.Load += new System.EventHandler(this.MyPluginControl_Load);
			this.toolStripMenu.ResumeLayout(false);
			this.toolStripMenu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolStrip toolStripMenu;
		private Greg.Xrm.Views.ToolStripBindableButton tsbClose;
		private System.Windows.Forms.ToolStripSeparator tssSeparator1;
		private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
		private Greg.Xrm.Views.ToolStripBindableButton tStartMonitoring;
		private Greg.Xrm.Views.ToolStripBindableButton tStopMonitoring;
		private Greg.Xrm.Views.ToolStripBindableLabel tStoppingLabel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}
