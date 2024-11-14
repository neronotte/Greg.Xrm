namespace Greg.Xrm.RoleEditor.Views.Search
{
	partial class SearchByPrivilegeDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchByPrivilegeDialog));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.pnlFooter = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.rSearchType2 = new System.Windows.Forms.RadioButton();
			this.txtPrivilegeName = new System.Windows.Forms.TextBox();
			this.rSearchType1 = new System.Windows.Forms.RadioButton();
			this.txtTables = new System.Windows.Forms.TextBox();
			this.cmbPrivilegeType = new Greg.Xrm.RoleEditor.Views.Search.EditableComboBox();
			this.txtMiscPrivilegeLabel = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.pnlHeader.SuspendLayout();
			this.pnlFooter.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.pnlHeader, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.pnlFooter, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 461);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.Color.White;
			this.pnlHeader.Controls.Add(this.label2);
			this.pnlHeader.Controls.Add(this.label1);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Margin = new System.Windows.Forms.Padding(0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(784, 60);
			this.pnlHeader.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(297, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Please peek the privilege you want to look for in the list below";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(272, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Look for roles having a given privilege";
			// 
			// pnlFooter
			// 
			this.pnlFooter.Controls.Add(this.btnCancel);
			this.pnlFooter.Controls.Add(this.btnOk);
			this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlFooter.Location = new System.Drawing.Point(0, 401);
			this.pnlFooter.Margin = new System.Windows.Forms.Padding(0);
			this.pnlFooter.Name = "pnlFooter";
			this.pnlFooter.Size = new System.Drawing.Size(784, 60);
			this.pnlFooter.TabIndex = 2;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(600, 15);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 30);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Location = new System.Drawing.Point(690, 15);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(80, 30);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.txtMiscPrivilegeLabel);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.txtTables);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.cmbPrivilegeType);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.rSearchType2);
			this.panel1.Controls.Add(this.txtPrivilegeName);
			this.panel1.Controls.Add(this.rSearchType1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 60);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(784, 341);
			this.panel1.TabIndex = 4;
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(596, 120);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(50, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Privilege:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(63, 120);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(37, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Table:";
			// 
			// rSearchType2
			// 
			this.rSearchType2.AutoSize = true;
			this.rSearchType2.Location = new System.Drawing.Point(17, 87);
			this.rSearchType2.Margin = new System.Windows.Forms.Padding(10);
			this.rSearchType2.Name = "rSearchType2";
			this.rSearchType2.Size = new System.Drawing.Size(143, 17);
			this.rSearchType2.TabIndex = 2;
			this.rSearchType2.Text = "Search by privilege label:";
			this.rSearchType2.UseVisualStyleBackColor = true;
			// 
			// txtPrivilegeName
			// 
			this.txtPrivilegeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPrivilegeName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.txtPrivilegeName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.txtPrivilegeName.Location = new System.Drawing.Point(32, 40);
			this.txtPrivilegeName.Name = "txtPrivilegeName";
			this.txtPrivilegeName.Size = new System.Drawing.Size(738, 20);
			this.txtPrivilegeName.TabIndex = 1;
			// 
			// rSearchType1
			// 
			this.rSearchType1.AutoSize = true;
			this.rSearchType1.Checked = true;
			this.rSearchType1.Location = new System.Drawing.Point(16, 10);
			this.rSearchType1.Margin = new System.Windows.Forms.Padding(10);
			this.rSearchType1.Name = "rSearchType1";
			this.rSearchType1.Size = new System.Drawing.Size(147, 17);
			this.rSearchType1.TabIndex = 0;
			this.rSearchType1.TabStop = true;
			this.rSearchType1.Text = "Search by privilege name:";
			this.rSearchType1.UseVisualStyleBackColor = true;
			// 
			// txtTables
			// 
			this.txtTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTables.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.txtTables.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.txtTables.Location = new System.Drawing.Point(106, 117);
			this.txtTables.Name = "txtTables";
			this.txtTables.Size = new System.Drawing.Size(469, 20);
			this.txtTables.TabIndex = 7;
			// 
			// cmbPrivilegeType
			// 
			this.cmbPrivilegeType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmbPrivilegeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPrivilegeType.FormattingEnabled = true;
			this.cmbPrivilegeType.Location = new System.Drawing.Point(649, 117);
			this.cmbPrivilegeType.Name = "cmbPrivilegeType";
			this.cmbPrivilegeType.Size = new System.Drawing.Size(121, 21);
			this.cmbPrivilegeType.TabIndex = 5;
			// 
			// txtMiscPrivilegeLabel
			// 
			this.txtMiscPrivilegeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMiscPrivilegeLabel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.txtMiscPrivilegeLabel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.txtMiscPrivilegeLabel.Location = new System.Drawing.Point(106, 143);
			this.txtMiscPrivilegeLabel.Name = "txtMiscPrivilegeLabel";
			this.txtMiscPrivilegeLabel.Size = new System.Drawing.Size(664, 20);
			this.txtMiscPrivilegeLabel.TabIndex = 9;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(23, 146);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(77, 13);
			this.label5.TabIndex = 8;
			this.label5.Text = "Misc. privilege:";
			// 
			// SearchByPrivilegeDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 461);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SearchByPrivilegeDialog";
			this.Text = "Search roles by privilege";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.pnlHeader.ResumeLayout(false);
			this.pnlHeader.PerformLayout();
			this.pnlFooter.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel pnlFooter;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Panel pnlHeader;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton rSearchType1;
		private System.Windows.Forms.TextBox txtPrivilegeName;
		private System.Windows.Forms.RadioButton rSearchType2;
		private System.Windows.Forms.Label label4;
		private EditableComboBox cmbPrivilegeType;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtTables;
		private System.Windows.Forms.TextBox txtMiscPrivilegeLabel;
		private System.Windows.Forms.Label label5;
	}
}