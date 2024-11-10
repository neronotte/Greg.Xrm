using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.Core.Views
{
	public static class CustomizableFloatWindowExtensions
	{
		public static void CustomizaFloatWindow(this DockPanel dockPanel, Action<FloatWindow> customizationCallback = null)
		{
			if (dockPanel == null)
				throw new ArgumentNullException(nameof(dockPanel));

			dockPanel.Theme.Extender.FloatWindowFactory = new CustomizableFloatWindowFactory(customizationCallback);
		}


		public static FloatWindow MakeResizable(this FloatWindow window)
		{
			if (window == null)
				throw new ArgumentNullException(nameof(window));

			window.FormBorderStyle = FormBorderStyle.Sizable;
			window.DoubleClickTitleBarToDock = false;
			return window;
		}

		public static FloatWindow AllowAltTab(this FloatWindow window)
		{
			if (window == null)
				throw new ArgumentNullException(nameof(window));

			window.ShowInTaskbar = true;
			window.Owner = null;
			return window;
		}
	}
}
