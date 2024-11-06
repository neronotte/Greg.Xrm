using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Views.Editor
{
	public class TableGroupModel : List<TableModel>
	{
        public TableGroupModel(string name = null)
        {
            this.Name = name;
        }


		public string Name { get; set; }

		public bool IsDirty => this.Any(x => x.IsDirty);

		public new void Add(TableModel table)
		{
			base.Add(table);
			table.Parent = this;
		}
	}
}
