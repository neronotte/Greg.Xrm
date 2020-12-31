using System.Windows.Forms;

namespace Greg.Xrm.Theming
{
	public static class ThemeExtensions
	{
		public static void ApplyTo(this ITheme theme, Control control)
		{
			control.BackColor = theme.PanelBackgroundColor;
			control.ForeColor = theme.PanelForeColor;
			control.Font = theme.PanelFont;
		}
	}
}
