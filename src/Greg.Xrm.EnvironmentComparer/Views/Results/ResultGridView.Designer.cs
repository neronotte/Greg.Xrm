
namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	partial class ResultGridView
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultGridView));
			this.listView1 = new System.Windows.Forms.ListView();
			this.cKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cResult = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cFields = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cEnv1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cEnv2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmiCopyToEnv2 = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.cmiCopyToEnv1 = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.cmiCompare = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.contextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(46)))), ((int)(((byte)(40)))));
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cKey,
            this.cResult,
            this.cFields,
            this.cEnv1,
            this.cEnv2});
			this.listView1.ContextMenuStrip = this.contextMenu;
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listView1.FullRowSelect = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(1025, 651);
			this.listView1.TabIndex = 1;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.OnRecordSelectionChanged);
			this.listView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnResultKeyUp);
			// 
			// cKey
			// 
			this.cKey.Text = "Key";
			this.cKey.Width = 200;
			// 
			// cResult
			// 
			this.cResult.Text = "Result";
			this.cResult.Width = 150;
			// 
			// cFields
			// 
			this.cFields.Text = "Attributes";
			this.cFields.Width = 200;
			// 
			// cEnv1
			// 
			this.cEnv1.Text = "ENV1 values";
			this.cEnv1.Width = 400;
			// 
			// cEnv2
			// 
			this.cEnv2.Text = "ENV2 values";
			this.cEnv2.Width = 400;
			// 
			// contextMenu
			// 
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiCopyToEnv2,
            this.cmiCopyToEnv1,
            this.cmiCompare});
			this.contextMenu.Name = "contextMenuStrip1";
			this.contextMenu.Size = new System.Drawing.Size(181, 92);
			// 
			// cmiCopyToEnv2
			// 
			this.cmiCopyToEnv2.Enabled = false;
			this.cmiCopyToEnv2.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.arrow_right;
			this.cmiCopyToEnv2.Name = "cmiCopyToEnv2";
			this.cmiCopyToEnv2.Size = new System.Drawing.Size(180, 22);
			this.cmiCopyToEnv2.Text = "Copy to ENV2";
			this.cmiCopyToEnv2.Click += new System.EventHandler(this.OnCopyToEnv2Click);
			// 
			// cmiCopyToEnv1
			// 
			this.cmiCopyToEnv1.Enabled = false;
			this.cmiCopyToEnv1.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.arrow_left;
			this.cmiCopyToEnv1.Name = "cmiCopyToEnv1";
			this.cmiCopyToEnv1.Size = new System.Drawing.Size(180, 22);
			this.cmiCopyToEnv1.Text = "Copy to ENV1";
			this.cmiCopyToEnv1.Click += new System.EventHandler(this.OnCopyToEnv1Click);
			// 
			// cmiCompare
			// 
			this.cmiCompare.Enabled = false;
			this.cmiCompare.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.compare;
			this.cmiCompare.Name = "cmiCompare";
			this.cmiCompare.Size = new System.Drawing.Size(180, 22);
			this.cmiCompare.Text = "Compare...";
			this.cmiCompare.Click += new System.EventHandler(this.OnCompareClick);
			// 
			// ResultGridView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1025, 651);
			this.CloseButtonVisible = false;
			this.Controls.Add(this.listView1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ResultGridView";
			this.TabText = "Comparison Result Details";
			this.contextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader cKey;
		private System.Windows.Forms.ColumnHeader cEnv1;
		private System.Windows.Forms.ColumnHeader cEnv2;
		private System.Windows.Forms.ColumnHeader cResult;
		private System.Windows.Forms.ColumnHeader cFields;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.ContextMenuStrip contextMenu;
		private Greg.Xrm.Views.ToolStripBindableMenuItem cmiCopyToEnv2;
		private Greg.Xrm.Views.ToolStripBindableMenuItem cmiCopyToEnv1;
		private Greg.Xrm.Views.ToolStripBindableMenuItem cmiCompare;
	}
}
