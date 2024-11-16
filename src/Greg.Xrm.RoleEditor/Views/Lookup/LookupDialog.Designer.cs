namespace Greg.Xrm.RoleEditor.Views.Lookup
{
	partial class LookupDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LookupDialog));
			this.table1 = new System.Windows.Forms.TableLayoutPanel();
			this.messages = new Greg.Xrm.Core.Views.NotificationPanel();
			this.grid = new BrightIdeasSoftware.ObjectListView();
			this.pnlFooter = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.lblDescription = new System.Windows.Forms.Label();
			this.btnNext = new System.Windows.Forms.Button();
			this.btnPrev = new System.Windows.Forms.Button();
			this.btnFirst = new System.Windows.Forms.Button();
			this.lblPage = new System.Windows.Forms.Label();
			this.table1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			this.pnlFooter.SuspendLayout();
			this.SuspendLayout();
			// 
			// table1
			// 
			this.table1.ColumnCount = 1;
			this.table1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.table1.Controls.Add(this.messages, 0, 0);
			this.table1.Controls.Add(this.grid, 0, 2);
			this.table1.Controls.Add(this.pnlFooter, 0, 3);
			this.table1.Controls.Add(this.lblDescription, 0, 1);
			this.table1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.table1.Location = new System.Drawing.Point(0, 0);
			this.table1.Name = "table1";
			this.table1.RowCount = 4;
			this.table1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.table1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.table1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.table1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.table1.Size = new System.Drawing.Size(723, 462);
			this.table1.TabIndex = 0;
			// 
			// messages
			// 
			this.messages.AutoSize = true;
			this.messages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.messages.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.messages.Location = new System.Drawing.Point(3, 3);
			this.messages.Name = "messages";
			this.messages.Size = new System.Drawing.Size(717, 1);
			this.messages.TabIndex = 0;
			// 
			// grid
			// 
			this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grid.HideSelection = false;
			this.grid.Location = new System.Drawing.Point(3, 49);
			this.grid.Name = "grid";
			this.grid.Size = new System.Drawing.Size(717, 350);
			this.grid.TabIndex = 1;
			this.grid.UseCompatibleStateImageBehavior = false;
			this.grid.View = System.Windows.Forms.View.Details;
			// 
			// pnlFooter
			// 
			this.pnlFooter.Controls.Add(this.lblPage);
			this.pnlFooter.Controls.Add(this.btnNext);
			this.pnlFooter.Controls.Add(this.btnPrev);
			this.pnlFooter.Controls.Add(this.btnFirst);
			this.pnlFooter.Controls.Add(this.btnCancel);
			this.pnlFooter.Controls.Add(this.btnOk);
			this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlFooter.Location = new System.Drawing.Point(3, 405);
			this.pnlFooter.Name = "pnlFooter";
			this.pnlFooter.Size = new System.Drawing.Size(717, 54);
			this.pnlFooter.TabIndex = 2;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(555, 11);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 30);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.OnCancelClick);
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Enabled = false;
			this.btnOk.Location = new System.Drawing.Point(636, 11);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 30);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.OnOkClick);
			// 
			// lblDescription
			// 
			this.lblDescription.AutoEllipsis = true;
			this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblDescription.Location = new System.Drawing.Point(3, 6);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Padding = new System.Windows.Forms.Padding(5);
			this.lblDescription.Size = new System.Drawing.Size(717, 40);
			this.lblDescription.TabIndex = 3;
			this.lblDescription.Text = "Please select a record below:";
			// 
			// btnNext
			// 
			this.btnNext.Enabled = false;
			this.btnNext.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.resultset_next;
			this.btnNext.Location = new System.Drawing.Point(148, 11);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(30, 30);
			this.btnNext.TabIndex = 4;
			this.btnNext.UseVisualStyleBackColor = true;
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			// 
			// btnPrev
			// 
			this.btnPrev.Enabled = false;
			this.btnPrev.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.resultset_previous;
			this.btnPrev.Location = new System.Drawing.Point(45, 11);
			this.btnPrev.Name = "btnPrev";
			this.btnPrev.Size = new System.Drawing.Size(30, 30);
			this.btnPrev.TabIndex = 3;
			this.btnPrev.UseVisualStyleBackColor = true;
			this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
			// 
			// btnFirst
			// 
			this.btnFirst.Enabled = false;
			this.btnFirst.Image = global::Greg.Xrm.RoleEditor.Properties.Resources.resultset_first;
			this.btnFirst.Location = new System.Drawing.Point(9, 11);
			this.btnFirst.Name = "btnFirst";
			this.btnFirst.Size = new System.Drawing.Size(30, 30);
			this.btnFirst.TabIndex = 2;
			this.btnFirst.UseVisualStyleBackColor = true;
			this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
			// 
			// lblPage
			// 
			this.lblPage.Location = new System.Drawing.Point(81, 11);
			this.lblPage.Name = "lblPage";
			this.lblPage.Size = new System.Drawing.Size(61, 30);
			this.lblPage.TabIndex = 5;
			this.lblPage.Text = "Page 1";
			this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LookupDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(723, 462);
			this.Controls.Add(this.table1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LookupDialog";
			this.Text = "LookupDialog";
			this.table1.ResumeLayout(false);
			this.table1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			this.pnlFooter.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel table1;
		private Core.Views.NotificationPanel messages;
		private BrightIdeasSoftware.ObjectListView grid;
		private System.Windows.Forms.Panel pnlFooter;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.Button btnFirst;
		private System.Windows.Forms.Button btnPrev;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.Label lblPage;
	}
}