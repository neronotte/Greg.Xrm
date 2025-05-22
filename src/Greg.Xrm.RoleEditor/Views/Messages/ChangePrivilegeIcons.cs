namespace Greg.Xrm.RoleEditor.Views.Messages
{
	/// <summary>
	/// This message is sent whenever the user changes the setting to use legacy privilege icons
	/// </summary>
	public class ChangePrivilegeIcons
	{
		public ChangePrivilegeIcons(bool useLegacyIcons)
		{
			UseLegacyIcons = useLegacyIcons;
		}

		public bool UseLegacyIcons { get; }
	}
}
