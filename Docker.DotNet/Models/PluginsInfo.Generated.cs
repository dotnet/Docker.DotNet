using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class PluginsInfo // (types.PluginsInfo)
    {
        [DataMember(Name = "Volume")]
        public IList<string> Volume { get; set; }

        [DataMember(Name = "Network")]
        public IList<string> Network { get; set; }

        [DataMember(Name = "Authorization")]
        public IList<string> Authorization { get; set; }
    }
}
