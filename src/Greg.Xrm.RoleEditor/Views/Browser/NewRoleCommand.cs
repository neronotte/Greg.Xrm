using Greg.Xrm.Messaging;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.RoleEditor.Views.RoleBrowser;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk;

namespace Greg.Xrm.RoleEditor.Views.Browser
{
	public class NewRoleCommand : CommandBase<DataverseEnvironment>
	{
		private readonly IMessenger messenger;

		public NewRoleCommand(IMessenger messenger, RoleBrowserViewModel viewModel)
		{
			this.messenger = messenger;

			viewModel.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName != nameof(RoleBrowserViewModel.IsEnabled))
					return;

				this.CanExecute = viewModel.IsEnabled;
			};
		}


		protected override void ExecuteInternal(DataverseEnvironment arg)
		{
			if (arg == null) return;

			var role = new Role(arg.Context, arg.Template)
			{
				name = RoleName.GetNewName(),
				isinherited = new OptionSetValue(1),
			};
			this.messenger.Send(new OpenRoleView(role));
		}
	}
}
