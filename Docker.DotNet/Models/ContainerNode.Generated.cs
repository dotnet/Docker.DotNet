using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerNode // (types.ContainerNode)
    {
        [DataMember(Name = "ID")]
        public string ID { get; set; }

        [DataMember(Name = "IP")]
        public string IPAddress { get; set; }

        [DataMember(Name = "Addr")]
        public string Addr { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Cpus")]
        public int Cpus { get; set; }

        [DataMember(Name = "Memory")]
        public int Memory { get; set; }

        [DataMember(Name = "Labels")]
        public IDictionary<string, string> Labels { get; set; }
    }
}
