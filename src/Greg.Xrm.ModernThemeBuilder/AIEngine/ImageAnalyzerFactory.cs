using System;

namespace Greg.Xrm.ModernThemeBuilder.AIEngine
{
	public static class ImageAnalyzerFactory
	{
		public static IImageAnalyzer CreateAnalyzer(AIProvider provider, Settings settings)
		{
			if (provider == AIProvider.AzureOpenAI)
			{
				return new AzureOpenAIImageAnalyzer(settings.AzureOpenAiEndpoint, settings.AzureOpenAiApiKey, settings.AzureOpenAiDeploymentName);
			}
			if (provider == AIProvider.Claude)
			{
				return new ClaudeImageAnalyzer(settings.ClaudeApiKey);
			}
			if (provider == AIProvider.OpenAI)
			{
				return new OpenAIImageAnalyzer(settings.ChatGptApiKey);
			}

			throw new ArgumentException($"Unsupported AI provider: {provider}");
		}
	}
}
