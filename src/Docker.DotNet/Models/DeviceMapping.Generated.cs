using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class DeviceMapping // (container.DeviceMapping)
    {
        [JsonPropertyName("PathOnHost")]
        public string PathOnHost { get; set; }

        [JsonPropertyName("PathInContainer")]
        public string PathInContainer { get; set; }

        [JsonPropertyName("CgroupPermissions")]
        public string CgroupPermissions { get; set; }
    }
}
