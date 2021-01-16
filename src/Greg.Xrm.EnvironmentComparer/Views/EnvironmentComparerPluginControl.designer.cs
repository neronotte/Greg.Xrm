
namespace Greg.Xrm.EnvironmentComparer.Views
{
	partial class EnvironmentComparerPluginControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnvironmentComparerPluginControl));
			this.tools = new System.Windows.Forms.ToolStrip();
			this.tClose = new Greg.Xrm.Views.ToolStripBindableButton();
			this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tEnv1Name = new System.Windows.Forms.ToolStripLabel();
			this.tConnectToEnv2 = new Greg.Xrm.Views.ToolStripBindableButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tEquals = new Greg.Xrm.Views.ToolStripBindableLabel();
			this.tHelp = new System.Windows.Forms.ToolStripLabel();
			this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.tools.SuspendLayout();
			this.SuspendLayout();
			// 
			// tools
			// 
			this.tools.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tClose,
            this.tssSeparator1,
            this.tEnv1Name,
            this.tConnectToEnv2,
            this.toolStripSeparator1,
            this.tEquals,
            this.tHelp});
			this.tools.Location = new System.Drawing.Point(0, 0);
			this.tools.Name = "tools";
			this.tools.Size = new System.Drawing.Size(1175, 25);
			this.tools.TabIndex = 4;
			this.tools.Text = "toolStrip1";
			// 
			// tClose
			// 
			this.tClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tClose.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.door_out;
			this.tClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tClose.Name = "tClose";
			this.tClose.Size = new System.Drawing.Size(23, 22);
			this.tClose.Text = "Close this tool";
			this.tClose.Click += new System.EventHandler(this.OnCloseToolClicked);
			// 
			// tssSeparator1
			// 
			this.tssSeparator1.Name = "tssSeparator1";
			this.tssSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tEnv1Name
			// 
			this.tEnv1Name.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tEnv1Name.Name = "tEnv1Name";
			this.tEnv1Name.Size = new System.Drawing.Size(155, 22);
			this.tEnv1Name.Text = "1. Connect to environment 1";
			// 
			// tConnectToEnv2
			// 
			this.tConnectToEnv2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tConnectToEnv2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tConnectToEnv2.Image = ((System.Drawing.Image)(resources.GetObject("tConnectToEnv2.Image")));
			this.tConnectToEnv2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tConnectToEnv2.Name = "tConnectToEnv2";
			this.tConnectToEnv2.Size = new System.Drawing.Size(163, 22);
			this.tConnectToEnv2.Text = "2. Connect to environment 2";
			this.tConnectToEnv2.Click += new System.EventHandler(this.OnConnectToEnvironment2);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tEquals
			// 
			this.tEquals.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tEquals.ForeColor = System.Drawing.Color.Red;
			this.tEquals.Name = "tEquals";
			this.tEquals.Size = new System.Drawing.Size(259, 22);
			this.tEquals.Text = "The two connections are equal! Are you sure?";
			// 
			// tHelp
			// 
			this.tHelp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tHelp.ForeColor = System.Drawing.Color.DarkGray;
			this.tHelp.Name = "tHelp";
			this.tHelp.Size = new System.Drawing.Size(161, 22);
			this.tHelp.Text = "Press F1 in any panel for help";
			// 
			// dockPanel
			// 
			this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingSdi;
			this.dockPanel.Location = new System.Drawing.Point(0, 25);
			this.dockPanel.Name = "dockPanel";
			this.dockPanel.Size = new System.Drawing.Size(1175, 602);
			this.dockPanel.TabIndex = 5;
			// 
			// EnvironmentComparerPluginControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dockPanel);
			this.Controls.Add(this.tools);
			this.Name = "EnvironmentComparerPluginControl";
			this.Size = new System.Drawing.Size(1175, 627);
			this.Load += new System.EventHandler(this.MyPluginControl_Load);
			this.tools.ResumeLayout(false);
			this.tools.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolStrip tools;
		private Greg.Xrm.Views.ToolStripBindableButton tClose;
		private System.Windows.Forms.ToolStripSeparator tssSeparator1;
		private System.Windows.Forms.ToolStripLabel tEnv1Name;
		private Greg.Xrm.Views.ToolStripBindableButton tConnectToEnv2;
		private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripLabel tHelp;
		private Greg.Xrm.Views.ToolStripBindableLabel tEquals;
	}
}
