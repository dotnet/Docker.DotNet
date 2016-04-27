using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class LogConfig // (container.LogConfig)
    {
        [DataMember(Name = "Type")]
        public string Type { get; set; }

        [DataMember(Name = "Config")]
        public IDictionary<string, string> Config { get; set; }
    }
}
