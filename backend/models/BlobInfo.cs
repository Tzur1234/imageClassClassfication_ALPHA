using Newtonsoft.Json;
namespace Company.Function
{
    public class StorageDiagnostics
    {
        [JsonProperty("batchId")]
        public string BatchId { get; set; }
    }

    public class BlobInfo
    {
        [JsonProperty("api")]
        public string Api { get; set; }

        [JsonProperty("clientRequestId")]
        public string ClientRequestId { get; set; }

        [JsonProperty("requestId")]
        public string RequestId { get; set; }

        [JsonProperty("eTag")]
        public string ETag { get; set; }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        [JsonProperty("contentLength")]
        public int ContentLength { get; set; }

        [JsonProperty("blobType")]
        public string BlobType { get; set; }

        [JsonProperty("accessTier")]
        public string AccessTier { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("sequencer")]
        public string Sequencer { get; set; }

        [JsonProperty("storageDiagnostics")]
        public StorageDiagnostics StorageDiagnostics { get; set; }
    }


}