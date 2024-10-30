using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ClusterVolume // (volume.ClusterVolume)
    {
        public ClusterVolume()
        {
        }

        public ClusterVolume(Meta Meta)
        {
            if (Meta != null)
            {
                this.Version = Meta.Version;
                this.CreatedAt = Meta.CreatedAt;
                this.UpdatedAt = Meta.UpdatedAt;
            }
        }

        [JsonPropertyName("ID")]
        public string ID { get; set; }

        [JsonPropertyName("Version")]
        public Version Version { get; set; }

        [JsonPropertyName("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("Spec")]
        public ClusterVolumeSpec Spec { get; set; }

        [JsonPropertyName("PublishStatus")]
        public IList<PublishStatus> PublishStatus { get; set; }

        [JsonPropertyName("Info")]
        public VolumeInfo Info { get; set; }
    }
}
