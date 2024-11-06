using Greg.Xrm.Logging;
using Greg.Xrm.Model;
using Greg.Xrm.RoleEditor.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Views.Editor
{
	public class RoleEditorModel : ViewModel
	{
		private readonly Role role;
		private readonly TemplateForRole template;

		public RoleEditorModel(Role role, TemplateForRole template)
        {
			this.role = role;
			this.template = template;
			this.OverrideSetDefaultValue(() => Name, () => this.role.name);
			this.OverrideSetDefaultValue(() => Description, () => this.role.description);

			this.InheritedValues = new[]
			{
				new IsInheritedValue(true, "Direct User (Basic) access level and Team privileges"),
				new IsInheritedValue(false, "Team privileges only")
			};

			this.OverrideSetDefaultValue(() => IsInherited, () => this.InheritedValues.First(v => v.IsInherited == this.role.isinherited));
		}

		public void Initialize()
		{
			this.TableGroups.Clear();
			var group = new TableGroupModel("Generic");
			this.TableGroups.Add(group);

			foreach (var tableTemplate in this.template.Tables.Values.OrderBy(x => x.Name))
			{
				var privilegeIdList = tableTemplate.Select(x => x.Value.PrivilegeId).ToArray();

				var privilegesForCurrentTable = role.Privileges.Where(x => privilegeIdList.Contains(x.PrivilegeId)).ToArray();

				var tableModel = new TableModel(tableTemplate, privilegesForCurrentTable);
				group.Add(tableModel);
			}


			this.MiscGroups.Clear();
			var miscGroup = new MiscGroupModel("Miscellaneous");
			this.MiscGroups.Add(miscGroup);

			foreach (var miscTemplate in this.template.Misc.Select(x => x.Value).OrderBy(x => x.Name))
			{
				var currentPrivilege = role.Privileges.FirstOrDefault(x => x.PrivilegeName == miscTemplate.PrivilegeName);
				var miscModel = new MiscModel(miscTemplate, currentPrivilege);
				miscGroup.Add(miscModel);
			}
		}

		public Guid Id => this.role.Id;


		public string Name 
		{
			get => Get<string>();
			set => Set(value);
		}

		public string Description
		{
			get => Get<string>();
			set => Set(value);
		}


		public IsInheritedValue[] InheritedValues { get; }


		public IsInheritedValue IsInherited
		{
			get => Get<IsInheritedValue>();
			set => Set(value);
		}

		public List<TableGroupModel> TableGroups { get; } = new List<TableGroupModel>();

		public List<MiscGroupModel> MiscGroups { get; } = new List<MiscGroupModel>();

		
		public void Reload(ILog log, IOrganizationService crm)
		{
			this.role.ReadPrivileges(log, crm);
			this.Initialize();
		}

		public bool IsDirty => 
			this.TableGroups.Any(x => x.IsDirty) 
			|| this.MiscGroups.Any(x => x.IsDirty);
	}
}
