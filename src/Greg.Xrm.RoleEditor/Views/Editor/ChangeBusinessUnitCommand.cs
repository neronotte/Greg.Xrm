using Greg.Xrm.RoleEditor.Views.Lookup;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Windows.Forms;

namespace Greg.Xrm.RoleEditor.Views.Editor
{
	public class ChangeBusinessUnitCommand : CommandBase<RoleEditorView>
	{
		private readonly IOrganizationService crm;
		private readonly RoleModel model;

		public ChangeBusinessUnitCommand(IOrganizationService crm, RoleModel model, bool canChange)
        {
			this.crm = crm;
			this.model = model;
			this.CanExecute = canChange;
		}


        protected override void ExecuteInternal(RoleEditorView parent)
		{
			var query = new QueryExpression("businessunit");
			query.ColumnSet.AddColumns("name", "parentbusinessunitid");

			using (var dialog = new LookupDialog(crm, query))
			{
				dialog.Text = "Business Unit";
				dialog.Description = "Please select a business unit from the list below:";
				dialog.StartPosition = FormStartPosition.CenterParent;

				if (dialog.ShowDialog(parent) != DialogResult.OK) return;

				this.model.BusinessUnit = dialog.SelectedItemReference;
			}
		}
	}
}
