using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Views.Editor
{
	public class MiscGroupModel : List<MiscModel>
	{

		public MiscGroupModel(string name = null)
		{
			this.Name = name;
		}

		public string Name { get; set; }

		public bool IsDirty => this.Any(x => x.IsChanged);


		public new void Add(MiscModel model)
		{
			base.Add(model);
			model.Parent = this;
		}
	}
}
