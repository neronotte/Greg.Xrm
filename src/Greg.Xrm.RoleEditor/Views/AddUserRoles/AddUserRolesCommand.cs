using Greg.Xrm.Core.Views;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.RoleEditor.Views.UserBrowser;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
			if (viewModel.Users.Count == 0)
			{
				MessageBox.Show(
					"Please select at least one user.",
					"Assign roles to users",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);

				return;
			}

			if (viewModel.Roles.Count == 0)
			{
				MessageBox.Show(
					"Please select at least one role.",
					"Assign roles to users",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);

				return;
			}


			var duplicateRoles = viewModel.Roles
				.GroupBy(x => x.Name + x.CurrentBusinessUnit.Id)
				.Where(x => x.Count() > 1)
				.Select(x => x.First())
				.ToList();

			if (duplicateRoles.Count > 0)
			{
				var plural = duplicateRoles.Count > 1 ? "roles" : "role";

				var sb = new StringBuilder();
				sb.Append("Please remove the following duplicate ").Append(plural).AppendLine(":");

				foreach (var role in duplicateRoles)
				{
					sb.Append("- ").Append(role.Name).Append(" (").Append(role.CurrentBusinessUnit.Name).AppendLine(")");
				}

				MessageBox.Show(
					sb.ToString(),
					"Assign roles to users",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);

				return;
			}





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

					var relationship = new Relationship("systemuserroles_association");

					foreach (var user in users)
					{
						messenger.Send(new SetWorkingMessage($"Adding roles to user {user.fullname}..."));

						// where I am here, the roles of the user may not have been loaded yet
						// if not loaded, I force the loading to avoid duplicate assignments
						if (!user.AreRolesLoaded)
						{
							user.LoadRoles(this.roleRepository);
						}

						var rolesToAdd = roles
							.Where(x => !user.Roles.Any(y => y.Role.Id == x.Id))
							.ToList();

						if (rolesToAdd.Count == 0)
						{
							log.Warn($"User {user.fullname} already has all selected roles.");
							continue;
						}

						var entityReferences = new EntityReferenceCollection(rolesToAdd
							.Select(x => x.ToEntityReference())
							.ToList());

						context.Associate(
							"systemuser",
							user.Id,
							relationship,
							entityReferences);

						user.LoadRoles(this.roleRepository);
						messenger.Send(new RefreshUserRequest(user));

						var roleNames = rolesToAdd.Select(x => x.name + " (" + x.businessunitidFormatted + ")").OrderBy(x => x).Join(", ");
						log.Info($"{entityReferences.Count} role(s) added to user {user.fullname}: {roleNames}");
					}

					messenger.Send(new SetWorkingMessage($"Operation completed!"));
				},
				PostWorkCallBack = (e) =>
				{
					messenger.Unfreeze();

					if (e.Error != null)
					{
						MessageBox.Show("Error while adding roles to users! See output window for more details.", "Assign roles to users", MessageBoxButtons.OK, MessageBoxIcon.Error);

						log.Error("Error while adding roles to users: " + e.Error.Message, e.Error);
						return;
					}

					MessageBox.Show("Operation completed! See output window for more details.", "Assign roles to users", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
