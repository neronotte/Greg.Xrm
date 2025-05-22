using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Greg.Xrm.DataModelWikiEditor.Model
{
	public class Config
	{
		public Config()
		{
			this.Groups = new List<Group>();
		}

		[JsonIgnore]
		public string FileName { get; set; }

		/// <summary>
		/// Absolute or relative path of the root folder
		/// </summary>
		[JsonProperty("rootFolderPath")]
		public string RootFolderPath { get; set; }

		/// <summary>
		/// Gets or sets the language of the documentation
		/// </summary>
		[JsonProperty("locale")]
		public int Locale { get; set; }

		/// <summary>
		/// Gets the list of groups
		/// </summary>
		[JsonProperty("groups")]
		public List<Group> Groups { get; }


		[JsonProperty("fieldExclusions")]
		public string FieldExclusions { get; set; }


		public string GetRootFolder()
		{
			var rootPath = this.RootFolderPath;
			if (rootPath.StartsWith("/"))
			{
				var fileInfo = new FileInfo(this.FileName);

				// è un path relativo.
				rootPath = Path.Combine(fileInfo.DirectoryName, this.RootFolderPath.TrimStart('/').Replace("/", "\\"));
			}
			return rootPath;
		}


		public bool MustSkipField(string logicalName)
		{
			return this.FieldExclusions
				.Split(',')
				.Select(_ => _.Trim())
				.Where(_ => !string.IsNullOrWhiteSpace(_))
				.Any(_ => string.Equals(_, logicalName, System.StringComparison.OrdinalIgnoreCase));
		}
	}
}
