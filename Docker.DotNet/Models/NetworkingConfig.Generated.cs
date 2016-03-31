using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class NetworkingConfig // (network.NetworkingConfig)
    {
        [DataMember(Name = "EndpointsConfig")]
        public IDictionary<string, EndpointSettings> EndpointsConfig { get; set; }
    }
}
