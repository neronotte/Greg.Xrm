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
using XrmToolBox.Extensibility;
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
	public class EnvironmentSolutionsComparerPlugin : PluginBase
	{
		public override IXrmToolBoxPluginControl GetControl()
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			var themeProvider = new ThemeProvider();
			return new MainView(themeProvider).SetIcon();
		}

		/// <summary>
		/// Constructor 
		/// </summary>
		public EnvironmentSolutionsComparerPlugin()
		{
			// If you have external assemblies that you need to load, uncomment the following to 
			// hook into the event that will fire when an Assembly fails to resolve
			// AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolveEventHandler);
		}

		/// <summary>
		/// Event fired by CLR when an assembly reference fails to load
		/// Assumes that related assemblies will be loaded from a subfolder named the same as the Plugin
		/// For example, a folder named Sample.XrmToolBox.MyPlugin 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		private Assembly AssemblyResolveEventHandler(object sender, ResolveEventArgs args)
		{
			Assembly loadAssembly = null;
			Assembly currAssembly = Assembly.GetExecutingAssembly();

			// base name of the assembly that failed to resolve
			var argName = args.Name.Substring(0, args.Name.IndexOf(","));

			// check to see if the failing assembly is one that we reference.
			List<AssemblyName> refAssemblies = currAssembly.GetReferencedAssemblies().ToList();
			var refAssembly = refAssemblies.Where(a => a.Name == argName).FirstOrDefault();

			// if the current unresolved assembly is referenced by our plugin, attempt to load
			if (refAssembly != null)
			{
				// load from the path to this plugin assembly, not host executable
				string dir = Path.GetDirectoryName(currAssembly.Location).ToLower();
				string folder = Path.GetFileNameWithoutExtension(currAssembly.Location);
				dir = Path.Combine(dir, folder);

				var assmbPath = Path.Combine(dir, $"{argName}.dll");

				if (File.Exists(assmbPath))
				{
					loadAssembly = Assembly.LoadFrom(assmbPath);
				}
				else
				{
					throw new FileNotFoundException($"Unable to locate dependency: {assmbPath}");
				}
			}

			return loadAssembly;
		}
	}
}