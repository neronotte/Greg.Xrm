using Greg.Xrm.Logging;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Common;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Views.Editor
{
	public class TableModel : IPrivilegeHolder
	{
		private readonly Dictionary<PrivilegeType, Level> preImage = new Dictionary<PrivilegeType, Level>();
		private readonly Dictionary<PrivilegeType, Level> target = new Dictionary<PrivilegeType, Level>();
		private readonly ITemplateForTable template;

		public TableModel(ITemplateForTable template, RolePrivilege[] currentPrivileges, bool isNew)
		{
			this.template = template;

			if (isNew)
			{
				foreach (var kvp in template)
				{
					var currentPrivilege = Array.Find(currentPrivileges, x => kvp.Value.PrivilegeId == x.PrivilegeId);

					preImage[kvp.Key] = Level.None;
					Set(kvp.Key, currentPrivilege.GetLevel());
				}
			}
			else
			{
				foreach (var kvp in template)
				{
					preImage[kvp.Key] = Array.Find(currentPrivileges, x => kvp.Value.PrivilegeId == x.PrivilegeId).GetLevel();
				}
				target.Clear();
			}

			this.Name = this.template.Name;
			this.Tooltip = this.template.LogicalName;
		}

		public TableGroupModel Parent { get; set; }


		public string Name { get; set; }

		public string Tooltip { get; set; }

		public bool IsDirty => this.target.Count > 0;

		public Level? this[PrivilegeType privilegeType]
		{
			get => Get(privilegeType);
			set => Set(value, privilegeType);
		}



		public bool HasAssignedPrivileges
		{
			get
			{
				return this.target.Any(x => x.Value != Level.None) || this.preImage.Any(x => x.Value != Level.None);
			}
		}


		public string GetPrivilegeName(PrivilegeType privilegeType)
		{
			if (!TryGetPrivilegeName(privilegeType, out var name))
				return null;

			return name;
		}

		public bool TryGetPrivilegeName(PrivilegeType privilege, out string name)
		{
			name = null;
			if (template[privilege] == null) return false;

			name = template[privilege].Name;
			return true;
		}

		public bool[] GetPrivilegeLevelValidityMatrix(PrivilegeType privilege)
		{
			var privilegeMetadata = template[privilege];
			if (privilegeMetadata == null) return new bool[0];
			return privilegeMetadata.GetValidLevels();
		}

		public bool IsChanged(PrivilegeType privilege)
		{
			return this.target.ContainsKey(privilege);
		}

		public void IncreaseAll()
		{
			foreach (var privilege in template.Select(x => x.Key))
			{
				Increase(privilege);
			}
		}

		public void Increase(PrivilegeType privilege)
		{
			var privilegeMetadata = template[privilege];
			if (privilegeMetadata == null) return;


			Level nextValue = Get(privilege) ?? Level.None;

			var i = 0;
			do
			{
				nextValue = (Level)((((int)nextValue) + 1) % 5);
				i++;
			}
			while (!privilegeMetadata.IsValidLevel(nextValue) && i <= 5);

			Set(nextValue, privilege);
		}



		public void Set(PrivilegeType privilege, Level level)
		{
			Set(level, privilege);
		}




		private Level? Get(PrivilegeType privilege)
		{
			if (template[privilege] == null) return null;

			if (this.target.ContainsKey(privilege))
				return this.target[privilege];

			return this.preImage[privilege];
		}


		private void Set(Level? value, PrivilegeType privilege)
		{
			if (template[privilege] == null) return;

			// here i need to check if the value is actually valid for the privilege
			// if is not a valid level, we don't perform any change.
			var privilegeMetadata = this.template[privilege];
			if (value != null && !privilegeMetadata.IsValidLevel(value.Value))
				return;

			if (value == preImage[privilege])
				this.target.Remove(privilege);
			else
				this.target[privilege] = value ?? Level.None;
		}


		/// <summary>
		/// This method generates the a command string that can be used to clone the current table profiles to other tables
		/// </summary>
		/// <returns>A string</returns>
		public string GenerateConfigurationCommand()
		{
			var command = PrivilegeConfigForTableMemento.CreateNew();
			foreach (var privilegeType in this.preImage.Keys)
			{
				command.Levels[privilegeType] = Get(privilegeType);
			}

			return JsonConvert.SerializeObject(command);
		}


		public void ApplyConfigurationCommand(ILog log, string commandText)
		{
			try
			{
				var command = JsonConvert.DeserializeObject<PrivilegeConfigForTableMemento>(commandText);
				if (!command.IsValid)
					return;

				if (command.Levels == null) return;

				foreach (var kvp in command.Levels)
				{
					Set(kvp.Value, kvp.Key);
				}
			}
			catch (Exception ex)
			{
				log.Error("Error while applying the configuration command: " + ex.Message, ex);
			}
		}

		public void CalculateChanges(ChangeSummary summary)
		{
			foreach (var privilegeType in this.preImage.Keys)
			{
				var original = preImage[privilegeType];
				if (!target.TryGetValue(privilegeType, out var current)) continue; // the privilege has not been changed

				if (original == Level.None)
				{
					summary.AddPrivilege(template[privilegeType], current.ToPrivilegeDepth());
				}
				else if (current == Level.None)
				{
					summary.RemovePrivilege(template[privilegeType]);
				}
				else
				{
					summary.ReplacePrivilege(template[privilegeType], original.ToPrivilegeDepth(), current.ToPrivilegeDepth());
				}
			}
		}
	}
}
