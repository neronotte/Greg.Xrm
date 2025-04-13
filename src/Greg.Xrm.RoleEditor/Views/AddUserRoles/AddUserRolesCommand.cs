using Greg.Xrm.Core.Messaging;
using Greg.Xrm.Core.Views;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.RoleEditor.Views.UserBrowser;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views.AddUserRoles
{
	public class AddUserRolesCommand : CommandBase
	{
		private readonly UserRolesViewModel viewModel;
		private readonly IRoleRepository roleRepository;

		public AddUserRolesCommand(
			UserRolesViewModel viewModel,
			IRoleRepository roleRepository)
		{
			this.viewModel = viewModel;
			this.roleRepository = roleRepository;
		}

		protected override void ExecuteInternal(object arg)
		{
			if (viewModel.Users.Count == 0 || viewModel.Roles.Count == 0) return;

			var context = viewModel.Environment.Context;
			var messenger = viewModel.Environment.Context.Messenger;
			var log = viewModel.Environment.Context.Log;

			messenger.Freeze();
			messenger.Send(new WorkAsyncInfo
			{
				Message = "Adding roles to users, please wait...",
				Work = (w, e) =>
				{
					var users = viewModel.GetUsers();

					var roles = GetRoles();

					foreach (var user in users)
					{
						messenger.Send(new SetWorkingMessage($"Adding roles to user {user.fullname}..."));
						context.Associate(
							"systemuser",
							user.Id,
							new Relationship("systemuserroles"),
							new EntityReferenceCollection(roles.Select(x => x.ToEntityReference()).ToList()));

						user.LoadRoles(this.roleRepository);
						messenger.Send(new RefreshUserRequest(user));
					}

					messenger.Send(new SetWorkingMessage($"Operation completed!"));
				},
				PostWorkCallBack =(e) =>
				{
					messenger.Unfreeze();

					if (e.Error != null)
					{
						log.Error("Error while adding roles to users", e.Error);
						return;
					}

					MessageBox.Show("Operation completed!");

					messenger.Send(new UserRolesViewClosed(this.viewModel.Environment));
				}
			});
		}

		private List<Role> GetRoles()
		{
			var messenger = this.viewModel.Environment.Context.Messenger;

			var tuplesToFetch = new List<Tuple<string, EntityReference>>();
			var roleList = new List<Role>();
			foreach (var roleNode in viewModel.Roles)
			{
				if (roleNode.RequiresFetch)
				{
					tuplesToFetch.Add(Tuple.Create(roleNode.Name, roleNode.CurrentBusinessUnit));
				}
				else
				{
					roleList.Add(roleNode.GetRole());
				}
			}

			if (tuplesToFetch.Count > 0)
			{
				messenger.Send(new SetWorkingMessage("Retrieving missing roles..."));
				var roles = this.roleRepository.GetRolesByNamesAndBusinessUnit(
					this.viewModel.Environment.Context,
					tuplesToFetch,
					this.viewModel.Environment.Template);
				foreach (var role in roles)
				{
					roleList.Add(role);
				}
			}
			return roleList;
		}
	}
}
