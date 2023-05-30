using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class UsageData // (volume.UsageData)
    {
        [DataMember(Name = "RefCount", EmitDefaultValue = false)]
        public long RefCount { get; set; }

        [DataMember(Name = "Size", EmitDefaultValue = false)]
        public long Size { get; set; }
    }
}
