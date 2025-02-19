using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class GetImageAnalysisResult
    {
        private readonly ILogger<GetImageAnalysisResult> _logger;
        private readonly ICosmosDbService _cosmosDbService;

        public GetImageAnalysisResult(ILogger<GetImageAnalysisResult> logger, ICosmosDbService cosmosDbService)
        {
            _logger = logger;
            _cosmosDbService = cosmosDbService;
        }

        [Function("GetImageAnalysisResult")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("Processing request to retrieve image analysis.");

            // Get "filename" parameter from the query string
            string filename = req.Query["filename"];

            if (string.IsNullOrEmpty(filename))
            {
                _logger.LogWarning("Filename parameter is missing in the request.");
                return new BadRequestObjectResult(new { message = "Filename parameter is required." });
            }

            try
            {
                // Query CosmosDB via injected service
                var analysisResult = await _cosmosDbService.GetAnalysisResultAsync(filename);

                if (analysisResult != null)
                {
                    _logger.LogInformation("Returning analysis result for filename: {filename}", filename);
                    return new OkObjectResult(analysisResult);
                }

                _logger.LogWarning("No analysis result found for filename: {filename}", filename);
                return new NotFoundObjectResult(new { message = "Analysis result not found.", filename });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error retrieving analysis result: {message}", ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
