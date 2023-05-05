
namespace Greg.Xrm.Logging
{
	partial class OutputView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutputView));
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmiCopyMessage = new System.Windows.Forms.ToolStripMenuItem();
			this.cmiCopyException = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tFilterByLevel = new System.Windows.Forms.ToolStripDropDownButton();
			this.debygToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.warnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.errorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.tFilterAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tExportLogs = new System.Windows.Forms.ToolStripButton();
			this.listView1 = new System.Windows.Forms.ListView();
			this.cTimestamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cException = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.contextMenu.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenu
			// 
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiCopyMessage,
            this.cmiCopyException});
			this.contextMenu.Name = "contextMenu";
			this.contextMenu.Size = new System.Drawing.Size(225, 48);
			// 
			// cmiCopyMessage
			// 
			this.cmiCopyMessage.Enabled = false;
			this.cmiCopyMessage.Image = global::Greg.Xrm.Core.Properties.Resources.page_copy;
			this.cmiCopyMessage.Name = "cmiCopyMessage";
			this.cmiCopyMessage.Size = new System.Drawing.Size(224, 22);
			this.cmiCopyMessage.Text = "Copy message to clipboard";
			this.cmiCopyMessage.Click += new System.EventHandler(this.OnCopyMessageClick);
			// 
			// cmiCopyException
			// 
			this.cmiCopyException.Enabled = false;
			this.cmiCopyException.Image = global::Greg.Xrm.Core.Properties.Resources.page_copy;
			this.cmiCopyException.Name = "cmiCopyException";
			this.cmiCopyException.Size = new System.Drawing.Size(224, 22);
			this.cmiCopyException.Text = "Copy exception to clipboard";
			this.cmiCopyException.Click += new System.EventHandler(this.OnCopyException);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tFilterByLevel,
            this.toolStripSeparator1,
            this.tExportLogs});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(1007, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tFilterByLevel
			// 
			this.tFilterByLevel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tFilterByLevel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debygToolStripMenuItem,
            this.infoToolStripMenuItem,
            this.warnToolStripMenuItem,
            this.errorToolStripMenuItem,
            this.toolStripMenuItem1,
            this.tFilterAll});
			this.tFilterByLevel.Image = ((System.Drawing.Image)(resources.GetObject("tFilterByLevel.Image")));
			this.tFilterByLevel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tFilterByLevel.Name = "tFilterByLevel";
			this.tFilterByLevel.Size = new System.Drawing.Size(89, 22);
			this.tFilterByLevel.Text = "Filter by level";
			// 
			// debygToolStripMenuItem
			// 
			this.debygToolStripMenuItem.Image = global::Greg.Xrm.Core.Properties.Resources.bug;
			this.debygToolStripMenuItem.Name = "debygToolStripMenuItem";
			this.debygToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
			this.debygToolStripMenuItem.Tag = "DEBUG";
			this.debygToolStripMenuItem.Text = "Debug";
			this.debygToolStripMenuItem.Click += new System.EventHandler(this.OnFilterByLevel);
			// 
			// infoToolStripMenuItem
			// 
			this.infoToolStripMenuItem.Image = global::Greg.Xrm.Core.Properties.Resources.information;
			this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
			this.infoToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
			this.infoToolStripMenuItem.Tag = "INFO";
			this.infoToolStripMenuItem.Text = "Info";
			this.infoToolStripMenuItem.Click += new System.EventHandler(this.OnFilterByLevel);
			// 
			// warnToolStripMenuItem
			// 
			this.warnToolStripMenuItem.Image = global::Greg.Xrm.Core.Properties.Resources.error;
			this.warnToolStripMenuItem.Name = "warnToolStripMenuItem";
			this.warnToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
			this.warnToolStripMenuItem.Tag = "WARN";
			this.warnToolStripMenuItem.Text = "Warn";
			this.warnToolStripMenuItem.Click += new System.EventHandler(this.OnFilterByLevel);
			// 
			// errorToolStripMenuItem
			// 
			this.errorToolStripMenuItem.Image = global::Greg.Xrm.Core.Properties.Resources.exclamation;
			this.errorToolStripMenuItem.Name = "errorToolStripMenuItem";
			this.errorToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
			this.errorToolStripMenuItem.Tag = "ERROR;FATAL";
			this.errorToolStripMenuItem.Text = "Error";
			this.errorToolStripMenuItem.Click += new System.EventHandler(this.OnFilterByLevel);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(106, 6);
			// 
			// tFilterAll
			// 
			this.tFilterAll.Name = "tFilterAll";
			this.tFilterAll.Size = new System.Drawing.Size(109, 22);
			this.tFilterAll.Text = "All";
			this.tFilterAll.Click += new System.EventHandler(this.OnFilterByLevel);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tExportLogs
			// 
			this.tExportLogs.Image = global::Greg.Xrm.Core.Properties.Resources.page_white;
			this.tExportLogs.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tExportLogs.Name = "tExportLogs";
			this.tExportLogs.Size = new System.Drawing.Size(61, 22);
			this.tExportLogs.Text = "Export";
			this.tExportLogs.Click += new System.EventHandler(this.OnExportLogs);
			// 
			// listView1
			// 
			this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(46)))), ((int)(((byte)(40)))));
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cTimestamp,
            this.cLevel,
            this.cMessage,
            this.cException});
			this.listView1.ContextMenuStrip = this.contextMenu;
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listView1.FullRowSelect = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(0, 25);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(1007, 304);
			this.listView1.TabIndex = 2;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.OnSelectedIndexChanged);
			// 
			// cTimestamp
			// 
			this.cTimestamp.Text = "Timestamp";
			this.cTimestamp.Width = 150;
			// 
			// cLevel
			// 
			this.cLevel.Text = "Level";
			// 
			// cMessage
			// 
			this.cMessage.Text = "Message";
			this.cMessage.Width = 500;
			// 
			// cException
			// 
			this.cException.Text = "Exception";
			this.cException.Width = 500;
			// 
			// OutputView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1007, 329);
			this.CloseButton = false;
			this.CloseButtonVisible = false;
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.toolStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "OutputView";
			this.TabText = "Output";
			this.Text = "Output";
			this.contextMenu.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ContextMenuStrip contextMenu;
		private System.Windows.Forms.ToolStripMenuItem cmiCopyMessage;
		private System.Windows.Forms.ToolStripMenuItem cmiCopyException;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripDropDownButton tFilterByLevel;
		private System.Windows.Forms.ToolStripMenuItem debygToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem errorToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tFilterAll;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader cTimestamp;
		private System.Windows.Forms.ColumnHeader cLevel;
		private System.Windows.Forms.ColumnHeader cMessage;
		private System.Windows.Forms.ColumnHeader cException;
		private System.Windows.Forms.ToolStripMenuItem warnToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tExportLogs;
	}
}
