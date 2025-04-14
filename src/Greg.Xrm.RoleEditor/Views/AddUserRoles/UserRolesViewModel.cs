using Greg.Xrm.Core;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views.AddUserRoles
{
    public class UserRolesViewModel : ViewModel
    {
		private readonly ILog log;
		private readonly IMessenger messenger;

		public UserRolesViewModel(
			DataverseEnvironment environment, 
			IRoleRepository roleRepository)
		{
			this.Environment = environment;
			this.messenger = environment.Context.Messenger;
			this.log = environment.Context.Log;

			this.ApplyCommand = new AddUserRolesCommand(this, roleRepository);
			this.Users = new TreeNodeUserList();
			this.Roles = new TreeNodeRoleList(this);
		}

		public DataverseEnvironment Environment { get; }
		public string EnvironmentName => this.Environment.Context.Details.ConnectionName;


		public bool IsRecordOwnershipAcrossBusinessUnitEnabled
		{
			get => base.Get<bool>();
			private set => base.Set(value);
		}


		public TreeNodeUserList Users { get; }

		public TreeNodeRoleList Roles { get; }


		public ICommand ApplyCommand { get; }



		public void Initialize()
		{
			this.messenger.Send<Freeze>();
			this.messenger.Send(new WorkAsyncInfo
			{
				Message = "Loading environment info, please wait...",
				Work = (w, e) =>
				{
					var settings = Environment.VerifySecurityModelSettings();
					e.Result = settings;
				},
				PostWorkCallBack = result =>
				{
					this.messenger.Send<Unfreeze>();

					if (result.Error != null)
					{
						log.Error("Error while loading environment info", result.Error);
						return;
					}

					var settings = result.Result as SecurityModelSettings;
					if (settings == null)
					{
						log.Error($"Error while loading environment info: invalid result type. Expected {typeof(SecurityModelSettings).Name}, found {result.Result?.GetType().Name}");
						return;
					}


					this.IsRecordOwnershipAcrossBusinessUnitEnabled = settings.IsRecordOwnershipAcrossBusinessUnitsEnabled;
				},
			});

		}


		public void Terminate()
		{
			this.messenger.Send(new UserRolesViewClosed(this.Environment));
		}

		public bool ContextIs(IXrmToolboxPluginContext context)
		{
			return XrmToolboxPluginContextLoggingDecorator.AreEqual(this.Environment.Context, context);
		}

		/// <summary>
		/// This method takes as input a string that may contain a list of user / roles
		/// and tries to parse it. If found, adds the users / roles to the list.
		/// </summary>
		/// <param name="text">The text to parse</param>
		/// <returns>
		/// 
		/// </returns>
		/// <exception cref="NotImplementedException"></exception>
		public bool PasteFrom(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				this.log.Warn("CTRL+V was pressed but clipboard is empty");
				return false;
			}

			

			var separators = (System.Environment.NewLine + ";,|").ToCharArray();

			var lines = text.Split(separators, StringSplitOptions.RemoveEmptyEntries)
				.Select(x => x.Trim())
				.ToArray();

			var allUsers = this.Environment.GetAllUsers();
			var allRoles = this.Environment.GetAllRoles();

			var found = false;
			foreach (var line in lines)
			{
				var user = allUsers.FirstOrDefault(x => line.Equals(x.domainname, StringComparison.OrdinalIgnoreCase));
				if (user == null) user = allUsers.FirstOrDefault(x => line.Equals(x.fullname, StringComparison.OrdinalIgnoreCase));
				if (user == null) user = allUsers.FirstOrDefault(x => x.domainname?.StartsWith(line + "@", StringComparison.OrdinalIgnoreCase) ?? false);
				if ( user != null )
				{
					log.Debug("Add roles to user: CTRL+V found user " + user.domainname);
					this.Users.Add(new TreeNodeUser(user));
					found = true;
					continue;
				}


				var role = allRoles.FirstOrDefault(x => line.Equals(x.name, StringComparison.OrdinalIgnoreCase));
				if (role != null)
				{
					log.Debug("Add roles to user: CTRL+V found role " + role.name);
					this.Roles.Add(new TreeNodeRole(this.Environment, role, this.IsRecordOwnershipAcrossBusinessUnitEnabled));
					found = true;
					continue;
				}
				else if (this.IsRecordOwnershipAcrossBusinessUnitEnabled && line.Contains(":"))
				{
					var parts = line.Split(':').Select(x => x.Trim()).ToArray();
					if (parts.Length == 2)
					{
						var roleName = parts.FirstOrDefault();
						var buName = parts.LastOrDefault();

						role = allRoles.FirstOrDefault(x => roleName.Equals(x.name, StringComparison.OrdinalIgnoreCase));
						if (role != null)
						{
							var roleNode = new TreeNodeRole(this.Environment, role, this.IsRecordOwnershipAcrossBusinessUnitEnabled);
							var businessUnit = roleNode.ValidBusinessUnits.FirstOrDefault(x => x.Name.Equals(buName, StringComparison.OrdinalIgnoreCase));
							if (businessUnit != null)
							{
								roleNode.CurrentBusinessUnit = businessUnit;
								this.Roles.Add(roleNode);
								found = true;

								log.Debug($"Add roles to user: CTRL+V found role <{role.name}> on business unit <{buName}>");
								continue;
							}

							log.Debug($"Add roles to user: CTRL+V found role <{role.name}> not valid for business unit <{buName}>");
							continue;
						}
					}
				}


				log.Debug($"Add roles to user: CTRL+V found no user/role <{line}>");
			}
			return found;
		}

		public List<SystemUser> GetUsers()
		{
			return this.Users.Select(x => x.GetUser()).ToList();
		}
	}
}
