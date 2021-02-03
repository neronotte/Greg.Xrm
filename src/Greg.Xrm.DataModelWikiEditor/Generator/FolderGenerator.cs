using Greg.Xrm.DataModelWikiEditor.Model;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.IO;
using System.Text;

namespace Greg.Xrm.DataModelWikiEditor.Generator
{
	public class FolderGenerator
	{
		private readonly PageGenerator pageGenerator;

		public FolderGenerator(PageGenerator pageGenerator)
		{
			this.pageGenerator = pageGenerator ?? throw new ArgumentNullException(nameof(pageGenerator));
		}


		public string Generate(string folderPath, Group group, EntityMetadata[] entityList, int locale, string[] fieldExclusions)
		{
			var folderName = group.Name.Replace(' ', '-');
			var folderFullPath = Path.Combine(folderPath, folderName);

			if (!Directory.Exists(folderFullPath))
				Directory.CreateDirectory(folderFullPath);

			var order = new StringBuilder();
			var index = new StringBuilder();

			index.Append("# ").Append(group.Name).AppendLine();


			foreach (var childGroup in group.Groups)
			{
				var pageName = Generate(folderFullPath, childGroup, entityList, locale, fieldExclusions);
				order.AppendLine(pageName);
			}

			foreach (var entityName in group.Entities)
			{
				var pageName = this.pageGenerator.Generate(folderFullPath, entityName, entityList, locale, fieldExclusions);
				order.AppendLine(pageName);

				index.Append("- ").Append(entityName).AppendLine();
			}


			// genero il file .order, che governa la sequenza con cui vengono mostrati i file nel wiki
			var fullPath = Path.Combine(folderFullPath, ".order");

			using (var writer = new StreamWriter(new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write)))
			{
				writer.Write(order.ToString());
				writer.Flush();
			}


			fullPath = Path.Combine(folderPath, folderName + ".md");
			using (var writer = new StreamWriter(new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write)))
			{
				writer.Write(index.ToString());
				writer.Flush();
			}


			return folderName;
		}
	}
}
