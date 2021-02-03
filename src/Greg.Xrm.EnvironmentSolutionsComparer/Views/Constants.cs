using System.Drawing;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views
{
	public static class Constants
	{
		public static Color ErrorBackColor { get; } = ColorTranslator.FromHtml("#FFC7CE");
		public static Color ErrorForeColor { get; } = ColorTranslator.FromHtml("#9C0006");
		public static Color WarnBackColor { get; } = ColorTranslator.FromHtml("#FFEB9C");
		public static Color WarnForeColor { get; } = ColorTranslator.FromHtml("#9C5700");
		public static Color SuccessBackColor { get; } = ColorTranslator.FromHtml("#C6EFCE");
		public static Color SuccessForeColor { get; } = ColorTranslator.FromHtml("#006100");
		public static Color GrayBackColor { get; } = ColorTranslator.FromHtml("#C9C9C9");
		public static Color GrayForeColor { get; } = Color.Black;
	}
}
