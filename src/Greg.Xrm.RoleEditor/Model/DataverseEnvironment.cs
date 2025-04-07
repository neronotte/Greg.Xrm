using Greg.Xrm.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Model
{
	public class DataverseEnvironment : List<BusinessUnit>
	{
        public DataverseEnvironment(IXrmToolboxPluginContext context, TemplateForRole template)
        {
            this.Context = context;
			this.Template = template;
		}

#pragma warning disable IDE1006 // Naming Styles
		public string name => this.Context.Details.ConnectionName;
#pragma warning restore IDE1006 // Naming Styles

		public IXrmToolboxPluginContext Context { get; }
		
		public TemplateForRole Template { get; }


		public bool Contains(Role role)
		{
			if (role == null) return false;
			return this.Exists(businessUnit => businessUnit.Contains(role, recursive: true));
		}

		public bool Contains(SystemUser user)
		{
			if (user == null) return false;
			return this.Exists(businessUnit => businessUnit.Contains(user, recursive: true));
		}



		public IReadOnlyList<Role> GetAllRoles()
		{
			var roleList = new List<Role>();
			GetAllRolesRecursive(roleList, this);
			return roleList;
		}

		private void GetAllRolesRecursive(List<Role> roleList, IEnumerable<BusinessUnit> businessUnitList)
		{
			foreach (var businessUnit in businessUnitList)
			{
				roleList.AddRange(businessUnit.Roles);
				GetAllRolesRecursive(roleList, businessUnit.Children);
			}
		}

		public IReadOnlyList<SystemUser> GetAllUsers()
		{
			var userList = new List<SystemUser>();
			GetAllUsersRecursive(userList, this);
			return userList;
		}

		private void GetAllUsersRecursive(List<SystemUser> userList, IEnumerable<BusinessUnit> businessUnitList)
		{
			foreach (var businessUnit in businessUnitList)
			{
				userList.AddRange(businessUnit.Users);
				GetAllUsersRecursive(userList, businessUnit.Children);
			}
		}


		public BusinessUnit FindBusinessUnit(Guid? id)
		{
			if (id == null) return null;
			if (id == Guid.Empty) return null;

			return FindRecursive(id.Value, this);
		}

		private BusinessUnit FindRecursive(Guid id, IEnumerable<BusinessUnit> businessUnitList)
		{
			foreach (var businessUnit in businessUnitList)
			{
				if (businessUnit.Id == id) return businessUnit;
				
				var found = FindRecursive(id, businessUnit.Children);
				if (found != null) return found;
			}
			return null;
		}


		public void RemoveUser(SystemUser user)
		{
			// iterate through all business units, hierarchically, in order
			// to find the user passed as input
			// and, when found, remove it

			if (user == null) return;
			RemoveUserRecursive(user, this);
		}

		private bool RemoveUserRecursive(SystemUser user, IEnumerable<BusinessUnit> businessUnitList)
		{
			foreach (var businessUnit in businessUnitList)
			{
				// Check if this business unit contains the user (non-recursively)
				if (businessUnit.Contains(user))
				{
					// Remove the user from the list
					businessUnit.RemoveUser(user);
					return true;
				}

				// If not found in current BU, check its children
				if (RemoveUserRecursive(user, businessUnit.Children))
				{
					return true;
				}
			}

			return false;
		}

		public void AddUser(SystemUser user)
		{
			if (user == null) return;
			foreach (var businessUnit in this)
			{
				businessUnit.AddUser(user);
			}
		}
	}
}
