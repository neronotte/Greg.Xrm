using Greg.Xrm.Logging;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Greg.Xrm.ModernThemeBuilder.AIEngine
{
	public class ClaudeImageAnalyzer : IImageAnalyzer
	{
		private readonly string _apiKey;

		private const string CLAUDE_API_URL = "https://api.anthropic.com/v1/messages";

		public ClaudeImageAnalyzer(string apiKey)
		{
			_apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
		}

		public async Task<string> AnalyzeImageAsync(ILog log, string imagePath, string prompt)
		{
			log.Debug("Starting image analysis with Claude...");
			// Read and encode the image
			byte[] imageBytes = File.ReadAllBytes(imagePath);
			string base64Image = Convert.ToBase64String(imageBytes);
			string mediaType = GetImageFormat(imagePath);

			log.Debug($"Image loaded and encoded: {imagePath} ({base64Image.Length} bytes, {mediaType})");
			// Create the request payload
			log.Debug("Creating request payload for Claude API...");
			var requestBody = new
			{
				model = "claude-3-5-sonnet-20241022",
				max_tokens = 1024,
				messages = new[]
				{
					new
					{
						role = "user",
						content = new object[]
						{
							new
							{
								type = "image",
								source = new
								{
									type = "base64",
									media_type = mediaType,
									data = base64Image
								}
							},
							new
							{
								type = "text",
								text = prompt
							}
						}
					}
				}
			};

			string json = JsonSerializer.Serialize(requestBody, new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
			});

			var request = new HttpRequestMessage(HttpMethod.Post, CLAUDE_API_URL)
			{
				Content = new StringContent(json, Encoding.UTF8, "application/json")
			};

			request.Headers.Add("x-api-key", _apiKey);
			request.Headers.Add("anthropic-version", "2023-06-01");

			log.Debug("Sending request to Claude...");

			using (var httpClient = new HttpClient())
			{
				var response = await httpClient.SendAsync(request);
				var responseContent = await response.Content.ReadAsStringAsync();

				if (!response.IsSuccessStatusCode)
				{
					log.Error($"Claude API request failed with status {response.StatusCode}: {responseContent}");
					throw new HttpRequestException($"Claude API request failed with status {response.StatusCode}: {responseContent}");
				}

				log.Debug("Response received from Claude is: " + responseContent);

				using (var document = JsonDocument.Parse(responseContent))
				{
					var content = document.RootElement.GetProperty("content")[0].GetProperty("text").GetString();
					return content ?? "No valid response received from Claude.";
				}
			}
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

		public void Dispose()
		{
		}
	}
}
