using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class PluginConfigInterface // (types.PluginConfigInterface)
    {
        [DataMember(Name = "Socket", EmitDefaultValue = false)]
        public string Socket { get; set; }

        [DataMember(Name = "Types", EmitDefaultValue = false)]
        public IList<PluginInterfaceType> Types { get; set; }
    }
}
