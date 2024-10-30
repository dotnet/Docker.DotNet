using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class GraphDriverData // (types.GraphDriverData)
    {
        [JsonPropertyName("Data")]
        public IDictionary<string, string> Data { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }
    }
}
