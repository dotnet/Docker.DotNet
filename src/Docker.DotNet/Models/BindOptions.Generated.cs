using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class BindOptions // (mount.BindOptions)
    {
        [JsonPropertyName("Propagation")]
        public string Propagation { get; set; }

        [JsonPropertyName("NonRecursive")]
        public bool NonRecursive { get; set; }

        [JsonPropertyName("CreateMountpoint")]
        public bool CreateMountpoint { get; set; }
    }
}
