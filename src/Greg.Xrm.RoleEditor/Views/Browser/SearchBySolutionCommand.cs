using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Lookup;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.RoleEditor.Views.RoleBrowser;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views.Browser
{
	public class SearchBySolutionCommand : CommandBase<RoleBrowserView>
	{
		private readonly RoleBrowserViewModel viewModel;
		private readonly IRoleRepository roleRepository;

		public SearchBySolutionCommand(RoleBrowserViewModel viewModel, IRoleRepository roleRepository)
		{
			this.viewModel = viewModel;
			this.roleRepository = roleRepository;

			this.viewModel.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(this.viewModel.IsEnabled))
					RefreshCanExecute();
			};
		}

		private void RefreshCanExecute()
		{
			this.CanExecute = this.viewModel.IsEnabled;
		}

		protected override void ExecuteInternal(RoleBrowserView owner)
		{
			var environment = this.viewModel.GetSelectedRoleEnvironmentOrDefault();
			if (environment == null) return;
			var messenger = environment.Context.Messenger;
			var log = environment.Context.Log;

			var query = new QueryExpression("solution");
			query.ColumnSet.AddColumns("friendlyname", "uniquename", "version", "publisherid", "ismanaged");
			query.Criteria.AddCondition("isvisible", ConditionOperator.Equal, true);

			var userlink = query.AddLink("systemuser", "createdby", "systemuserid");
			userlink.LinkCriteria.AddCondition("fullname", ConditionOperator.NotEqual, "SYSTEM");
			query.AddOrder("ismanaged", OrderType.Descending);
			query.AddOrder("friendlyname", OrderType.Ascending);


			EntityReference solutionRef;
			using (var dialog = new LookupDialog(environment.Context, query))
			{
				dialog.Text = "Search by solution...";
				dialog.Description = "Select the solution you want to get the roles from.";
				dialog.StartPosition = FormStartPosition.CenterParent;

				if (dialog.ShowDialog(owner) != DialogResult.OK) return;
				solutionRef = dialog.SelectedItemReference;
			}

			messenger.Send<Freeze>();

			messenger.Send(new WorkAsyncInfo
			{
				Message = $"Searching for roles in solution {solutionRef.Name}...",
				Work = (worker, args) =>
				{
					var roles = this.roleRepository.GetRolesBySolution(environment.Context, solutionRef, environment.Template);
					args.Result = new SearchRoleCompleted(roles.ToArray(), $"Roles in sol. <{solutionRef.Name}>");
				},
				PostWorkCallBack = (e) =>
				{
					messenger.Send<Unfreeze>();

					if (e.Error != null)
					{
						log.Error("Error while retrieving roles by solution: " + e.Error.Message, e.Error);
						return;
					}


					var result = (e.Result as SearchRoleCompleted);
					if (result == null) return;
					messenger.Send(result);
				}
			});
		}
	}
}
