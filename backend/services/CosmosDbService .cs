using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class CosmosDbService : ICosmosDbService
    {
        private readonly Container _container;
        private readonly ILogger<CosmosDbService> _logger;

        public CosmosDbService(IConfiguration configuration, ILogger<CosmosDbService> logger)
        {
            _logger = logger;
            
            // Read CosmosDB settings from config
            string cosmosDbConnection = configuration["CosmosDbConnectionSetting"];
            string databaseName = "ImageAnalysisDB";
            string containerName = "AnalysisResults";

            try
            {
                CosmosClient cosmosClient = new CosmosClient(cosmosDbConnection);
                _container = cosmosClient.GetContainer(databaseName, containerName);
                _logger.LogInformation("CosmosDB service initialized successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to initialize CosmosDB service: {message}", ex.Message);
                throw;
            }
        }

        public async Task UpsertAnalysisResultAsync(string id, string analysisResult)
        {
            var document = new
            {
                id = id,
                analysis = analysisResult,
                timestamp = DateTime.UtcNow
            };

            try
            {
                await _container.UpsertItemAsync(document, new PartitionKey(id));
                _logger.LogInformation("Stored analysis result in CosmosDB with imageID: {id}", id);
            }
            catch (CosmosException cosmosEx)
            {
                _logger.LogError("CosmosDB Error: {message} (Status Code: {statusCode})", cosmosEx.Message, cosmosEx.StatusCode);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error storing data in CosmosDB: {message}", ex.Message);
                throw;
            }
        }

        public async Task<dynamic> GetAnalysisResultAsync(string filename)
        {
            try
            {
                ItemResponse<ImageAnalysisResult> response = await _container.ReadItemAsync<ImageAnalysisResult>(filename, new PartitionKey(filename));
                ImageAnalysisResult result = response.Resource;
                return result;

                
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                _logger.LogWarning("No analysis result found for filename: {filename}", filename);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error retrieving analysis result: {message}", ex.Message);
                throw;
            }
        }
    }
}
