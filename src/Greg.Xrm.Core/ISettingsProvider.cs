namespace Greg.Xrm
{
	public interface ISettingsProvider<T>
		where T : ISettings
	{
		T GetSettings();
	}
}
