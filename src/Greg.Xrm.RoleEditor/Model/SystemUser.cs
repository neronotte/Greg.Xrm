using Greg.Xrm.Core;
using Greg.Xrm.Logging;
using Greg.Xrm.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Model
{
	public class SystemUser : EntityWrapper
	{
		private readonly TemplateForRole template;

		protected SystemUser(Entity entity, IXrmToolboxPluginContext context, TemplateForRole template) : base(entity)
		{
			this.ExecutionContext = context;
			this.template = template;
			this.AreRolesLoaded = false;
		}

		/// <summary>
		/// The object containing details about the environment connection.
		/// </summary>
		public IXrmToolboxPluginContext ExecutionContext { get; }

		public bool IsTechnicalUser =>
			this.fullname.StartsWith("# ")
			|| this.fullname.Equals("SYSTEM")
			 || this.fullname.Equals("INTEGRATION");

		public string fullname => Get<string>();

		public string domainname => Get<string>();

		public EntityReference businessunitid => Get<EntityReference>();
		public string businessunitidFormatted => this.businessunitid?.Name ?? GetFormatted(nameof(businessunitid));

		public bool AreRolesLoaded { get; private set; }



		public List<UserRole> Roles { get; } = new List<UserRole>();

		
		/// <summary>
		/// Reloads all the info of the current system user.
		/// Is invoked after changing the business unit, or whenever we perform an administrative operation 
		/// onto the current user.
		/// </summary>
		public void Reload(IRoleRepository roleRepository)
		{
			if (ExecutionContext == null)
				throw new InvalidOperationException(nameof(ExecutionContext) + " cannot be null!");

			var preImage = ExecutionContext.Retrieve(this.ToEntityReference(), "fullname", "domainname", "businessunitid");

			var me = (IEntityWrapperInternal)this;

			me.GetPostImage().Attributes.Clear();
			var target = me.GetTarget();
			foreach (var kvp in preImage.Attributes)
			{
				target[kvp.Key] = kvp.Value;
			}
			foreach (var kvp in preImage.FormattedValues)
			{
				target.FormattedValues[kvp.Key] = kvp.Value;
			}

			LoadRoles(roleRepository);
		}


		public void LoadRoles(IRoleRepository roleRepository)
		{
			var roleList = roleRepository.GetRolesByUser(this.ExecutionContext, this.ToEntityReference(), template);

			var rolesInCurrentBusinessUnit = roleList.Where(x => x.businessunitid.Id == this.businessunitid.Id).ToArray();
			var otherRoles = roleList.Except(rolesInCurrentBusinessUnit).ToArray();

			this.Roles.Clear();
			this.Roles.AddRange(rolesInCurrentBusinessUnit
				.OrderBy(x => x.name)
				.Select(x => new UserRole(this, x)));
			this.Roles.AddRange(otherRoles
				.OrderBy(x => x.businessunitidFormatted)
				.ThenBy(x => x.name)
				.Select(x => new UserRole(this, x)));
			this.AreRolesLoaded = true;
		}

		public bool Match(string searchText)
		{
			if (string.IsNullOrWhiteSpace(searchText)) return true;

			return
				(this.fullname ?? string.Empty).ToLowerInvariant().Contains(searchText.ToLowerInvariant()) ||
				(this.domainname ?? string.Empty).ToLowerInvariant().Contains(searchText.ToLowerInvariant());
		}

		public class Repository : ISystemUserRepository
		{
			private readonly ILog log;

			public Repository(ILog log)
			{
				this.log = log;
			}


			public IReadOnlyList<SystemUser> GetActiveUsers(IXrmToolboxPluginContext executionContext, TemplateForRole template)
			{
				using (this.log.Track("Retrieving active users"))
				{
					var query = new QueryExpression("systemuser");
					query.ColumnSet.AddColumns("fullname", "domainname", "businessunitid");
					query.Criteria.AddCondition("isdisabled", ConditionOperator.Equal, false);
					query.AddOrder("fullname", OrderType.Ascending);

					var users = executionContext.RetrieveAll(query, x => new SystemUser(x, executionContext, template));
					return users;
				}
			}
		}
	}

	public class UserRole
	{
        public UserRole(SystemUser user, Role role)
        {
			this.User = user;
			this.Role = role.ToEntityReference();
			this.Role.Name = role.name;
			this.BusinessUnit = role.businessunitid;
		}

        public EntityReference Role { get; }

		public SystemUser User { get; }

		public EntityReference BusinessUnit { get; }
	}
}
