using Greg.Xrm.Core.Views.Help;
using Greg.Xrm.RoleEditor.Help;
using Greg.Xrm.RoleEditor.Model;
using System;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.RoleEditor.Views.UsageInspector
{
	public partial class UsageInspectorView : DockContent
	{
		private readonly UsageInspectorViewModel viewModel;

		public UsageInspectorView(IDependencyRepository dependencyRepository, Role role)
		{
			this.RegisterHelp(role.ExecutionContext.Messenger, Topics.Inspector);

			InitializeComponent();
			this.Text = this.TabText = $"Usage Inspector for : {role.name} ({role.ExecutionContext.Details.ConnectionName})";

			this.viewModel = new UsageInspectorViewModel(dependencyRepository, role);
			this.btnStart.BindCommand(() => this.viewModel.StartInspectionCommand);

			this.txtResult.Bind(x => x.Text, this.viewModel, vm => vm.Output);
		}


		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			this.viewModel.Dispose();
		}
	}
}
