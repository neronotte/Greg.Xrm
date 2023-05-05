using Greg.Xrm.EnvironmentSolutionsComparer.Views;
using Greg.Xrm.Info;
using Greg.Xrm.Theming;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using XrmToolBox.Extensibility.Interfaces;

namespace Greg.Xrm.EnvironmentSolutionsComparer
{
	// Do not forget to update version number and author (company attribute) in AssemblyInfo.cs class
	// To generate Base64 string for Images below, you can use https://www.base64-image.de/
	[Export(typeof(IXrmToolBoxPlugin)),
		ExportMetadata("Name", "_n.EnvironmentsSolutionComparer"),
		ExportMetadata("Description", "This plugin compares the installed solutions versions between different environments."),
		// Please specify the base64 content of a 32x32 pixels image
		ExportMetadata("SmallImageBase64", PluginConstants.SmallImageBase64),
		// Please specify the base64 content of a 80x80 pixels image
		ExportMetadata("BigImageBase64", PluginConstants.BigImageBase64),
		ExportMetadata("BackgroundColor", PluginConstants.BackgroundColor),
		ExportMetadata("PrimaryFontColor", PluginConstants.PrimaryFontColor),
		ExportMetadata("SecondaryFontColor", PluginConstants.SecondaryFontColor)]
	public class EnvironmentSolutionsComparerPlugin : GregPluginBase
	{
		public override IXrmToolBoxPluginControl GetControl()
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			var themeProvider = new ThemeProvider();
			return new MainView(themeProvider).SetIcon();
		}
	}
}