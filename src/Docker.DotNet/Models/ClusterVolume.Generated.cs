using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
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

        [DataMember(Name = "ID", EmitDefaultValue = false)]
        public string ID { get; set; }

        [DataMember(Name = "Version", EmitDefaultValue = false)]
        public Version Version { get; set; }

        [DataMember(Name = "CreatedAt", EmitDefaultValue = false)]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "UpdatedAt", EmitDefaultValue = false)]
        public DateTime UpdatedAt { get; set; }

        [DataMember(Name = "Spec", EmitDefaultValue = false)]
        public ClusterVolumeSpec Spec { get; set; }

        [DataMember(Name = "PublishStatus", EmitDefaultValue = false)]
        public IList<PublishStatus> PublishStatus { get; set; }

        [DataMember(Name = "Info", EmitDefaultValue = false)]
        public VolumeInfo Info { get; set; }
    }
}
