using Greg.Xrm.Core.Views;
using Greg.Xrm.Model;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Services;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using System;
using System.Text;

namespace Greg.Xrm.RoleEditor.Views.Editor
{
	public class RoleEditorViewModel : ViewModel, INotificationProvider
	{
		private readonly IPrivilegeClassificationProvider privilegeClassificationProvider;
		private readonly Role role;
		private readonly TemplateForRole template;

		public RoleEditorViewModel(
			IPrivilegeClassificationProvider privilegeClassificationProvider,
			Role role)
		{
			this.privilegeClassificationProvider = privilegeClassificationProvider;
			this.role = role;
			this.template = role.Template;

			this.OverrideSetDefaultValue(() => IsEnabled, () => true);

			this.ShowAllPrivilegesCommand = new RelayCommand(() => {
				this.ShouldShowOnlyAssignedPrivileges = false;
			}, () => IsEnabled && ShouldShowOnlyAssignedPrivileges);
			this.ShowOnlyAssignedPrivilegesCommand = new RelayCommand(() =>
			{
				this.ShouldShowOnlyAssignedPrivileges = true;
			}, () => IsEnabled && !ShouldShowOnlyAssignedPrivileges);


			this.SaveCommand = new SaveCommand(this);
			this.AddRoleToSolutionCommand = new AddRoleToSolutionCommand(this);
			this.ExportExcelCommand = new ExportExcelCommand(this);
			this.ExportMarkdownCommand = new ExportMarkdownCommand(this);


			this.WhenChanges(() => IsEnabled)
				.Refresh(this.ShowAllPrivilegesCommand)
				.Refresh(this.ShowOnlyAssignedPrivilegesCommand)
				.Execute(_ => RefreshSaveCommand());
			this.WhenChanges(() => ShouldShowOnlyAssignedPrivileges)
				.Refresh(this.ShowAllPrivilegesCommand)
				.Refresh(this.ShowOnlyAssignedPrivilegesCommand);
			this.WhenChanges(() => Model)
				.ChangesAlso(() => ViewTitle)
				.Execute(_ => this.BindModel())
				.Execute(_ => RefreshSaveCommand());


			this.Initialize();
			this.BindModel();
			this.RefreshSaveCommand();

			var messenger = this.role.ExecutionContext.Messenger;
			messenger.Register<Freeze>(m => IsEnabled = false);
			messenger.Register<Unfreeze>(m => IsEnabled = true);
		}

		private void RefreshSaveCommand()
		{
			this.SaveCommand.SetEnabled(this.IsCustomizable && this.IsEnabled && this.Model != null && this.Model.IsDirty);
		}

		private void BindModel()
		{
			this.Model.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(RoleModel.Name) || e.PropertyName == nameof(RoleModel.IsDirty))
				{
					this.OnPropertyChanged(nameof(ViewTitle), ViewTitle);
					this.RefreshSaveCommand();
				}
			};
		}



		private void Initialize()
		{
			this.Model = new RoleModel(this.role, this.template, this.privilegeClassificationProvider);
			this.ShouldShowOnlyAssignedPrivileges = this.role.HasAssignedPrivileges;
		}




		public void RefreshView()
		{
			Initialize();
			this.role.ExecutionContext.Messenger.Send(new RefreshRoleBrowser(this.role));
		}

		public string ViewTitle 
		{
			get 
			{
				var sb = new StringBuilder();
				sb.Append(this.Model.Name).Append(" (");
				sb.Append(this.Model.GetContext().Details.ConnectionName);
				sb.Append(")");
				sb.Append(this.Model.IsDirty ? "*" : "");
				return sb.ToString();
			}
		}



		public bool IsEnabled
		{
			get => Get<bool>();
			private set => Set(value);
		}

		public bool IsCustomizable => this.role.iscustomizable;


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
		public string SearchMiscText
		{
			get => Get<string>();
			set => Set(value);
		}


		public RoleModel Model
		{
			get => Get<RoleModel>();
			private set => Set(value);
		}


		public SaveCommand SaveCommand { get; }

		public AddRoleToSolutionCommand AddRoleToSolutionCommand { get; }

		public ExportExcelCommand ExportExcelCommand { get; }
		public ExportMarkdownCommand ExportMarkdownCommand  { get; }



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

			Model.CalculateChanges(summary);

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



		public event EventHandler<NotificationEventArgs> Notify;
		public void SendNotification(NotificationType type, string message)
		{
			Notify?.Invoke(this, new NotificationEventArgs(type, message));
		}



		/// <summary>
		/// We have a problem: we don't actually know if the role is really dirty because the model is complex and,
		/// if you change some privilege, the event is not propagated automatically 
		/// (it's a design decision: it will fire too much when you have massive operations).
		/// So we have to reevaluate the dirtyness manually. It's the view that tells us "i've changed something, go ahead and check".
		/// </summary>
		public void EvaluateDirty()
		{
			this.Model.EvaluateDirty();
		}
	}
}
