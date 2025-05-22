using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.RoleEditor.Views.RoleBrowser;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk;
using System;
using System.Linq;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views.Browser
{
	public class NewRoleFromExistingCommand : CommandBase
	{
		private readonly RoleBrowserViewModel viewModel;
		private readonly string existingRoleName;

		public NewRoleFromExistingCommand(RoleBrowserViewModel viewModel, string existingRoleName)
		{
			this.viewModel = viewModel;
			this.existingRoleName = existingRoleName;
			this.GetRole();

			viewModel.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName != nameof(RoleBrowserViewModel.Environments))
					return;

				GetRole();
			};

			this.WhenChanges(() => this.SelectedRole)
				.Execute(_ => GetRole());

			this.CanExecute = false;
		}

		private void GetRole()
		{
			if (this.SelectedRole == null)
			{
				this.CanExecute = false;
				return;
			}

			if (viewModel.Environments.Count == 0)
			{
				this.CanExecute = false;
				return;
			}

			// should find the environment that contains the selected role

			var environment = viewModel.Environments.Find(e => e.Contains(SelectedRole));
			if (environment == null)
			{
				this.CanExecute = false;
				return;
			}

			this.RoleToCopy = environment.GetAllRoles().FirstOrDefault(r => string.Equals(r.name, this.existingRoleName, StringComparison.OrdinalIgnoreCase));
			this.CanExecute = this.RoleToCopy != null;
		}

		public Role RoleToCopy { get; private set; }

		public Role SelectedRole
		{
			get => Get<Role>();
			set => Set(value);
		}



		protected override void ExecuteInternal(object arg)
		{
			if (this.RoleToCopy == null) return;

			var context = this.RoleToCopy.ExecutionContext;
			var log = context.Log;
			var messenger = context.Messenger;

			var newRole = new Role(context, this.RoleToCopy.Template)
			{
				name = RoleName.GetNewName(),
				isinherited = new OptionSetValue(1),
			};

			if (this.RoleToCopy.Privileges.Count > 0)
			{
				newRole.ClonePrivilegesFrom(this.RoleToCopy);
				messenger.Send(new OpenRoleView(newRole));
				return;
			}

			messenger.Send(new WorkAsyncInfo
			{
				Message = "Reading role details...",
				Work = (worker, args) =>
				{
					messenger.Send<Freeze>();

					this.RoleToCopy.ReadPrivileges();
				},
				PostWorkCallBack = e =>
				{
					messenger.Send<Unfreeze>();
					if (e.Error != null)
					{
						log.Error("Error retrieving role info: " + e.Error.Message, e.Error);
						return;
					}

					newRole.ClonePrivilegesFrom(this.RoleToCopy);
					messenger.Send(new OpenRoleView(newRole));
				}
			});
		}
	}
}
