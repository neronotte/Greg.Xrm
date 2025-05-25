namespace Greg.Xrm.ModernThemeBuilder.Views.Config
{
	partial class ConfigDialog
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigDialog));
			this.tabs = new System.Windows.Forms.TabControl();
			this.tabAI = new System.Windows.Forms.TabPage();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtClaudeApiKey = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtChatGptApiKey = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.gAzure = new System.Windows.Forms.GroupBox();
			this.txtAzureEndpoint = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtAzureDeployment = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtAzureApiKey = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.tabs.SuspendLayout();
			this.tabAI.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.gAzure.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabs
			// 
			this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabs.Controls.Add(this.tabAI);
			this.tabs.Location = new System.Drawing.Point(12, 12);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(622, 442);
			this.tabs.TabIndex = 0;
			// 
			// tabAI
			// 
			this.tabAI.Controls.Add(this.label1);
			this.tabAI.Controls.Add(this.groupBox2);
			this.tabAI.Controls.Add(this.groupBox1);
			this.tabAI.Controls.Add(this.gAzure);
			this.tabAI.Location = new System.Drawing.Point(4, 22);
			this.tabAI.Name = "tabAI";
			this.tabAI.Padding = new System.Windows.Forms.Padding(3);
			this.tabAI.Size = new System.Drawing.Size(614, 416);
			this.tabAI.TabIndex = 0;
			this.tabAI.Text = "Artificial Intelligence";
			this.tabAI.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(16, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(580, 28);
			this.label1.TabIndex = 16;
			this.label1.Text = "You can configure here the credentials needed to leverage your favorite AI tool t" +
    "o generate the theme definition.\r\n";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.txtClaudeApiKey);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Location = new System.Drawing.Point(6, 322);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(600, 83);
			this.groupBox2.TabIndex = 15;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Claude";
			// 
			// txtClaudeApiKey
			// 
			this.txtClaudeApiKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtClaudeApiKey.Location = new System.Drawing.Point(10, 50);
			this.txtClaudeApiKey.Name = "txtClaudeApiKey";
			this.txtClaudeApiKey.PasswordChar = '*';
			this.txtClaudeApiKey.Size = new System.Drawing.Size(580, 20);
			this.txtClaudeApiKey.TabIndex = 13;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 30);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 13);
			this.label3.TabIndex = 12;
			this.label3.Text = "API Key:";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.txtChatGptApiKey);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(6, 233);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(600, 83);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "ChatGPT";
			// 
			// txtChatGptApiKey
			// 
			this.txtChatGptApiKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtChatGptApiKey.Location = new System.Drawing.Point(10, 50);
			this.txtChatGptApiKey.Name = "txtChatGptApiKey";
			this.txtChatGptApiKey.PasswordChar = '*';
			this.txtChatGptApiKey.Size = new System.Drawing.Size(580, 20);
			this.txtChatGptApiKey.TabIndex = 13;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 13);
			this.label2.TabIndex = 12;
			this.label2.Text = "API Key:";
			// 
			// gAzure
			// 
			this.gAzure.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gAzure.Controls.Add(this.txtAzureEndpoint);
			this.gAzure.Controls.Add(this.label5);
			this.gAzure.Controls.Add(this.txtAzureDeployment);
			this.gAzure.Controls.Add(this.label6);
			this.gAzure.Controls.Add(this.txtAzureApiKey);
			this.gAzure.Controls.Add(this.label7);
			this.gAzure.Location = new System.Drawing.Point(6, 42);
			this.gAzure.Name = "gAzure";
			this.gAzure.Size = new System.Drawing.Size(600, 185);
			this.gAzure.TabIndex = 8;
			this.gAzure.TabStop = false;
			this.gAzure.Text = "Azure OpenAI";
			// 
			// txtAzureEndpoint
			// 
			this.txtAzureEndpoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAzureEndpoint.Location = new System.Drawing.Point(10, 50);
			this.txtAzureEndpoint.Name = "txtAzureEndpoint";
			this.txtAzureEndpoint.Size = new System.Drawing.Size(580, 20);
			this.txtAzureEndpoint.TabIndex = 13;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(10, 30);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(52, 13);
			this.label5.TabIndex = 12;
			this.label5.Text = "Endpoint:";
			// 
			// txtAzureDeployment
			// 
			this.txtAzureDeployment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAzureDeployment.Location = new System.Drawing.Point(10, 100);
			this.txtAzureDeployment.Name = "txtAzureDeployment";
			this.txtAzureDeployment.Size = new System.Drawing.Size(580, 20);
			this.txtAzureDeployment.TabIndex = 11;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(10, 80);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(66, 13);
			this.label6.TabIndex = 10;
			this.label6.Text = "Deployment:";
			// 
			// txtAzureApiKey
			// 
			this.txtAzureApiKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAzureApiKey.Location = new System.Drawing.Point(10, 150);
			this.txtAzureApiKey.Name = "txtAzureApiKey";
			this.txtAzureApiKey.PasswordChar = '*';
			this.txtAzureApiKey.Size = new System.Drawing.Size(580, 20);
			this.txtAzureApiKey.TabIndex = 9;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(10, 130);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(48, 13);
			this.label7.TabIndex = 8;
			this.label7.Text = "API Key:";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Image = global::Greg.Xrm.ModernThemeBuilder.Properties.Resources.cross;
			this.btnCancel.Location = new System.Drawing.Point(559, 460);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 11;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Image = global::Greg.Xrm.ModernThemeBuilder.Properties.Resources.tick;
			this.btnOk.Location = new System.Drawing.Point(478, 460);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 10;
			this.btnOk.Text = "OK";
			this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.OnOkClick);
			// 
			// SettingsDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(646, 495);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.tabs);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsDialog";
			this.Text = "_n.ModernThemeEditor Settings";
			this.tabs.ResumeLayout(false);
			this.tabAI.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.gAzure.ResumeLayout(false);
			this.gAzure.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabs;
		private System.Windows.Forms.TabPage tabAI;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.GroupBox gAzure;
		private System.Windows.Forms.TextBox txtAzureEndpoint;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtAzureDeployment;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtAzureApiKey;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox txtClaudeApiKey;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtChatGptApiKey;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}