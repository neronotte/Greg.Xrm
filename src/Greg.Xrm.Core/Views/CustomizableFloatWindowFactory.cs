using System;
using System.Drawing;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.Core.Views
{
	public class CustomizableFloatWindowFactory : DockPanelExtender.IFloatWindowFactory
	{
		private readonly Action<FloatWindow> customizationCallback;

		public CustomizableFloatWindowFactory(Action<FloatWindow> customizationCallback = null)
		{
			this.customizationCallback = customizationCallback ?? (fw => { });
		}



		public FloatWindow CreateFloatWindow(DockPanel dockPanel, DockPane pane, Rectangle bounds)
		{
			return new CustomizableFloatWindow(dockPanel, pane, bounds, customizationCallback);
		}

		public FloatWindow CreateFloatWindow(DockPanel dockPanel, DockPane pane)
		{
			return new CustomizableFloatWindow(dockPanel, pane, customizationCallback);
		}
	}
}
