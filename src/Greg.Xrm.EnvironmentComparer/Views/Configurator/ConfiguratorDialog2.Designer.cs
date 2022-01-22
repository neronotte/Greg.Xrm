
namespace Greg.Xrm.EnvironmentComparer.Views.Configurator
{
	partial class ConfiguratorDialog2
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfiguratorDialog2));
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.bKeyUnselectAll = new System.Windows.Forms.Button();
			this.bKeySelectAll = new System.Windows.Forms.Button();
			this.panel3 = new System.Windows.Forms.Panel();
			this.bIgnoreUnselectAll = new System.Windows.Forms.Button();
			this.bIgnoreSelectAll = new System.Windows.Forms.Button();
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.panel6 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel6.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(3, 9);
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
			this.txtEntitySearch.Location = new System.Drawing.Point(3, 3);
			this.txtEntitySearch.Name = "txtEntitySearch";
			this.txtEntitySearch.Size = new System.Drawing.Size(323, 20);
			this.txtEntitySearch.TabIndex = 2;
			this.txtEntitySearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnEntitySearchKeyUp);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(930, 614);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.OnCancelClick);
			// 
			// lstEntities
			// 
			this.lstEntities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstEntities.FormattingEnabled = true;
			this.lstEntities.IntegralHeight = false;
			this.lstEntities.Location = new System.Drawing.Point(3, 29);
			this.lstEntities.Name = "lstEntities";
			this.lstEntities.Size = new System.Drawing.Size(323, 527);
			this.lstEntities.TabIndex = 4;
			this.lstEntities.SelectedIndexChanged += new System.EventHandler(this.OnEntitySelectionChanged);
			// 
			// btnAccept
			// 
			this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAccept.Location = new System.Drawing.Point(849, 614);
			this.btnAccept.Name = "btnAccept";
			this.btnAccept.Size = new System.Drawing.Size(75, 23);
			this.btnAccept.TabIndex = 5;
			this.btnAccept.Text = "OK";
			this.btnAccept.UseVisualStyleBackColor = true;
			this.btnAccept.Click += new System.EventHandler(this.OnOKClick);
			// 
			// chlKey
			// 
			this.chlKey.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.chlKey.Enabled = false;
			this.chlKey.FormattingEnabled = true;
			this.chlKey.IntegralHeight = false;
			this.chlKey.Location = new System.Drawing.Point(21, 52);
			this.chlKey.Name = "chlKey";
			this.chlKey.Size = new System.Drawing.Size(315, 475);
			this.chlKey.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(4, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(194, 17);
			this.label2.TabIndex = 7;
			this.label2.Text = "...and the key for comparison...";
			// 
			// rUseGuid
			// 
			this.rUseGuid.AutoSize = true;
			this.rUseGuid.Checked = true;
			this.rUseGuid.Location = new System.Drawing.Point(7, 6);
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
			this.rUseAttributes.Location = new System.Drawing.Point(7, 29);
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
			this.label3.Location = new System.Drawing.Point(4, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(163, 17);
			this.label3.TabIndex = 10;
			this.label3.Text = "...and attributes to ignore";
			// 
			// chlSkip
			// 
			this.chlSkip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.chlSkip.FormattingEnabled = true;
			this.chlSkip.IntegralHeight = false;
			this.chlSkip.Location = new System.Drawing.Point(3, 28);
			this.chlSkip.Name = "chlSkip";
			this.chlSkip.Size = new System.Drawing.Size(325, 499);
			this.chlSkip.TabIndex = 11;
			// 
			// chkOnlyActive
			// 
			this.chkOnlyActive.AutoSize = true;
			this.chkOnlyActive.Checked = true;
			this.chkOnlyActive.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkOnlyActive.Location = new System.Drawing.Point(8, 7);
			this.chkOnlyActive.Name = "chkOnlyActive";
			this.chkOnlyActive.Size = new System.Drawing.Size(159, 17);
			this.chkOnlyActive.TabIndex = 12;
			this.chkOnlyActive.Text = "Consider only active records";
			this.chkOnlyActive.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel3, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel5, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel6, 2, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1017, 605);
			this.tableLayoutPanel1.TabIndex = 13;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.txtEntitySearch);
			this.panel1.Controls.Add(this.lstEntities);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 43);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(329, 559);
			this.panel1.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.bKeyUnselectAll);
			this.panel2.Controls.Add(this.bKeySelectAll);
			this.panel2.Controls.Add(this.chlKey);
			this.panel2.Controls.Add(this.rUseGuid);
			this.panel2.Controls.Add(this.rUseAttributes);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(338, 43);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(339, 559);
			this.panel2.TabIndex = 1;
			// 
			// bKeyUnselectAll
			// 
			this.bKeyUnselectAll.Location = new System.Drawing.Point(255, 533);
			this.bKeyUnselectAll.Name = "bKeyUnselectAll";
			this.bKeyUnselectAll.Size = new System.Drawing.Size(75, 23);
			this.bKeyUnselectAll.TabIndex = 11;
			this.bKeyUnselectAll.Text = "Unselect all";
			this.bKeyUnselectAll.UseVisualStyleBackColor = true;
			this.bKeyUnselectAll.Click += new System.EventHandler(this.OnKeyUnselectAll);
			// 
			// bKeySelectAll
			// 
			this.bKeySelectAll.Location = new System.Drawing.Point(174, 533);
			this.bKeySelectAll.Name = "bKeySelectAll";
			this.bKeySelectAll.Size = new System.Drawing.Size(75, 23);
			this.bKeySelectAll.TabIndex = 10;
			this.bKeySelectAll.Text = "Select all";
			this.bKeySelectAll.UseVisualStyleBackColor = true;
			this.bKeySelectAll.Click += new System.EventHandler(this.OnKeySelectAll);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.bIgnoreUnselectAll);
			this.panel3.Controls.Add(this.chlSkip);
			this.panel3.Controls.Add(this.bIgnoreSelectAll);
			this.panel3.Controls.Add(this.chkOnlyActive);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(683, 43);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(331, 559);
			this.panel3.TabIndex = 2;
			// 
			// bIgnoreUnselectAll
			// 
			this.bIgnoreUnselectAll.Location = new System.Drawing.Point(247, 533);
			this.bIgnoreUnselectAll.Name = "bIgnoreUnselectAll";
			this.bIgnoreUnselectAll.Size = new System.Drawing.Size(75, 23);
			this.bIgnoreUnselectAll.TabIndex = 13;
			this.bIgnoreUnselectAll.Text = "Unselect all";
			this.bIgnoreUnselectAll.UseVisualStyleBackColor = true;
			this.bIgnoreUnselectAll.Click += new System.EventHandler(this.OnIgnoreUnselectAll);
			// 
			// bIgnoreSelectAll
			// 
			this.bIgnoreSelectAll.Location = new System.Drawing.Point(166, 533);
			this.bIgnoreSelectAll.Name = "bIgnoreSelectAll";
			this.bIgnoreSelectAll.Size = new System.Drawing.Size(75, 23);
			this.bIgnoreSelectAll.TabIndex = 12;
			this.bIgnoreSelectAll.Text = "Select all";
			this.bIgnoreSelectAll.UseVisualStyleBackColor = true;
			this.bIgnoreSelectAll.Click += new System.EventHandler(this.OnIgnoreSelectAll);
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.Color.White;
			this.panel4.Controls.Add(this.label1);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.Location = new System.Drawing.Point(3, 3);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(329, 34);
			this.panel4.TabIndex = 3;
			// 
			// panel5
			// 
			this.panel5.BackColor = System.Drawing.Color.White;
			this.panel5.Controls.Add(this.label2);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel5.Location = new System.Drawing.Point(338, 3);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(339, 34);
			this.panel5.TabIndex = 4;
			// 
			// panel6
			// 
			this.panel6.BackColor = System.Drawing.Color.White;
			this.panel6.Controls.Add(this.label3);
			this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel6.Location = new System.Drawing.Point(683, 3);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(331, 34);
			this.panel6.TabIndex = 5;
			// 
			// ConfiguratorDialog2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1017, 646);
			this.ControlBox = false;
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.btnAccept);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConfiguratorDialog2";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Add entity";
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.panel5.ResumeLayout(false);
			this.panel5.PerformLayout();
			this.panel6.ResumeLayout(false);
			this.panel6.PerformLayout();
			this.ResumeLayout(false);

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
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Button bKeyUnselectAll;
		private System.Windows.Forms.Button bKeySelectAll;
		private System.Windows.Forms.Button bIgnoreUnselectAll;
		private System.Windows.Forms.Button bIgnoreSelectAll;
	}
}