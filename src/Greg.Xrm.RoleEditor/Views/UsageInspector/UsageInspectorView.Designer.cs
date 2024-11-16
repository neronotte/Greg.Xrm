namespace Greg.Xrm.RoleEditor.Views.UsageInspector
{
	partial class UsageInspectorView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsageInspectorView));
			this.notifications = new Greg.Xrm.Core.Views.NotificationPanel();
			this.pnlBody = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.btnStart = new System.Windows.Forms.Button();
			this.txtResult = new System.Windows.Forms.TextBox();
			this.pnlBody.SuspendLayout();
			this.SuspendLayout();
			// 
			// notifications
			// 
			this.notifications.AutoSize = true;
			this.notifications.Dock = System.Windows.Forms.DockStyle.Top;
			this.notifications.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.notifications.Location = new System.Drawing.Point(0, 0);
			this.notifications.Name = "notifications";
			this.notifications.Size = new System.Drawing.Size(731, 0);
			this.notifications.TabIndex = 0;
			// 
			// pnlBody
			// 
			this.pnlBody.Controls.Add(this.txtResult);
			this.pnlBody.Controls.Add(this.btnStart);
			this.pnlBody.Controls.Add(this.label1);
			this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlBody.Location = new System.Drawing.Point(0, 0);
			this.pnlBody.Margin = new System.Windows.Forms.Padding(10);
			this.pnlBody.Name = "pnlBody";
			this.pnlBody.Size = new System.Drawing.Size(737, 412);
			this.pnlBody.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoEllipsis = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(713, 86);
			this.label1.TabIndex = 0;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// button1
			// 
			this.btnStart.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.application_form_magnify;
			this.btnStart.Location = new System.Drawing.Point(13, 99);
			this.btnStart.Name = "button1";
			this.btnStart.Size = new System.Drawing.Size(120, 45);
			this.btnStart.TabIndex = 1;
			this.btnStart.Text = "Start Inspection";
			this.btnStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnStart.UseVisualStyleBackColor = true;
			// 
			// txtResult
			// 
			this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtResult.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtResult.Location = new System.Drawing.Point(15, 151);
			this.txtResult.Multiline = true;
			this.txtResult.Name = "txtResult";
			this.txtResult.ReadOnly = true;
			this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtResult.Size = new System.Drawing.Size(710, 249);
			this.txtResult.TabIndex = 2;
			// 
			// UsageInspectorView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(737, 412);
			this.Controls.Add(this.pnlBody);
			this.Controls.Add(this.notifications);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "UsageInspectorView";
			this.pnlBody.ResumeLayout(false);
			this.pnlBody.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Core.Views.NotificationPanel notifications;
		private System.Windows.Forms.Panel pnlBody;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtResult;
		private System.Windows.Forms.Button btnStart;
	}
}
