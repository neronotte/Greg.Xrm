using System.Drawing;

namespace Greg.Xrm.ModernThemeBuilder.Model
{
	public static class ColorExtensions
	{
		public static Color ToColor(this string colorHtmlValue)
		{
			return ColorTranslator.FromHtml(colorHtmlValue);
		}

		public static string ToHtml(this Color color)
		{
			return ColorTranslator.ToHtml(color);
		}
	}
}
