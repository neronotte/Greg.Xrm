﻿using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Messages;
using System;
using System.Collections.Generic;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views.UserBrowser
{
	public class UserBrowserViewModel : ViewModel
	{
		private readonly ILog log;
		private readonly IMessenger messenger;
		private readonly IRoleRepository roleRepository;

		public UserBrowserViewModel(ILog log, IMessenger messenger, IRoleRepository roleRepository)
		{
			this.log = log;
			this.messenger = messenger;
			this.roleRepository = roleRepository;


			this.OpenRoleCommand = new OpenRoleFromUserCommand(this);

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
			});

			this.messenger.Register<Freeze>(m => this.IsEnabled = false);
			this.messenger.Register<Unfreeze>(m => this.IsEnabled = true);
			this.IsEnabled = false;
		}







		public void LoadRolesFor(SystemUser user)
		{	
			this.messenger.Send(new WorkAsyncInfo
			{
				Message = $"Loading roles of {user.fullname}...",
				Work = (worker, args) =>
				{
					user.LoadRoles(this.roleRepository);
				},
				PostWorkCallBack = args =>
				{
					if (args.Error != null)
					{
						this.log.Error("An error occurred while loading roles: " + args.Error.Message, args.Error);
						return;
					}

					this.RefreshUser?.Invoke(this, new RefreshUserEventArgs(user));
				}
			});
		}

		public event EventHandler<RefreshUserEventArgs> RefreshUser;






		public bool IsEnabled
		{
			get => this.Get<bool>();
			set => this.Set(value);
		}



		public List<DataverseEnvironment> Environments { get; } = new List<DataverseEnvironment>();
		public UserRole SelectedRole
		{
			get => this.Get<UserRole>();
			set => this.Set(value);
		}
		public OpenRoleFromUserCommand OpenRoleCommand { get; }
	}
}