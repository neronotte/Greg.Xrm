using System;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Greg.Xrm
{
	public class GregMultipleConnectionsPluginControlBase<TPlugin> : MultipleConnectionsPluginControlBase, IPayPalPlugin, IGitHubPlugin
		where TPlugin : PluginBase
	{
		public string RepositoryName => GitHubPluginConstants.RepositoryName;

		public string UserName => GitHubPluginConstants.UserName;

		public string DonationDescription => GetDonationDescription();

		public string EmailAccount => PayPalPluginConstants.EmailAccount;

		protected override void ConnectionDetailsUpdated(NotifyCollectionChangedEventArgs e)
		{
		}

		protected virtual string GetDonationDescription()
		{
			var exportMetadataAttributeList = (ExportMetadataAttribute[])typeof(TPlugin).GetCustomAttributes(typeof(ExportMetadataAttribute), false);

			var nameAttribute = exportMetadataAttributeList
				.Where(x => string.Equals(x.Name, "Name", StringComparison.OrdinalIgnoreCase))
				.Select(x => x.Value?.ToString())
				.FirstOrDefault();

			if (!string.IsNullOrWhiteSpace(nameAttribute))
			{
				return $"Donation for {nameAttribute}";
			}

			return "Donation for your contribution to the XrmToolbox community!";
		}
	}
}
