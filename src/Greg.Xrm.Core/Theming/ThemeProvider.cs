namespace Greg.Xrm.Theming
{
	public class ThemeProvider : IThemeProvider
	{
		public ITheme GetCurrentTheme()
		{
			return Theme.Default;
		}
	}
}
