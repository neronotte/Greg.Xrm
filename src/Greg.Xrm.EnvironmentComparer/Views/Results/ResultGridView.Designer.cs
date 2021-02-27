
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
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmiCopyToEnv2 = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.cmiCopyToEnv1 = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.cmiDeleteFromEnv1 = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.cmiDeleteFromEnv2 = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.cmiCompare = new Greg.Xrm.Views.ToolStripBindableMenuItem();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.lLegend1 = new System.Windows.Forms.Label();
			this.lLegend2 = new System.Windows.Forms.Label();
			this.lLegend3 = new System.Windows.Forms.Label();
			this.lLegend4 = new System.Windows.Forms.Label();
			this.lLegend5 = new System.Windows.Forms.Label();
			this.contextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(46)))), ((int)(((byte)(40)))));
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cKey,
            this.cResult,
            this.cFields,
            this.cEnv1,
            this.cEnv2});
			this.listView1.ContextMenuStrip = this.contextMenu;
			this.listView1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listView1.FullRowSelect = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(1025, 610);
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
            this.toolStripMenuItem1,
            this.cmiDeleteFromEnv1,
            this.cmiDeleteFromEnv2,
            this.toolStripMenuItem2,
            this.cmiCompare});
			this.contextMenu.Name = "contextMenuStrip1";
			this.contextMenu.Size = new System.Drawing.Size(181, 148);
			// 
			// cmiCopyToEnv2
			// 
			this.cmiCopyToEnv2.Enabled = false;
			this.cmiCopyToEnv2.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.arrow_right;
			this.cmiCopyToEnv2.Name = "cmiCopyToEnv2";
			this.cmiCopyToEnv2.Size = new System.Drawing.Size(180, 22);
			this.cmiCopyToEnv2.Text = "Copy to ENV2";
			// 
			// cmiCopyToEnv1
			// 
			this.cmiCopyToEnv1.Enabled = false;
			this.cmiCopyToEnv1.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.arrow_left;
			this.cmiCopyToEnv1.Name = "cmiCopyToEnv1";
			this.cmiCopyToEnv1.Size = new System.Drawing.Size(180, 22);
			this.cmiCopyToEnv1.Text = "Copy to ENV1";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
			// 
			// cmiDeleteFromEnv1
			// 
			this.cmiDeleteFromEnv1.Enabled = false;
			this.cmiDeleteFromEnv1.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.delete_left;
			this.cmiDeleteFromEnv1.Name = "cmiDeleteFromEnv1";
			this.cmiDeleteFromEnv1.Size = new System.Drawing.Size(180, 22);
			this.cmiDeleteFromEnv1.Text = "Delete from ENV1";
			// 
			// cmiDeleteFromEnv2
			// 
			this.cmiDeleteFromEnv2.Enabled = false;
			this.cmiDeleteFromEnv2.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.delete_right;
			this.cmiDeleteFromEnv2.Name = "cmiDeleteFromEnv2";
			this.cmiDeleteFromEnv2.Size = new System.Drawing.Size(180, 22);
			this.cmiDeleteFromEnv2.Text = "Delete from ENV2";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
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
			// lLegend1
			// 
			this.lLegend1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lLegend1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lLegend1.Location = new System.Drawing.Point(5, 615);
			this.lLegend1.Name = "lLegend1";
			this.lLegend1.Size = new System.Drawing.Size(150, 30);
			this.lLegend1.TabIndex = 2;
			this.lLegend1.Text = "Matching";
			this.lLegend1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lLegend2
			// 
			this.lLegend2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lLegend2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lLegend2.Location = new System.Drawing.Point(161, 615);
			this.lLegend2.Name = "lLegend2";
			this.lLegend2.Size = new System.Drawing.Size(150, 30);
			this.lLegend2.TabIndex = 3;
			this.lLegend2.Text = "Missing on ENV1";
			this.lLegend2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lLegend3
			// 
			this.lLegend3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lLegend3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lLegend3.Location = new System.Drawing.Point(317, 615);
			this.lLegend3.Name = "lLegend3";
			this.lLegend3.Size = new System.Drawing.Size(150, 30);
			this.lLegend3.TabIndex = 4;
			this.lLegend3.Text = "Missing on ENV2";
			this.lLegend3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lLegend4
			// 
			this.lLegend4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lLegend4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lLegend4.Location = new System.Drawing.Point(473, 615);
			this.lLegend4.Name = "lLegend4";
			this.lLegend4.Size = new System.Drawing.Size(150, 30);
			this.lLegend4.TabIndex = 5;
			this.lLegend4.Text = "Matching, with differences";
			this.lLegend4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lLegend5
			// 
			this.lLegend5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lLegend5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lLegend5.Location = new System.Drawing.Point(629, 615);
			this.lLegend5.Name = "lLegend5";
			this.lLegend5.Size = new System.Drawing.Size(150, 30);
			this.lLegend5.TabIndex = 6;
			this.lLegend5.Text = "Action ready in pipeline";
			this.lLegend5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ResultGridView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1025, 651);
			this.CloseButtonVisible = false;
			this.Controls.Add(this.lLegend5);
			this.Controls.Add(this.lLegend4);
			this.Controls.Add(this.lLegend3);
			this.Controls.Add(this.lLegend2);
			this.Controls.Add(this.lLegend1);
			this.Controls.Add(this.listView1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ResultGridView";
			this.TabText = "Comparison Result Details";
			this.Text = "Comparison Result Details";
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
		private System.Windows.Forms.Label lLegend1;
		private System.Windows.Forms.Label lLegend2;
		private System.Windows.Forms.Label lLegend3;
		private System.Windows.Forms.Label lLegend4;
		private System.Windows.Forms.Label lLegend5;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private Greg.Xrm.Views.ToolStripBindableMenuItem cmiDeleteFromEnv1;
		private Greg.Xrm.Views.ToolStripBindableMenuItem cmiDeleteFromEnv2;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
	}
}
