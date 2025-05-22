using Greg.Xrm.Core.Views;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using Microsoft.Crm.Sdk.Messages;
using System;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views.UserBrowser
{
	public class ChangeBusinessUnitCommand : CommandBase<(DataverseEnvironment, SystemUser[], BusinessUnit)>
	{
		private readonly UserBrowserViewModel viewModel;
		private readonly IRoleRepository roleRepository;

		public ChangeBusinessUnitCommand(UserBrowserViewModel viewModel, IRoleRepository roleRepository)
		{
			this.viewModel = viewModel;
			this.roleRepository = roleRepository;
		}

		protected override void ExecuteInternal((DataverseEnvironment, SystemUser[], BusinessUnit) arg)
		{
			var env = arg.Item1;
			var systemUserList = arg.Item2;
			var businessUnit = arg.Item3;

			if (systemUserList.Length == 0) return;
			if (businessUnit == null) return;

			var messenger = systemUserList[0].ExecutionContext.Messenger;
			var log = systemUserList[0].ExecutionContext.Log;
			messenger.Send<Freeze>();

			messenger.Send(new WorkAsyncInfo
			{
				Message = "Assigning user(s) to business unit...",
				Work = (worker, args) =>
				{
					foreach (var user in systemUserList)
					{
						messenger.Send(new SetWorkingMessage($"Assigning {user.fullname} to business unit {businessUnit.name}..."));

						var request = new SetBusinessSystemUserRequest
						{
							BusinessId = businessUnit.Id,
							DoNotMoveAllRecords = true,
							UserId = user.Id,
							ReassignPrincipal = user.ToEntityReference()
						};

						user.ExecutionContext.Execute(request);
						user.Reload(this.roleRepository);

						env.RemoveUser(user);
						env.AddUser(user);
					}
				},
				PostWorkCallBack = (e) =>
				{
					messenger.Send<Unfreeze>();

					if (e.Error != null)
					{
						log.Error("Error assigning user(s) to business unit: " + e.Error.Message, e.Error);
						return;
					}

					this.OnMoveCompleted?.Invoke(this, new ChangeBusinessUnitCompletedEventArgs(env, systemUserList, businessUnit));
				},
			});
		}


		public event EventHandler<ChangeBusinessUnitCompletedEventArgs> OnMoveCompleted;
	}

	public class ChangeBusinessUnitCompletedEventArgs : EventArgs
	{
		public ChangeBusinessUnitCompletedEventArgs()
		{
		}

		public ChangeBusinessUnitCompletedEventArgs(DataverseEnvironment environment, SystemUser[] systemUsers, BusinessUnit businessUnit)
		{
			Environment = environment;
			SystemUsers = systemUsers;
			BusinessUnit = businessUnit;
		}

		public DataverseEnvironment Environment { get; }
		public SystemUser[] SystemUsers { get; }
		public BusinessUnit BusinessUnit { get; }
	}
}
