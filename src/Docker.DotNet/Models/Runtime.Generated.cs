using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class Runtime // (types.Runtime)
    {
        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("runtimeArgs")]
        public IList<string> Args { get; set; }

        [JsonPropertyName("runtimeType")]
        public string Type { get; set; }

        [JsonPropertyName("options")]
        public IDictionary<string, object> Options { get; set; }
    }
}
