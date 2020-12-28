
namespace Greg.Xrm.SolutionManager.Views.SolutionProgress
{
	partial class SolutionProgressView
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
			this.gCurrentImport = new System.Windows.Forms.GroupBox();
			this.lCurrentImport = new System.Windows.Forms.Label();
			this.pbCurrentImport = new System.Windows.Forms.ProgressBar();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.txtOutput = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pFind = new System.Windows.Forms.Panel();
			this.txtFind = new System.Windows.Forms.TextBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.sPosition = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblOccurrences = new System.Windows.Forms.Label();
			this.gCurrentImport.SuspendLayout();
			this.panel1.SuspendLayout();
			this.pFind.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// gCurrentImport
			// 
			this.gCurrentImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gCurrentImport.Controls.Add(this.lCurrentImport);
			this.gCurrentImport.Controls.Add(this.pbCurrentImport);
			this.gCurrentImport.Location = new System.Drawing.Point(12, 12);
			this.gCurrentImport.Name = "gCurrentImport";
			this.gCurrentImport.Size = new System.Drawing.Size(886, 51);
			this.gCurrentImport.TabIndex = 0;
			this.gCurrentImport.TabStop = false;
			this.gCurrentImport.Text = "Last import";
			// 
			// lCurrentImport
			// 
			this.lCurrentImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lCurrentImport.AutoSize = true;
			this.lCurrentImport.Location = new System.Drawing.Point(830, 25);
			this.lCurrentImport.Name = "lCurrentImport";
			this.lCurrentImport.Size = new System.Drawing.Size(36, 13);
			this.lCurrentImport.TabIndex = 1;
			this.lCurrentImport.Text = "0,00%";
			// 
			// pbCurrentImport
			// 
			this.pbCurrentImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pbCurrentImport.Location = new System.Drawing.Point(8, 20);
			this.pbCurrentImport.Name = "pbCurrentImport";
			this.pbCurrentImport.Size = new System.Drawing.Size(817, 23);
			this.pbCurrentImport.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.pbCurrentImport.TabIndex = 0;
			// 
			// timer
			// 
			this.timer.Interval = 5000;
			this.timer.Tick += new System.EventHandler(this.OnTimerExpired);
			// 
			// txtOutput
			// 
			this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(46)))), ((int)(((byte)(40)))));
			this.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtOutput.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(203)))), ((int)(((byte)(203)))));
			this.txtOutput.HideSelection = false;
			this.txtOutput.Location = new System.Drawing.Point(10, 10);
			this.txtOutput.Margin = new System.Windows.Forms.Padding(10);
			this.txtOutput.Multiline = true;
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.ReadOnly = true;
			this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtOutput.Size = new System.Drawing.Size(861, 448);
			this.txtOutput.TabIndex = 1;
			this.txtOutput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextAreaKeyUp);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(46)))), ((int)(((byte)(40)))));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.txtOutput);
			this.panel1.Location = new System.Drawing.Point(13, 70);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(885, 472);
			this.panel1.TabIndex = 2;
			// 
			// pFind
			// 
			this.pFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pFind.Controls.Add(this.lblOccurrences);
			this.pFind.Controls.Add(this.txtFind);
			this.pFind.Location = new System.Drawing.Point(85, 69);
			this.pFind.Name = "pFind";
			this.pFind.Size = new System.Drawing.Size(769, 26);
			this.pFind.TabIndex = 3;
			this.pFind.Visible = false;
			// 
			// txtFind
			// 
			this.txtFind.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFind.Location = new System.Drawing.Point(3, 3);
			this.txtFind.Name = "txtFind";
			this.txtFind.Size = new System.Drawing.Size(697, 20);
			this.txtFind.TabIndex = 0;
			this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnFindKeyDown);
			this.txtFind.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnFindKeyUp);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sPosition});
			this.statusStrip1.Location = new System.Drawing.Point(0, 553);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(910, 22);
			this.statusStrip1.TabIndex = 4;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// sPosition
			// 
			this.sPosition.Name = "sPosition";
			this.sPosition.Size = new System.Drawing.Size(16, 17);
			this.sPosition.Text = "...";
			// 
			// lblOccurrences
			// 
			this.lblOccurrences.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblOccurrences.ForeColor = System.Drawing.Color.Red;
			this.lblOccurrences.Location = new System.Drawing.Point(706, 3);
			this.lblOccurrences.Name = "lblOccurrences";
			this.lblOccurrences.Size = new System.Drawing.Size(60, 20);
			this.lblOccurrences.TabIndex = 1;
			this.lblOccurrences.Text = "...";
			this.lblOccurrences.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SolutionProgressView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(910, 575);
			this.CloseButton = false;
			this.CloseButtonVisible = false;
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.pFind);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.gCurrentImport);
			this.Name = "SolutionProgressView";
			this.TabText = "Solution progress";
			this.Text = "Solution progress";
			this.gCurrentImport.ResumeLayout(false);
			this.gCurrentImport.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.pFind.ResumeLayout(false);
			this.pFind.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox gCurrentImport;
		private System.Windows.Forms.Label lCurrentImport;
		private System.Windows.Forms.ProgressBar pbCurrentImport;
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.TextBox txtOutput;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel pFind;
		private System.Windows.Forms.TextBox txtFind;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel sPosition;
		private System.Windows.Forms.Label lblOccurrences;
	}
}
