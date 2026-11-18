
using Greg.Xrm.Views;
using System;

namespace Greg.Xrm.ModernThemeBuilder.Views
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
			this.toolStripMenu = new System.Windows.Forms.ToolStrip();
			this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.tCurrentTheme = new Greg.Xrm.Views.ToolStripBindableLabel();
			this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.tClose = new System.Windows.Forms.ToolStripButton();
			this.tSettings = new Greg.Xrm.Views.ToolStripBindableButton();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.tLoadSolutions = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.tCreateNewSolution = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.tCreateNewTheme = new Greg.Xrm.Views.ToolStripBindableButton();
			this.tSaveTheme = new Greg.Xrm.Views.ToolStripBindableButton();
			this.tSetAsCurrentTheme = new Greg.Xrm.Views.ToolStripBindableButton();
			this.tResetDefaults = new System.Windows.Forms.ToolStripButton();
			this.tResetDefaultTheme = new ToolStripBindableButton();
			this.toolStripMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripMenu
			// 
			this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tClose,
			this.tSettings,
            this.tssSeparator1,
            this.toolStripDropDownButton1,
            this.toolStripSeparator2,
            this.tCreateNewTheme,
            this.tSaveTheme,
            this.tSetAsCurrentTheme,
            this.tResetDefaultTheme,
            this.toolStripSeparator1,
            this.tResetDefaults,
            this.toolStripLabel1,
            this.tCurrentTheme});
			this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
			this.toolStripMenu.Name = "toolStripMenu";
			this.toolStripMenu.Size = new System.Drawing.Size(1020, 25);
			this.toolStripMenu.TabIndex = 4;
			this.toolStripMenu.Text = "toolStrip1";
			// 
			// tssSeparator1
			// 
			this.tssSeparator1.Name = "tssSeparator1";
			this.tssSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(87, 22);
			this.toolStripLabel1.Text = "Current theme:";
			// 
			// tCurrentTheme
			// 
			this.tCurrentTheme.Name = "tCurrentTheme";
			this.tCurrentTheme.Size = new System.Drawing.Size(12, 22);
			this.tCurrentTheme.Text = "-";
			// 
			// dockPanel1
			// 
			this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dockPanel1.DockBackColor = System.Drawing.Color.White;
			this.dockPanel1.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingSdi;
			this.dockPanel1.Location = new System.Drawing.Point(0, 25);
			this.dockPanel1.Name = "dockPanel1";
			this.dockPanel1.Size = new System.Drawing.Size(1020, 559);
			this.dockPanel1.TabIndex = 5;
			// 
			// tClose
			// 
			this.tClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tClose.Image = ((System.Drawing.Image)(resources.GetObject("tClose.Image")));
			this.tClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tClose.Name = "tClose";
			this.tClose.Size = new System.Drawing.Size(23, 22);
			this.tClose.Text = "Close this tool";
			this.tClose.Click += new System.EventHandler(this.OnCloseClick);
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tLoadSolutions,
            this.tCreateNewSolution});
			this.toolStripDropDownButton1.Image = global::Greg.Xrm.ModernThemeBuilder.Properties.Resources.dataverse;
			this.toolStripDropDownButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new System.Drawing.Size(87, 22);
			this.toolStripDropDownButton1.Text = "Dataverse";
			// 
			// tLoadSolutions
			// 
			this.tLoadSolutions.Image = global::Greg.Xrm.ModernThemeBuilder.Properties.Resources.folder_explore;
			this.tLoadSolutions.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tLoadSolutions.Name = "tLoadSolutions";
			this.tLoadSolutions.Size = new System.Drawing.Size(225, 22);
			this.tLoadSolutions.Text = "Open working solution";
			this.tLoadSolutions.ToolTipText = "Select the solution that is or will be used as container for themes";
			// 
			// tCreateNewSolution
			// 
			this.tCreateNewSolution.Image = global::Greg.Xrm.ModernThemeBuilder.Properties.Resources.folder_add;
			this.tCreateNewSolution.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tCreateNewSolution.Name = "tCreateNewSolution";
			this.tCreateNewSolution.Size = new System.Drawing.Size(225, 22);
			this.tCreateNewSolution.Text = "Create new working solution";
			// 
			// tCreateNewTheme
			// 
			this.tCreateNewTheme.Enabled = false;
			this.tCreateNewTheme.Image = ((System.Drawing.Image)(resources.GetObject("tCreateNewTheme.Image")));
			this.tCreateNewTheme.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tCreateNewTheme.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tCreateNewTheme.Name = "tCreateNewTheme";
			this.tCreateNewTheme.Size = new System.Drawing.Size(123, 22);
			this.tCreateNewTheme.Text = "Create new theme";
			// 
			// tSaveTheme
			// 
			this.tSaveTheme.Enabled = false;
			this.tSaveTheme.Image = ((System.Drawing.Image)(resources.GetObject("tSaveTheme.Image")));
			this.tSaveTheme.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tSaveTheme.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tSaveTheme.Name = "tSaveTheme";
			this.tSaveTheme.Size = new System.Drawing.Size(88, 22);
			this.tSaveTheme.Text = "Save theme";
			// 
			// tSetAsCurrentTheme
			// 
			this.tSetAsCurrentTheme.Enabled = false;
			this.tSetAsCurrentTheme.Image = ((System.Drawing.Image)(resources.GetObject("tSetAsCurrentTheme.Image")));
			this.tSetAsCurrentTheme.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tSetAsCurrentTheme.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tSetAsCurrentTheme.Name = "tSetAsCurrentTheme";
			this.tSetAsCurrentTheme.Size = new System.Drawing.Size(135, 22);
			this.tSetAsCurrentTheme.Text = "Set as current theme";
			// 
			// tSettings
			// 
			this.tSettings.Image = global::Greg.Xrm.ModernThemeBuilder.Properties.Resources.cog;
			this.tSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tSettings.Name = "tSettings";
			this.tSettings.Size = new System.Drawing.Size(23, 22);
			this.tSettings.Text = "Settings";
			this.tSettings.ToolTipText = "Open the settings dialog";
			this.tSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			// 
			// tResetDefaults
			// 
			this.tResetDefaults.Enabled = false;
			this.tResetDefaults.Image = ((System.Drawing.Image)(resources.GetObject("tResetDefaults.Image")));
			this.tResetDefaults.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tResetDefaults.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tResetDefaults.Name = "tResetDefaults";
			this.tResetDefaults.Size = new System.Drawing.Size(130, 22);
			this.tResetDefaults.Text = "Reset default colors";
			this.tResetDefaults.Visible = false;
			this.tResetDefaults.Click += new System.EventHandler(this.OnResetDefaultsClick);
			// 
			// tResetDefaultTheme
			// 
			this.tResetDefaultTheme.Enabled = false;
			this.tResetDefaultTheme.Image = global::Greg.Xrm.ModernThemeBuilder.Properties.Resources.powerplatform;
			this.tResetDefaultTheme.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tResetDefaultTheme.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tResetDefaultTheme.Name = "tResetDefaultTheme";
			this.tResetDefaultTheme.Size = new System.Drawing.Size(132, 22);
			this.tResetDefaultTheme.Text = "Reset default theme";
			// 
			// MainView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dockPanel1);
			this.Controls.Add(this.toolStripMenu);
			this.Name = "MainView";
			this.Size = new System.Drawing.Size(1020, 584);
			this.Load += new System.EventHandler(this.AfterLoad);
			this.toolStripMenu.ResumeLayout(false);
			this.toolStripMenu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolStrip toolStripMenu;
		private System.Windows.Forms.ToolStripButton tClose;
		private System.Windows.Forms.ToolStripSeparator tssSeparator1;
		private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.ToolStripButton tResetDefaults;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private ToolStripBindableButton tCreateNewTheme;
		private ToolStripBindableButton tSaveTheme;
		private ToolStripBindableButton tSetAsCurrentTheme;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
		private ToolStripBindableMenuItem tLoadSolutions;
		private ToolStripBindableMenuItem tCreateNewSolution;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private ToolStripBindableLabel tCurrentTheme;
		private Greg.Xrm.Views.ToolStripBindableButton tSettings;
		private ToolStripBindableButton tResetDefaultTheme;
	}
}
