using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class LogConfig // (container.LogConfig)
    {
        [JsonPropertyName("Type")]
        public string Type { get; set; }

        [JsonPropertyName("Config")]
        public IDictionary<string, string> Config { get; set; }
    }
}
