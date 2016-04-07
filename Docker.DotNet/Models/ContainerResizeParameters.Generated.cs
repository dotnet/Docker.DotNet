using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerResizeParameters // (types.ResizeOptions)
    {
        [DataMember(Name = "ID")]
        public string ID { get; set; }

        [DataMember(Name = "Height")]
        public int Height { get; set; }

        [DataMember(Name = "Width")]
        public int Width { get; set; }
    }
}
