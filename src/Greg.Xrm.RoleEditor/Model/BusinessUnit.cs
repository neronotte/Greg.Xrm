using Greg.Xrm.Core;
using Greg.Xrm.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Model
{
	public class BusinessUnit : EntityWrapper
	{
		private BusinessUnit(Entity entity) : base(entity)
		{
		}

		public string name => Get<string>();

		public EntityReference parentbusinessunitid => Get<EntityReference>();


		private readonly List<BusinessUnit> children = new List<BusinessUnit>();
		public IReadOnlyList<BusinessUnit> Children => this.children;

		private void AddChild(BusinessUnit child)
		{
			this.children.Add(child);
			this.children.Sort((a, b) => string.Compare(a.name, b.name));
		}


		#region Roles

		private readonly List<Role> roles = new List<Role>();
		public IReadOnlyList<Role> Roles => this.roles;

		public bool HasAnyRole()
		{
			if (this.roles.Count > 0) return true;
			return this.children.Exists(x => x.HasAnyRole());
		}

		public bool Contains(Role role, bool recursive = false)
		{
			if (role == null) return false;

			if (this.roles.Contains(role)) return true;

			if (recursive)
			{
				return this.children.Exists(x => x.Contains(role));
			}
			return false;
		}

		public void AddRole(Role role)
		{
			if (role.businessunitid?.Id == this.Id)
			{
				this.roles.Add(role);
				this.roles.Sort((a, b) => string.Compare(a.name, b.name));
			}
			else
			{
				foreach (var child in this.Children)
				{
					child.AddRole(role);
				}
			}
		}

		public void AddRoles(IEnumerable<Role> roleList)
		{
			this.roles.AddRange(roleList.OrderBy(x => x.name));
		}

		#endregion


		#region Users

		private readonly List<SystemUser> users = new List<SystemUser>();
		public IReadOnlyList<SystemUser> Users => this.users;
		public bool HasAnyUser()
		{
			if (this.users.Count > 0) return true;
			return this.children.Exists(x => x.HasAnyUser());
		}


		public int CountUsers()
		{
			return this.users.Count + this.children.Sum(x => x.CountUsers());
		}

		public int CountUsers(string searchCriteria)
		{
			if (string.IsNullOrWhiteSpace(searchCriteria)) return CountUsers();
			
			return this.users.Count(x => x.Match(searchCriteria)) + this.children.Sum(x => x.CountUsers(searchCriteria));
		}

		public bool Contains(SystemUser user, bool recursive = false)
		{
			if (user == null) return false;

			if (this.users.Contains(user)) return true;

			if (recursive)
			{
				return this.children.Exists(x => x.Contains(user));
			}
			return false;
		}

		public void AddUser(SystemUser user)
		{
			if (user.businessunitid?.Id == this.Id)
			{
				this.users.Add(user);
				this.users.Sort((a, b) => string.Compare(a.fullname, b.fullname));
			}
			else
			{
				foreach (var child in this.Children)
				{
					child.AddUser(user);
				}
			}
		}

		public void AddUsers(IEnumerable<SystemUser> userList)
		{
			this.users.AddRange(userList.OrderBy(x => x.fullname));
		}


		#endregion


		public class Repository : IBusinessUnitRepository
		{
			public (BusinessUnit, Dictionary<Guid, BusinessUnit>) GetTree(IXrmToolboxPluginContext context)
			{
				using (context.Log.Track($"Reading list of business units from <{context.Details.ConnectionName}>"))
				{
					var query = new QueryExpression("businessunit");
					query.ColumnSet = new ColumnSet("name", "parentbusinessunitid");

					var buList = context.RetrieveAll(query, x => new BusinessUnit(x));
					var buDict = buList.ToDictionary(x => x.Id);


					BusinessUnit root = null;
					foreach (var bu in buList)
					{
						if (bu.parentbusinessunitid == null)
						{
							root = bu;
							continue;
						}

						if (buDict.TryGetValue(bu.parentbusinessunitid.Id, out var parent))
						{
							parent.AddChild(bu);
						}
					}
					return (root, buDict);
				}
			}
		}
	}
}
