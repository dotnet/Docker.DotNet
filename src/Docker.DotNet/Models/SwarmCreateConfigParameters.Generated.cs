using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class SwarmCreateConfigParameters // (main.SwarmCreateConfigParameters)
    {
        [DataMember(Name = "Config", EmitDefaultValue = false)]
        public ConfigSpec Config { get; set; }
    }
}
