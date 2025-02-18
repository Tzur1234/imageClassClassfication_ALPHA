using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class FileUpload
    {
        private readonly ILogger<FileUpload> _logger;

        public FileUpload(ILogger<FileUpload> logger)
        {
            _logger = logger;
        }

        [Function("FileUpload")]
        public async Task <IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous , "post")] HttpRequest req)
        {
            string Connection = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            string containerName = Environment.GetEnvironmentVariable("ContainerName");
            Stream myBlob = new MemoryStream();
            var file = req.Form.Files["File"];
            myBlob = file.OpenReadStream();
            var blobClient = new BlobContainerClient(Connection, containerName);
            var blob = blobClient.GetBlobClient(file.FileName);
            await blob.UploadAsync(myBlob);
            _logger.LogInformation("file uploaded successfylly");
            return new OkObjectResult("file uploaded successfylly");
        }
    }
}
