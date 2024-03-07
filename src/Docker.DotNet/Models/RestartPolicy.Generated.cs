using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class RestartPolicy // (container.RestartPolicy)
    {
        [JsonPropertyName("Name")]
        public RestartPolicyKind Name { get; set; }

        [JsonPropertyName("MaximumRetryCount")]
        public long MaximumRetryCount { get; set; }
    }
}
