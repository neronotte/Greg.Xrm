
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
			this.listView1 = new System.Windows.Forms.ListView();
			this.cTimestamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cException = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmiCopyMessage = new System.Windows.Forms.ToolStripMenuItem();
			this.cmiCopyException = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenu.SuspendLayout();
			this.SuspendLayout();
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
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(1007, 269);
			this.listView1.TabIndex = 0;
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
			this.cmiCopyMessage.Image = global::Greg.Xrm.Properties.Resources.page_copy;
			this.cmiCopyMessage.Name = "cmiCopyMessage";
			this.cmiCopyMessage.Size = new System.Drawing.Size(224, 22);
			this.cmiCopyMessage.Text = "Copy message to clipboard";
			this.cmiCopyMessage.Click += new System.EventHandler(this.OnCopyMessageClick);
			// 
			// cmiCopyException
			// 
			this.cmiCopyException.Enabled = false;
			this.cmiCopyException.Image = global::Greg.Xrm.Properties.Resources.page_copy;
			this.cmiCopyException.Name = "cmiCopyException";
			this.cmiCopyException.Size = new System.Drawing.Size(224, 22);
			this.cmiCopyException.Text = "Copy exception to clipboard";
			this.cmiCopyException.Click += new System.EventHandler(this.OnCopyException);
			// 
			// OutputView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1007, 269);
			this.CloseButton = false;
			this.CloseButtonVisible = false;
			this.Controls.Add(this.listView1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "OutputView";
			this.TabText = "Output";
			this.Text = "Output";
			this.contextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader cTimestamp;
		private System.Windows.Forms.ColumnHeader cLevel;
		private System.Windows.Forms.ColumnHeader cMessage;
		private System.Windows.Forms.ColumnHeader cException;
		private System.Windows.Forms.ContextMenuStrip contextMenu;
		private System.Windows.Forms.ToolStripMenuItem cmiCopyMessage;
		private System.Windows.Forms.ToolStripMenuItem cmiCopyException;
	}
}
