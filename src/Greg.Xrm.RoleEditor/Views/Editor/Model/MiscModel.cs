using Greg.Xrm.RoleEditor.Model;
using Microsoft.Crm.Sdk.Messages;
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
				if (i > 5)
				{
					throw new InvalidOperationException("Cannot find a valid level for the privilege.");
				}
			}
			while (!template.IsValidLevel(nextValue));

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
			if (value == preImage)
			{
				target = null;
			}
			else
			{
				target = value;
			}
		}
	}
}
