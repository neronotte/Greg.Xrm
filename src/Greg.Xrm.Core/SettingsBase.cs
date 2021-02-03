using XrmToolBox.Extensibility;

namespace Greg.Xrm
{
	public abstract class SettingsBase<TPlugin> : ISettings
		where TPlugin : PluginBase
	{
		public virtual void Save()
		{
			SettingsManager.Instance.Save(typeof(TPlugin), this);
		}
	}
}
