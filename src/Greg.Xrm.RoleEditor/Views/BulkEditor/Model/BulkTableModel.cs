using Greg.Xrm.Logging;
using Greg.Xrm.Model;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Common;
using Microsoft.Xrm.Sdk.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Views.BulkEditor.Model
{
	public class BulkTableModel : ViewModel, IPrivilegeHolder, IEditorChild
	{
		private readonly Dictionary<PrivilegeType, Level?> currentPrivilegeValues = new Dictionary<PrivilegeType, Level?>();
		private readonly ITemplateForTable template;
		private readonly Role[] roleList;



		public BulkTableModel(ITemplateForTable tableTemplate, Role[] roleList)
		{
			this.template = tableTemplate ?? throw new ArgumentNullException(nameof(tableTemplate));
			this.roleList = roleList ?? throw new ArgumentNullException(nameof(roleList));

			foreach (var metadata in this.template.GetAllPrivileges())
			{
				this.currentPrivilegeValues[metadata.PrivilegeType] = null;
			}
		}
		public Level? this[PrivilegeType privilegeType]
		{
			get => Get(privilegeType);
			set => Set(privilegeType, value);
		}

		public object Parent { get; set; }

		public string Name => this.template.Name;

		public string LogicalName => this.template.LogicalName;





		public bool IsValid(PrivilegeType privilegeType)
		{
			return this.currentPrivilegeValues.ContainsKey(privilegeType);
		}

		public bool[] GetPrivilegeLevelValidityMatrix(PrivilegeType privilege)
		{
			var privilegeMetadata = template[privilege];
			if (privilegeMetadata == null) return new bool[0];
			return privilegeMetadata.GetValidLevels();
		}

		public bool TryGetPrivilegeName(PrivilegeType privilege, out string name)
		{
			name = null;
			if (template[privilege] == null) return false;

			name = template[privilege].Name;
			return true;
		}

		public bool IsDirty => this.currentPrivilegeValues.Values.Any(x => x != null);



		public bool CheckDirty(PrivilegeType privilegeType)
		{
			if (this.currentPrivilegeValues.TryGetValue(privilegeType, out var level))
			{
				return level.HasValue;
			}
			return false;
		}


		public void Remove(PrivilegeType privilegeType)
		{
			if (this.currentPrivilegeValues.ContainsKey(privilegeType))
			{
				this.currentPrivilegeValues[privilegeType] = null;
			}
		}

		public void IncreaseAll()
		{
			foreach (var privilege in template.Select(x => x.Key))
			{
				Increase(privilege);
			}
		}

		public void Increase(PrivilegeType privilegeType)
		{
			var privilegeMetadata = template[privilegeType];
			if (privilegeMetadata == null) return;


			var nextValue = Get(privilegeType);
			if (!nextValue.HasValue)
			{
				nextValue = Level.None;
				this.currentPrivilegeValues[privilegeType] = nextValue;
				this.OnPropertyChanged(nameof(IsDirty), this.IsDirty);
				return;
			}



			var i = 0;
			var nextIntValue = (int)nextValue;
			do
			{
				nextIntValue = (nextIntValue + 1) % 6;
				nextValue = nextIntValue == 5 ? null : (Level?)nextIntValue;
				i++;
			}
			while (nextValue.HasValue && !privilegeMetadata.IsValidLevel(nextValue.Value));

			this.currentPrivilegeValues[privilegeType] = nextValue;
			this.OnPropertyChanged(nameof(IsDirty), this.IsDirty);
		}



		public Level? Get(PrivilegeType privilegeType)
		{
			if (this.currentPrivilegeValues.TryGetValue(privilegeType, out var level))
			{
				return level;
			}

			return null;
		}

		public void Set(PrivilegeType privilegeType, Level? level)
		{
			if (!this.currentPrivilegeValues.ContainsKey(privilegeType)) return;

			this.currentPrivilegeValues[privilegeType] = level;
			this.OnPropertyChanged(nameof(IsDirty), this.IsDirty);
		}



		/// <summary>
		/// This method generates the a command string that can be used to clone the current table profiles to other tables
		/// </summary>
		/// <returns>A string</returns>
		public string GenerateConfigurationCommand()
		{
			var command = PrivilegeConfigForTableMemento.CreateNew();
			foreach (var privilegeType in this.currentPrivilegeValues.Keys)
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
					if (kvp.Value != null)
					{
						Set(kvp.Key, kvp.Value);
					}
				}
			}
			catch (Exception ex)
			{
				log.Error("Error while applying the configuration command: " + ex.Message, ex);
			}
		}


		public void CalculateChanges(BulkChangeSummary summary)
		{

			foreach (var kvp in this.currentPrivilegeValues)
			{
				if (kvp.Value == null) continue; // the value for the specified privilege is unchanged, doing nothing

				var privilegeType = kvp.Key;
				var privilegeValue = kvp.Value.Value;

				var metadata = this.template[privilegeType];

				foreach (var role in this.roleList)
				{
					var currentValueForRole = role.Privileges.FirstOrDefault(x => x.PrivilegeId == metadata.PrivilegeId);
					if (currentValueForRole == null)
					{
						// nothing to do, the previous value is empty, and the current it's still empty
						if (privilegeValue == Level.None)
							continue;

						summary.Add(role, metadata.PrivilegeId, metadata.Name, privilegeValue);
						continue;
					}


					// if we're here it means we already have a value for the privilege

					// if the two values are equal, do nothing
					if (currentValueForRole.GetLevel() == privilegeValue)
						continue;

					if (privilegeValue == Level.None)
					{
						summary.Remove(role, metadata.PrivilegeId, metadata.Name);
						continue;
					}

					summary.Replace(role, metadata.PrivilegeId, metadata.Name, currentValueForRole.GetLevel(), privilegeValue);
				}
			}
		}
	}
}
