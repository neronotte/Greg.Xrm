using OpenAI.Chat;
using OpenAI;
using System;
using System.Threading.Tasks;
using System.IO;
using Greg.Xrm.Logging;

namespace Greg.Xrm.ModernThemeBuilder.AIEngine
{
	public class OpenAIImageAnalyzer : IImageAnalyzer
	{
		private readonly OpenAIClient _client;

		public OpenAIImageAnalyzer(string apiKey)
		{
			_client = new OpenAIClient(apiKey);
		}

		public async Task<string> AnalyzeImageAsync(ILog log, string imagePath, string prompt)
		{
			log.Debug("Starting image analysis with ChatGPT...");

			var chatClient = _client.GetChatClient("gpt-4o");

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

			log.Debug("Sending request to ChatGPT...");

			// Send the request and get response
			var response = await chatClient.CompleteChatAsync(messages);

			var responseText = response.Value.Content[0].Text;

			log.Debug("Response received from ChatGPT is: " + responseText );

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
