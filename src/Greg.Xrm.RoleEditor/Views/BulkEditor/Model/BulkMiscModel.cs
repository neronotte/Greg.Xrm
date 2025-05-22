using Greg.Xrm.Logging;
using Greg.Xrm.Model;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Common;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Views.BulkEditor.Model
{
	public class BulkMiscModel : ViewModel, IEditorChild
	{
		private readonly TemplateForGenericPrivilege template;
		private readonly Role[] roleList;
		private Level? currentValue;

		public BulkMiscModel(TemplateForGenericPrivilege template, Role[] roleList)
		{
			this.template = template;
			this.roleList = roleList;
		}

		public object Parent { get; set; }

		public string Name => this.template.Name;

		public Level? Value => this.currentValue;

		public string Tooltip => this.template.PrivilegeName;


		public bool IsDirty => this.currentValue.HasValue;




		public void Increase()
		{
			var nextValue = this.currentValue;
			if (!nextValue.HasValue)
			{
				nextValue = Level.None;
				this.currentValue = nextValue;
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
			while (nextValue.HasValue && !template.IsValidLevel(nextValue.Value));

			this.currentValue = nextValue;
			this.OnPropertyChanged(nameof(IsDirty), this.IsDirty);
		}

		public void Set(Level? level)
		{
			this.currentValue = level;
			this.OnPropertyChanged(nameof(IsDirty), this.IsDirty);
		}


		public bool[] GetPrivilegeLevelValidityMatrix()
		{
			var result = new bool[5];
			for (var i = 0; i < 5; i++)
			{
				result[i] = this.template.IsValidLevel((Level)i);
			}
			return result;
		}

		public string GenerateConfigurationCommand()
		{
			var command = PrivilegeConfigForMiscMemento.CreateNew();
			command.Level = this.currentValue;
			return JsonConvert.SerializeObject(command);
		}

		public void ApplyConfigurationCommand(ILog log, string commandText)
		{
			try
			{
				var command = JsonConvert.DeserializeObject<PrivilegeConfigForMiscMemento>(commandText);
				if (!command.IsValid)
					return;

				Set(command.Level);
			}
			catch (Exception ex)
			{
				log.Error("Error while applying the configuration command: " + ex.Message, ex);
			}
		}

		public void CalculateChanges(BulkChangeSummary summary)
		{
			if (this.Value == null) // the value is unchanged, do nothing
				return;

			foreach (var role in this.roleList)
			{
				var currentValueForRole = role.Privileges.FirstOrDefault(x => x.PrivilegeId == this.template.PrivilegeId);

				if (currentValueForRole == null)
				{
					// nothing to do, the previous value is empty, and the current it's still empty
					if (this.Value == Level.None)
						continue;

					summary.Add(role, this.template.PrivilegeId, this.template.PrivilegeName, this.Value.Value);
					continue;
				}


				// if we're here it means we already have a value for the privilege

				// if the two values are equal, do nothing
				if (currentValueForRole.GetLevel() == this.Value)
					continue;

				if (this.Value == Level.None)
				{
					summary.Remove(role, this.template.PrivilegeId, this.template.PrivilegeName);
					continue;
				}

				summary.Replace(role, this.template.PrivilegeId, this.template.PrivilegeName, currentValueForRole.GetLevel(), this.Value.Value);
			}
		}
	}
}
