using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;

namespace Greg.Xrm.RoleEditor.Services
{
	public static class RequestLogger
	{
		static DirectoryInfo storageFolder;

		static DirectoryInfo GetOrCreateStorageFolder()
		{
			if (storageFolder != null)
				return storageFolder;

			var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			if (!Directory.Exists(folderPath))
			{
				Directory.CreateDirectory(folderPath);
			}

			folderPath = Path.Combine(folderPath, "Greg.Xrm.RoleEditor");
			if (!Directory.Exists(folderPath))
			{
				Directory.CreateDirectory(folderPath);
			}

			storageFolder = new DirectoryInfo(folderPath);
			return storageFolder;
		}


		public static void Log(OrganizationRequest request)
		{
			try
			{

				var fileName = $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}_{request.RequestName}.JSON";

				var requestJson = JsonConvert.SerializeObject(request, Formatting.Indented);

				File.WriteAllText(Path.Combine(GetOrCreateStorageFolder().FullName, fileName), requestJson);
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}

		internal static void Clear()
		{
			var folder = GetOrCreateStorageFolder();
			foreach (var file in folder.GetFiles())
			{
				try
				{
					file.Delete();
				}
				catch { }
			}
		}
	}
}
