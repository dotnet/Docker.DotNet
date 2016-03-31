using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class NetworksCreateResponse // (types.NetworkCreateResponse)
    {
        [DataMember(Name = "Id")]
        public string ID { get; set; }

        [DataMember(Name = "Warning")]
        public string Warning { get; set; }
    }
}
