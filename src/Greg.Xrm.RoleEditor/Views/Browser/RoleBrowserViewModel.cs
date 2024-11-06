using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using System.Collections.Generic;
using System.Windows.Navigation;

namespace Greg.Xrm.RoleEditor.Views.RoleBrowser
{
	public class RoleBrowserViewModel : ViewModel
	{
		private readonly ILog log;
		private readonly IMessenger messenger;

		public RoleBrowserViewModel(ILog log, IMessenger messenger)
        {
			this.log = log;
			this.messenger = messenger;

			this.OverrideSetDefaultValue(() => ShouldHideNotCustomizableRoles, () => true);
			this.OverrideSetDefaultValue(() => ShouldHideManagedRoles, () => false);
			this.OverrideSetDefaultValue(() => EmptyListMessage, () => "Click on \"Load tables, privileges and roles\" button to load the roles.");



			this.OpenRoleCommand = new OpenRoleCommand(messenger);

			this.ShouldHideNotCustomizableRolesCommand = new RelayCommand(() =>
			{
				this.ShouldHideNotCustomizableRoles = !this.ShouldHideNotCustomizableRoles;
			}, () => IsEnabled);
			this.ShouldHideManagedRolesCommand = new RelayCommand(() =>
			{
				this.ShouldHideManagedRoles = !this.ShouldHideManagedRoles;
			}, () => IsEnabled);


			this.messenger.Register<RoleListLoaded>(x =>
			{
				this.RoleList = x.RoleList;
				this.RoleTemplate = x.RoleTemplate;
				this.IsEnabled = x.RoleList != null;
				if (x.RoleList.Count == 0)
				{
					this.EmptyListMessage = "No roles found on the system.";
				}
				else
				{
					this.EmptyListMessage = "No roles found matching the specified filters. You can change the filters in the toolbar above this panel.";
				}
			});


			this.WhenChanges(() => RoleList)
				.Refresh(ShouldHideNotCustomizableRolesCommand)
				.Refresh(ShouldHideManagedRolesCommand);

			this.messenger.Register<Freeze>(m => IsEnabled = false);
			this.messenger.Register<Unfreeze>(m => IsEnabled = true);
		}





		public bool IsEnabled
		{
			get =>Get<bool>();
			private set => Set(value);
		}




		public IReadOnlyList<Role> RoleList
		{
			get => Get<IReadOnlyList<Role>>();
			private set => Set(value);
		}

		public TemplateForRole RoleTemplate
		{
			get => Get<TemplateForRole>();
			private set => Set(value);
		}


		public Role SelectedRole 
		{
			get => Get<Role>();
			set => Set(value);
		}


		public string EmptyListMessage
		{
			get => Get<string>();
			private set => Set(value);
		}



		public bool ShouldHideNotCustomizableRoles
		{
			get => Get<bool>();
			set => Set(value);
		}

		public RelayCommand ShouldHideNotCustomizableRolesCommand { get; }

		public bool ShouldHideManagedRoles
		{
			get => Get<bool>();
			set => Set(value);
		}

		public RelayCommand ShouldHideManagedRolesCommand { get; }


		public OpenRoleCommand OpenRoleCommand { get; }
	}
}
