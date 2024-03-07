using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class TypeMount // (volume.TypeMount)
    {
        [JsonPropertyName("FsType")]
        public string FsType { get; set; }

        [JsonPropertyName("MountFlags")]
        public IList<string> MountFlags { get; set; }
    }
}
