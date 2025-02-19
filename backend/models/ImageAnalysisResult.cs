using System;
using Newtonsoft.Json;

namespace Company.Function
{
    public class ImageAnalysisResult
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("analysis")]
        public string Analysis { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
