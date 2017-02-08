using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class SwarmJoinParameters // (swarm.JoinRequest)
    {
        [DataMember(Name = "ListenAddr", EmitDefaultValue = false)]
        public string ListenAddr { get; set; }

        [DataMember(Name = "AdvertiseAddr", EmitDefaultValue = false)]
        public string AdvertiseAddr { get; set; }

        [DataMember(Name = "RemoteAddrs", EmitDefaultValue = false)]
        public IList<string> RemoteAddrs { get; set; }

        [DataMember(Name = "JoinToken", EmitDefaultValue = false)]
        public string JoinToken { get; set; }
    }
}
