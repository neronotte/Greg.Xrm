using Greg.Xrm.RoleEditor.Model;
using Newtonsoft.Json;
using System;

namespace Greg.Xrm.RoleEditor
{
	/// <summary>
	/// This class can help you to store settings for your plugin
	/// </summary>
	/// <remarks>
	/// This class must be XML serializable
	/// </remarks>

	[Serializable]
	public class Settings : SettingsBase<RoleEditorPlugin>
	{
        public Settings()
        {
			// set default values
			this.PrivilegeClassificationForMisc = JsonConvert.SerializeObject(PrivilegeClassification.DefaultForMisc, Formatting.Indented);
			this.PrivilegeClassificationForTable = JsonConvert.SerializeObject(PrivilegeClassification.DefaultForTable, Formatting.Indented);
			this.UseLegacyPrivilegeIcons = true;

			this.HideNotCustomizableRoles = true;
			this.HideManagedRoles = false;
		}


		public bool HideNotCustomizableRoles { get; set; }

		public bool HideManagedRoles { get; set; }


		public bool UseLegacyPrivilegeIcons { get; set; }

		public string PrivilegeClassificationForMisc { get; set; }

		public string PrivilegeClassificationForTable { get; set; }
	}
}