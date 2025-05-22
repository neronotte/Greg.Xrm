namespace Greg.Xrm.ModernThemeBuilder.Views.Messages
{
	public class CurrentThemeSelected
	{
		public CurrentThemeSelected(string themeName)
		{
			this.ThemeName = themeName;
		}

		public string ThemeName { get; }
	}
}
