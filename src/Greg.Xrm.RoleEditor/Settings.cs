using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Services.Snippets;
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
			this.PrivilegeSnippets = JsonConvert.SerializeObject(PrivilegeSnippet.DefaultSnippets, Formatting.Indented);
			this.UseLegacyPrivilegeIcons = true;
			this.AutoLoadRolesWhenConnectonChanges = false;
			this.HideNotCustomizableRoles = true;
			this.HideManagedRoles = false;
		}


		public bool AutoLoadRolesWhenConnectonChanges { get; set; }

		public bool HideNotCustomizableRoles { get; set; }

		public bool HideManagedRoles { get; set; }


		public bool UseLegacyPrivilegeIcons { get; set; }

		public string PrivilegeClassificationForMisc { get; set; }

		public string PrivilegeClassificationForTable { get; set; }

		public string PrivilegeSnippets { get; set; }
	}
}