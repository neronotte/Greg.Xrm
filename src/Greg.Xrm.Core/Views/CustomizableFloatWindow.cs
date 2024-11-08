using System;
using System.Drawing;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.Core.Views
{
	public class CustomizableFloatWindow : FloatWindow
	{
		public CustomizableFloatWindow(DockPanel dockPanel, DockPane pane, Action<FloatWindow> customizationCallback)
			: base(dockPanel, pane)
		{
			customizationCallback?.Invoke(this);
		}

		public CustomizableFloatWindow(DockPanel dockPanel, DockPane pane, Rectangle bounds, Action<FloatWindow> customizationCallback)
			: base(dockPanel, pane, bounds)
		{
			customizationCallback?.Invoke(this);
		}
	}
}
