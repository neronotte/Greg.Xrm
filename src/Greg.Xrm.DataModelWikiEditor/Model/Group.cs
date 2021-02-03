using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Greg.Xrm.DataModelWikiEditor.Model
{
	public class Group
	{
		public Group(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace", nameof(name));
			}

			this.Name = name;
			this.Groups = new List<Group>();
			this.Entities = new List<string>();
		}

		protected Group()
		{
			this.Groups = new List<Group>();
			this.Entities = new List<string>();
		}

		[JsonProperty("name")]
		public string Name { get; }


		[JsonProperty("entities")]
		public List<string> Entities { get; }


		[JsonProperty("groups")]
		public List<Group> Groups { get; }
	}
}
