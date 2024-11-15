using BrightIdeasSoftware;
using System.Collections.Generic;
using System.Drawing;

namespace Greg.Xrm.RoleEditor.Model
{
	public class TreeNode : List<TreeNode>
	{
		private readonly string name;

		public TreeNode(string name, string tooltip = null, string icon = null, IEnumerable<TreeNode> children = null)	
		{
			this.name = name;
			this.Tooltip = tooltip;
			this.Icon = icon;

			if (children != null)
				this.AddRange(children);
		}



		public string Text
		{
			get
			{
				return this.name + (this.Count > 0 ? $" ({this.Count})" : string.Empty);
			}
		}

		public string Tooltip { get; set; }

		public string Icon { get; set; }


		public static void Bind(TreeListView tree)
		{
			tree.CanExpandGetter = x => (x as TreeNode)?.Count > 0;
			tree.ChildrenGetter = x => x as TreeNode;
			tree.CellToolTipShowing += (s, e) =>
			{
				if (!(e.Model is TreeNode node)) return;
				e.Text = node.Tooltip;
			};
			tree.UseCellFormatEvents = true;
			tree.FormatCell += (s, e) =>
			{
				var model = e.Model as TreeNode;
				if (model == null) return;

                if ( model.Count > 0)
                {
					e.SubItem.ForeColor = Color.FromArgb(2, 53, 158);
                }
            };


			var column = tree.Columns[0] as OLVColumn;
			if (column != null)
			{
				column.AspectName = "Text";
				column.ImageGetter = x => (x as TreeNode)?.Icon;
			}
		}
	}
}
