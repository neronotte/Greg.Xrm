namespace Greg.Xrm.ConstantsExtractor
{
	/// <summary>
	/// This class can help you to store settings for your plugin
	/// </summary>
	/// <remarks>
	/// This class must be XML serializable
	/// </remarks>
	public class Settings
	{
		public Settings()
		{
			this.GetCsConstants = true;
			this.GetJsConstants = true;
			this.ExtractTypes = true;
			this.ExtractDescriptions = true;
			this.NamespaceCs = string.Empty;
			this.NamespaceJs = string.Empty;
			this.JsHeaderLines = string.Empty;
			this.CsFolder = string.Empty;
			this.JsFolder = string.Empty;
			this.SolutionName = string.Empty;
		}


		public bool GetCsConstants { get; set; }
		public bool GetJsConstants { get; set; }
		public bool ExtractTypes { get; set; }
		public bool ExtractDescriptions { get; set; }
		public string NamespaceCs { get; set; }
		public string NamespaceJs { get; set; }
		public string JsHeaderLines { get; set; }

		public string CsFolder { get; set; }

		public string JsFolder { get; set; }

		public string SolutionName { get; set; }
	}
}