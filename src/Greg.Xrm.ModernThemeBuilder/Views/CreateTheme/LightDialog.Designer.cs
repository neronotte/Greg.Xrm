namespace Greg.Xrm.ModernThemeBuilder.Views.CreateTheme
{
	partial class LightDialog
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
			this.components = new System.ComponentModel.Container();
			this.label1 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblSuffix = new System.Windows.Forms.Label();
			this.lblPrefix = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(345, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Please specify the name of the webresource that will contain the theme:";
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.Location = new System.Drawing.Point(69, 36);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(327, 20);
			this.txtName.TabIndex = 1;
			this.txtName.Text = "/themes/theme";
			// 
			// lblSuffix
			// 
			this.lblSuffix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblSuffix.AutoSize = true;
			this.lblSuffix.Location = new System.Drawing.Point(399, 39);
			this.lblSuffix.Name = "lblSuffix";
			this.lblSuffix.Size = new System.Drawing.Size(25, 13);
			this.lblSuffix.TabIndex = 2;
			this.lblSuffix.Text = ".xml";
			// 
			// lblPrefix
			// 
			this.lblPrefix.Location = new System.Drawing.Point(12, 39);
			this.lblPrefix.Name = "lblPrefix";
			this.lblPrefix.Size = new System.Drawing.Size(56, 23);
			this.lblPrefix.TabIndex = 3;
			this.lblPrefix.Text = "greg_";
			this.lblPrefix.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Image = global::Greg.Xrm.ModernThemeBuilder.Properties.Resources.cross;
			this.btnCancel.Location = new System.Drawing.Point(357, 112);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.OnCancelClick);
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Image = global::Greg.Xrm.ModernThemeBuilder.Properties.Resources.tick;
			this.btnOk.Location = new System.Drawing.Point(276, 112);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 5;
			this.btnOk.Text = "OK";
			this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.OnOkClick);
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
			this.label2.Location = new System.Drawing.Point(12, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(420, 26);
			this.label2.TabIndex = 7;
			this.label2.Text = "As best practice, we suggest to put the theme into a /themes virtual folder.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// LightDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(444, 147);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.lblPrefix);
			this.Controls.Add(this.lblSuffix);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "LightDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Create new theme in the current solution";
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label lblSuffix;
		private System.Windows.Forms.Label lblPrefix;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.Label label2;
	}
}