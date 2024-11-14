using Greg.Xrm.RoleEditor.Model;
using Microsoft.Xrm.Sdk.Metadata;
using System.Linq;
using System.Windows.Forms;

namespace Greg.Xrm.RoleEditor.Views.Search
{
	public partial class SearchByPrivilegeDialog : Form
	{
		private readonly SearchByPrivilegeViewModel viewModel;

		public SearchByPrivilegeDialog(
			DataverseEnvironment environment,
			IRoleRepository roleRepository)
		{
			this.viewModel = new SearchByPrivilegeViewModel(environment, roleRepository);

			InitializeComponent();


			this.rSearchType1.Checked = this.viewModel.IsSearchByName;
			this.rSearchType1.CheckedChanged += (s, e) => this.viewModel.IsSearchByName = this.rSearchType1.Checked;


			this.txtPrivilegeName.Bind(x => x.Enabled, this.viewModel, vm => vm.IsSearchByName);
			this.txtPrivilegeName.AutoCompleteCustomSource = new AutoCompleteStringCollection();
			this.txtPrivilegeName.AutoCompleteCustomSource.AddRange(this.viewModel.PrivilegeNames);
			this.txtPrivilegeName.Bind(x => x.Text, this.viewModel, vm => vm.SelectedPrivilegeName);


			this.txtTables.Bind(x => x.Enabled, this.viewModel, vm => vm.IsSearchByLabel);
			this.txtTables.AutoCompleteCustomSource = new AutoCompleteStringCollection();
			this.txtTables.AutoCompleteCustomSource.AddRange(this.viewModel.Tables.Select(x => x.Key).ToArray());
			this.txtTables.KeyUp+= (s, e) =>
			{
				var table = this.viewModel.Tables.FirstOrDefault(x => x.Key == this.txtTables.Text);
				this.viewModel.SelectedTable = table;
				e.Handled = true;
			};
			this.viewModel.WhenChanges(() => this.viewModel.SelectedTable)
				.Execute(_ => this.txtTables.Text = this.viewModel.SelectedTable?.Key ?? string.Empty);


			this.cmbPrivilegeType.Bind(x => x.Enabled, this.viewModel, vm => vm.IsTablePrivilegeTypesEnabled);
			this.viewModel.WhenChanges(() => this.viewModel.TablePrivilegeTypes)
			.Execute(_ =>
			{
				this.cmbPrivilegeType.DataSource = this.viewModel.TablePrivilegeTypes;
			});
			this.cmbPrivilegeType.SelectedIndexChanged += (s, e) =>
			{
				if (this.cmbPrivilegeType.SelectedItem == null)
				{
					this.viewModel.SelectedPrivilegeType = null;
				}
				else
				{
					this.viewModel.SelectedPrivilegeType = (PrivilegeType)this.cmbPrivilegeType.SelectedItem;
				}
			};


			this.txtMiscPrivilegeLabel.Bind(x => x.Enabled, this.viewModel, vm => vm.IsSearchByLabel);
			this.txtMiscPrivilegeLabel.AutoCompleteCustomSource = new AutoCompleteStringCollection();
			this.txtMiscPrivilegeLabel.AutoCompleteCustomSource.AddRange(this.viewModel.Misc.Select(x => x.Name).ToArray());
			this.txtMiscPrivilegeLabel.KeyUp += (s, e) =>
			{
				var misc = this.viewModel.Misc.FirstOrDefault(x => x.Name == this.txtMiscPrivilegeLabel.Text);
				this.viewModel.SelecteMiscPrivilege = misc;
				e.Handled = true;
			};
			this.viewModel.WhenChanges(() => this.viewModel.SelecteMiscPrivilege)
				.Execute(_ => this.txtMiscPrivilegeLabel.Text = this.viewModel.SelecteMiscPrivilege?.Name ?? string.Empty);


			this.btnOk.BindCommand(() => this.viewModel.SearchCommand, () => this);
		}
	}
}
