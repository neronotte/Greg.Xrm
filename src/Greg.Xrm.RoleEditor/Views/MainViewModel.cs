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


		private readonly List<Role> rolesCurrentlyOpened = new List<Role>();




		public MainViewModel(
			ILog log,
			IMessenger messenger,
			RoleTemplateBuilder roleTemplateBuilder,
			IRoleRepository roleRepository,
			IBusinessUnitRepository businessUnitRepository)
		{
			this.log = log;
			this.messenger = messenger;

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


			if (this.InitCommand.Context != null)
			{
				MessageBox.Show($"A new connection has been established.{Environment.NewLine}You can use the \"Load tables, privileges and roles\" from this environment.", "Connection Updated", MessageBoxButton.OK, MessageBoxImage.Information);
			}

			this.InitCommand.Context = context;
			this.messenger.Send(new ConnectionUpdated(context));
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
			var role = message.Role;

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
		}


		public event EventHandler<CloseRoleView> CloseRoleRequested;
		private void OnCloseRoleRequested(CloseRoleView message)
		{
			if (!this.rolesCurrentlyOpened.Contains(message.Role)) return;

			this.rolesCurrentlyOpened.Remove(message.Role);
			this.log.Debug($"Role editor closed for <{message.Role.name}>. Total #roles opened: {this.rolesCurrentlyOpened.Count}");
			CloseRoleRequested?.Invoke(this, message);
		}
	}
}
