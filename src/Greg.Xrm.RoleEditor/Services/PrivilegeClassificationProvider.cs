using Greg.Xrm.RoleEditor.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Services
{
	public class PrivilegeClassificationProvider : IPrivilegeClassificationProvider
	{
		private readonly object syncRoot = new object();
		private readonly ISettingsProvider<Settings> settingsProvider;

		public PrivilegeClassificationProvider(ISettingsProvider<Settings> settingsProvider)
        {
			this.settingsProvider = settingsProvider;
		}

		public Dictionary<string, string[]> GetForMiscPrivileges()
		{
			lock(syncRoot)
			{
				var settings = this.settingsProvider.GetSettings();


				if (settings == null)
				{
					settings = new Settings();
				}
				settings.PrivilegeClassificationForMisc = JsonConvert.SerializeObject(PrivilegeClassification.DefaultForMisc);
				settings.Save();

				if (settings.PrivilegeClassificationForMisc == null)
				{
					settings.PrivilegeClassificationForMisc = JsonConvert.SerializeObject( PrivilegeClassification.DefaultForMisc );
					settings.Save();

					return PrivilegeClassification.DefaultForMisc;
				}

				try
				{
					return JsonConvert.DeserializeObject<Dictionary<string, string[]>>(settings.PrivilegeClassificationForMisc);
				}
				catch
				{
					settings.PrivilegeClassificationForMisc = JsonConvert.SerializeObject(PrivilegeClassification.DefaultForMisc);
					settings.Save();
					return PrivilegeClassification.DefaultForMisc;
				}
			}
			
		}

		public void SaveForMiscPrivileges(Dictionary<string, string[]> classification)
		{
			if (classification == null) classification = new Dictionary<string, string[]>();

			lock (syncRoot)
			{
				var settings = this.settingsProvider.GetSettings();

				if (settings == null)
				{
					settings = new Settings();
				}

				settings.PrivilegeClassificationForMisc = JsonConvert.SerializeObject( classification);
				settings.Save();
			}
		}

		public void ResetMiscPrivileges()
		{
			lock (syncRoot)
			{
				var settings = this.settingsProvider.GetSettings();
				if (settings == null)
				{
					settings = new Settings();
				}

				settings.PrivilegeClassificationForMisc = JsonConvert.SerializeObject(PrivilegeClassification.DefaultForMisc);
				settings.Save();
			}
		}
	}
}
