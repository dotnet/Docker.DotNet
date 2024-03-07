using System;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ImageMetadata // (types.ImageMetadata)
    {
        [JsonPropertyName("LastTagTime")]
        public DateTime LastTagTime { get; set; }
    }
}
