using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class RootFS // (types.RootFS)
    {
        [JsonPropertyName("Type")]
        public string Type { get; set; }

        [JsonPropertyName("Layers")]
        public IList<string> Layers { get; set; }
    }
}
