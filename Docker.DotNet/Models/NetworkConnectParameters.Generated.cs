using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class NetworkConnectParameters // (types.NetworkConnect)
    {
        [DataMember(Name = "Container")]
        public string Container { get; set; }

        [DataMember(Name = "EndpointConfig")]
        public EndpointSettings EndpointConfig { get; set; }
    }
}
