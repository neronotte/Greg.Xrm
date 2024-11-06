using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Services;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views
{
	public class MainViewModel : PluginViewModelBase
	{
		private readonly ILog log;
		private readonly IMessenger messenger;
		private readonly RoleTemplateBuilder roleTemplateBuilder;
		private readonly IRoleRepository roleRepository;


		private List<Role> rolesCurrentlyOpened = new List<Role>();




		public MainViewModel(
			ILog log,
			IMessenger messenger,
			RoleTemplateBuilder roleTemplateBuilder,
			IRoleRepository roleRepository)
		{
			this.log = log;
			this.messenger = messenger;
			this.roleTemplateBuilder = roleTemplateBuilder;
			this.roleRepository = roleRepository;
			this.InitCommand = new RelayCommand(Initialize, CanInitialize);

			this.messenger.Register<OpenRoleView>(OnOpenRoleRequested);
			this.messenger.Register<CloseRoleView>(OnCloseRoleRequested);


			this.WhenChanges(() => this.Crm).Refresh(this.InitCommand);
		}




		public string ConnectionName 
		{
			get => base.Get<string>();
			protected set => base.Set(value);
		}

		public IOrganizationService Crm
		{
			get => base.Get<IOrganizationService>();
			protected set => base.Set(value);
		}

		public TemplateForRole RoleTemplate
		{
			get => Get<TemplateForRole>();
			private set => Set(value);
		}




		public void Reset(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
		{
			this.Crm = newService;
			this.ConnectionName = detail.ConnectionName;

			this.messenger.Send(new RoleListLoaded() { RoleList = Array.Empty<Role>(), RoleTemplate = null });
			foreach (var role in this.rolesCurrentlyOpened.ToArray())
			{
				OnCloseRoleRequested(new CloseRoleView(role));
			}
		}



		#region Init Command
		public RelayCommand InitCommand { get; }

		private bool CanInitialize()
		{
			return this.Crm != null;
		}


		private void Initialize()
		{
			this.messenger.Send(new WorkAsyncInfo
			{
				Message = "Loading tables and privileges...",
				Work = (worker, args) =>
				{
					// do some work here
					this.messenger.Send<Freeze>();

					var roleTemplate = this.roleTemplateBuilder.CreateTemplate(this.Crm);

					IReadOnlyList<Role> roleList;
					using(this.log.Track("Retrieving roles..."))
					{
						roleList = this.roleRepository.GetParentRoles(this.Crm);
						this.log.Info($"Found {roleList.Count} roles");
					}

					args.Result = Tuple.Create(roleTemplate, roleList);
				},
				PostWorkCallBack = e =>
				{
					this.messenger.Send<Unfreeze>();

					// do some work here
					var result = e.Result as Tuple<TemplateForRole, IReadOnlyList<Role>>;
					if (result == null) return;


					this.RoleTemplate = result.Item1;
					var roleList = result.Item2;

					this.messenger.Send(new RoleListLoaded{ RoleList = roleList, RoleTemplate = this.RoleTemplate });
				}
			});
		}

		#endregion





		public event EventHandler<OpenRoleView> OpenRoleRequested;
		public event EventHandler<OpenRoleView> ShowRoleRequested;

		private void OnOpenRoleRequested(OpenRoleView message)
		{
			if (this.rolesCurrentlyOpened.Contains(message.Role))
			{
				// we should highlight the panel of the role if it is already opened
				this.log.Debug("Role editor highlighted for: " + message.Role.name);
				ShowRoleRequested?.Invoke(this, message);
			}
			else
			{
				this.messenger.Send(new WorkAsyncInfo
				{
					Message = "Reading role details...",
					Work = (worker, args) =>
					{
						message.Role.ReadPrivileges(this.log, this.Crm);

						// do some work here
						args.Result = message.Role;
					},
					PostWorkCallBack = e =>
					{
						if (e.Error != null)
						{
							this.log.Error("Error retrieving role info: " + e.Error.Message, e.Error);
							return;
						}

						// do some work here
						var role = e.Result as Role;
						if (role == null) return;

						this.rolesCurrentlyOpened.Add(role);

						this.log.Debug($"Role editor opened for <{role.name}>. Total #roles opened: {this.rolesCurrentlyOpened.Count}");
						OpenRoleRequested?.Invoke(this, message);
					}
				});
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
