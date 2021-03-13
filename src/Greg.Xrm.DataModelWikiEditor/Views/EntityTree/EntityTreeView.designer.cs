using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greg.Xrm.DataModelWikiEditor.Views.EntityTree
{
	public partial class EntityTreeView
	{
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntityTreeView));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tree = new System.Windows.Forms.TreeView();
			this.tLoadEntities = new Greg.Xrm.Views.ToolStripBindableButton();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tLoadEntities});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(400, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tree
			// 
			this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tree.Location = new System.Drawing.Point(0, 25);
			this.tree.Name = "tree";
			this.tree.Size = new System.Drawing.Size(400, 671);
			this.tree.TabIndex = 1;
			this.tree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.OnBeforeExpand);
			// 
			// tLoadEntities
			// 
			this.tLoadEntities.Image = ((System.Drawing.Image)(resources.GetObject("tLoadEntities.Image")));
			this.tLoadEntities.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tLoadEntities.Name = "tLoadEntities";
			this.tLoadEntities.Size = new System.Drawing.Size(94, 22);
			this.tLoadEntities.Text = "Load entities";
			// 
			// EntityTreeView
			// 
			this.ClientSize = new System.Drawing.Size(400, 696);
			this.Controls.Add(this.tree);
			this.Controls.Add(this.toolStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "EntityTreeView";
			this.TabText = "Entity tree";
			this.Text = "Entity tree";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.TreeView tree;
		private Greg.Xrm.Views.ToolStripBindableButton tLoadEntities;
	}
}
