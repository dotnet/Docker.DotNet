using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ConfigSpec
    {
        public ConfigSpec()
        {
        }

        /// <summary>
        /// User-defined name of the config
        /// </summary>
        [DataMember(Name = "Name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// User-defined key/value metadata
        /// </summary>
        [DataMember(Name = "Labels", EmitDefaultValue = false)]
        public IDictionary<string, string> Labels { get; set; }

        /// <summary>
        /// Base64-url-safe-encoded (RFC 4648) config data
        /// </summary>
        [DataMember(Name = "Data", EmitDefaultValue = false)]
        public string Data { get; set; }

        /// <summary>
        /// Driver represents a driver (network, logging, secrets).
        /// </summary>
        [DataMember(Name = "Templating", EmitDefaultValue = false)]
        public Templating Templating { get; set; }
    }
}
