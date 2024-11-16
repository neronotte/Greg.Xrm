using Greg.Xrm.RoleEditor.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Greg.Xrm.RoleEditor.Views.Browser
{
	public partial class SearchResultView : UserControl
	{
		private Role selectedRole;

		public SearchResultView()
		{
			InitializeComponent();
			this.tBack.Click += (s, e) => this.BackClicked?.Invoke(this, EventArgs.Empty);

			this.cRoleName.ImageGetter = x => "role";
			this.cBusinessUnit.ImageGetter = x => "bu";


			this.roleList.UseCellFormatEvents = true;
			this.roleList.SelectedIndexChanged += (s, e) =>
			{
				this.selectedRole = this.roleList.SelectedObject as Role;
			};
			this.roleList.MouseDoubleClick += (s, e) =>
			{
				if (this.selectedRole == null) return;
				this.RoleDoubleClicked?.Invoke(this, new RoleEventArgs(this.selectedRole));
			};
			this.roleList.FormatRow += (s, e) =>
			{
				if (!(e.Model is Role role)) return;

				if (!role.iscustomizable)
				{
					e.Item.ForeColor = Color.Gray;
				}
			};
			this.roleList.CellRightClick += (s, e) =>
			{
				if (e.Model == null) return;
				this.roleList.SelectedObject = e.Model;
				this.selectedRole = e.Model as Role;
				if (this.selectedRole != null)
				{
					this.contextMenu.Show(this.roleList, e.Location);
				}
			};

			this.tCloneSelected.Click += (s, e) =>
			{
				if (this.selectedRole == null) return;
				this.CloneSelectedRoleClicked?.Invoke(this, new RoleEventArgs(this.selectedRole));
			};
		}


		public string SearchDescription
		{
			get => this.tSearchDescription.Text;
			set => this.tSearchDescription.Text = value;
		}

		public Role[] Roles
		{
			get => this.roleList.Objects as Role[];
			set => this.roleList.Objects = value;
		}

		public Role[] SelectedRoles
		{
			get => (this.roleList.SelectedObjects as Role[]) ?? new Role[0];
		}

		public event EventHandler SelectedRolesChanged
		{
			add => this.roleList.SelectionChanged += value;
			remove => this.roleList.SelectionChanged -= value;
		}

		public event EventHandler BackClicked;

		public event EventHandler<RoleEventArgs> RoleDoubleClicked;

		public event EventHandler<RoleEventArgs> CloneSelectedRoleClicked;
	}
}
