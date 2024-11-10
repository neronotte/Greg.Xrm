using Greg.Xrm.Logging;
using Greg.Xrm.RoleEditor.Model;
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


		public MiscModel(TemplateForGenericPrivilege template, RolePrivilege rolePrivilege)
		{
			this.template = template;
			this.preImage = rolePrivilege.GetLevel();
			this.target = null;
		}

		public string Name => this.template.Name;

		public string Tooltip => this.template.PrivilegeName;


		public MiscGroupModel Parent { get; set; }

		public Level Value
		{
			get => Get();
			set => Set(value);
		}

		public bool IsAssigned => Get() != Level.None;

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

		public void CalculateChanges(ChangeSummary summary)
		{
			if (target == null) return;

			if (target.Value == Level.None)
			{
				summary.RemovePrivilege(this.template);

			}
			else
			{
				summary.AddPrivilege(this.template, target.Value.ToPrivilegeDepth());
			}
		}

		private Level Get()
		{
			return target.HasValue ? target.Value : preImage;
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
			var command = new Command();
			command.Name = Command.CommandName;
			command.Level = Get();
			return JsonConvert.SerializeObject(command);
		}

		public void ApplyConfigurationCommand(ILog log, string commandText)
		{
			try
			{
				var command = JsonConvert.DeserializeObject<Command>(commandText);
				if (!command.IsValid)
					return;

				Set(command.Level);
			}
			catch (Exception ex)
			{
				log.Error("Error while applying the configuration command: " + ex.Message, ex);
			}
		}


		class Command
		{
			public const string CommandName = "Greg.Xrm.RoleEditor-misc";

			public string Name { get; set; }
			public Level Level { get; set; }

			[Newtonsoft.Json.JsonIgnore]
			public bool IsValid
			{
				get
				{
					return CommandName.Equals(this.Name);
				}
			}
		}
	}
}
