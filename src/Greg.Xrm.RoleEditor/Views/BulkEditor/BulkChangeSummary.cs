using Greg.Xrm.RoleEditor.Model;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Greg.Xrm.RoleEditor.Views.BulkEditor
{
	public class BulkChangeSummary: IEnumerable<IBulkChangeOperation>
	{
		private readonly List<IBulkChangeOperation> changes = new List<IBulkChangeOperation>();




		public void Add(Role role, Guid privilegeId, string privilegeName, Level value)
		{
			this.changes.Add(new ChangePrivilegeAdd(role, privilegeId, privilegeName, value));
		}

		internal void Remove(Role role, Guid privilegeId, string privilegeName)
		{
			this.changes.Add(new ChangePrivilegeRemove(role, privilegeId, privilegeName));
		}

		internal void Replace(Role role, Guid privilegeId, string privilegeName, Level oldValue, Level newValue)
		{
			this.changes.Add(new ChangePrivilegeReplace(role, privilegeId, privilegeName, oldValue, newValue));
		}


		public IReadOnlyList<TreeNode> ToTree()
		{
			return this.changes.GroupBy(r => r.Role)
				.Select(r => new TreeNode(
					"Role: " + r.Key.name, 
					r.Key.description, 
					"role", 
					r.GroupBy(o => o.OperationType).Select(o => new TreeNode(
						o.Key,
						$"{o.Count()} operations",
						o.Key.ToLowerInvariant(), 
						o.Select(c => new TreeNode(c.Text, icon: c.OperationType.ToLowerInvariant())))
					)
				))
				.ToList();
		}


			#region IEnumerable implementation
			public IEnumerator<IBulkChangeOperation> GetEnumerator()
		{
			return this.changes.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion


		public bool HasAnyPrivilegeChange()
		{
			return this.changes.Count > 0;
		}

		public int GetChangedRoleCount()
		{
			return this.changes.Select(x => x.Role.Id).Distinct().Count();
		}

		public Dictionary<Role, List<OrganizationRequest>> CreateRequests()
		{
			var result = new Dictionary<Role, List<OrganizationRequest>>();
			foreach (var changesByRole in this.changes.GroupBy(x => x.Role))
			{
				var role = changesByRole.Key;
				var requestList = new List<OrganizationRequest>();


				// mananging adds
				var addList = changesByRole.OfType<ChangePrivilegeAdd>();
				var addRequest = new AddPrivilegesRoleRequest
				{
					RoleId = role.Id,
					Privileges = addList.Select(x => new RolePrivilege
					{
						Depth = x.Value,
						PrivilegeId = x.PrivilegeId,
						PrivilegeName = x.PrivilegeName,
					}).ToArray()
				};
				if (addRequest.Privileges.Length > 0)
					requestList.Add(addRequest);


				// managing removals
				var removeList = changesByRole.OfType<ChangePrivilegeRemove>();
				foreach (var remove in removeList)
				{
					var removeRequest = new RemovePrivilegeRoleRequest();
					removeRequest.RoleId = role.Id;
					removeRequest.PrivilegeId = remove.PrivilegeId;

					requestList.Add(removeRequest);
				}


				// managing replacements
				var replaceList = changesByRole.OfType<ChangePrivilegeReplace>();
				var replaceRequest = new ReplacePrivilegesRoleRequest
				{
					RoleId = role.Id,
					Privileges = replaceList.Select(x => new RolePrivilege
					{
						Depth = x.NewValue,
						PrivilegeId = x.PrivilegeId,
						PrivilegeName = x.PrivilegeName,
					}).ToArray()
				};
				if (replaceRequest.Privileges.Length > 0)
					requestList.Add(replaceRequest);


				result[role] = requestList;
			}
			return result;
		}

		class ChangePrivilegeAdd : IBulkChangeOperation
		{

			public ChangePrivilegeAdd(Role role, Guid privilegeId, string privilegeName, Level value)
			{
				Role = role;
				PrivilegeId = privilegeId;
				PrivilegeName = privilegeName;
				Value = value.ToPrivilegeDepth().GetValueOrDefault();
			}

			public string Text => $"Add privilege {PrivilegeName} with value {Value} onto role {Role.name}";

			public Role Role { get; }
			public Guid PrivilegeId { get; }
			public string PrivilegeName { get; }
			public PrivilegeDepth Value { get; }

			public string OperationType => "Add";
		}


		class ChangePrivilegeRemove : IBulkChangeOperation
		{

			public ChangePrivilegeRemove(Role role, Guid privilegeId, string privilegeName)
			{
				Role = role;
				PrivilegeId = privilegeId;
				PrivilegeName = privilegeName;
			}

			public string Text => $"Remove privilege {PrivilegeName} from role {Role.name}";

			public Role Role { get; }
			public Guid PrivilegeId { get; }
			public string PrivilegeName { get; }

			public string OperationType => "Remove";
		}
		class ChangePrivilegeReplace : IBulkChangeOperation
		{

			public ChangePrivilegeReplace(Role role, Guid privilegeId, string privilegeName, Level oldValue, Level newValue)
			{
				Role = role;
				PrivilegeId = privilegeId;
				PrivilegeName = privilegeName;
				OldValue = oldValue.ToPrivilegeDepth().GetValueOrDefault();
				NewValue = newValue.ToPrivilegeDepth().GetValueOrDefault();
			}

			public string Text => $"Change privilege {PrivilegeName} from {OldValue} to {NewValue} onto role {Role.name}";

			public Role Role { get; }
			public Guid PrivilegeId { get; }
			public string PrivilegeName { get; }
			public PrivilegeDepth OldValue { get; }
			public PrivilegeDepth NewValue { get; }

			public string OperationType => "Replace";
		}
	}
}
