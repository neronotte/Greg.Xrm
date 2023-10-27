namespace Greg.Xrm.ModernThemeBuilder.Views.CreateTheme
{
	partial class FullDialog
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
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.grid = new System.Windows.Forms.DataGridView();
			this.cDisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cPublisher = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnSearch = new System.Windows.Forms.Button();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblPrefix = new System.Windows.Forms.Label();
			this.lblSuffix = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Image = global::Greg.Xrm.ModernThemeBuilder.Properties.Resources.cross;
			this.btnCancel.Location = new System.Drawing.Point(407, 415);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 9;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.OnCancelClick);
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Image = global::Greg.Xrm.ModernThemeBuilder.Properties.Resources.tick;
			this.btnOk.Location = new System.Drawing.Point(326, 415);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 8;
			this.btnOk.Text = "OK";
			this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.OnOkClick);
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
			this.grid.Location = new System.Drawing.Point(12, 86);
			this.grid.Name = "grid";
			this.grid.ReadOnly = true;
			this.grid.RowHeadersVisible = false;
			this.grid.Size = new System.Drawing.Size(470, 198);
			this.grid.TabIndex = 7;
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
			// btnSearch
			// 
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.Enabled = false;
			this.btnSearch.Image = global::Greg.Xrm.ModernThemeBuilder.Properties.Resources.zoom;
			this.btnSearch.Location = new System.Drawing.Point(407, 48);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(75, 23);
			this.btnSearch.TabIndex = 6;
			this.btnSearch.Text = "Search";
			this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new System.EventHandler(this.OnSearchClick);
			// 
			// txtSearch
			// 
			this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSearch.Enabled = false;
			this.txtSearch.Location = new System.Drawing.Point(12, 48);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(389, 20);
			this.txtSearch.TabIndex = 5;
			this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnSearchKeyUp);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(600, 32);
			this.label1.TabIndex = 10;
			this.label1.Text = "Themes must be created as XML WebResources inside a solution. \r\nPlease select the" +
    " solution that will contain the generated XML files:";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
			this.label2.Location = new System.Drawing.Point(12, 369);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(470, 26);
			this.label2.TabIndex = 15;
			this.label2.Text = "As best practice, we suggest to put the theme into a /themes virtual folder.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lblPrefix
			// 
			this.lblPrefix.Location = new System.Drawing.Point(12, 336);
			this.lblPrefix.Name = "lblPrefix";
			this.lblPrefix.Size = new System.Drawing.Size(56, 23);
			this.lblPrefix.TabIndex = 14;
			this.lblPrefix.Text = "greg_";
			this.lblPrefix.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblSuffix
			// 
			this.lblSuffix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblSuffix.AutoSize = true;
			this.lblSuffix.Location = new System.Drawing.Point(457, 336);
			this.lblSuffix.Name = "lblSuffix";
			this.lblSuffix.Size = new System.Drawing.Size(25, 13);
			this.lblSuffix.TabIndex = 13;
			this.lblSuffix.Text = ".xml";
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.Location = new System.Drawing.Point(70, 333);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(384, 20);
			this.txtName.TabIndex = 12;
			this.txtName.Text = "/themes/theme";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 303);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(345, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "Please specify the name of the webresource that will contain the theme:";
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// FullDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(494, 450);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lblPrefix);
			this.Controls.Add(this.lblSuffix);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.grid);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.txtSearch);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FullDialog";
			this.Text = "Create new theme";
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.DataGridView grid;
		private System.Windows.Forms.DataGridViewTextBoxColumn cDisplayName;
		private System.Windows.Forms.DataGridViewTextBoxColumn cVersion;
		private System.Windows.Forms.DataGridViewTextBoxColumn cPublisher;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblPrefix;
		private System.Windows.Forms.Label lblSuffix;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ErrorProvider errorProvider;
	}
}