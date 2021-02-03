using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public partial class SolutionsView
	{
		private void InitializeComponent()
		{			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolutionsView));
			this.listView = new System.Windows.Forms.ListView();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.lSuccess = new System.Windows.Forms.Label();
			this.lWarn = new System.Windows.Forms.Label();
			this.lError = new System.Windows.Forms.Label();
			this.lGray = new System.Windows.Forms.Label();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.label1 = new System.Windows.Forms.Label();
			this.tExport = new Greg.Xrm.Views.ToolStripBindableButton();
			this.tShowOnlyVisible = new Greg.Xrm.Views.ToolStripBindableButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.tFind = new System.Windows.Forms.ToolStripTextBox();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listView
			// 
			this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView.FullRowSelect = true;
			this.listView.HideSelection = false;
			this.listView.Location = new System.Drawing.Point(0, 25);
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(1119, 576);
			this.listView.TabIndex = 0;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = System.Windows.Forms.View.Details;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tFind,
            this.toolStripSeparator2,
            this.tExport,
            this.toolStripSeparator1,
            this.tShowOnlyVisible});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(1119, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// lSuccess
			// 
			this.lSuccess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lSuccess.BackColor = System.Drawing.Color.White;
			this.lSuccess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lSuccess.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lSuccess.ForeColor = System.Drawing.Color.Black;
			this.lSuccess.Location = new System.Drawing.Point(12, 612);
			this.lSuccess.Name = "lSuccess";
			this.lSuccess.Size = new System.Drawing.Size(200, 30);
			this.lSuccess.TabIndex = 2;
			this.lSuccess.Text = "Success";
			this.lSuccess.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lWarn
			// 
			this.lWarn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lWarn.BackColor = System.Drawing.Color.White;
			this.lWarn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lWarn.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lWarn.ForeColor = System.Drawing.Color.Black;
			this.lWarn.Location = new System.Drawing.Point(218, 612);
			this.lWarn.Name = "lWarn";
			this.lWarn.Size = new System.Drawing.Size(200, 30);
			this.lWarn.TabIndex = 3;
			this.lWarn.Text = "Warn";
			this.lWarn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lError
			// 
			this.lError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lError.BackColor = System.Drawing.Color.White;
			this.lError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lError.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lError.ForeColor = System.Drawing.Color.Black;
			this.lError.Location = new System.Drawing.Point(424, 612);
			this.lError.Name = "lError";
			this.lError.Size = new System.Drawing.Size(200, 30);
			this.lError.TabIndex = 4;
			this.lError.Text = "Error";
			this.lError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lGray
			// 
			this.lGray.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lGray.BackColor = System.Drawing.Color.White;
			this.lGray.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lGray.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lGray.ForeColor = System.Drawing.Color.Black;
			this.lGray.Location = new System.Drawing.Point(630, 612);
			this.lGray.Name = "lGray";
			this.lGray.Size = new System.Drawing.Size(200, 30);
			this.lGray.TabIndex = 5;
			this.lGray.Text = "Gray";
			this.lGray.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.BackColor = System.Drawing.Color.White;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Location = new System.Drawing.Point(836, 612);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(200, 30);
			this.label1.TabIndex = 6;
			this.label1.Text = "M: managed, UM: unmanaged";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tExport
			// 
			this.tExport.Image = global::Greg.Xrm.EnvironmentSolutionsComparer.Properties.Resources.page_white_excel;
			this.tExport.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tExport.Name = "tExport";
			this.tExport.Size = new System.Drawing.Size(70, 22);
			this.tExport.Text = "Export...";
			// 
			// tShowOnlyVisible
			// 
			this.tShowOnlyVisible.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tShowOnlyVisible.Image = ((System.Drawing.Image)(resources.GetObject("tShowOnlyVisible.Image")));
			this.tShowOnlyVisible.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tShowOnlyVisible.Name = "tShowOnlyVisible";
			this.tShowOnlyVisible.Size = new System.Drawing.Size(153, 22);
			this.tShowOnlyVisible.Text = "Show only visible solutions";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(33, 22);
			this.toolStripLabel1.Text = "Find:";
			// 
			// tFind
			// 
			this.tFind.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.tFind.Name = "tFind";
			this.tFind.Size = new System.Drawing.Size(200, 25);
			this.tFind.ToolTipText = "Press F3 to search next item";
			this.tFind.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnFindKeyUp);
			// 
			// SolutionsView
			// 
			this.ClientSize = new System.Drawing.Size(1119, 651);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lGray);
			this.Controls.Add(this.lError);
			this.Controls.Add(this.lWarn);
			this.Controls.Add(this.lSuccess);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.listView);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SolutionsView";
			this.Text = "Solution list";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

}

		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.Label lSuccess;
		private System.Windows.Forms.Label lWarn;
		private System.Windows.Forms.Label lError;
		private System.Windows.Forms.Label lGray;
		private Greg.Xrm.Views.ToolStripBindableButton tExport;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private Xrm.Views.ToolStripBindableButton tShowOnlyVisible;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripTextBox tFind;
	}
}
