
namespace Greg.Xrm.EnvironmentComparer.Views.Configurator
{
	partial class ConfiguratorDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfiguratorDialog));
			this.label1 = new System.Windows.Forms.Label();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.txtEntitySearch = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lstEntities = new System.Windows.Forms.ListBox();
			this.btnAccept = new System.Windows.Forms.Button();
			this.chlKey = new System.Windows.Forms.CheckedListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.rUseGuid = new System.Windows.Forms.RadioButton();
			this.rUseAttributes = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.chlSkip = new System.Windows.Forms.CheckedListBox();
			this.chkOnlyActive = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(98, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Pick an entity...";
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// txtEntitySearch
			// 
			this.txtEntitySearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtEntitySearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
			this.txtEntitySearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.txtEntitySearch.Location = new System.Drawing.Point(12, 34);
			this.txtEntitySearch.Name = "txtEntitySearch";
			this.txtEntitySearch.Size = new System.Drawing.Size(460, 20);
			this.txtEntitySearch.TabIndex = 2;
			this.txtEntitySearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnEntitySearchKeyUp);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(397, 473);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.OnCancelClick);
			// 
			// lstEntities
			// 
			this.lstEntities.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstEntities.FormattingEnabled = true;
			this.lstEntities.Location = new System.Drawing.Point(12, 57);
			this.lstEntities.Name = "lstEntities";
			this.lstEntities.Size = new System.Drawing.Size(460, 56);
			this.lstEntities.TabIndex = 4;
			this.lstEntities.SelectedIndexChanged += new System.EventHandler(this.OnEntitySelectionChanged);
			// 
			// btnAccept
			// 
			this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAccept.Location = new System.Drawing.Point(316, 473);
			this.btnAccept.Name = "btnAccept";
			this.btnAccept.Size = new System.Drawing.Size(75, 23);
			this.btnAccept.TabIndex = 5;
			this.btnAccept.Text = "OK";
			this.btnAccept.UseVisualStyleBackColor = true;
			this.btnAccept.Click += new System.EventHandler(this.OnOKClick);
			// 
			// chlKey
			// 
			this.chlKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.chlKey.Enabled = false;
			this.chlKey.FormattingEnabled = true;
			this.chlKey.Location = new System.Drawing.Point(30, 200);
			this.chlKey.Name = "chlKey";
			this.chlKey.Size = new System.Drawing.Size(441, 94);
			this.chlKey.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(16, 131);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(194, 17);
			this.label2.TabIndex = 7;
			this.label2.Text = "...and the key for comparison...";
			// 
			// rUseGuid
			// 
			this.rUseGuid.AutoSize = true;
			this.rUseGuid.Checked = true;
			this.rUseGuid.Location = new System.Drawing.Point(16, 154);
			this.rUseGuid.Name = "rUseGuid";
			this.rUseGuid.Size = new System.Drawing.Size(74, 17);
			this.rUseGuid.TabIndex = 8;
			this.rUseGuid.TabStop = true;
			this.rUseGuid.Text = "Use GUID";
			this.rUseGuid.UseVisualStyleBackColor = true;
			// 
			// rUseAttributes
			// 
			this.rUseAttributes.AutoSize = true;
			this.rUseAttributes.Location = new System.Drawing.Point(16, 177);
			this.rUseAttributes.Name = "rUseAttributes";
			this.rUseAttributes.Size = new System.Drawing.Size(99, 17);
			this.rUseAttributes.TabIndex = 9;
			this.rUseAttributes.Text = "Use attributes...";
			this.rUseAttributes.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(16, 321);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(163, 17);
			this.label3.TabIndex = 10;
			this.label3.Text = "...and attributes to ignore";
			// 
			// chlSkip
			// 
			this.chlSkip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.chlSkip.FormattingEnabled = true;
			this.chlSkip.Location = new System.Drawing.Point(12, 345);
			this.chlSkip.Name = "chlSkip";
			this.chlSkip.Size = new System.Drawing.Size(460, 94);
			this.chlSkip.TabIndex = 11;
			// 
			// chkOnlyActive
			// 
			this.chkOnlyActive.AutoSize = true;
			this.chkOnlyActive.Checked = true;
			this.chkOnlyActive.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkOnlyActive.Location = new System.Drawing.Point(16, 445);
			this.chkOnlyActive.Name = "chkOnlyActive";
			this.chkOnlyActive.Size = new System.Drawing.Size(159, 17);
			this.chkOnlyActive.TabIndex = 12;
			this.chkOnlyActive.Text = "Consider only active records";
			this.chkOnlyActive.UseVisualStyleBackColor = true;
			// 
			// ConfiguratorDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 508);
			this.ControlBox = false;
			this.Controls.Add(this.chkOnlyActive);
			this.Controls.Add(this.chlSkip);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.rUseAttributes);
			this.Controls.Add(this.rUseGuid);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.chlKey);
			this.Controls.Add(this.btnAccept);
			this.Controls.Add(this.lstEntities);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.txtEntitySearch);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ConfiguratorDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Add entity";
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.TextBox txtEntitySearch;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ListBox lstEntities;
		private System.Windows.Forms.Button btnAccept;
		private System.Windows.Forms.RadioButton rUseAttributes;
		private System.Windows.Forms.RadioButton rUseGuid;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckedListBox chlKey;
		private System.Windows.Forms.CheckedListBox chlSkip;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox chkOnlyActive;
	}
}