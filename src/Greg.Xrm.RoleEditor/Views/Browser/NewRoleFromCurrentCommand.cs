using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views.Browser
{
	public class NewRoleFromCurrentCommand : CommandBase<Role>
	{

		public NewRoleFromCurrentCommand()
		{
			this.CanExecute = false;

			this.WhenChanges(() => this.Role)
				.Execute(_ => this.CanExecute = this.Role != null);
		}


		public Role Role
		{
			get => Get<Role>();
			set => Set(value);
		}


		protected override void ExecuteInternal(Role arg)
		{
			if (this.Role == null) return;

			var crm = this.Role.ExecutionContext;
			var messenger = crm.Messenger;
			var log = crm.Log;

			var newRole = new Role(this.Role.ExecutionContext, this.Role.Template)
			{
				name = RoleName.GetNewName(),
				isinherited = new OptionSetValue(1),
			};

			if (this.Role.Privileges.Count > 0)
			{
				newRole.ClonePrivilegesFrom(this.Role);
				messenger.Send(new OpenRoleView(newRole));
				return;
			}

			messenger.Send(new WorkAsyncInfo
			{
				Message = "Reading role details...",
				Work = (worker, args) =>
				{
					messenger.Send<Freeze>();

					this.Role.ReadPrivileges();
				},
				PostWorkCallBack = e =>
				{
					messenger.Send<Unfreeze>();
					if (e.Error != null)
					{
						log.Error("Error retrieving role info: " + e.Error.Message, e.Error);
						return;
					}

					newRole.ClonePrivilegesFrom(this.Role);
					messenger.Send(new OpenRoleView(newRole));
				}
			});
		}
	}
}
