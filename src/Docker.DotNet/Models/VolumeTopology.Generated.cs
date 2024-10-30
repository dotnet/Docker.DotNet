using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class VolumeTopology // (volume.Topology)
    {
        [JsonPropertyName("Segments")]
        public IDictionary<string, string> Segments { get; set; }
    }
}
