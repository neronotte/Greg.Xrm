using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk;
using System;

namespace Greg.Xrm.RoleEditor.Views.Editor
{
	public class RoleEditorViewModel : ViewModel
	{
		private readonly Role role;
		private readonly TemplateForRole template;

		public RoleEditorViewModel(ILog log, IMessenger messenger, IOrganizationService crm, Role role, TemplateForRole template)
        {
			this.role = role;
			this.template = template;

			this.OverrideSetDefaultValue(() => IsEnabled, () => true);

			this.ShowAllPrivilegesCommand = new RelayCommand(() => {
				this.ShouldShowOnlyAssignedPrivileges = false;
			}, () => IsEnabled && ShouldShowOnlyAssignedPrivileges);
			this.ShowOnlyAssignedPrivilegesCommand = new RelayCommand(() =>
			{
				this.ShouldShowOnlyAssignedPrivileges = true;
			}, () => IsEnabled && !ShouldShowOnlyAssignedPrivileges);

			this.SaveCommand = new SaveCommand(log, messenger, crm, this);


			this.WhenChanges(() => IsEnabled)
				.Refresh(this.ShowAllPrivilegesCommand)
				.Refresh(this.ShowOnlyAssignedPrivilegesCommand);
			this.WhenChanges(() => ShouldShowOnlyAssignedPrivileges)
				.Refresh(this.ShowAllPrivilegesCommand)
				.Refresh(this.ShowOnlyAssignedPrivilegesCommand);


			Initialize();

			messenger.Register<Freeze>(m => IsEnabled = false);
			messenger.Register<Unfreeze>(m => IsEnabled = true);
		}

		private void Initialize()
		{
			var model = new RoleEditorModel(this.role, this.template);
			model.Initialize();
			
			this.Model = model;
			this.ShouldShowOnlyAssignedPrivileges = this.role.HasAssignedPrivileges;
		}

		public void RefreshView()
		{
			Initialize();
		}

		public bool IsEnabled
		{
			get => Get<bool>();
			private set => Set(value);
		}

		public bool ShouldShowOnlyAssignedPrivileges
		{
			get => Get<bool>();
			set => Set(value);
		}

		public RelayCommand ShowAllPrivilegesCommand { get; }
		public RelayCommand ShowOnlyAssignedPrivilegesCommand { get; }

		public string SearchTableText
		{
			get => Get<string>();
			set => Set(value);
		}

		public RoleEditorModel Model { get; private set; }


		public SaveCommand SaveCommand { get; }



		/// <summary>
		/// This method navigates the model tree and returns the summary of changes to be applied
		/// </summary>
		/// <returns>
		/// The summary of changes to be applied in order for this role to be updated properly
		/// </returns>
		public ChangeSummary GetChangeSummary()
		{
			if (Model == null) return null;

			var summary = new ChangeSummary(this.Model.Id);

			foreach (var group in Model.TableGroups)
			{
				foreach (var table in group)
				{
					if (table.IsDirty)
					{
						table.CalculateChanges(summary);
					}
				}
			}

			foreach (var group in Model.MiscGroups)
			{
				foreach (var item in group)
				{
					if (item.IsChanged)
					{
						item.CalculateChanges(summary);
					}
				}
			}

			return summary;
		}
	}
}
