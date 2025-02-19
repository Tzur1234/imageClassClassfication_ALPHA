using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Function
{
    public class ImageDescriptionService : IImageDescriptionService
    {
        private readonly ILogger<BlobStorageImageAnalyzer> _logger;
        private readonly string _blobSasToken;
        private readonly Uri _endpoint;
        private readonly AzureKeyCredential _credentials;
        private readonly string _deploymentName;

        // Constructor that accepts configuration parameters
        public ImageDescriptionService(IConfiguration configuration, ILogger<BlobStorageImageAnalyzer> logger)
        {
            _logger = logger;
            // Store sensitive data securely from configuration
            _endpoint = new Uri(configuration["AZURE_OPENAI_ENDPOINT"]);
            _credentials = new AzureKeyCredential(configuration["AZURE_OPENAI_KEY"]);
            _deploymentName = "gpt-4o";  // Could be moved to configuration as well if needed
        }

        public async Task<string> AnalyzeImageAsync(string imageUrl)
        {

            // Initialize Azure OpenAI client
            AzureOpenAIClient openAIClient = new AzureOpenAIClient(_endpoint, _credentials);
            var chatClient = openAIClient.GetChatClient(_deploymentName);

            // Create the message to send to the OpenAI model
            List<ChatMessage> messages = new List<ChatMessage>
            {
                new UserChatMessage(
                    ChatMessageContentPart.CreateTextPart("Please describe the following image:"),
                    ChatMessageContentPart.CreateImagePart(new Uri(imageUrl), "auto"))
            };

            // Call the OpenAI model and await the response
            ChatCompletion chatCompletion = await chatClient.CompleteChatAsync(messages);
            string analysisResult = chatCompletion.Content[0].Text;
            
            // Log the result
            _logger.LogInformation($"[Image Process Result]: {analysisResult}");
             return analysisResult;

        }
    }
}
