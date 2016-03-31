using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class VolumesListResponse // (main.VolumesListResponse)
    {
        [DataMember(Name = "Volumes")]
        public IList<VolumeResponse> Volumes { get; set; }

        [DataMember(Name = "Warnings")]
        public IList<string> Warnings { get; set; }
    }
}
