using Greg.Xrm.Core.Views;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Services;
using Greg.Xrm.RoleEditor.Services.Snippets;
using Greg.Xrm.RoleEditor.Views.BulkEditor.Model;
using Greg.Xrm.RoleEditor.Views.Common;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Views.BulkEditor
{
	public class BulkEditorViewModel : ViewModel, INotificationProvider
	{
		private readonly TemplateForRole template;
		private readonly IPrivilegeClassificationProvider privilegeClassificationProvider;
		private readonly Role[] roleList;

		public BulkEditorViewModel(
			IPrivilegeClassificationProvider privilegeClassificationProvider,
			IPrivilegeSnippetRepository snippetRepository,
			Role[] roleList
			)
		{
			if (roleList == null || roleList.Length == 0)
				throw new ArgumentException("Role list cannot be null or empty", nameof(roleList));


			this.privilegeClassificationProvider = privilegeClassificationProvider;
			this.roleList = roleList;
			this.template = roleList[0].Template;
			this.Log = roleList[0].ExecutionContext.Log;
			this.Messenger = roleList[0].ExecutionContext.Messenger;

			InitializeHierarchy(roleList);

			this.ShouldShowOnlyAssignedPrivileges = false;
			this.IsEnabled = true;

			this.ShowAllPrivilegesCommand = new RelayCommand(() =>
			{
				this.ShouldShowOnlyAssignedPrivileges = false;
			}, () => IsEnabled && ShouldShowOnlyAssignedPrivileges);
			this.ShowOnlyAssignedPrivilegesCommand = new RelayCommand(() =>
			{
				this.ShouldShowOnlyAssignedPrivileges = true;
			}, () => IsEnabled && !ShouldShowOnlyAssignedPrivileges);


			this.SaveSnippetCommand = new SaveSnippetCommand(snippetRepository, this);
			this.PasteSnippetCommand = new PasteSnippetCommand(snippetRepository, this);
			this.SaveCommand = new SaveCommand(this);


			this.WhenChanges(() => IsEnabled)
				.Refresh(this.ShowAllPrivilegesCommand)
				.Refresh(this.ShowOnlyAssignedPrivilegesCommand);
			this.WhenChanges(() => this.ShouldShowOnlyAssignedPrivileges)
				.Refresh(this.ShowAllPrivilegesCommand)
				.Refresh(this.ShowOnlyAssignedPrivilegesCommand);


			this.CalculateTitle();


			this.Messenger.Register<Freeze>(m => IsEnabled = false);
			this.Messenger.Register<Unfreeze>(m => IsEnabled = true);
		}
		public ILog Log { get; }
		public IMessenger Messenger { get; }

		public int RoleCount => this.roleList.Length;


		private void InitializeHierarchy(Role[] roleList)
		{
			var tableMap = this.privilegeClassificationProvider.GetForTablePrivileges();
			var reverseTableMap = tableMap.CreateReverseMap();

			var tableGroupDict = tableMap.Keys
				.Select(x => new BulkGroupModel<BulkTableModel>(x))
				.ToDictionary(x => x.Name);
			tableGroupDict["General"] = new BulkGroupModel<BulkTableModel>("General");


			foreach (var tableTemplate in this.template.Tables.Values.OrderBy(x => x.Name))
			{
				var tableModel = new BulkTableModel(tableTemplate, roleList);
				if (reverseTableMap.TryGetValue(tableModel.LogicalName, out var groupNames) && groupNames.Count > 0)
				{
					tableGroupDict[groupNames[0]].Add(tableModel);
				}
				else
				{
					tableGroupDict["General"].Add(tableModel);
				}
			}

			this.TableGroups.Clear();
			foreach (var g in tableGroupDict.Values.Where(x => x.Count > 0))
			{
				this.TableGroups.Add(g);
			}






			var miscMap = this.privilegeClassificationProvider.GetForMiscPrivileges();
			var reverseMiscMap = miscMap.CreateReverseMap();

			var miscGroupDict = miscMap.Keys
				.Select(x => new BulkGroupModel<BulkMiscModel>(x))
				.ToDictionary(x => x.Name);
			miscGroupDict["Miscellaneous"] = new BulkGroupModel<BulkMiscModel>("Miscellaneous");


			foreach (var miscTemplate in this.template.Misc.Select(x => x.Value).OrderBy(x => x.Name))
			{
				var privilegeName = miscTemplate.PrivilegeName;

				var miscModel = new BulkMiscModel(miscTemplate, roleList);
				if (reverseMiscMap.TryGetValue(privilegeName, out var groupNames) && groupNames.Count > 0)
				{
					miscGroupDict[groupNames[0]].Add(miscModel);
				}
				else
				{
					miscGroupDict["Miscellaneous"].Add(miscModel);
				}
			}

			this.MiscGroups.Clear();
			foreach (var g in miscGroupDict.Values.Where(x => x.Count > 0))
			{
				this.MiscGroups.Add(g);
			}
		}



		public BulkChangeSummary GetChangeSummary()
		{
			var summary = new BulkChangeSummary();

			foreach (var table in this.TableGroups.SelectMany(x => x))
			{
				table.CalculateChanges(summary);
			}

			foreach (var misc in this.MiscGroups.SelectMany(x => x))
			{
				misc.CalculateChanges(summary);
			}

			return summary;
		}


		public bool IsEnabled
		{
			get => Get<bool>();
			private set => Set(value);
		}

		public string Title
		{
			get => Get<string>();
			private set => Set(value);
		}

		private void CalculateTitle(object obj = null)
		{
			this.Title = $"Edit {this.roleList.Length} roles ({this.roleList[0].ExecutionContext.Details.ConnectionName})";
		}



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




		public ObservableCollection<BulkGroupModel<BulkTableModel>> TableGroups { get; } = new ObservableCollection<BulkGroupModel<BulkTableModel>>();
		public ObservableCollection<BulkGroupModel<BulkMiscModel>> MiscGroups { get; } = new ObservableCollection<BulkGroupModel<BulkMiscModel>>();


		#region INotificationProvider implementation

		public event EventHandler<NotificationEventArgs> Notify;
		public void SendNotification(NotificationType type, string message, int? timerInSeconds = null)
		{
			Notify?.Invoke(this, new NotificationEventArgs(type, message, timerInSeconds));
		}

		#endregion



		public bool ShouldShowOnlyAssignedPrivileges
		{
			get => Get<bool>();
			set => Set(value);
		}

		public RelayCommand ShowAllPrivilegesCommand { get; }
		public RelayCommand ShowOnlyAssignedPrivilegesCommand { get; }

		public ICommand SaveSnippetCommand { get; }

		public ICommand PasteSnippetCommand { get; }

		public SaveCommand SaveCommand { get; }
	}
}
