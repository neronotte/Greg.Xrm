using Greg.Xrm.RoleEditor.Model;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Views.Editor
{
	public class ChangeSummary : IEnumerable<IChangeOperation>
	{
		private readonly Guid roleId;
		private readonly List<RolePrivilege> privilegesToAdd = new List<RolePrivilege>();
		private readonly List<RolePrivilege> privilegesToReplace = new List<RolePrivilege>();
		private readonly List<Guid> privilegesToRemove = new List<Guid>();
		private readonly List<IChangeOperation> changeList = new List<IChangeOperation>();

		public ChangeSummary(Guid roleId)
		{
			this.roleId = roleId;
		}


		public void AddPrivilege(TemplateForGenericPrivilege template, PrivilegeDepth? v)
		{
			this.privilegesToAdd.Add(new RolePrivilege
			{
				PrivilegeId = template.PrivilegeId,
				Depth = v.Value
			});

			this.changeList.Add(new ChangeOperationAdd
			{
				PrivilegeName = template.PrivilegeName,
				Depth = v.Value
			});
		}

		public void AddPrivilege(SecurityPrivilegeMetadata metadata, PrivilegeDepth? v)
		{
			this.privilegesToAdd.Add(new RolePrivilege
			{
				PrivilegeId = metadata.PrivilegeId,
				Depth = v.Value
			});

			this.changeList.Add(new ChangeOperationAdd
			{
				PrivilegeName = metadata.Name,
				Depth = v.Value
			});
		}

		public void RemovePrivilege(TemplateForGenericPrivilege template)
		{
			this.privilegesToRemove.Add(template.PrivilegeId);
			this.changeList.Add(new ChangeOperationRemove
			{
				PrivilegeName = template.PrivilegeName
			});
		}

		public void RemovePrivilege(SecurityPrivilegeMetadata metadata)
		{
			this.privilegesToRemove.Add(metadata.PrivilegeId);
			this.changeList.Add(new ChangeOperationRemove
			{
				PrivilegeName = metadata.Name
			});
		}

		public void ReplacePrivilege(SecurityPrivilegeMetadata metadata, PrivilegeDepth? oldValue, PrivilegeDepth? newValue)
		{
			this.privilegesToReplace.Add(new RolePrivilege
			{
				PrivilegeId = metadata.PrivilegeId,
				Depth = newValue.Value
			});

			this.changeList.Add(new ChangeOperationReplace
			{
				PrivilegeName = metadata.Name,
				OldValue = oldValue.Value,
				NewValue = newValue.Value
			});
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<IChangeOperation> GetEnumerator()
		{
			return this.changeList.GetEnumerator();
		}



		public ExecuteTransactionRequest CreateRequest()
		{
			var request = new ExecuteTransactionRequest();
			request.ReturnResponses = true;
			request.Requests = new OrganizationRequestCollection();

			if (this.privilegesToAdd.Count >0)
			{
				var request1 = new AddPrivilegesRoleRequest
				{
					RoleId = this.roleId,
					Privileges = this.privilegesToAdd.ToArray()
				};
				request.Requests.AddRange(request1);
			}

			if (this.privilegesToRemove.Count > 0)
			{
				foreach (var privilegeId in this.privilegesToRemove)
				{
					var request1 = new RemovePrivilegeRoleRequest
					{
						RoleId = this.roleId,
						PrivilegeId = privilegeId
					};
					request.Requests.Add(request1);
				}
			}

			if (this.privilegesToReplace.Count > 0)
			{
				var request1 = new ReplacePrivilegesRoleRequest
				{
					RoleId = this.roleId,
					Privileges = this.privilegesToReplace.ToArray()
				};
				request.Requests.Add(request1);
			}

			return request;
		}
	}

	public class ChangeOperationAdd : IChangeOperation
	{
		public string PrivilegeName { get; set; }

		public PrivilegeDepth Depth { get; set; }


		public string Text => ToString();

		public override string ToString()
		{
			return $"Add privilege \"{PrivilegeName}\" set to {Depth}";
		}
	}

	public class ChangeOperationReplace : IChangeOperation
	{
		public string PrivilegeName { get; set; }
		public PrivilegeDepth OldValue { get; set; }

		public PrivilegeDepth NewValue { get; set; }


		public string Text => ToString();

		public override string ToString()
		{
			return $"Change privilege \"{PrivilegeName}\" from {OldValue} to {NewValue}";
		}
	}

	public class ChangeOperationRemove : IChangeOperation
	{
		public string PrivilegeName { get; set; }


		public string Text => ToString();

		public override string ToString()
		{
			return $"Remove privilege \"{PrivilegeName}\"";
		}
	}

	public interface IChangeOperation
	{
		string Text { get; }
	}
}