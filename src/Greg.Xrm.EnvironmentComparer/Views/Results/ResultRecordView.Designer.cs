
namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	partial class ResultRecordView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultRecordView));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.txtValue1 = new ScintillaNET.Scintilla();
			this.txtValue2 = new ScintillaNET.Scintilla();
			this.panel1 = new System.Windows.Forms.Panel();
			this.cmbAttributes = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lblEnv1 = new System.Windows.Forms.Label();
			this.lblEnv2 = new System.Windows.Forms.Label();
			this.pnlMessage = new System.Windows.Forms.Panel();
			this.lblMessage = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.pnlMessage.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.txtValue1, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.txtValue2, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.lblEnv1, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.lblEnv2, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.pnlMessage, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1011, 596);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// txtValue1
			// 
			this.txtValue1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtValue1.Location = new System.Drawing.Point(3, 113);
			this.txtValue1.Name = "txtValue1";
			this.txtValue1.Size = new System.Drawing.Size(499, 550);
			this.txtValue1.TabIndex = 0;
			// 
			// txtValue2
			// 
			this.txtValue2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtValue2.Location = new System.Drawing.Point(508, 113);
			this.txtValue2.Name = "txtValue2";
			this.txtValue2.Size = new System.Drawing.Size(500, 550);
			this.txtValue2.TabIndex = 1;
			// 
			// panel1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
			this.panel1.Controls.Add(this.cmbAttributes);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 43);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1005, 34);
			this.panel1.TabIndex = 2;
			// 
			// cmbAttributes
			// 
			this.cmbAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmbAttributes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbAttributes.FormattingEnabled = true;
			this.cmbAttributes.Location = new System.Drawing.Point(65, 7);
			this.cmbAttributes.Name = "cmbAttributes";
			this.cmbAttributes.Size = new System.Drawing.Size(931, 21);
			this.cmbAttributes.TabIndex = 1;
			this.cmbAttributes.SelectionChangeCommitted += new System.EventHandler(this.OnSelectionChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Attribute:";
			// 
			// lblEnv1
			// 
			this.lblEnv1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblEnv1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblEnv1.Location = new System.Drawing.Point(3, 80);
			this.lblEnv1.Name = "lblEnv1";
			this.lblEnv1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 3);
			this.lblEnv1.Size = new System.Drawing.Size(499, 30);
			this.lblEnv1.TabIndex = 3;
			this.lblEnv1.Text = "Environment...";
			this.lblEnv1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblEnv2
			// 
			this.lblEnv2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblEnv2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblEnv2.Location = new System.Drawing.Point(508, 80);
			this.lblEnv2.Name = "lblEnv2";
			this.lblEnv2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 3);
			this.lblEnv2.Size = new System.Drawing.Size(500, 30);
			this.lblEnv2.TabIndex = 4;
			this.lblEnv2.Text = "Environment...";
			this.lblEnv2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnlMessage
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.pnlMessage, 2);
			this.pnlMessage.Controls.Add(this.lblMessage);
			this.pnlMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMessage.Location = new System.Drawing.Point(3, 3);
			this.pnlMessage.Name = "pnlMessage";
			this.pnlMessage.Size = new System.Drawing.Size(1005, 34);
			this.pnlMessage.TabIndex = 5;
			// 
			// lblMessage
			// 
			this.lblMessage.AutoSize = true;
			this.lblMessage.Location = new System.Drawing.Point(13, 10);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(116, 13);
			this.lblMessage.TabIndex = 0;
			this.lblMessage.Text = "Please select a result...";
			// 
			// ResultRecordView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1011, 596);
			this.CloseButton = false;
			this.CloseButtonVisible = false;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ResultRecordView";
			this.TabText = "Record info";
			this.Text = "Record info";
			this.ToolTipText = "";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.pnlMessage.ResumeLayout(false);
			this.pnlMessage.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private ScintillaNET.Scintilla txtValue1;
		private ScintillaNET.Scintilla txtValue2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmbAttributes;
		private System.Windows.Forms.Label lblEnv1;
		private System.Windows.Forms.Label lblEnv2;
		private System.Windows.Forms.Panel pnlMessage;
		private System.Windows.Forms.Label lblMessage;
	}
}
