namespace Greg.Xrm.ModernThemeBuilder
{
	/// <summary>
	/// This class can help you to store settings for your plugin
	/// </summary>
	/// <remarks>
	/// This class must be XML serializable
	/// </remarks>
	public class Settings : SettingsBase<ModernThemeBuilderPlugin>
	{
		public string AzureOpenAiApiKey { get; set; }
		public string AzureOpenAiEndpoint { get; set; }
		public string AzureOpenAiDeploymentName { get; set; }
		public string ChatGptApiKey { get; set; }
		public string ClaudeApiKey { get; set; }

		public bool IsValid()
		{
			return IsAzureOpenAiValid() ||
				   IsChatGptValid() ||
				   IsClaudeValid();
		}

		public bool IsClaudeValid()
		{
			return !string.IsNullOrWhiteSpace(ClaudeApiKey);
		}

		public bool IsChatGptValid()
		{
			return !string.IsNullOrWhiteSpace(ChatGptApiKey);
		}

		public bool IsAzureOpenAiValid()
		{
			return !string.IsNullOrWhiteSpace(AzureOpenAiApiKey) &&
				   !string.IsNullOrWhiteSpace(AzureOpenAiEndpoint) &&
				   !string.IsNullOrWhiteSpace(AzureOpenAiDeploymentName);
		}
	}
}