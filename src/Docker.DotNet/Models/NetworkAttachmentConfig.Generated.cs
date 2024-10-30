using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class NetworkAttachmentConfig // (swarm.NetworkAttachmentConfig)
    {
        [JsonPropertyName("Target")]
        public string Target { get; set; }

        [JsonPropertyName("Aliases")]
        public IList<string> Aliases { get; set; }

        [JsonPropertyName("DriverOpts")]
        public IDictionary<string, string> DriverOpts { get; set; }
    }
}
