using XrmToolBox.Extensibility;

namespace Greg.Xrm
{
	public class SettingsProvider<TSettings, TPlugin> : ISettingsProvider<TSettings>
		where TSettings : ISettings, new()
	{
		private TSettings settings;


		public TSettings GetSettings()
		{
			if (!SettingsManager.Instance.TryLoad(typeof(TPlugin), out settings))
			{
				this.settings = new TSettings();
			}

			return this.settings;
		}
	}
}
