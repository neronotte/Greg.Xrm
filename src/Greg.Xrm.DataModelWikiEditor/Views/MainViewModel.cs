using Greg.Xrm.Model;
using Greg.Xrm.Views;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;

namespace Greg.Xrm.DataModelWikiEditor.Views
{
	public class MainViewModel : PluginViewModelBase
	{
		public MainViewModel(ISettingsProvider<Settings> settingsProvider)
		{
			this.WhenChanges(() => Env)
				.ChangesAlso(() => ConnectionName)
				.ChangesAlso(() => IsConnectToEnvirnmentVisible)
				.ChangesAlso(() => IsEnvironmentNameVisible);

			this.OpenWikiFolder = new OpenWikiFolderCommand(settingsProvider);
		}

		public ConnectionDetail Env
		{
			get => Get<ConnectionDetail>();
			set
			{
				Set(value);
				Set(value?.GetCrmServiceClient(), nameof(Crm));
			}
		}

		public IOrganizationService Crm { get => Get<IOrganizationService>(); }

		public string ConnectionName { get => this.Env?.ConnectionName; }

		public bool IsConnectToEnvirnmentVisible
		{
			get => string.IsNullOrWhiteSpace(this.ConnectionName);
		}

		public bool IsEnvironmentNameVisible
		{
			get => !string.IsNullOrWhiteSpace(this.ConnectionName);
		}

		public ICommand OpenWikiFolder { get; }
	}
}
