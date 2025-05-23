﻿using Greg.Xrm.Logging;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Common;
using Microsoft.Crm.Sdk.Messages;
using Newtonsoft.Json;
using System;

namespace Greg.Xrm.RoleEditor.Views.Editor
{
	public class MiscModel
	{
		private readonly TemplateForGenericPrivilege template;
		private readonly Level preImage;
		private Level? target;


		public MiscModel(TemplateForGenericPrivilege template, RolePrivilege rolePrivilege, bool isNew)
		{
			this.template = template;

			if (isNew)
			{
				// if the privilege is new, then the pre-image is None
				this.preImage = Level.None;
				Set(rolePrivilege.GetLevel());
			}
			else
			{
				// if the privilege is not new, then the pre-image is the current level
				this.preImage = rolePrivilege.GetLevel();
				this.target = null;
			}
		}

		public string Name => this.template.Name;

		public string Tooltip => this.template.PrivilegeName;


		public MiscGroupModel Parent { get; set; }

		public Level Value
		{
			get => Get();
			set => Set(value);
		}

		public bool IsAssigned => this.preImage != Level.None || (this.target != null && this.target != Level.None);

		public bool IsChanged => this.target.HasValue;

		public void Increase()
		{
			Level nextValue = Get();
			var i = 0;
			do
			{
				nextValue = (Level)((((int)nextValue) + 1) % 5);
				i++;
			}
			while (!template.IsValidLevel(nextValue) && i <= 5);

			Set(nextValue);
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

		public void CalculateChanges(ChangeSummary summary)
		{
			if (target == null) return;

			if (target.Value == Level.None)
			{
				summary.RemovePrivilege(this.template);

			}
			else if (preImage == Level.None)
			{
				summary.AddPrivilege(this.template, target.Value.ToPrivilegeDepth());
			}
			else
			{
				summary.ReplacePrivilege(this.template, this.preImage.ToPrivilegeDepth(), this.target.Value.ToPrivilegeDepth());
			}
		}

		private Level Get()
		{
			return target ?? preImage;
		}

		private void Set(Level value)
		{
			// if the specified level is not valid for the current privilege, then ignore it
			if (!this.template.IsValidLevel(value)) return;

			if (value == this.preImage)
			{
				this.target = null;
			}
			else
			{
				this.target = value;
			}
		}

		public string GenerateConfigurationCommand()
		{
			var command = PrivilegeConfigForMiscMemento.CreateNew();
			command.Level = this.Value;
			return JsonConvert.SerializeObject(command);
		}

		public void ApplyConfigurationCommand(ILog log, string commandText)
		{
			try
			{
				var command = JsonConvert.DeserializeObject<PrivilegeConfigForMiscMemento>(commandText);
				if (!command.IsValid)
					return;

				if (command.Level == null) return;
				Set(command.Level.Value);
			}
			catch (Exception ex)
			{
				log.Error("Error while applying the configuration command: " + ex.Message, ex);
			}
		}
	}
}
