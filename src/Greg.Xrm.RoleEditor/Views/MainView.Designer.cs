namespace Greg.Xrm.RoleEditor.Views
{
	partial class MainView
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
			this.tools = new System.Windows.Forms.ToolStrip();
			this.tExit = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tInit = new Greg.Xrm.Views.ToolStripBindableButton();
			this.tSettings = new Greg.Xrm.Views.ToolStripBindableButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.status = new System.Windows.Forms.StatusStrip();
			this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.tools.SuspendLayout();
			this.SuspendLayout();
			// 
			// tools
			// 
			this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tExit,
			this.tSettings,
			this.toolStripSeparator1,
			this.tInit,
			this.toolStripSeparator2,
            this.toolStripLabel1});
			this.tools.Location = new System.Drawing.Point(0, 0);
			this.tools.Name = "tools";
			this.tools.Size = new System.Drawing.Size(935, 25);
			this.tools.TabIndex = 0;
			this.tools.Text = "toolStrip1";
			// 
			// tExit
			// 
			this.tExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tExit.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.door_out;
			this.tExit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tExit.Name = "tExit";
			this.tExit.Size = new System.Drawing.Size(23, 22);
			this.tExit.Text = "Exit";
			this.tExit.ToolTipText = "Close this tool";
			this.tExit.Click += new System.EventHandler(this.OnExitClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tInit
			// 
			this.tInit.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.folder;
			this.tInit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tInit.Name = "tInit";
			this.tInit.Size = new System.Drawing.Size(163, 22);
			this.tInit.Text = "Load tables, privileges and roles";
			// 
			// tSettings
			// 
			this.tSettings.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.settings;
			this.tSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tSettings.Name = "tSettings";
			this.tSettings.Size = new System.Drawing.Size(23, 22);
			this.tSettings.Text = "Settings";
			this.tSettings.ToolTipText = "Open the settings dialog";
			this.tSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.toolStripLabel1.ForeColor = System.Drawing.Color.Gray;
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(161, 22);
			this.toolStripLabel1.Text = "Press F1 in any panel for help";
			// 
			// status
			// 
			this.status.Location = new System.Drawing.Point(0, 556);
			this.status.Name = "status";
			this.status.Size = new System.Drawing.Size(935, 22);
			this.status.TabIndex = 1;
			this.status.Text = "statusStrip1";
			// 
			// dockPanel
			// 
			this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dockPanel.Location = new System.Drawing.Point(0, 25);
			this.dockPanel.Name = "dockPanel";
			this.dockPanel.Size = new System.Drawing.Size(935, 531);
			this.dockPanel.TabIndex = 2;
			// 
			// MainView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dockPanel);
			this.Controls.Add(this.status);
			this.Controls.Add(this.tools);
			this.Name = "MainView";
			this.Size = new System.Drawing.Size(935, 578);
			this.tools.ResumeLayout(false);
			this.tools.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip tools;
		private System.Windows.Forms.StatusStrip status;
		private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
		private System.Windows.Forms.ToolStripButton tExit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private Greg.Xrm.Views.ToolStripBindableButton tInit;
		private Greg.Xrm.Views.ToolStripBindableButton tSettings;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	}
}
