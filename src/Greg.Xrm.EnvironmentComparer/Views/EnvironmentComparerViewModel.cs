using Greg.Xrm.EnvironmentComparer.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;

namespace Greg.Xrm.EnvironmentComparer.Views
{
	public class EnvironmentComparerViewModel : PluginViewModelBase
	{
		private readonly ILog log;
		private readonly IMessenger messenger;

		public EnvironmentComparerViewModel(ILog log, IMessenger messenger)
		{
			this.log = log;
			this.messenger = messenger;

			this.WhenChanges(() => Env1)
				.ChangesAlso(() => ConnectionName1)
				.ChangesAlso(() => AreEqual);

			this.WhenChanges(() => Env2)
				.ChangesAlso(() => ConnectionName2)
				.ChangesAlso(() => AreEqual);

			this.WhenChanges(() => Crm1).NotifyOthers(messenger);
			this.WhenChanges(() => Crm2).NotifyOthers(messenger);
			this.WhenChanges(() => ConnectionName1).NotifyOthers(messenger);
			this.WhenChanges(() => ConnectionName2).NotifyOthers(messenger);
		}


		public ConnectionDetail Env1
		{
			get => Get<ConnectionDetail>();
			set
			{
				Set(value);
				Set(value?.GetCrmServiceClient(), nameof(Crm1));
			}
		}
		public IOrganizationService Crm1 { get => Get<IOrganizationService>(); }
		public string ConnectionName1 { get => this.Env1?.ConnectionName; }

		/// <summary>
		/// Indicates whether the two connections match the same environment
		/// </summary>
		public bool AreEqual
		{
			get
			{
				if (this.Env1 == null) return false;
				if (this.Env2 == null) return false;

				return this.Env1.CompareTo(this.Env2) == 0;
			}
		}


		public ConnectionDetail Env2
		{
			get => Get<ConnectionDetail>();
			set
			{
				Set(value);
				Set(value?.GetCrmServiceClient(), nameof(Crm2));
			}
		}

		public IOrganizationService Crm2 { get => Get<IOrganizationService>(); }

		public string ConnectionName2 { get => this.Env2?.ConnectionName; }
	}
}
