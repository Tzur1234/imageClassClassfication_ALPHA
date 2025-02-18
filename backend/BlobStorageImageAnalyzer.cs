// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}

using System;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public class BlobStorageImageAnalyzer
    {

        private readonly ILogger<BlobStorageImageAnalyzer> _logger;

        private readonly IImageDescriptionService _imageDescriptionService;

        public BlobStorageImageAnalyzer(ILogger<BlobStorageImageAnalyzer> logger, IImageDescriptionService imageDescriptionService)
        {
            _logger = logger;
            _imageDescriptionService = imageDescriptionService;
        }

        [Function(nameof(BlobStorageImageAnalyzer))]
        public async Task Run([EventGridTrigger] CloudEvent cloudEvent)
        {
            // Log basic event details
            _logger.LogInformation("Event Grid Triggered: EventType: {eventType}, Subject: {eventSubject}", cloudEvent.Type, cloudEvent.Subject);

            // Check if the event is of type "BlobCreated"
            if (!FuncUtils.IsBlobCreatedEventType(cloudEvent.Type))
            {
                _logger.LogInformation("Received non-BlobCreated event. Skipping...");
                return;
            }

            // Check if cloudEvent.Data is not null
            if (cloudEvent.Data == null)
            {
                _logger.LogInformation("Received an event with no data. Skipping...");
                return;
            }

            try
            {
                // Deserialize the cloudEvent.Data into the strongly typed object
                BlobInfo eventData = JsonConvert.DeserializeObject<BlobInfo>(cloudEvent.Data.ToString());

                // Check if event data was successfully deserialized
                if (eventData == null)
                {
                    _logger.LogWarning("Failed to deserialize event data.");
                    return;
                }

                // Log blob creation details
                _logger.LogInformation("Blob Created: URL: {url}, ContentType: {contentType}", eventData.Url, eventData.ContentType);

                // Process the image asynchronously
                // await Test2.RunAsync2(eventData.Url);
                await _imageDescriptionService.AnalyzeImageAsync(eventData.Url);
            }
            catch (Exception ex)
            {
                // Log the exception details
                _logger.LogError("Error while processing event data: {message}, StackTrace: {stackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
