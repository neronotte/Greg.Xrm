using Greg.Xrm.Core;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Model
{
	/// <summary>
	/// This is the model object that represents a Role in the CRM.
	/// It carries around his own execution context to interact with the CRM service.
	/// This allows to have simultaneously opened multiple roles belonging to different CRM environments.
	/// </summary>

	[DebuggerDisplay("{name}")]
	public class Role : EntityWrapper
	{

		private Role(Entity entity, IXrmToolboxPluginContext executionContext, TemplateForRole template) : base(entity)
		{
			this.ExecutionContext = new XrmToolboxPluginContextLoggingDecorator(executionContext);
			this.Template = template;
		}

		public Role(IXrmToolboxPluginContext executionContext, TemplateForRole template) : base("role")
		{
			var preImage = ((IEntityWrapperInternal)this).GetPreImage();
			preImage["iscustomizable"] = new BooleanManagedProperty(true);
			this.ExecutionContext = new XrmToolboxPluginContextLoggingDecorator(executionContext);
			this.Template = template;
		}


		/// <summary>
		/// The object containing details about the environment connection.
		/// </summary>
		public IXrmToolboxPluginContext ExecutionContext { get; }


		/// <summary>
		/// Each role carries around also his own template
		/// </summary>
		public TemplateForRole Template { get; }


		/// <summary>
		/// The name of the role.
		/// </summary>
		public string name
		{
			get => Get<string>();
			set => SetValue(value);
		}

		/// <summary>
		/// A description of the role.
		/// </summary>
		public string description
		{
			get => Get<string>();
			set => SetValue(value);
		}

		/// <summary>
		/// The business unit that owns the role.
		/// </summary>
		public EntityReference businessunitid
		{
			get => Get<EntityReference>();
			set => SetValue(value);
		}


		public string businessunitidFormatted => this.businessunitid?.Name ?? GetFormatted(nameof(businessunitid));

		/// <summary>
		/// For inherited roles, the parent role.
		/// </summary>
		public EntityReference parentroleid
		{
			get => Get<EntityReference>();
			set => SetValue(value);
		}

		/// <summary>
		/// 0 - Team privileges only
		/// 1 - Direct User (Basic) access level and Team privileges
		/// </summary>
		public OptionSetValue isinherited
		{
			get => Get<OptionSetValue>();
			set => SetValue(value);
		}

		/// <summary>
		/// Indicates whether the role is customizable or not.
		/// </summary>
		public bool iscustomizable
		{
			get => Get<BooleanManagedProperty>()?.Value ?? false;
		}

		/// <summary>
		/// Indicates whether the role is managed or not.
		/// </summary>
		public bool ismanaged => Get<bool>();


		/// <summary>
		/// The list of privileges associated to the current role.
		/// Should be retrieved via ReadPrivileges method.
		/// </summary>
		public IReadOnlyList<RolePrivilege> Privileges { get; private set; } = new List<RolePrivilege>();


		/// <summary>
		/// Indicates whether the role has any privileges already assigned.
		/// </summary>
		public bool HasAssignedPrivileges => Privileges.Count > 0;


		/// <summary>
		/// Reloads the content of the current role.
		/// The newId must be provided only when the role is new and has been saved outside the current entity wrapper.
		/// </summary>
		/// <param name="newId">The generated id for the current role</param>
		public void Reload(Guid? newId = null)
		{
			if (IsNew)
			{
				if ((newId == null || newId == Guid.Empty)) return;
				((IEntityWrapperInternal)this).SetId(newId.Value);
			}

			var crm = this.ExecutionContext;
			var log = crm.Log;

			using (log.Track($"Reloading data of role <{name}> (env:{crm})"))
			{
				var query = new QueryExpression("role");
				query.ColumnSet.AddColumns("name", "description", "businessunitid", "iscustomizable", "ismanaged", "isinherited");
				query.Criteria.AddCondition("roleid", ConditionOperator.Equal, this.Id);
				query.NoLock = true;
				query.TopCount = 1;

				var response = crm.RetrieveMultiple(query);

				var roleEntity = response.Entities.FirstOrDefault();
				if (roleEntity == null)
				{
					log.Error($"Something went wrong: role <{name}> not found in the CRM.");
					return;
				}


				((IEntityWrapperInternal)this).Reload(roleEntity);
			}

			this.ReadPrivileges();
		}



		/// <summary>
		/// Initializes the list of privileges for the current role.
		/// </summary>
		public void ReadPrivileges()
		{
			if (IsNew) return;

			var log = this.ExecutionContext.Log;
			var crm = this.ExecutionContext;

			using (log.Track($"Reading privileges for role <{name}>"))
			{
				var request = new RetrieveRolePrivilegesRoleRequest
				{
					RoleId = this.Id,
				};

				var response = (RetrieveRolePrivilegesRoleResponse)crm.Execute(request);
				this.Privileges = response.RolePrivileges;
			}
		}





		/// <summary>
		/// When creating a new role starting from an existing one, 
		/// this method clones the privileges from the other role.
		/// </summary>
		/// <param name="other"></param>
		public void ClonePrivilegesFrom(Role other)
		{
			var privilegeList = other.Privileges.Select(x => new RolePrivilege
			{
				Depth = x.Depth,
				PrivilegeId = x.PrivilegeId,
				PrivilegeName = x.PrivilegeName,
			}).ToList();

			this.Privileges = privilegeList;
		}



		public override string ToString()
		{
			if (this.ExecutionContext == null)
				return this.name;

			return $"{this.name} ({this.ExecutionContext.Details.ConnectionName})";
		}










		public class Repository : IRoleRepository
		{
			private readonly ILog log;
			private readonly IMessenger messenger;

			public Repository(ILog log, IMessenger messenger)
			{
				this.log = log;
				this.messenger = messenger;
			}


			public IReadOnlyList<Role> GetParentRoles(IXrmToolboxPluginContext executionContext, TemplateForRole template)
			{
				if (executionContext == null)
					throw new ArgumentNullException(nameof(executionContext));
				if (template == null)
					throw new ArgumentNullException(nameof(template));


				var query = new QueryExpression("role");
				query.ColumnSet.AddColumns("name", "description", "businessunitid", "iscustomizable", "ismanaged", "isinherited", "parentroleid");
				query.Criteria.AddCondition("parentroleid", ConditionOperator.Null);
				query.AddOrder("name", OrderType.Ascending);
				query.NoLock = true;

				// each role carries around his own execution context.
				return executionContext.RetrieveAll(query, x => new Role(x, executionContext, template));
			}

			public IReadOnlyList<Role> GetRolesByPrivilege(IXrmToolboxPluginContext executionContext, string privilegeName, TemplateForRole template)
			{
				if (executionContext == null)
					throw new ArgumentNullException(nameof(executionContext));
				if (string.IsNullOrWhiteSpace(privilegeName))
					throw new ArgumentNullException(nameof(privilegeName));


				var query = new QueryExpression("role");
				query.ColumnSet.AddColumns("name", "description", "businessunitid", "iscustomizable", "ismanaged", "isinherited", "parentroleid");
				query.Criteria.AddCondition("parentroleid", ConditionOperator.Null);

				var rolePrivilegeLink = query.AddLink("roleprivileges", "roleid", "roleid");
				var privilegeLink = rolePrivilegeLink.AddLink("privilege", "privilegeid", "privilegeid");
				privilegeLink.LinkCriteria.AddCondition("name", ConditionOperator.Equal, privilegeName);

				query.AddOrder("name", OrderType.Ascending);


				// each role carries around his own execution context.
				return executionContext.RetrieveAll(query, x => new Role(x, executionContext, template));
			}

			public IReadOnlyList<Role> GetRolesBySolution(IXrmToolboxPluginContext executionContext, EntityReference solutionRef, TemplateForRole template)
			{
				if (executionContext == null)
					throw new ArgumentNullException(nameof(executionContext));
				if (solutionRef == null)
					throw new ArgumentNullException(nameof(solutionRef));


				var query = new QueryExpression("role");
				query.ColumnSet.AddColumns("name", "description", "businessunitid", "iscustomizable", "ismanaged", "isinherited", "parentroleid");
				query.Criteria.AddCondition("parentroleid", ConditionOperator.Null);

				var solutioncomponent = query.AddLink("solutioncomponent", "roleid", "objectid");
				solutioncomponent.LinkCriteria.AddCondition("solutionid", ConditionOperator.Equal, solutionRef.Id);

				query.AddOrder("name", OrderType.Ascending);


				// each role carries around his own execution context.
				return executionContext.RetrieveAll(query, x => new Role(x, executionContext, template));
			}

			public IReadOnlyList<Role> GetRolesByUser(IXrmToolboxPluginContext executionContext, EntityReference userRef, TemplateForRole template)
			{
				if (executionContext == null)
					throw new ArgumentNullException(nameof(executionContext));
				if (userRef == null)
					throw new ArgumentNullException(nameof(userRef));


				var query = new QueryExpression("role");
				query.ColumnSet.AddColumns("name", "description", "businessunitid", "iscustomizable", "ismanaged", "isinherited", "parentroleid");

				var suLink = query.AddLink("systemuserroles", "roleid", "roleid");
				suLink.LinkCriteria.AddCondition("systemuserid", ConditionOperator.Equal, userRef.Id);

				query.AddOrder("name", OrderType.Ascending);

				// each role carries around his own execution context.
				return executionContext.RetrieveAll(query, x => new Role(x, executionContext, template));
			}



			public IReadOnlyList<Role> GetRolesByNamesAndBusinessUnit(IXrmToolboxPluginContext executionContext, List<Tuple<string, EntityReference>> tuplesToFetch, TemplateForRole template)
			{
				if (executionContext == null)
					throw new ArgumentNullException(nameof(executionContext));
				if (tuplesToFetch == null)
					throw new ArgumentNullException(nameof(tuplesToFetch));
				if (tuplesToFetch.Count == 0)
					throw new ArgumentException("The list of tuples to fetch cannot be empty", nameof(tuplesToFetch));

				if (tuplesToFetch.Any(x => string.IsNullOrWhiteSpace(x.Item1) || x.Item2 == null))
					throw new ArgumentException("The list of tuples to fetch cannot contain null values", nameof(tuplesToFetch));

				var query = new QueryExpression("role");
				query.ColumnSet.AddColumns("name", "description", "businessunitid", "iscustomizable", "ismanaged", "isinherited", "parentroleid");

				query.Criteria.FilterOperator = LogicalOperator.Or;
				foreach (var tuple in tuplesToFetch)
				{
					var subFilter = query.Criteria.AddFilter(LogicalOperator.And);
					subFilter.AddCondition("name", ConditionOperator.Equal, tuple.Item1);
					subFilter.AddCondition("businessunitid", ConditionOperator.Equal, tuple.Item2.Id);
				}

				query.AddOrder("name", OrderType.Ascending);
				query.AddOrder("businessunitid", OrderType.Ascending);

				// each role carries around his own execution context.
				return executionContext.RetrieveAll(query, x => new Role(x, executionContext, template));
			}
		}
	}
}
