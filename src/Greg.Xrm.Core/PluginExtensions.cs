using Greg.Xrm.Properties;
using System;
using XrmToolBox.Extensibility;

namespace Greg.Xrm
{
	public static class PluginExtensions
	{
		public static T SetIcon<T>(this T pluginControl)
			where T : PluginControlBase
		{
			if (pluginControl is null)
			{
				throw new ArgumentNullException(nameof(pluginControl));
			}

			pluginControl.PluginIcon = Resources.Icon;
			return pluginControl;
		}
	}
}
