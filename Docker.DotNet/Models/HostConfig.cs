using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class HostConfig
    {
        [DataMember(Name = "Binds")]
        public IList<string> Binds { get; set; }

        [DataMember(Name = "Links")]
        public IList<string> Links { get; set; }

        [DataMember(Name = "ContainerIDFile")]
        public string ContainerIdFile { get; set; }

        [DataMember(Name = "Privileged")]
        public bool Privileged { get; set; }

        [DataMember(Name = "PortBindings")] public IDictionary<string, IList<PortBinding>> PortBindings;

        [DataMember(Name = "PublishAllPorts")]
        public bool PublishAllPorts { get; set; }

        [DataMember(Name = "Dns")]
        public IList<string> Dns { get; set; }

        [DataMember(Name = "DnsSearch")]
        public IList<string> DnsSearch { get; set; }

        [DataMember(Name = "VolumesFrom")]
        public IList<string> VolumesFrom { get; set; }

        [DataMember(Name = "RestartPolicy")]
        public RestartPolicy RestartPolicy { get; set; }

        // Commenting out LxcConf parameter because its type in the request
        // form and response form are not the same. (one example says it's [], another
        // example says it's {"key": "value"}, API is totally messed up with such inconsistencies.
        // In order to make methods using this HostConfig type to work, commenting this out.
        //[DataMember(Name = "LxcConf")]
        //public IDictionary<string, string> LxcConf { get; set; }

        public HostConfig()
        {
        }
    }
}