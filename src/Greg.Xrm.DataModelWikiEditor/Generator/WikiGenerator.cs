using Greg.Xrm.DataModelWikiEditor.Model;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Greg.Xrm.DataModelWikiEditor.Generator
{
	public class WikiGenerator
	{
		private readonly FolderGenerator folderGenerator;

		public WikiGenerator(FolderGenerator folderGenerator)
		{
			this.folderGenerator = folderGenerator ?? throw new ArgumentNullException(nameof(folderGenerator));
		}


		public string Generate(Config config, EntityMetadata[] entityList)
		{
			var fieldExclusions = (config.FieldExclusions ?? string.Empty).Split(',').Select(_ => _.Trim()).ToArray();

			var rootPath = config.GetRootFolder();

			var directory = new DirectoryInfo(rootPath);
			if (!directory.Exists)
			{
				throw new ConfigurationErrorsException($"The directory <{rootPath}> does not exists!");
			}

			directory.Delete(true);
			directory.Create();


			var order = new StringBuilder();

			var readmeFileName = CreateReadme(rootPath);
			order.AppendLine(readmeFileName);



			foreach (var group in config.Groups)
			{
				var pageName = this.folderGenerator.Generate(rootPath, group, entityList, config.Locale, fieldExclusions);
				order.AppendLine(pageName);
			}


			// genero il file .order, che governa la sequenza con cui vengono mostrati i file nel wiki
			var fullPath = Path.Combine(rootPath, ".order");

			using (var writer = new StreamWriter(new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write)))
			{
				writer.Write(order.ToString());
				writer.Flush();
			}

			return rootPath;
		}

		private string CreateReadme(string rootPath)
		{
			var resourceName = "Avanade.DataModelDocumentGenerator.Templates.README.md";
			var resourceContent = Resources.GetContent(resourceName);

			var fullPath = Path.Combine(rootPath, "README.md");

			using (var writer = new StreamWriter(new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write)))
			{
				writer.Write(resourceContent);
				writer.Flush();
			}

			return "README";
		}
	}
}
