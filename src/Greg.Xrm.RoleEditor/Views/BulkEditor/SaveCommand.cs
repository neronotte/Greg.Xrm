using Greg.Xrm.Core.Views;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using XrmToolBox.Extensibility;
using System;

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
							messenger.Send(new SetWorkingMessage($"Saving changes to {role.name} ({i}/{this.viewModel.RoleCount}), please wait..."));


							var request = new ExecuteTransactionRequest
							{
								ReturnResponses = true,
								Requests = new OrganizationRequestCollection()
							};
							request.Requests.AddRange(requests);

							var response = (ExecuteTransactionResponse)role.ExecutionContext.Execute(request);

							role.ExecutionContext.Log.Info($"Role {role.name} saved successfully, {response.Responses.Count} returned.");
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
