using BrightIdeasSoftware;
using Greg.Xrm.Core.Views.Help;
using Greg.Xrm.RoleEditor.Help;
using Greg.Xrm.RoleEditor.Model;
using Microsoft.Xrm.Sdk;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.RoleEditor.Views.AddUserRoles
{
	public partial class UserRolesView : DockContent
	{
		private readonly UserRolesViewModel viewModel;
		private readonly DataverseEnvironment environment;

		public UserRolesView(DataverseEnvironment environment, IRoleRepository roleRepository)
		{
			InitializeComponent();

			this.RegisterHelp(environment.Context.Messenger, Topics.AddUserRoles);

			this.viewModel = new UserRolesViewModel(environment, roleRepository);
			this.Text = this.TabText = $"Assign roles to users ({this.viewModel.EnvironmentName})";

			this.Load += (s, e) => this.viewModel.Initialize();
			this.FormClosed += (s, e) => this.viewModel.Terminate();

			this.cName.ImageGetter = x =>
			{
				if (x is TreeNodeUser) return "user";
				if (x is TreeNodeUserList) return "group";
				if (x is TreeNodeRole) return "role";
				if (x is TreeNodeRoleList) return "rolelist";
				return null;
			};
			this.treeListView1.ChildrenGetter = x =>
			{
				if (x is IEnumerable list) return list;
				return null;
			};
			this.treeListView1.CanExpandGetter = x =>
			{
				if (x is TreeNodeUserList userList) return userList.Count > 0;
				if (x is TreeNodeRoleList roleList) return roleList.Count > 0;
				return false;
			};

			this.treeListView1.FullRowSelect = false;
			this.treeListView1.IsSimpleDropSink = true;
			this.treeListView1.ModelCanDrop += OnModelCanDrop;
			this.treeListView1.ModelDropped += OnUserListModelDropped;
			this.treeListView1.CellEditActivation = TreeListView.CellEditActivateMode.SingleClick;
			this.treeListView1.CellEditStarting += OnBusinessUnitEditStarting;
			this.treeListView1.CellEditFinishing += OnBusinessUnitEditFinishing;
			this.treeListView1.AddObject(this.viewModel.Users);
			this.treeListView1.AddObject(this.viewModel.Roles);
			this.treeListView1.ExpandAll();

			this.treeListView1.KeyDown += HandlePaste;

			this.environment = environment;

			this.viewModel.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(this.viewModel.IsRecordOwnershipAcrossBusinessUnitEnabled))
				{
					this.cBusinessUnit.IsEditable = this.viewModel.IsRecordOwnershipAcrossBusinessUnitEnabled;
					this.tMatrixVisibility.Visible = this.viewModel.IsRecordOwnershipAcrossBusinessUnitEnabled;
				}
			};

			this.tApply.BindCommand(() => this.viewModel.ApplyCommand);
		}

		private void OnBusinessUnitEditStarting(object sender, CellEditEventArgs e)
		{
			if (!(e.ListViewItem.RowObject is TreeNodeRole role))
			{
				e.Cancel = true;
				return;
			}

			if (e.Column == this.cBusinessUnit)
			{
				var control = new ComboBox();
				control.DropDownStyle = ComboBoxStyle.DropDownList;
				control.Items.AddRange(role.ValidBusinessUnits.ToArray());
				control.SelectedItem = role.CurrentBusinessUnit;
				control.DisplayMember = "Name";
				control.Bounds = e.CellBounds;
				e.Control = control;
				e.Cancel = false;
				return;
			}

			e.Cancel = true;
		}

		private void OnBusinessUnitEditFinishing(object sender, CellEditEventArgs e)
		{
			var control = (ComboBox)e.Control;
			var currentValue = (EntityReference)control.SelectedItem;

			var role = (TreeNodeRole)e.RowObject;
			role.CurrentBusinessUnit = currentValue;
			this.treeListView1.RefreshObject(role);

			e.Cancel = true;
		}

		private void OnModelCanDrop(object sender, ModelDropEventArgs e)
		{
			var userList = e.SourceModels.OfType<SystemUser>().ToArray();
			var roleList = e.SourceModels.OfType<Role>().ToArray();
			if (userList.Length == 0 && roleList.Length == 0)
			{
				e.Effect = DragDropEffects.None;
				e.InfoMessage = "You can drop here users from the User Browser panel," + System.Environment.NewLine + "or roles from the Role Browser panel.";
				return;
			}

			if (userList.Length > 0)
			{
				OnUserListModelCanDrop(userList, e);
			}
			else
			{
				OnRoleListModelCanDrop(roleList, e);
			}

		}

		private void OnUserListModelCanDrop(SystemUser[] userList, ModelDropEventArgs e)
		{
			var contextList = userList.Select(x => x.ExecutionContext).Distinct().ToArray();
			if (contextList.Length > 1)
			{
				e.Effect = DragDropEffects.None;
				e.InfoMessage = "You can drop here users from one single environment.";
				return;
			}

			var context = contextList[0];
			if (!this.viewModel.ContextIs(context))
			{
				e.Effect = DragDropEffects.None;
				e.InfoMessage = $"You can drop here only users belonging to {this.viewModel.EnvironmentName}.";
				return;
			}

			e.Effect = DragDropEffects.Copy;
		}

		private void OnRoleListModelCanDrop(Role[] roleList, ModelDropEventArgs e)
		{
			var contextList = roleList.Select(x => x.ExecutionContext).Distinct().ToArray();
			if (contextList.Length > 1)
			{
				e.Effect = DragDropEffects.None;
				e.InfoMessage = "You can drop here roles from one single environment.";
				return;
			}

			var context = contextList[0];
			if (!this.viewModel.ContextIs(context))
			{
				e.Effect = DragDropEffects.None;
				e.InfoMessage = $"You can drop here only roles belonging to {this.viewModel.EnvironmentName}.";
				return;
			}

			e.Effect = DragDropEffects.Copy;
		}

		private void OnUserListModelDropped(object sender, ModelDropEventArgs e)
		{
			var userList = e.SourceModels.OfType<SystemUser>().ToArray();
			var roleList = e.SourceModels.OfType<Role>().ToArray();

			if (userList.Length > 0)
			{
				this.viewModel.Users.AddRange(userList
					.Select(x => new TreeNodeUser(x))
					.OrderBy(x => x.Name));
				this.treeListView1.RefreshObject(this.viewModel.Users);
				this.treeListView1.Expand(this.viewModel.Users);
			}

			if (roleList.Length > 0)
			{
				this.viewModel.Roles.AddRange(roleList
					.Select(x => new TreeNodeRole(this.environment, x, this.viewModel.IsRecordOwnershipAcrossBusinessUnitEnabled))
					.OrderBy(x => x.Name)
					.ToList());
				this.treeListView1.RefreshObject(this.viewModel.Roles);
				this.treeListView1.Expand(this.viewModel.Roles);
			}
		}



		private void HandlePaste(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode.Equals(Keys.V))
			{
				var text = Clipboard.GetText();
				var initialUserCount = this.viewModel.Users.Count;
				var initialRoleCount = this.viewModel.Roles.Count;

				if (this.viewModel.PasteFrom(text))
				{
					this.treeListView1.RefreshObject(this.viewModel.Users);
					this.treeListView1.RefreshObject(this.viewModel.Roles);

					if (this.viewModel.Users.Count != initialUserCount)
					{
						this.treeListView1.Expand(this.viewModel.Users);
					}
					if (this.viewModel.Roles.Count != initialRoleCount)
					{
						this.treeListView1.Expand(this.viewModel.Roles);
					}
				}
				e.Handled = true;
				e.SuppressKeyPress = true;
				return;
			}

			if (!e.Control && !e.Shift && !e.Alt && e.KeyCode.Equals(Keys.Delete))
			{
				var selectedUsers = this.treeListView1.SelectedObjects.OfType<TreeNodeUser>().ToArray();
				var selectedRoles = this.treeListView1.SelectedObjects.OfType<TreeNodeRole>().ToArray();
				if (selectedUsers.Length == 0 && selectedRoles.Length == 0) return;

				e.Handled = true;
				e.SuppressKeyPress = true;
				var confirm = MessageBox.Show("Are you sure you want to remove the selected users and roles?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (confirm != DialogResult.Yes) return;

				this.viewModel.Users.RemoveAll(selectedUsers);
				this.viewModel.Roles.RemoveAll(selectedRoles);
				this.treeListView1.RefreshObject(this.viewModel.Users);
				this.treeListView1.RefreshObject(this.viewModel.Roles);
			}
		}
	}
}
