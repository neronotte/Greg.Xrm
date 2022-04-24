using System;
using System.ComponentModel.Composition;
using System.Linq;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Greg.Xrm
{
	public abstract class GregPluginBase : PluginBase, IPayPalPlugin
	{
		public string DonationDescription => GetDonationDescription();

		public string EmailAccount => PayPalPluginConstants.EmailAccount;


		protected virtual string GetDonationDescription()
		{
			var exportMetadataAttributeList = (ExportMetadataAttribute[])GetType().GetCustomAttributes(typeof(ExportMetadataAttribute), false);

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
