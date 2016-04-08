using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class DeviceMapping // (container.DeviceMapping)
    {
        [DataMember(Name = "PathOnHost")]
        public string PathOnHost { get; set; }

        [DataMember(Name = "PathInContainer")]
        public string PathInContainer { get; set; }

        [DataMember(Name = "CgroupPermissions")]
        public string CgroupPermissions { get; set; }
    }
}
