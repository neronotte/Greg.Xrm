namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	partial class ResultRecordViewWithDiffPlex
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultRecordViewWithDiffPlex));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.cmbAttributes = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.pnlMessage = new System.Windows.Forms.Panel();
			this.bMoveDown = new System.Windows.Forms.Button();
			this.bMoveUp = new System.Windows.Forms.Button();
			this.lblMessage = new System.Windows.Forms.Label();
			this.pnlContainer = new System.Windows.Forms.Panel();
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
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.pnlMessage, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.pnlContainer, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(987, 715);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// panel1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
			this.panel1.Controls.Add(this.cmbAttributes);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 43);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(981, 34);
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
			this.cmbAttributes.Size = new System.Drawing.Size(907, 21);
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
			// pnlMessage
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.pnlMessage, 2);
			this.pnlMessage.Controls.Add(this.bMoveDown);
			this.pnlMessage.Controls.Add(this.bMoveUp);
			this.pnlMessage.Controls.Add(this.lblMessage);
			this.pnlMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMessage.Location = new System.Drawing.Point(3, 3);
			this.pnlMessage.Name = "pnlMessage";
			this.pnlMessage.Size = new System.Drawing.Size(981, 34);
			this.pnlMessage.TabIndex = 5;
			// 
			// bMoveDown
			// 
			this.bMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bMoveDown.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.arrow_down;
			this.bMoveDown.Location = new System.Drawing.Point(947, 1);
			this.bMoveDown.Name = "bMoveDown";
			this.bMoveDown.Size = new System.Drawing.Size(32, 32);
			this.bMoveDown.TabIndex = 2;
			this.bMoveDown.UseVisualStyleBackColor = true;
			this.bMoveDown.Click += new System.EventHandler(this.OnSelectNext);
			// 
			// bMoveUp
			// 
			this.bMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bMoveUp.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.arrow_up;
			this.bMoveUp.Location = new System.Drawing.Point(914, 1);
			this.bMoveUp.Name = "bMoveUp";
			this.bMoveUp.Size = new System.Drawing.Size(32, 32);
			this.bMoveUp.TabIndex = 1;
			this.bMoveUp.UseVisualStyleBackColor = true;
			this.bMoveUp.Click += new System.EventHandler(this.OnSelectPrevious);
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
			// pnlContainer
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.pnlContainer, 2);
			this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContainer.Location = new System.Drawing.Point(3, 83);
			this.pnlContainer.Name = "pnlContainer";
			this.pnlContainer.Size = new System.Drawing.Size(981, 629);
			this.pnlContainer.TabIndex = 6;
			// 
			// ResultRecordViewWithDiffPlex
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(987, 715);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ResultRecordViewWithDiffPlex";
			this.TabText = "Record info";
			this.Text = "Record info";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.pnlMessage.ResumeLayout(false);
			this.pnlMessage.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ComboBox cmbAttributes;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel pnlMessage;
		private System.Windows.Forms.Button bMoveDown;
		private System.Windows.Forms.Button bMoveUp;
		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.Panel pnlContainer;
	}
}
