namespace Greg.Xrm.DataModelWikiEditor
{
	/// <summary>
	/// This class can help you to store settings for your plugin
	/// </summary>
	/// <remarks>
	/// This class must be XML serializable
	/// </remarks>
	public class Settings : SettingsBase<DataModelWikiEditorPlugin>
	{
		public string LastUsedFolder { get; set; }

		public int LastUsedLocaleID { get; set; }
	}
}