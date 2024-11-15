using Greg.Xrm.RoleEditor.Views.Lookup;
using Greg.Xrm.Views;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views.Editor
{
	public class AddRoleToSolutionCommand: CommandBase
	{
		private readonly RoleEditorViewModel viewModel;

		public AddRoleToSolutionCommand(RoleEditorViewModel viewModel)
        {
			this.viewModel = viewModel;

			viewModel.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(viewModel.Model))
				{
					CalculateCanExecute();
				}
			};
			this.CalculateCanExecute();
		}

		private void CalculateCanExecute()
		{
			this.CanExecute = this.viewModel.Model != null && this.viewModel.Model.Id != Guid.Empty;
		}



		protected override void ExecuteInternal(object arg)
		{
			var owner = (Control)arg;

			var context = this.viewModel.Model.GetContext();
			var messenger = context.Messenger;
			var roleId = this.viewModel.Model.Id;



			var query = new QueryExpression("solution");
			query.ColumnSet.AddColumns("friendlyname", "uniquename", "version", "publisherid");
			query.Criteria.AddCondition("ismanaged", ConditionOperator.Equal, false);
			query.Criteria.AddCondition("isvisible", ConditionOperator.Equal, true);

			var userlink = query.AddLink("systemuser", "createdby", "systemuserid");
			userlink.LinkCriteria.AddCondition("fullname", ConditionOperator.NotEqual, "SYSTEM");

			EntityReference solutionRef;
			using (var dialog = new LookupDialog(context, query))
			{
				dialog.Text = "Add Role to Solution";
				dialog.Description = "Select the unmanaged solution you want to add the role to.";
				dialog.StartPosition = FormStartPosition.CenterParent;

				if (dialog.ShowDialog(owner) != DialogResult.OK) return;
				solutionRef = dialog.SelectedItem;
			}


			messenger.Send(new WorkAsyncInfo
			{
				Message = "Adding role to solution...",
				Work = (worker, args) =>
				{
					var request = new AddSolutionComponentRequest
					{
						ComponentType = 20, // role
						SolutionUniqueName = solutionRef.Name,
						ComponentId = roleId
					};

					context.Execute(request);
				},
				PostWorkCallBack = args =>
				{
					if (args.Error != null)
					{
						this.viewModel.SendNotification(Core.Views.NotificationType.Error, "Error adding role to solution: " + args.Error.Message);
					}
					else
					{
						this.viewModel.SendNotification( Core.Views.NotificationType.Success, "Role added to solution!");
					}
				}
			});
		}
	}
}
