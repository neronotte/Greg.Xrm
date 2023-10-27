namespace Greg.Xrm.ModernThemeBuilder.Views
{
	partial class SolutionDialog
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
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.grid = new System.Windows.Forms.DataGridView();
			this.cDisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cPublisher = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnSearch = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			this.SuspendLayout();
			// 
			// txtSearch
			// 
			this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSearch.Enabled = false;
			this.txtSearch.Location = new System.Drawing.Point(12, 12);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(519, 20);
			this.txtSearch.TabIndex = 0;
			this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnSearchKeyUp);
			// 
			// grid
			// 
			this.grid.AllowUserToAddRows = false;
			this.grid.AllowUserToDeleteRows = false;
			this.grid.AllowUserToResizeColumns = false;
			this.grid.AllowUserToResizeRows = false;
			this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grid.BackgroundColor = System.Drawing.Color.White;
			this.grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cDisplayName,
            this.cVersion,
            this.cPublisher});
			this.grid.Location = new System.Drawing.Point(12, 50);
			this.grid.Name = "grid";
			this.grid.ReadOnly = true;
			this.grid.RowHeadersVisible = false;
			this.grid.Size = new System.Drawing.Size(600, 258);
			this.grid.TabIndex = 2;
			this.grid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnCellDoubleClick);
			this.grid.SelectionChanged += new System.EventHandler(this.OnSelectionChanged);
			// 
			// cDisplayName
			// 
			this.cDisplayName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.cDisplayName.DataPropertyName = "friendlyname";
			this.cDisplayName.HeaderText = "Display Name";
			this.cDisplayName.MinimumWidth = 200;
			this.cDisplayName.Name = "cDisplayName";
			this.cDisplayName.ReadOnly = true;
			this.cDisplayName.Width = 200;
			// 
			// cVersion
			// 
			this.cVersion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.cVersion.DataPropertyName = "version";
			this.cVersion.HeaderText = "Version";
			this.cVersion.Name = "cVersion";
			this.cVersion.ReadOnly = true;
			this.cVersion.Width = 67;
			// 
			// cPublisher
			// 
			this.cPublisher.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.cPublisher.DataPropertyName = "publisheridfriendlyname";
			this.cPublisher.HeaderText = "Publisher";
			this.cPublisher.Name = "cPublisher";
			this.cPublisher.ReadOnly = true;
			// 
			// btnCancel
			// 
			this.btnCancel.Image = global::Greg.Xrm.ModernThemeBuilder.Properties.Resources.cross;
			this.btnCancel.Location = new System.Drawing.Point(537, 326);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.OnCancelClick);
			// 
			// btnOk
			// 
			this.btnOk.Enabled = false;
			this.btnOk.Image = global::Greg.Xrm.ModernThemeBuilder.Properties.Resources.tick;
			this.btnOk.Location = new System.Drawing.Point(456, 326);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 3;
			this.btnOk.Text = "OK";
			this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.OnOkClick);
			// 
			// btnSearch
			// 
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.Enabled = false;
			this.btnSearch.Image = global::Greg.Xrm.ModernThemeBuilder.Properties.Resources.zoom;
			this.btnSearch.Location = new System.Drawing.Point(537, 12);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(75, 23);
			this.btnSearch.TabIndex = 1;
			this.btnSearch.Text = "Search";
			this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new System.EventHandler(this.OnSearchClick);
			// 
			// SolutionDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(624, 361);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.grid);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.txtSearch);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SolutionDialog";
			this.ShowInTaskbar = false;
			this.Text = "Select a solution";
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.DataGridView grid;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.DataGridViewTextBoxColumn cDisplayName;
		private System.Windows.Forms.DataGridViewTextBoxColumn cVersion;
		private System.Windows.Forms.DataGridViewTextBoxColumn cPublisher;
	}
}