using Greg.Xrm.Views;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Greg.Xrm.DataModelWikiEditor.Views
{
	public class OpenWikiFolderCommand : CommandBase
	{
		private readonly ISettingsProvider<Settings> settingsProvider;

		public OpenWikiFolderCommand(ISettingsProvider<Settings> settingsProvider)
		{
			this.settingsProvider = settingsProvider ?? throw new ArgumentNullException(nameof(settingsProvider));
		}


		protected override void ExecuteInternal(object arg)
		{
			var settings = this.settingsProvider.GetSettings();

			using (var dialog = new FolderBrowserDialog())
			{
				dialog.Description = "Open wiki folder...";
				dialog.ShowNewFolderButton = true;

				if (!string.IsNullOrWhiteSpace(settings.LastUsedFolder) && Directory.Exists(settings.LastUsedFolder))
				{
					dialog.SelectedPath = settings.LastUsedFolder;
				}

				if (dialog.ShowDialog() != DialogResult.OK) return;


				settings.LastUsedFolder = dialog.SelectedPath;
				settings.Save();
			}

			var directory = new DirectoryInfo(settings.LastUsedFolder);

			//var configFile = directory.GetFiles("config.json").FirstOrDefault();
		}
	}
}
