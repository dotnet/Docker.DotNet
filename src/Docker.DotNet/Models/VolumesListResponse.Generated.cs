using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class VolumesListResponse // (main.VolumesListResponse)
    {
        [JsonPropertyName("Volumes")]
        public IList<VolumeResponse> Volumes { get; set; }

        [JsonPropertyName("Warnings")]
        public IList<string> Warnings { get; set; }
    }
}
