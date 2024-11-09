using Greg.Xrm.Core.Views;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Views
{
	public class SettingsViewModel : ViewModel, INotificationProvider
	{
		private readonly IMessenger messenger;
		private readonly ISettingsProvider<Settings> settingsProvider;

		public SettingsViewModel(IMessenger messenger, ISettingsProvider<Settings> settingsProvider)
		{
			this.messenger = messenger;
			this.settingsProvider = settingsProvider;

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
			var settings = this.settingsProvider.GetSettings();

			if (!this.ValidatePrivilegeClassification()) return;

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
	}
}
