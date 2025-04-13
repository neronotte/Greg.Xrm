namespace Greg.Xrm.RoleEditor.Views.AddUserRoles
{
	public class TreeNodeUserList : TreeNodeList<TreeNodeUser>
	{
		public TreeNodeUserList()
		{
			this.Name = "Selected Users";
		}

		public override void Add(TreeNodeUser item)
		{
			if (this.Contains(item)) return;
			base.Add(item);
		}
	}
}
