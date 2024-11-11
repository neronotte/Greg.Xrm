using Greg.Xrm.Logging;
using Newtonsoft.Json;
using System;

namespace Greg.Xrm.RoleEditor.Services.Snippets
{
	public class PrivilegeSnippetRepository : IPrivilegeSnippetRepository
	{
		private readonly object syncRoot = new object();
		private readonly ILog log;
		private readonly ISettingsProvider<Settings> settingsProvider;

		public PrivilegeSnippetRepository(ILog log, ISettingsProvider<Settings> settingsProvider)
        {
			this.log = log;
			this.settingsProvider = settingsProvider ?? throw new ArgumentNullException(nameof(settingsProvider));
		}


		public PrivilegeSnippet this[int index]
		{
			get => Get(index);
			set => Set(index, value);
		}



		public PrivilegeSnippet Get(int index)
		{
			lock(this.syncRoot)
			{
				var settings = this.settingsProvider.GetSettings();

				PrivilegeSnippet[] snippets = null;
				try
				{
					snippets = JsonConvert.DeserializeObject<PrivilegeSnippet[]>(settings.PrivilegeSnippets);
				}
				catch (Exception ex)
				{
					this.log.Error("Error deserializing privilege snippets: " + ex.Message, ex);
					snippets = PrivilegeSnippet.DefaultSnippets;
				}

				if (snippets.Length < 10)
				{
					var newSnippets = new PrivilegeSnippet[10];
					Array.Copy(snippets, newSnippets, snippets.Length);
					snippets = newSnippets;
				}

				return snippets[index];
			}
		}




		public void Set(int index, PrivilegeSnippet snippet)
		{
			if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), "index should be >= 0 and < 10");
			if (index >= 10) throw new ArgumentOutOfRangeException(nameof(index), "index should be >= 0 and < 10");

			lock (this.syncRoot)
			{
				var settings = this.settingsProvider.GetSettings();

				PrivilegeSnippet[] snippets = null;
				try
				{
					snippets = JsonConvert.DeserializeObject<PrivilegeSnippet[]>(settings.PrivilegeSnippets);
				}
				catch(Exception ex)
				{
					this.log.Error("Error deserializing privilege snippets: " + ex.Message, ex);
					snippets = PrivilegeSnippet.DefaultSnippets;
				}

				if (snippets.Length < 10)
				{
					var newSnippets = new PrivilegeSnippet[10];
					Array.Copy(snippets, newSnippets, snippets.Length);
					snippets = newSnippets;
				}

				snippets[index] = snippet;
				settings.PrivilegeSnippets = JsonConvert.SerializeObject(snippets, Formatting.Indented);
				settings.Save();
			}
		}
	}
}
