using System.Drawing;

namespace Greg.Xrm.Theming
{
	public class Theme : ITheme
	{
		public static ITheme Default { get; } = new Theme();



		public Color PanelBackgroundColor { get; set; } = Color.FromArgb(45, 46, 40);

		public Color PanelForeColor { get; set; } = Color.FromArgb(203, 203, 203);

		public Font PanelFont { get; set; } = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
	}
}
