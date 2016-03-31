using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class SummaryNetworkSettings // (types.SummaryNetworkSettings)
    {
        [DataMember(Name = "Networks")]
        public IDictionary<string, EndpointSettings> Networks { get; set; }
    }
}
