using Greg.Xrm.Core.Views;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Services;
using Greg.Xrm.RoleEditor.Services.Snippets;
using Greg.Xrm.RoleEditor.Views.Config;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Greg.Xrm.RoleEditor.Views
{
	public class SettingsViewModel : ViewModel, INotificationProvider
	{
		private readonly IMessenger messenger;
		private readonly ISettingsProvider<Settings> settingsProvider;
		private readonly IPrivilegeSnippetRepository snippetRepository;

		public SettingsViewModel(
			IMessenger messenger, 
			ISettingsProvider<Settings> settingsProvider,
			IPrivilegeSnippetRepository snippetRepository)
		{
			this.messenger = messenger;
			this.settingsProvider = settingsProvider;
			this.snippetRepository = snippetRepository;
			var settings = settingsProvider.GetSettings();
			this.HideNotCustomizableRoles = settings.HideNotCustomizableRoles;
			this.HideManagedRoles = settings.HideManagedRoles;
			this.UseLegacyIcons = settings.UseLegacyPrivilegeIcons;
			this.PrivilegeClassificationForTable = settings.PrivilegeClassificationForTable?.Replace("\n", Environment.NewLine);
			this.PrivilegeClassificationForMisc = settings.PrivilegeClassificationForMisc?.Replace("\n", Environment.NewLine);
			this.CanConfirm = true;

			this.ResetPrivilegeClassificationForTableCommand = new RelayCommand(OnResetPrivilegeClassificationForTable);
			this.ResetPrivilegeClassificationForMiscCommand = new RelayCommand(OnResetPrivilegeClassificationForMisc);
			this.ConfirmCommand = new RelayCommand(OnConfirm);



			var snippetViewModelList = new PrivilegeSnippetViewModel[10];
			for (int i = 0; i < snippetViewModelList.Length; i++)
			{
				var snippet = this.snippetRepository[i];
				snippetViewModelList[i] = new PrivilegeSnippetViewModel(i, snippet);
			}
			this.Snippets = snippetViewModelList;
		}


		public bool HideNotCustomizableRoles
		{
			get => Get<bool>();
			set => Set(value);
		}

		public bool HideManagedRoles
		{
			get => Get<bool>();
			set => Set(value);
		}

		public bool UseLegacyIcons
		{
			get => Get<bool>();
			set => Set(value);
		}



		public string PrivilegeClassificationForTable
		{
			get => Get<string>();
			set => Set(value);
		}


		public string PrivilegeClassificationForMisc
		{
			get => Get<string>();
			set => Set(value);
		}


		public PrivilegeSnippetViewModel[] Snippets { get; private set; }


		private bool ValidatePrivilegeClassification()
		{
			try
			{
				var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(this.PrivilegeClassificationForTable);
				foreach (var kvp in dictionary)
				{
					if (kvp.Value == null || kvp.Value.Length == 0)
					{
						this.SendNotification(NotificationType.Error, $"On the table privilege classification JSON, there must be at least one privilege in group '{kvp.Key}'.");
						return false;
					}
				}

				var reverseDict = dictionary.CreateReverseMap();
				var duplicated = reverseDict
					.Where(x => x.Value.Count > 1)
					.Select(x => x.Key)
					.OrderBy(x => x)
					.ToList();

				if (duplicated.Count > 0)
				{
					this.SendNotification(NotificationType.Error, $"On the table privilege classification JSON there are duplicated tables: " + string.Join(", ", duplicated));
					return false;
				}
			}
			catch
			{
				this.SendNotification(NotificationType.Error, $"Invalid format of the table privilege classification JSON.");
				return false;
			}


			try
			{
				var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(this.PrivilegeClassificationForMisc);
				foreach (var kvp in dictionary)
				{
					if (kvp.Value == null || kvp.Value.Length == 0)
					{
						this.SendNotification(NotificationType.Error, $"On the misc. privilege classification JSON, there must be at least one privilege in group '{kvp.Key}'.");
						return false;
					}
				}



				var reverseDict = dictionary.CreateReverseMap();
				var duplicated = reverseDict
					.Where(x => x.Value.Count > 1)
					.Select(x => x.Key)
					.OrderBy(x => x)
					.ToList();

				if (duplicated.Count > 0)
				{
					this.SendNotification(NotificationType.Error, $"On the misc privilege classification JSON there are duplicated privileges: " + string.Join(", ", duplicated));
					return false;
				}
			}
			catch
			{
				this.SendNotification(NotificationType.Error, $"Invalid format of the misc. privilege classification JSON.");
				return false;
			}

			return true;
		}



		public RelayCommand ResetPrivilegeClassificationForTableCommand { get; }

		public void OnResetPrivilegeClassificationForTable()
		{
			this.PrivilegeClassificationForTable = JsonConvert.SerializeObject(PrivilegeClassification.DefaultForTable, Formatting.Indented);
		}

		public RelayCommand ResetPrivilegeClassificationForMiscCommand { get; }

		public void OnResetPrivilegeClassificationForMisc()
		{
			this.PrivilegeClassificationForMisc = JsonConvert.SerializeObject(PrivilegeClassification.DefaultForMisc, Formatting.Indented);
		}


		public RelayCommand ConfirmCommand { get; }

		public bool CanConfirm
		{
			get => Get<bool>();
			set => Set(value);
		}

		public void OnConfirm()
		{

			if (!this.ValidatePrivilegeClassification()) return;

			for (int i = 0; i < this.Snippets.Length; i++)
			{
				var snippet = this.Snippets[i].ToPrivilegeSnippet();
				this.snippetRepository.Set(i, snippet);
			}




			var settings = this.settingsProvider.GetSettings();
			settings.HideNotCustomizableRoles = this.HideNotCustomizableRoles;
			settings.HideManagedRoles = this.HideManagedRoles;
			settings.UseLegacyPrivilegeIcons = this.UseLegacyIcons;
			settings.PrivilegeClassificationForTable = this.PrivilegeClassificationForTable;
			settings.PrivilegeClassificationForMisc = this.PrivilegeClassificationForMisc;
			settings.Save();

			this.messenger.Send(new ChangePrivilegeIcons(this.UseLegacyIcons));

			this.Close?.Invoke(this, EventArgs.Empty);
		}

		public event EventHandler Close;



		public event EventHandler<NotificationEventArgs> Notify;
		public void SendNotification(NotificationType type, string message)
		{
			Notify?.Invoke(this, new NotificationEventArgs(type, message));
		}

		public void ResetSnippets()
		{
			var result = MessageBox.Show("This will reset all your customizations on the privilege snippets. Are you sure?", "Reset Snippets", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result != DialogResult.Yes) return;


			var defaults = PrivilegeSnippet.DefaultSnippets;
			for (int i = 0; i < defaults.Length; i++)
			{
				if (this.Snippets.Length > i)
				{
					this.Snippets[i].SetFrom(defaults[i]);
				}
			}
		}
	}
}
