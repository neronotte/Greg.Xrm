using Greg.Xrm.Core;
using Greg.Xrm.Model;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Services;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Views.Editor
{
	public class RoleModel : ViewModel
	{
		private readonly Role role;
		private readonly TemplateForRole template;
		private readonly IPrivilegeClassificationProvider privilegeClassificationProvider;

		public RoleModel(
			Role role, 
			TemplateForRole template, 
			IPrivilegeClassificationProvider privilegeClassificationProvider)
        {
			this.role = role;
			this.template = template;
			this.privilegeClassificationProvider = privilegeClassificationProvider;
			this.InheritedValues = new[]
			{
				new IsInheritedValue(1, "Direct User (Basic) access level and Team privileges"),
				new IsInheritedValue(0, "Team privileges only")
			};


			this.CanChangeBusinessUnit = new ChangeBusinessUnitCommand(role.ExecutionContext, this, this.role.IsNew);

			this.Initialize();

			this.WhenChanges(() => BusinessUnit).ChangesAlso(() => BusinessUnitName);
			this.WhenChanges(() => Name).ChangesAlso(() => IsDirty);
			this.WhenChanges(() => Description).ChangesAlso(() => IsDirty);
			this.WhenChanges(() => BusinessUnit).ChangesAlso(() => IsDirty);
			this.WhenChanges(() => IsInherited).ChangesAlso(() => IsDirty);
		}



		private void Initialize()
		{
			this.Name = this.role.name;
			this.Description = this.role.description;
			this.BusinessUnit = this.role.businessunitid;
			this.IsInherited = this.InheritedValues.First(v => v.Value == this.role.isinherited?.Value);


			this.TableGroups.Clear();

			var tableMap = this.privilegeClassificationProvider.GetForTablePrivileges();
			var reverseTableMap = tableMap.CreateReverseMap();


			var tableGroupDict = new Dictionary<string, TableGroupModel>();
			foreach (var name in tableMap.Keys)
			{
				var g = new TableGroupModel(name);
				tableGroupDict[name] = g;
			}
			tableGroupDict["General"] = new TableGroupModel("General");




			foreach (var tableTemplate in this.template.Tables.Values.OrderBy(x => x.Name))
			{
				var privilegeIdList = tableTemplate.Select(x => x.Value.PrivilegeId).ToArray();

				var privilegesForCurrentTable = role.Privileges.Where(x => privilegeIdList.Contains(x.PrivilegeId)).ToArray();

				var tableModel = new TableModel(tableTemplate, privilegesForCurrentTable, role.IsNew);

				if (reverseTableMap.TryGetValue(tableModel.Tooltip, out var groupNames) && groupNames.Count > 0)
				{
					tableGroupDict[groupNames[0]].Add(tableModel);
				}
				else
				{
					tableGroupDict["General"].Add(tableModel);
				}
			}

			foreach (var g in tableGroupDict.Values)
			{
				if (g.Count > 0)
				{
					this.TableGroups.Add(g);
				}
			}







			this.MiscGroups.Clear();

			var miscMap = this.privilegeClassificationProvider.GetForMiscPrivileges();
			var reverseMiscMap = miscMap.CreateReverseMap();

			var miscGroupDict = new Dictionary<string, MiscGroupModel>();
			foreach (var name in miscMap.Keys)
			{
				var g = new MiscGroupModel(name);
				miscGroupDict[name] = g;
			}
			miscGroupDict["Miscellaneous"] = new MiscGroupModel("Miscellaneous");




			foreach (var miscTemplate in this.template.Misc.Select(x => x.Value).OrderBy(x => x.Name))
			{
				var privilegeName = miscTemplate.PrivilegeName;

				var currentPrivilege = role.Privileges.FirstOrDefault(x => x.PrivilegeName == privilegeName);
				var miscModel = new MiscModel(miscTemplate, currentPrivilege, role.IsNew);

				if (reverseMiscMap.TryGetValue(privilegeName, out var groupNames) && groupNames.Count > 0)
				{
					miscGroupDict[groupNames[0]].Add(miscModel);
				}
				else
				{
					miscGroupDict["Miscellaneous"].Add(miscModel);
				}
			}

			foreach (var g in miscGroupDict.Values)
			{
				if (g.Count > 0)
				{
					this.MiscGroups.Add(g);
				}
			}
		}


		public IXrmToolboxPluginContext GetContext()
		{
			return this.role.ExecutionContext;
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




		public string BusinessUnitName => BusinessUnit?.Name;

		public EntityReference BusinessUnit
		{
			get => Get<EntityReference>();
			set => Set(value);
		}


		public ICommand CanChangeBusinessUnit { get; }



		public IsInheritedValue[] InheritedValues { get; }


		public IsInheritedValue IsInherited
		{
			get => Get<IsInheritedValue>();
			set => Set(value);
		}

		public List<TableGroupModel> TableGroups { get; } = new List<TableGroupModel>();

		public List<MiscGroupModel> MiscGroups { get; } = new List<MiscGroupModel>();

		




		public void CommitChanges(Guid? id)
		{
			this.role.Reload(id);
			this.Initialize();
		}




		public void CalculateChanges(ChangeSummary summary)
		{
			var entity = new Entity("role");
			if (this.Id == Guid.Empty)
			{
				entity["name"] = this.Name;
				entity["description"] = this.Description;
				entity["businessunitid"] = this.BusinessUnit;
				entity["isinherited"] = new OptionSetValue(this.IsInherited.Value);
			}
			else
			{
				entity.Id = role.Id;

				if (this.Name != this.role.name)
				{
					entity["name"] = this.Name;
				}
				if (this.Description != this.role.description)
				{
					entity["description"] = this.Description;
				}
				if (this.BusinessUnit != null && this.BusinessUnit?.Id != this.role.businessunitid?.Id)
				{
					entity["businessunitid"] = this.BusinessUnit;
				}
				if (this.IsInherited?.Value != this.role.isinherited.Value)
				{
					entity["isinherited"] = new OptionSetValue(this.IsInherited.Value);
				}
			}


			if ( entity.Attributes.Count > 0)
			{
				summary.Add(entity);
			}
		}

		internal void EvaluateDirty()
		{
			this.OnPropertyChanged(nameof(IsDirty), IsDirty);
		}

		public bool IsDirty => 
			this.role.IsNew
			|| this.TableGroups.Exists(x => x.IsDirty) 
			|| this.MiscGroups.Exists(x => x.IsDirty)
			|| this.Name != this.role.name
			|| this.Description != this.role.description
			|| this.BusinessUnit?.Id != this.role.businessunitid?.Id
			|| this.IsInherited?.Value != this.role.isinherited.Value;
	}
}
