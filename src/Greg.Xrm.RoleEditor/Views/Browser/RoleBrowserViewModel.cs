using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Browser;
using Greg.Xrm.RoleEditor.Views.Comparer;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Views.RoleBrowser
{
	public class RoleBrowserViewModel : ViewModel
	{
		public RoleBrowserViewModel(
			ILog log,
			IMessenger messenger,
			ISettingsProvider<Settings> settingsProvider,
			IRoleRepository roleRepository)
		{
			this.OverrideSetDefaultValue(() => EmptyListMessage, () => "Click on \"Load tables, privileges and roles\" button to load the roles.");


			this.NewRoleCommand = new NewRoleCommand(messenger, this);
			this.NewRoleFromAppOpenerCommand = new NewRoleFromExistingCommand(this, "App Opener");
			this.NewRoleFromBasicUserCommand = new NewRoleFromExistingCommand(this, "Basic User");
			this.NewRoleFromCurrentCommand = new NewRoleFromCurrentCommand();
			this.OpenRoleCommand = new OpenRoleCommand();
			this.OpenMultipleRolesCommand = new OpenMultipleRolesCommand();
			this.SearchByPrivilegeCommand = new SearchByPrivilegeCommand(this, roleRepository);
			this.SearchBySolutionCommand = new SearchBySolutionCommand(this, roleRepository);
			this.OpenUsageInspectorCommand = new OpenUsageInspectorCommand();
			this.CompareRolesCommand = new CompareRolesCommand();

			var settings = settingsProvider.GetSettings();
			this.ShouldHideManagedRoles = settings.HideManagedRoles;
			this.ShouldHideNotCustomizableRoles = settings.HideNotCustomizableRoles;


			this.ShouldHideNotCustomizableRolesCommand = new RelayCommand(() =>
			{
				this.ShouldHideNotCustomizableRoles = !this.ShouldHideNotCustomizableRoles;
			}, () => IsEnabled);
			this.ShouldHideManagedRolesCommand = new RelayCommand(() =>
			{
				this.ShouldHideManagedRoles = !this.ShouldHideManagedRoles;
			}, () => IsEnabled);


			messenger.Register<RoleListLoaded>(x =>
			{
				var newEnvironment = x.Environment;

				var currentMatchingEnvironment = this.Environments.Find(e => e.Context.Details.ConnectionId == newEnvironment.Context.Details.ConnectionId);
				if (currentMatchingEnvironment != null)
				{
					this.Environments.Remove(currentMatchingEnvironment);
				}

				this.Environments.Add(x.Environment);
				this.IsEnabled = this.Environments.Count > 0;

				this.OnPropertyChanged(nameof(Environments), this.Environments);

				if (x.RoleList == null)
				{
					this.EmptyListMessage = "Click on \"Load tables, privileges and roles\" button to load the roles.";
				}
				else if (x.RoleList.Count == 0)
				{
					this.EmptyListMessage = "No roles found on the system.";
				}
				else
				{
					this.EmptyListMessage = "No roles found matching the specified filters. You can change the filters in the toolbar above this panel.";
				}
			});


			this.WhenChanges(() => Environments)
				.Refresh(ShouldHideNotCustomizableRolesCommand)
				.Refresh(ShouldHideManagedRolesCommand);

			this.WhenChanges(() => SelectedRole)
				.Execute(_ => this.NewRoleFromCurrentCommand.Role = this.SelectedRole)
				.Execute(_ => this.NewRoleFromAppOpenerCommand.SelectedRole = this.SelectedRole)
				.Execute(_ => this.NewRoleFromBasicUserCommand.SelectedRole = this.SelectedRole);


			messenger.Register<Freeze>(m => IsEnabled = false);
			messenger.Register<Unfreeze>(m => IsEnabled = true);
			messenger.Register<RefreshRoleBrowser>(m =>
			{
				var role = m.Role;

				var env = this.Environments.Find(x => x.Context == role.ExecutionContext);
				if (env == null) return;

				var bu = env.FindBusinessUnit(role.businessunitid?.Id);
				if (bu == null) return;

				bu.AddRole(role);

				OnPropertyChanged(nameof(Environments), this.Environments);
			});
		}



		public List<DataverseEnvironment> Environments { get; } = new List<DataverseEnvironment>();

		public bool IsEnabled
		{
			get => Get<bool>();
			private set => Set(value);
		}


		internal DataverseEnvironment GetSelectedRoleEnvironmentOrDefault()
		{
			if (this.SelectedRole == null) return this.Environments.FirstOrDefault();

			return this.Environments.Find(x => x.Contains(this.SelectedRole));
		}

		internal DataverseEnvironment[] GetRoleEnvironment(Role[] selectedRoles)
		{
			var environmentList = new List<DataverseEnvironment>();
			foreach (var role in selectedRoles)
			{
				var environment = this.Environments.Find(x => x.Contains(role));
				if (environment != null && !environmentList.Contains(environment))
				{
					environmentList.Add(environment);
				}
			}
			return environmentList.ToArray();
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


		public ICommand NewRoleCommand { get; }
		public NewRoleFromExistingCommand NewRoleFromAppOpenerCommand { get; }
		public NewRoleFromExistingCommand NewRoleFromBasicUserCommand { get; }

		public NewRoleFromCurrentCommand NewRoleFromCurrentCommand { get; }
		public OpenRoleCommand OpenRoleCommand { get; }
		public OpenMultipleRolesCommand OpenMultipleRolesCommand { get; }

		public SearchByPrivilegeCommand SearchByPrivilegeCommand { get; }
		public SearchBySolutionCommand SearchBySolutionCommand { get; }

		public OpenUsageInspectorCommand OpenUsageInspectorCommand { get; }

		public CompareRolesCommand CompareRolesCommand { get; }
	}
}
