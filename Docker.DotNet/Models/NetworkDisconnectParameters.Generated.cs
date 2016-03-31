using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class NetworkDisconnectParameters // (types.NetworkDisconnect)
    {
        [DataMember(Name = "Container")]
        public string Container { get; set; }

        [DataMember(Name = "Force")]
        public bool Force { get; set; }
    }
}
