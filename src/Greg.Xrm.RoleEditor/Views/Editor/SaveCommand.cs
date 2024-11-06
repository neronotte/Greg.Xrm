using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views.Editor
{
	public class SaveCommand : CommandBase
	{
		private readonly ILog log;
		private readonly IMessenger messenger;
		private readonly IOrganizationService crm;
		private readonly RoleEditorViewModel viewModel;

		public SaveCommand(ILog log, IMessenger messenger, IOrganizationService crm, RoleEditorViewModel viewModel)
        {
			this.log = log;
			this.messenger = messenger;
			this.crm = crm;
			this.viewModel = viewModel;
		}

		protected override void ExecuteInternal(object arg)
		{
			var model = this.viewModel.Model;
			if (model == null) return;
			if (!model.IsDirty) 
			{
				log.Warn($"Save on role <{model.Name}> has no effect, the role is not dirty.");
				return;
			}

			var changes = this.viewModel.GetChangeSummary();

			this.messenger.Send(new WorkAsyncInfo
			{
				Message = $"Saving role \"{model.Name}\", please wait...",
				Work = (worker, e) => 
				{
					var request = changes.CreateRequest();

					var response = (ExecuteTransactionResponse)crm.Execute(request);

					// now, if everything'sg good, we should reload role privil

					model.Reload(log, crm);

				},
				PostWorkCallBack = e => 
				{
					if (e.Error != null)
					{
						this.log.Error("Error while trying to save the role: " + e.Error.Message, e.Error);
						MessageBox.Show("Error while trying to save the role: " + Environment.NewLine + e.Error.Message, "Save role", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}


					// refresh the viewmodel and reload the view
					this.viewModel.RefreshView();
				}
			});

		}
	}
}
