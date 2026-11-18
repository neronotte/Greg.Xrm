using Azure;
using Azure.AI.OpenAI;
using Greg.Xrm.Logging;
using OpenAI.Chat;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Greg.Xrm.ModernThemeBuilder.AIEngine
{
	public class AzureOpenAIImageAnalyzer : IImageAnalyzer
	{
		private readonly AzureOpenAIClient _client;
		private readonly string _deploymentName;

		public AzureOpenAIImageAnalyzer(string endpoint, string apiKey, string deploymentName)
		{
			if (string.IsNullOrEmpty(endpoint)) throw new ArgumentNullException(nameof(endpoint));
			if (string.IsNullOrEmpty(apiKey)) throw new ArgumentNullException(nameof(apiKey));
			if (string.IsNullOrEmpty(deploymentName)) throw new ArgumentNullException(nameof(deploymentName));

			_deploymentName = deploymentName;
			_client = new AzureOpenAIClient(
				new Uri(endpoint),
				new AzureKeyCredential(apiKey));
		}

		public async Task<string> AnalyzeImageAsync(ILog log, string imagePath, string prompt)
		{
			log.Debug("Starting image analysis with Azure OpenAI...");

			var chatClient = _client.GetChatClient(_deploymentName);

			// Read image as BinaryData
			byte[] imageBytes = File.ReadAllBytes(imagePath);
			BinaryData imageBinaryData = new BinaryData(imageBytes);

			// Determine image format
			string imageFormat = GetImageFormat(imagePath);

			// Create the chat completion request using BinaryData
			var messages = new ChatMessage[]
			{
				new UserChatMessage(
					ChatMessageContentPart.CreateTextPart(prompt),
					ChatMessageContentPart.CreateImagePart(imageBinaryData, imageFormat)
				)
			};

			log.Debug("Sending request to Azure OpenAI...");

			// Send the request and get response
			var response = await chatClient.CompleteChatAsync(messages);

			var responseText = response.Value.Content[0].Text;

			log.Debug("Response received from ChatGPT is: " + responseText);

			return responseText;
		}

		private static string GetImageFormat(string imagePath)
		{
			string extension = Path.GetExtension(imagePath).ToLower();
			switch (extension)
			{
				case ".jpg":
				case ".jpeg":
					return "image/jpeg";
				case ".png":
					return "image/png";
				case ".gif":
					return "image/gif";
				default:
					return "image/jpeg";
			}
		}
	}
}
