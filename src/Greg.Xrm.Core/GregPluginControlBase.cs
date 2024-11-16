using Greg.Xrm.Core.Views;
using Greg.Xrm.Messaging;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Greg.Xrm
{
	public class GregPluginControlBase<TPlugin> : PluginControlBase, IPayPalPlugin, IGitHubPlugin
		where TPlugin : PluginBase
	{
		public string RepositoryName => GitHubPluginConstants.RepositoryName;

		public string UserName => GitHubPluginConstants.UserName;

		public string DonationDescription => GetDonationDescription();

		public string EmailAccount => PayPalPluginConstants.EmailAccount;


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

		protected void Register(IMessenger messenger)
		{
			messenger.Register<SetWorkingMessage>(m => this.SetWorkingMessage(m.Message, m.Width, m.Heigth));
		}
	}
}
