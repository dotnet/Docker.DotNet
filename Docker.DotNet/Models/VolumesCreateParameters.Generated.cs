using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class VolumesCreateParameters // (types.VolumeCreateRequest)
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Driver")]
        public string Driver { get; set; }

        [DataMember(Name = "DriverOpts")]
        public IDictionary<string, string> DriverOpts { get; set; }

        [DataMember(Name = "Labels")]
        public IDictionary<string, string> Labels { get; set; }
    }
}
