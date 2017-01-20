using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class SwarmLeaveParameters // (main.SwarmLeaveParameters)
    {
        [DataMember(Name = "Force", EmitDefaultValue = false)]
        public bool Force { get; set; }
    }
}
