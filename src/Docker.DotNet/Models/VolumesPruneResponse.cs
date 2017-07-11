using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class VolumesPruneResponse
    {
        /// <summary>
        /// Volumes that were deleted.
        /// </summary>
        [DataMember(Name = "VolumesDeleted", EmitDefaultValue = false)]
        public string[] VolumesDeleted { get; set; }

        /// <summary>
        /// Disk space reclaimed in bytes.
        /// </summary>
        [DataMember(Name = "SpaceReclaimed", EmitDefaultValue = false)]
        public long SpaceReclaimed { get; set; }
    }
}
