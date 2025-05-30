﻿using Greg.Xrm.Core;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

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

		public void AddRole(Role role)
		{
			if (role == null) return;
			foreach (var businessUnit in this)
			{
				businessUnit.AddRole(role);
			}
		}


		/// <summary>
		/// Checks the value of the security model flags on the organization table,
		/// as described here https://dev.to/_neronotte/record-ownership-across-business-units-under-the-hood-2l15
		/// </summary>
		/// <returns>
		/// The security model settings fields
		/// </returns>
		public SecurityModelSettings VerifySecurityModelSettings()
		{
			var settings = new SecurityModelSettings();


			var query = new QueryExpression("organization");
			query.ColumnSet.AddColumns("orgdborgsettings");
			query.TopCount = 1;
			query.NoLock = true;

			var org = this.Context.RetrieveMultiple(query).Entities.FirstOrDefault();
			if (org == null) return settings;

			var orgSettings = org.GetAttributeValue<string>("orgdborgsettings");
			if (string.IsNullOrWhiteSpace(orgSettings)) return settings;

			var xml = XDocument.Parse(orgSettings);

			var node = xml.Root?.Element("EnableOwnershipAcrossBusinessUnits");
			if (node != null)
			{
				settings.IsRecordOwnershipAcrossBusinessUnitsEnabled = bool.Parse(node.Value);
			}

			node = xml.Root?.Element("AlwaysMoveRecordToOwnerBusinessUnit");
			if (node != null)
			{
				settings.AlwaysMoveRecordToOwnerBusinessUnit = bool.Parse(node.Value);
			}

			node = xml.Root?.Element("DoNotRemoveRolesOnChangeBusinessUnit");
			if (node != null)
			{
				settings.DoNotRemoveRolesOnChangeBusinessUnit = bool.Parse(node.Value);
			}

			return settings;
		}
	}
}
