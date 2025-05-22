using BrightIdeasSoftware;
using Greg.Xrm.Core.Views.Help;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.RoleEditor.Help;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.RoleBrowser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.RoleEditor.Views.UserBrowser
{
	public partial class UserBrowserView : DockContent
	{
		private readonly ILog log;
		private readonly IScopedMessenger messenger;
		private readonly UserBrowserViewModel viewModel;


		public UserBrowserView(ILog log, IMessenger messenger, IRoleRepository roleRepository)
		{
			this.log = log;
			this.messenger = messenger.CreateScope();
			this.viewModel = new UserBrowserViewModel(log, this.messenger, roleRepository);
			this.CloseButtonVisible = false;

			this.RegisterHelp(messenger, Topics.UserBrowser);

			InitializeComponent();

			this.Text = this.TabText = "User Browser";

			this.userTree.IsSimpleDragSource = true;
			this.userTree.IsSimpleDropSink = true;

			SimpleDropSink sink1 = (SimpleDropSink)this.userTree.DropSink;
			sink1.AcceptExternal = false;
			sink1.CanDropBetween = false;
			sink1.CanDropOnBackground = false;

			this.userTree.ModelCanDrop += HandleModelCanDrop;
			this.userTree.ModelDropped += HandleModelDropped;


			this.userTree.CanExpandGetter = x =>
			{
				if (x is DataverseEnvironment env)
				{
					return env.Count > 0;
				}
				if (x is BusinessUnit bu)
				{
					return bu.Children.Count > 0 || bu.Users.Count > 0;
				}
				if (x is UserGroup ug)
				{
					return ug.Count > 0;
				}
				if (x is SystemUser user)
				{
					return true;
				}

				return false;
			};
			this.userTree.ChildrenGetter = x =>
			{
				if (x is DataverseEnvironment env)
				{
					return env;
				}
				if (x is BusinessUnit bu)
				{
					var children = new List<object>();
					children.AddRange(bu.Children.OrderBy(u => u.name));

					var businessUsers = bu.Users.Where(u => !u.IsTechnicalUser).ToArray();
					if (businessUsers.Length > 0)
					{
						var businessGroup = new UserGroup("Business Users", false);
						businessGroup.AddRange(businessUsers.OrderBy(u => u.fullname));
						children.Add(businessGroup);
					}

					var technicalUsers = bu.Users.Where(u => u.IsTechnicalUser).ToArray();
					if (technicalUsers.Length > 0)
					{
						var techGroup = new UserGroup("Technical Users", false);
						techGroup.AddRange(technicalUsers.OrderBy(u => u.fullname));
						children.Add(techGroup);
					}

					return children;
				}
				if (x is UserGroup ug)
				{
					return ug;
				}

				if (x is SystemUser user)
				{
					if (!user.AreRolesLoaded)
					{
						this.viewModel.LoadRolesFor(user);
						return new[] { Loader.Instance };
					}

					if (user.Roles.Count == 0)
					{
						return new[] { NoRole.Instance };
					}
					return user.Roles;
				}

				return new object[0];
			};

			this.cName.AspectGetter = x =>
			{
				if (x is DataverseEnvironment env)
				{
					return env.name;
				}
				if (x is BusinessUnit bu)
				{
					return $"{bu.name} (Users: {bu.CountUsers()})";
				}
				if (x is UserGroup ug)
				{
					return $"{ug.Name} (Users: {ug.Count})";
				}
				if (x is SystemUser user)
				{
					return user.fullname;
				}
				if (x is UserRole role)
				{
					return role.Role.Name;
				}
				if (x is Loader)
				{
					return "Loading... please wait";
				}
				if (x is NoRole)
				{
					return "The current user has no roles";
				}

				return string.Empty;
			};
			this.cName.ImageGetter = x =>
			{
				if (x is DataverseEnvironment) return "env";
				if (x is BusinessUnit) return "businessunit";
				if (x is UserGroup) return "group";
				if (x is SystemUser) return "user";
				if (x is UserRole) return "role";
				if (x is Loader) return "loading";
				if (x is NoRole) return "role";

				return null;
			};

			this.cBusinessUnit.AspectGetter = x =>
			{
				if (x is SystemUser user)
				{
					return user.businessunitidFormatted;
				}
				if (x is UserRole role)
				{
					return role.BusinessUnit.Name;
				}

				return string.Empty;
			};



			this.viewModel.PropertyChanged += OnViewModelPropertyChanged;
			this.viewModel.RefreshUser += OnViewModelRefreshUser;
			this.viewModel.ChangeBusinessUnitCommand.OnMoveCompleted += (s, e) =>
			{
				this.userTree.RefreshObject(e.Environment);
			};

			this.tools.Bind(x => x.Enabled, viewModel, vm => vm.IsEnabled);
			this.userTree.Bind(x => x.Enabled, viewModel, vm => vm.IsEnabled);

			this.userTree.SelectedIndexChanged += (s, e) =>
			{
				this.viewModel.SelectedRole = this.userTree.SelectedObject as UserRole;
			};
			this.userTree.MouseDoubleClick += (s, e) =>
			{
				if (this.viewModel.SelectedRole == null) return;
				this.viewModel.OpenRoleCommand.Execute(this.viewModel.SelectedRole);
			};
			this.userTree.CellRightClick += (s, e) =>
			{
				var selectedEnvironment = this.userTree.SelectedObjects.OfType<DataverseEnvironment>().ToArray();
				this.tAddUserRoles.Visible = selectedEnvironment.Length == 1;
				this.tAddUserRoles.Tag = selectedEnvironment.FirstOrDefault();

				this.contextMenu.Show(this.userTree, e.Location);
			};
			this.tAddUserRoles.Click += (s, e) =>
			{
				var environment = this.tAddUserRoles.Tag as DataverseEnvironment;
				if (environment == null) return;
				this.viewModel.OpenAddUserRoleViewCommand.Execute(environment);
			};

			this.tSearchText.KeyUp += (s, e) =>
			{
				RefreshModelFilter();
				e.Handled = true;
			};

			this.FormClosed += (s, e) => this.messenger.Dispose();
		}

		private void HandleModelCanDrop(object sender, ModelDropEventArgs e)
		{
			e.Handled = true;
			e.Effect = DragDropEffects.None;

			var models = e.SourceModels.OfType<object>().ToArray();

			var systemUserList = models.OfType<SystemUser>().ToArray();

			if (models.Length != systemUserList.Length)
			{
				e.InfoMessage = "Only system users can be moved.";
				return;
			}
			var environmentList = this.viewModel.FindEnvironmentsByUsers(systemUserList);
			if (environmentList.Length != 1)
			{
				e.InfoMessage = "Please select users belonging to the same environment.";
				return;
			}

			var businessUnit = e.TargetModel as BusinessUnit;
			if (businessUnit == null)
			{
				e.InfoMessage = "Select the business unit where the user should be moved.";
				return;
			}

			var usersOnTheSameBu = systemUserList.Count(x => businessUnit.Users.Contains(x));
			if (usersOnTheSameBu > 0)
			{
				if (usersOnTheSameBu == 1)
				{
					e.InfoMessage = "Cannot assign an user to the business unit they belong to.";
				}
				else
				{
					e.InfoMessage = $"You are trying to move {usersOnTheSameBu} users onto the business unit they belong to. Peek another business unit.";
				}
				return;
			}

			e.Effect = DragDropEffects.Move;
		}


		private void HandleModelDropped(object sender, ModelDropEventArgs e)
		{
			if (e.DropTargetLocation != DropTargetLocation.Item) return;

			var systemUserList = e.SourceModels.OfType<SystemUser>().ToArray();
			var businessUnit = e.TargetModel as BusinessUnit;

			if (systemUserList.Length == 0 || businessUnit == null) return;
			var environmentList = this.viewModel.FindEnvironmentsByUsers(systemUserList);
			if (environmentList.Length == 0) return;
			if (environmentList.Length > 1)
			{
				MessageBox.Show("Please select users belonging to the same environment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var text = string.Empty;
			if (systemUserList.Length == 1)
			{
				text = systemUserList[0].fullname;
			}
			else
			{
				text = $"{systemUserList.Length} users";
			}


			var message = $"Do you really want to move {text} to business unit \"{businessUnit.name}\"?"
				+ Environment.NewLine
				+ Environment.NewLine
				+ "This operation will remove all the user roles, you will need to reassign them manually.";

			var result = MessageBox.Show(message, "Change Business Unit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result != DialogResult.Yes) return;

			this.viewModel.ChangeBusinessUnitCommand.Execute((environmentList[0], systemUserList, businessUnit));
		}





		private void OnViewModelRefreshUser(object sender, RefreshUserEventArgs e)
		{
			try
			{
				this.userTree.RefreshObject(e.User);
				//this.userTree.Expand(e.User);

			}
			catch
			{
			}
		}

		private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(this.viewModel.Environments))
			{
				this.userTree.Roots = this.viewModel.Environments;

				foreach (var item in this.viewModel.Environments)
				{
					this.userTree.Expand(item);
				}
			}
			if (e.PropertyName == nameof(RoleBrowserViewModel.IsEnabled))
			{
				this.tSearchText.Enabled = this.viewModel.IsEnabled;
			}
		}

		private void RefreshModelFilter()
		{
			var searchText = this.tSearchText.Text?.ToLowerInvariant();

			var modelFilter = new ModelFilter(model =>
			{
				if (string.IsNullOrWhiteSpace(searchText)) return true;

				if (model is DataverseEnvironment) return true;
				if (model is UserGroup ug) return ug.Match(searchText);
				if (model is UserRole) return true;
				if (model is BusinessUnit businessUnit) return businessUnit.CountUsers(searchText) > 0;
				if (!(model is SystemUser user)) return true;
				if (user.Match(searchText)) return true;

				return false;
			});

			this.userTree.ModelFilter = modelFilter;
		}
	}
}
