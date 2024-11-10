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

		public Dictionary<string, string[]> GetForTablePrivileges()
		{
			lock (syncRoot)
			{
				var settings = this.settingsProvider.GetSettings();

				if (settings.PrivilegeClassificationForTable == null)
				{
					settings.PrivilegeClassificationForTable = JsonConvert.SerializeObject(PrivilegeClassification.DefaultForTable);
					settings.Save();

					return PrivilegeClassification.DefaultForTable;
				}

				try
				{
					return JsonConvert.DeserializeObject<Dictionary<string, string[]>>(settings.PrivilegeClassificationForTable);
				}
				catch
				{
					settings.PrivilegeClassificationForTable = JsonConvert.SerializeObject(PrivilegeClassification.DefaultForTable);
					settings.Save();
					return PrivilegeClassification.DefaultForTable;
				}
			}

		}
	}
}
