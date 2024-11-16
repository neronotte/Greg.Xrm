using Greg.Xrm.Core.Views;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using XrmToolBox.Extensibility;
using System;
using System.Linq;
using Microsoft.Crm.Sdk.Messages;
using XrmToolBox.Extensibility.Args;

namespace Greg.Xrm.RoleEditor.Views.BulkEditor
{
	public class SaveCommand : CommandBase
	{
		private readonly BulkEditorViewModel viewModel;

		public SaveCommand(BulkEditorViewModel viewModel)
        {
			this.viewModel = viewModel;
		}

		public event EventHandler SaveCompleted;

		protected override void ExecuteInternal(object arg)
		{
			var log = this.viewModel.Log;
			var messenger = this.viewModel.Messenger;


			var changes = this.viewModel.GetChangeSummary();
			if (!changes.HasAnyPrivilegeChange())
			{
				this.viewModel.SendNotification(NotificationType.Error, "Nothing changed on the selected roles.");
				return;
			}

			var changedRoleCount = changes.GetChangedRoleCount();
			var plural = changedRoleCount > 1 ? "s" : string.Empty;

			messenger.Send(new WorkAsyncInfo
			{
				Message = $"Saving changes to {changedRoleCount}/{this.viewModel.RoleCount} role{plural}, please wait...",
				Work = (worker, e) =>
				{
					messenger.Send<Freeze>();

					var requestList = changes.CreateRequests();

					var i = 0;
					foreach (var kvp in requestList)
					{
						var role = kvp.Key;
						var requests = kvp.Value;

						using (role.ExecutionContext.Log.Track($"Saving changes to role{role.name}"))
						{
							i++;



							var addRequestList = requests.OfType<AddPrivilegesRoleRequest>().ToList();
							var removeRequestList = requests.OfType<RemovePrivilegeRoleRequest>().Pool(10).ToList();
							var replaceRequestList = requests.OfType<ReplacePrivilegesRoleRequest>().ToList();

							var totalRequestCount = addRequestList.Count + removeRequestList.Count + replaceRequestList.Count;

							var j = 0;
							foreach (var request in addRequestList)
							{
								j++;
								messenger.Send(new SetWorkingMessage($"Saving changes to {role.name} ({i}/{this.viewModel.RoleCount}) in blocks.{Environment.NewLine}Processing block {j}/{totalRequestCount}.{Environment.NewLine}Please wait..."));
								messenger.Send(new StatusBarMessageEventArgs((j * 100) / totalRequestCount));
								role.ExecutionContext.Execute(request);
							}

							foreach (var requestBlock in removeRequestList)
							{
								j++;
								messenger.Send(new SetWorkingMessage($"Saving changes to {role.name} ({i}/{this.viewModel.RoleCount}) in blocks.{Environment.NewLine}Processing block {j}/{totalRequestCount}.{Environment.NewLine}Please wait..."));
								messenger.Send(new StatusBarMessageEventArgs((j * 100) / totalRequestCount));
								var request = new ExecuteTransactionRequest
								{
									ReturnResponses = false,
									Requests = new OrganizationRequestCollection()
								};
								request.Requests.AddRange(requestBlock);

								role.ExecutionContext.Execute(request);
							}

							foreach (var request in replaceRequestList)
							{
								j++;
								messenger.Send(new SetWorkingMessage($"Saving changes to {role.name} ({i}/{this.viewModel.RoleCount}) in blocks.{Environment.NewLine}Processing block {j}/{totalRequestCount}.{Environment.NewLine}Please wait..."));
								messenger.Send(new StatusBarMessageEventArgs((j * 100) / totalRequestCount));
								role.ExecutionContext.Execute(request);
							}

							

							role.ExecutionContext.Log.Info($"Role {role.name} saved successfully.");
						}

						role.ReadPrivileges();
					}
				},
				PostWorkCallBack = e =>
				{
					messenger.Send<Unfreeze>();

					if (e.Error != null)
					{
						log.Error("Error while trying to save the role: " + e.Error.Message, e.Error);
						this.viewModel.SendNotification(NotificationType.Error, "Error saving roles, see Output for details: " + e.Error.Message);
						return;
					}

					this.viewModel.SendNotification(NotificationType.Success, "Roles saved successfully!");
					this.SaveCompleted?.Invoke(this, EventArgs.Empty);
				}
			});
		}
	}
}
