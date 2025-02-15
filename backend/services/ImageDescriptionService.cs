using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Function
{
    public class ImageDescriptionService : IImageDescriptionService
    {
        private readonly string _blobSasToken;
        private readonly Uri _endpoint;
        private readonly AzureKeyCredential _credentials;
        private readonly string _deploymentName;

        // Constructor that accepts configuration parameters
        public ImageDescriptionService(IConfiguration configuration)
        {
            // Store sensitive data securely from configuration
            _blobSasToken = configuration["BLOB_SAS_TOKEN"];
            _endpoint = new Uri(configuration["AZURE_OPENAI_ENDPOINT"]);
            _credentials = new AzureKeyCredential(configuration["AZURE_OPENAI_KEY"]);
            _deploymentName = "gpt-4o";  // Could be moved to configuration as well if needed
        }

        public async Task AnalyzeImageAsync(string imageUrl)
        {
            // Prepare the image URL with SAS token
            string finalImageUrlWithToken = $"{imageUrl}?{_blobSasToken}";

            // Initialize Azure OpenAI client
            AzureOpenAIClient openAIClient = new AzureOpenAIClient(_endpoint, _credentials);
            var chatClient = openAIClient.GetChatClient(_deploymentName);

            // Create the message to send to the OpenAI model
            List<ChatMessage> messages = new List<ChatMessage>
            {
                new UserChatMessage(
                    ChatMessageContentPart.CreateTextPart("Please describe the following image:"),
                    ChatMessageContentPart.CreateImagePart(new Uri(finalImageUrlWithToken), "auto"))
            };

            // Call the OpenAI model and await the response
            ChatCompletion chatCompletion = await chatClient.CompleteChatAsync(messages);

            // Log the result
            Console.WriteLine($"[ASSISTANT]: {chatCompletion.Content[0].Text}");
        }
    }
}
