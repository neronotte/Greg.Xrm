using System.IO;
using System.Reflection;

namespace Greg.Xrm.DataModelWikiEditor.Model
{
	public static class Resources
	{
		public static string GetContent(string resourceName)
		{
			var assembly = Assembly.GetExecutingAssembly();

			using (var stream = assembly.GetManifestResourceStream(resourceName))
			using (var reader = new StreamReader(stream))
			{
				string result = reader.ReadToEnd();
				return result;
			}
		}
	}
}
