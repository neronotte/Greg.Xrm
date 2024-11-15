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
		private Entity entityChange;
		private readonly List<RolePrivilege> privilegesToAdd = new List<RolePrivilege>();
		private readonly List<RolePrivilege> privilegesToReplace = new List<RolePrivilege>();
		private readonly List<Guid> privilegesToRemove = new List<Guid>();
		private readonly List<IChangeOperation> changeList = new List<IChangeOperation>();

		public ChangeSummary(Guid roleId)
		{
			this.roleId = roleId;
		}

		public void Add(Entity entity)
		{
			this.entityChange = entity;
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
				Template = template,
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
				Metadata = metadata,
				Depth = v.Value
			});
		}

		public void RemovePrivilege(TemplateForGenericPrivilege template)
		{
			this.privilegesToRemove.Add(template.PrivilegeId);
			this.changeList.Add(new ChangeOperationRemove
			{
				Template = template
			});
		}

		public void RemovePrivilege(SecurityPrivilegeMetadata metadata)
		{
			this.privilegesToRemove.Add(metadata.PrivilegeId);
			this.changeList.Add(new ChangeOperationRemove
			{
				Metadata = metadata,
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
				Metadata = metadata,
				OldValue = oldValue.Value,
				NewValue = newValue.Value
			});
		}

		public void ReplacePrivilege(TemplateForGenericPrivilege template, PrivilegeDepth? oldValue, PrivilegeDepth? newValue)
		{
			this.privilegesToReplace.Add(new RolePrivilege
			{
				PrivilegeId = template.PrivilegeId,
				Depth = newValue.Value
			});

			this.changeList.Add(new ChangeOperationReplace
			{
				Template = template,
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


		public bool HasAnyPrivilegeChange()
		{
			return this.changeList.Count > 0;
		}


		public IReadOnlyList<OrganizationRequest> CreateRequest()
		{
			var requestList = new List<OrganizationRequest>();

			if (this.entityChange != null)
			{
				if (this.entityChange.Id == Guid.Empty)
				{
					var request1 = new CreateRequest
					{
						Target = this.entityChange
					};
					requestList.Add(request1);
				}
				else
				{
					var request1 = new UpdateRequest
					{
						Target = this.entityChange
					};
					requestList.Add(request1);
				}
			}

			if (this.privilegesToAdd.Count >0)
			{
				var request1 = new AddPrivilegesRoleRequest
				{
					RoleId = this.roleId,
					Privileges = this.privilegesToAdd.ToArray()
				};
				requestList.Add(request1);
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
					requestList.Add(request1);
				}
			}

			if (this.privilegesToReplace.Count > 0)
			{
				var request1 = new ReplacePrivilegesRoleRequest
				{
					RoleId = this.roleId,
					Privileges = this.privilegesToReplace.ToArray()
				};
				requestList.Add(request1);
			}

			return requestList;
		}
	}

	public class ChangeOperationAdd : IChangeOperation
	{
		public TemplateForGenericPrivilege Template { get; set; }
		public SecurityPrivilegeMetadata Metadata { get; set; }

		public string PrivilegeName => Metadata?.Name ?? Template?.PrivilegeName;

		public PrivilegeDepth Depth { get; set; }


		public string Text => ToString();

		public override string ToString()
		{
			return $"Add privilege \"{PrivilegeName}\" set to {Depth}";
		}
	}

	public class ChangeOperationReplace : IChangeOperation
	{
		public TemplateForGenericPrivilege Template { get; set; }
		public SecurityPrivilegeMetadata Metadata { get; set; }

		public string PrivilegeName => Metadata?.Name ?? Template?.PrivilegeName;
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
		public TemplateForGenericPrivilege Template { get; set; }
		public SecurityPrivilegeMetadata Metadata { get; set; }
		public string PrivilegeName => Metadata?.Name ?? Template?.PrivilegeName;


		public string Text => ToString();

		public override string ToString()
		{
			return $"Remove privilege \"{PrivilegeName}\"";
		}
	}
}