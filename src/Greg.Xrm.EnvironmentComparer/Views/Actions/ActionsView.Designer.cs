
namespace Greg.Xrm.EnvironmentComparer.Views.Actions
{
	partial class ActionsView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActionsView));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.chlActionList = new System.Windows.Forms.CheckedListBox();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tClear = new System.Windows.Forms.ToolStripButton();
			this.tApplyAll = new System.Windows.Forms.ToolStripButton();
			this.tApplyChecked = new System.Windows.Forms.ToolStripButton();
			this.tCheckAll = new System.Windows.Forms.ToolStripButton();
			this.tUncheckAll = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tClear,
            this.tApplyAll,
            this.tApplyChecked,
            this.toolStripSeparator1,
            this.tCheckAll,
            this.tUncheckAll});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(447, 25);
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// chlActionList
			// 
			this.chlActionList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chlActionList.FormattingEnabled = true;
			this.chlActionList.Location = new System.Drawing.Point(0, 25);
			this.chlActionList.Name = "chlActionList";
			this.chlActionList.Size = new System.Drawing.Size(447, 433);
			this.chlActionList.TabIndex = 3;
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tClear
			// 
			this.tClear.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.bin_empty;
			this.tClear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tClear.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tClear.Name = "tClear";
			this.tClear.Size = new System.Drawing.Size(54, 22);
			this.tClear.Text = "Clear";
			this.tClear.Click += new System.EventHandler(this.OnClearClick);
			// 
			// tApplyAll
			// 
			this.tApplyAll.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.script_go;
			this.tApplyAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tApplyAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tApplyAll.Name = "tApplyAll";
			this.tApplyAll.Size = new System.Drawing.Size(73, 22);
			this.tApplyAll.Text = "Apply all";
			this.tApplyAll.Click += new System.EventHandler(this.OnApplyAllClick);
			// 
			// tApplyChecked
			// 
			this.tApplyChecked.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.script_go;
			this.tApplyChecked.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tApplyChecked.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tApplyChecked.Name = "tApplyChecked";
			this.tApplyChecked.Size = new System.Drawing.Size(105, 22);
			this.tApplyChecked.Text = "Apply checked";
			this.tApplyChecked.Click += new System.EventHandler(this.OnApplyCheckedClick);
			// 
			// tCheckAll
			// 
			this.tCheckAll.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.picture_tick;
			this.tCheckAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tCheckAll.Name = "tCheckAll";
			this.tCheckAll.Size = new System.Drawing.Size(73, 22);
			this.tCheckAll.Text = "Select all";
			this.tCheckAll.Click += new System.EventHandler(this.OnSelectAllClick);
			// 
			// tUncheckAll
			// 
			this.tUncheckAll.Image = global::Greg.Xrm.EnvironmentComparer.Properties.Resources.picture_empty;
			this.tUncheckAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tUncheckAll.Name = "tUncheckAll";
			this.tUncheckAll.Size = new System.Drawing.Size(86, 22);
			this.tUncheckAll.Text = "Deselect all";
			this.tUncheckAll.Click += new System.EventHandler(this.OnUnselectAllClick);
			// 
			// ActionsView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(447, 458);
			this.CloseButton = false;
			this.CloseButtonVisible = false;
			this.Controls.Add(this.chlActionList);
			this.Controls.Add(this.toolStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ActionsView";
			this.TabText = "Actions";
			this.Text = "Actions";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tClear;
		private System.Windows.Forms.ToolStripButton tApplyAll;
		private System.Windows.Forms.ToolStripButton tApplyChecked;
		private System.Windows.Forms.CheckedListBox chlActionList;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tCheckAll;
		private System.Windows.Forms.ToolStripButton tUncheckAll;
	}
}
