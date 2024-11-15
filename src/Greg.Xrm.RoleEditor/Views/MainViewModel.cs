using Greg.Xrm.Core;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Services;
using Greg.Xrm.RoleEditor.Views.Messages;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Greg.Xrm.RoleEditor.Views
{
	public class MainViewModel : PluginViewModelBase
	{
		const string LoadButtonTextBase = "Load tables, privileges and roles";

		private readonly ILog log;
		private readonly IMessenger messenger;
		private readonly ISettingsProvider<Settings> settingsProvider;
		private readonly List<Role> rolesCurrentlyOpened = new List<Role>();




		public MainViewModel(
			ILog log,
			IMessenger messenger,
			ISettingsProvider<Settings> settingsProvider,
			RoleTemplateBuilder roleTemplateBuilder,
			IRoleRepository roleRepository,
			IBusinessUnitRepository businessUnitRepository)
		{
			this.log = log;
			this.messenger = messenger;
			this.settingsProvider = settingsProvider;
			this.LoadDataButtonText = LoadButtonTextBase;


			this.InitCommand = new LoadDataCommand(roleTemplateBuilder, roleRepository, businessUnitRepository);


			this.messenger.Register<OpenRoleView>(OnOpenRoleRequested);
			this.messenger.Register<CloseRoleView>(OnCloseRoleRequested);
		}




		public void Reset(IOrganizationService newService, ConnectionDetail detail)
		{
			if (newService == null || detail == null) return;

			var context = new XrmToolboxPluginContext(this.log, this.messenger, detail, newService);

			this.LoadDataButtonText = LoadButtonTextBase + $" ({detail.ConnectionName})";

			var settings = this.settingsProvider.GetSettings();

			if (this.InitCommand.Context != null && !settings.AutoLoadRolesWhenConnectonChanges)
			{
				MessageBox.Show($"A new connection has been established.{Environment.NewLine}You can use the \"Load tables, privileges and roles\" from this environment.", "Connection Updated", MessageBoxButton.OK, MessageBoxImage.Information);
			}

			this.InitCommand.Context = context;
			this.messenger.Send(new ConnectionUpdated(context));

			if (settings.AutoLoadRolesWhenConnectonChanges)
			{
				this.InitCommand.Execute(null);
			}
		}



		#region Init Command

		public string LoadDataButtonText
		{
			get => Get<string>();
			private set => Set(value);
		}

		public LoadDataCommand InitCommand { get; }

		#endregion





		public event EventHandler<OpenRoleView> OpenRoleRequested;
		public event EventHandler<OpenRoleView> ShowRoleRequested;


		private void OnOpenRoleRequested(OpenRoleView message)
		{
			if (message.Roles.Length == 1)
			{
				var role = message.Roles[0];

				if (this.rolesCurrentlyOpened.Contains(role))
				{
					// we should highlight the panel of the role if it is already opened
					this.log.Debug("Role editor highlighted for: " + role.name);
					ShowRoleRequested?.Invoke(this, message);
				}
				else
				{
					this.rolesCurrentlyOpened.Add(role);

					this.log.Debug($"Role editor opened for <{role.name}>. Total #roles opened: {this.rolesCurrentlyOpened.Count}");
					OpenRoleRequested?.Invoke(this, message);
				}

				return;
			}

			var alreadyOpenedRole = Array.Find(message.Roles, x => this.rolesCurrentlyOpened.Contains(x));
			if (alreadyOpenedRole != null)
			{
				MessageBox.Show($"The role <{alreadyOpenedRole.name}> is already opened, cannot launch multiple edit.", "Role Editor", MessageBoxButton.OK, MessageBoxImage.Information);
				this.log.Debug("Role editor highlighted for: " + alreadyOpenedRole.name);
				ShowRoleRequested?.Invoke(this, message);
				return;
			}


			this.rolesCurrentlyOpened.AddRange(message.Roles);
			this.log.Debug($"Role editor opened for multiple edit: <{message.Roles.Length}> roles will be affected. Total #roles opened: {this.rolesCurrentlyOpened.Count}");
			OpenRoleRequested?.Invoke(this, message);
		}


		public event EventHandler<CloseRoleView> CloseRoleRequested;
		private void OnCloseRoleRequested(CloseRoleView message)
		{
			foreach (var role in message.Roles)
			{
				if (!this.rolesCurrentlyOpened.Contains(role)) continue;

				this.rolesCurrentlyOpened.Remove(role);
				CloseRoleRequested?.Invoke(this, message);
			}

			if (message.Roles.Length == 1)
			{
				this.log.Debug($"Role editor closed for <{message.Roles[0].name}>. Total #roles still opened: {this.rolesCurrentlyOpened.Count}");
			}
			else
			{
				this.log.Debug($"Role editor closed for <{message.Roles.Length}> roles. Total #roles still opened: {this.rolesCurrentlyOpened.Count}");
			}
		}
	}
}
