using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class WeightDevice // (blkiodev.WeightDevice)
    {
        [DataMember(Name = "Path")]
        public string Path { get; set; }

        [DataMember(Name = "Weight")]
        public ushort Weight { get; set; }
    }
}
