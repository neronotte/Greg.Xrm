using Greg.Xrm.Logging;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.UserBrowser;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk;
using System.Windows.Forms;
using Windows.System;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views.AddUserRoles
{
	public class RemoveUserRolesCommand : CommandBase<UserRole>
	{
        private readonly IRoleRepository roleRepository;

        public RemoveUserRolesCommand(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        protected override void ExecuteInternal(UserRole ur)
		{
			var confirm = MessageBox.Show($"Do you really want to remove role {ur.Role.Name} (BU: {ur.BusinessUnit.Name}) from user {ur.User.fullname}?", "Remove role from user", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (confirm != DialogResult.Yes) return;

			var context = ur.User.ExecutionContext;
			var messenger = ur.User.ExecutionContext.Messenger;
			var log = ur.User.ExecutionContext.Log;

			messenger.Freeze();
			messenger.Send(new WorkAsyncInfo
			{
				Message = $"Removing role {ur.Role.Name} (BU: {ur.BusinessUnit.Name}) from user {ur.User.fullname}, please wait ...",
				Work = (w, e) =>
				{
					var relationship = new Relationship("systemuserroles_association");

					var entityReferences = new EntityReferenceCollection
                    {
                        ur.Role
                    };

					context.Disassociate(
						"systemuser",
						ur.User.Id,
						relationship,
						entityReferences);


					ur.User.LoadRoles(this.roleRepository);
					messenger.Send(new RefreshUserRequest(ur.User));

					log.Info($"{entityReferences.Count} role(s) removed from user {ur.User.fullname}");
				},
				PostWorkCallBack = (e) =>
				{
					messenger.Unfreeze();

					if (e.Error != null)
					{
						MessageBox.Show("Error while removing role! See output window for more details.", "Remove role from user", MessageBoxButtons.OK, MessageBoxIcon.Error);

						log.Error("Error while adding roles to users: " + e.Error.Message, e.Error);
						return;
					}

					MessageBox.Show("Operation completed! See output window for more details.", "Remove role from user", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			});
		}
	}
}
