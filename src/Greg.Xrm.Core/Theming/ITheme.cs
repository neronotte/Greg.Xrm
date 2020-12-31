using System.ComponentModel;
using System.Drawing;

namespace Greg.Xrm.Theming
{
	public interface ITheme
	{
		[DisplayName("Panels background color")]
		[Description("The background color of the main window panels")]
		Color PanelBackgroundColor { get; }


		[DisplayName("Panels foreground color")]
		[Description("The color of the text in the main window panels")]
		Color PanelForeColor { get; }


		[DisplayName("Panels default font")]
		[Description("The font of the main window panels")]
		Font PanelFont { get; }
	}
}
