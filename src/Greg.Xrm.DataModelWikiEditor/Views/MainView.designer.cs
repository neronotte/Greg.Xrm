
using System;

namespace Greg.Xrm.DataModelWikiEditor.Views
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
			this.tClose = new System.Windows.Forms.ToolStripButton();
			this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tEnvironmentName = new Greg.Xrm.Views.ToolStripBindableLabel();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tOpenFolder = new Greg.Xrm.Views.ToolStripBindableButton();
			this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.toolStripMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripMenu
			// 
			this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tClose,
            this.tssSeparator1,
            this.tEnvironmentName,
            this.toolStripSeparator1,
            this.tOpenFolder});
			this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
			this.toolStripMenu.Name = "toolStripMenu";
			this.toolStripMenu.Size = new System.Drawing.Size(1020, 25);
			this.toolStripMenu.TabIndex = 4;
			this.toolStripMenu.Text = "toolStrip1";
			// 
			// tClose
			// 
			this.tClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tClose.Image = global::Greg.Xrm.DataModelWikiEditor.Properties.Resources.door_out;
			this.tClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tClose.Name = "tClose";
			this.tClose.Size = new System.Drawing.Size(23, 22);
			this.tClose.Text = "Close this tool";
			this.tClose.Click += new System.EventHandler(this.OnCloseClick);
			// 
			// tssSeparator1
			// 
			this.tssSeparator1.Name = "tssSeparator1";
			this.tssSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tEnvironmentName
			// 
			this.tEnvironmentName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tEnvironmentName.Name = "tEnvironmentName";
			this.tEnvironmentName.Size = new System.Drawing.Size(16, 22);
			this.tEnvironmentName.Text = "...";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tOpenFolder
			// 
			this.tOpenFolder.Image = global::Greg.Xrm.DataModelWikiEditor.Properties.Resources.folder;
			this.tOpenFolder.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tOpenFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tOpenFolder.Name = "tOpenFolder";
			this.tOpenFolder.Size = new System.Drawing.Size(90, 22);
			this.tOpenFolder.Text = "Open folder";
			// 
			// dockPanel1
			// 
			this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dockPanel1.Location = new System.Drawing.Point(0, 25);
			this.dockPanel1.Name = "dockPanel1";
			this.dockPanel1.Size = new System.Drawing.Size(1020, 559);
			this.dockPanel1.TabIndex = 5;
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
		private Greg.Xrm.Views.ToolStripBindableLabel tEnvironmentName;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private Greg.Xrm.Views.ToolStripBindableButton tOpenFolder;
		private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
	}
}
