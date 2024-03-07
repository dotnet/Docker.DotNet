using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class PublishStatus // (volume.PublishStatus)
    {
        [JsonPropertyName("NodeID")]
        public string NodeID { get; set; }

        [JsonPropertyName("State")]
        public string State { get; set; }

        [JsonPropertyName("PublishContext")]
        public IDictionary<string, string> PublishContext { get; set; }
    }
}
