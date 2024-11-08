using Greg.Xrm.Core.Views;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Linq;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views.Editor
{
	public class SaveCommand : CommandBase
	{
		private readonly RoleEditorViewModel viewModel;



		public SaveCommand(RoleEditorViewModel viewModel)
        {
			this.viewModel = viewModel;
		}

		public void SetEnabled(bool enabled)
		{
			this.CanExecute = enabled;
		}




		protected override void ExecuteInternal(object arg)
		{
			var model = this.viewModel.Model;
			if (model == null) return;

			var crm = model.GetContext();
			var log = crm.Log;
			var messenger = crm.Messenger;

			if (!model.IsDirty) 
			{
				log.Warn($"Save on role <{model.Name}> has no effect, the role is not dirty.");
				return;
			}

			if (model.Name == null)
			{
				this.viewModel.SendNotification(NotificationType.Error, "Role name cannot be empty.");
				return;
			}

			if (model.BusinessUnit == null)
			{
				this.viewModel.SendNotification(NotificationType.Error, "Role business unit cannot be empty.");
				return;
			}

			var changes = this.viewModel.GetChangeSummary();
			if (model.Id == Guid.Empty && !changes.HasAnyPrivilegeChange())
			{
				this.viewModel.SendNotification(NotificationType.Error, "Please add at least one privilege before saving.");
				return;
			}


			messenger.Send(new WorkAsyncInfo
			{
				Message = $"Saving role \"{model.Name}\", please wait...",
				Work = (worker, e) => 
				{
					messenger.Send<Freeze>();

					var requestList = changes.CreateRequest();

					var createRequest = requestList.OfType<CreateRequest>().FirstOrDefault();
					Guid? id = null;
					if (createRequest == null)
					{
						// we don't have any creation. Let's do a simple ExecuteTransactionRequest
						var request = new ExecuteTransactionRequest();
						request.ReturnResponses = false;
						request.Requests = new OrganizationRequestCollection();
						request.Requests.AddRange(requestList);
						crm.Execute(request);
					}
					else
					{
						
						// we need to process the creation first, then the rest
						var createResponse = (CreateResponse)crm.Execute(createRequest);
						id = createResponse.id;


						var request = new ExecuteTransactionRequest();
						request.ReturnResponses = false;
						request.Requests = new OrganizationRequestCollection();
						foreach (var otherRequest in requestList.OfType<AddPrivilegesRoleRequest>())
						{
							otherRequest.RoleId = id.Value;
							request.Requests.Add(otherRequest);
						}
						foreach (var otherRequest in requestList.OfType<RemovePrivilegeRoleRequest>())
						{
							otherRequest.RoleId = id.Value;
							request.Requests.Add(otherRequest);
						}
						foreach (var otherRequest in requestList.OfType<ReplacePrivilegesRoleRequest>() )
						{
							otherRequest.RoleId = id.Value;
							request.Requests.Add(otherRequest);
						}

						crm.Execute(request);
					}

					// now, if everything'sg good, we should reload role privil
					model.CommitChanges(id);
				},
				PostWorkCallBack = e => 
				{
					messenger.Send<Unfreeze>();

					if (e.Error != null)
					{
						log.Error("Error while trying to save the role: " + e.Error.Message, e.Error);
						this.viewModel.SendNotification(NotificationType.Error, "Error saving role, see Output for details: " + e.Error.Message);
						return;
					}

					this.viewModel.SendNotification(NotificationType.Success, "Role saved successfully!");

					// refresh the viewmodel and reload the view
					this.viewModel.RefreshView();
				}
			});

		}
	}
}
