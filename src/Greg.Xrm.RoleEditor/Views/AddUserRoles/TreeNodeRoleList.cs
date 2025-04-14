namespace Greg.Xrm.RoleEditor.Views.AddUserRoles
{
	public class TreeNodeRoleList : TreeNodeList<TreeNodeRole>
	{
		private readonly UserRolesViewModel viewModel;

		public TreeNodeRoleList(UserRolesViewModel viewModel)
		{
			this.Name = "Selected Roles";
			this.viewModel = viewModel;
		}

		public override void Add(TreeNodeRole item)
		{
			if (!viewModel.IsRecordOwnershipAcrossBusinessUnitEnabled && this.Contains(item))
			{
				return;
			}

			base.Add(item);
		}
	}
}
