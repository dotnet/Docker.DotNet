using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class DeviceRequest // (container.DeviceRequest)
    {
        [JsonPropertyName("Driver")]
        public string Driver { get; set; }

        [JsonPropertyName("Count")]
        public long Count { get; set; }

        [JsonPropertyName("DeviceIDs")]
        public IList<string> DeviceIDs { get; set; }

        [JsonPropertyName("Capabilities")]
        public IList<IList<string>> Capabilities { get; set; }

        [JsonPropertyName("Options")]
        public IDictionary<string, string> Options { get; set; }
    }
}
