using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views.UserBrowser
{
	public class OpenRoleFromUserCommand : CommandBase<UserRole>
	{
		private readonly UserBrowserViewModel viewModel;

		public OpenRoleFromUserCommand(UserBrowserViewModel viewModel)
		{
			this.viewModel = viewModel;
		}


		protected override void ExecuteInternal(UserRole userRole)
		{
			if (userRole == null) return;

			var messenger = userRole.User.ExecutionContext.Messenger;
			var log = userRole.User.ExecutionContext.Log;

			var environment = this.viewModel.Environments.Find(e => e.GetAllUsers().FirstOrDefault(u => u.Id == userRole.User.Id) != null);

			var rootRoles = environment.GetAllRoles();

			if (TrySearchInRootRoles(log, messenger, rootRoles, userRole.Role.Id))
			{
				return;
			}





			// in this case we need to recourse the role hierarchy until we found that given role
			messenger.Send<Freeze>();
			messenger.Send(new WorkAsyncInfo
			{
				Message = "Reading role details...",
				Work = (worker, args) =>
				{
					var roleRef = userRole.Role;

					Entity role;
					using (log.Track("Recoursing role hierarchy"))
					{
						do
						{
							role = environment.Context.Retrieve(roleRef, "parentroleid");
							roleRef = role.GetAttributeValue<EntityReference>("parentroleid");
						}
						while (roleRef != null);
					}


					var rootRole = rootRoles.FirstOrDefault(r => r.Id == role.Id);
					if (rootRole == null)
						return;

					if (rootRole.Privileges.Count == 0)
					{
						rootRole.ReadPrivileges();
					}

					args.Result = rootRole;
				},
				PostWorkCallBack = e =>
				{
					messenger.Send<Unfreeze>();
					if (e.Error != null)
					{
						log.Error("Error retrieving role info: " + e.Error.Message, e.Error);
						return;
					}

					var role = e.Result as Role;

					if (role != null)
					{
						messenger.Send(new OpenRoleView(role));
					}

				}
			});

		}



		public bool TrySearchInRootRoles(ILog log, IMessenger messenger, IReadOnlyList<Role> rootRoles, Guid roleId)
		{
			var rootRole = rootRoles.FirstOrDefault(r => r.Id == roleId);
			if (rootRole == null)
			{
				return false;
			}


			// it means that the role is already loaded
			if (rootRole.Privileges.Count > 0)
			{
				messenger.Send(new OpenRoleView(rootRole));
				return true;
			}


			messenger.Send(new WorkAsyncInfo
			{
				Message = "Reading role details...",
				Work = (worker, args) =>
				{
					messenger.Send<Freeze>();
					rootRole.ReadPrivileges();
				},
				PostWorkCallBack = e =>
				{
					messenger.Send<Unfreeze>();
					if (e.Error != null)
					{
						log.Error("Error retrieving role info: " + e.Error.Message, e.Error);
						return;
					}

					messenger.Send(new OpenRoleView(rootRole));
				}
			});

			return true;
		}
	}
}
