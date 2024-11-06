using Greg.Xrm.Logging;
using Greg.Xrm.Model;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Model
{
	public class Role : EntityWrapper
	{
		private Role(Entity entity) : base(entity)
		{
		}

		public Role() : base("role")
		{
		}

		public string name
		{
			get => Get<string>();
			set => SetValue(value);
		}

		public string description
		{
			get => Get<string>();
			set => SetValue(value);
		}

		public EntityReference businessunitid
		{
			get => Get<EntityReference>();
			set => SetValue(value);
		}

		public string businessunitidFormatted => GetFormatted(nameof(businessunitid));

		/// <summary>
		/// 0 - Team privileges only
		/// 1 - Direct User (Basic) access level and Team privileges
		/// </summary>
		public bool isinherited
		{
			get => Get<bool>();
			set => SetValue(value);
		}

		public string isinheritedFormatted => GetFormatted(nameof(isinherited));

		public bool iscustomizable => Get<BooleanManagedProperty>()?.Value ?? false;

		public bool ismanaged => Get<bool>();


		public IReadOnlyList<RolePrivilege> Privileges { get; private set; } = new List<RolePrivilege>();

		public bool HasAssignedPrivileges => Privileges.Count > 0;


		public void ReadPrivileges(ILog log, IOrganizationService crm)
		{
			using(log.Track($"Initializing privileges for role <{name}>"))
			{
				var request = new RetrieveRolePrivilegesRoleRequest
				{
					RoleId = this.Id,
				};

				var response = (RetrieveRolePrivilegesRoleResponse)crm.Execute(request);
				this.Privileges = response.RolePrivileges;
			}
		}







		public class Repository : IRoleRepository
		{
			public IReadOnlyList<Role> GetParentRoles(IOrganizationService crm)
			{
				var query = new QueryExpression("role");
				query.ColumnSet.AddColumns("name", "description", "businessunitid", "iscustomizable", "ismanaged", "isinherited");
				query.Criteria.AddCondition("parentroleid", ConditionOperator.Null);
				query.AddOrder("name", OrderType.Ascending);
				query.NoLock = true;

				return crm.RetrieveAll(query, x => new Role(x));
			}
		}
	}
}
