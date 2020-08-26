using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ConfigUpdateParameters
    {
        /// <summary>
        /// The version number of the config object being updated. This is required to avoid conflicting writes
        /// </summary>
        [QueryStringParameter("version", true)]
        public long Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "Config", EmitDefaultValue = false)]
        public ConfigSpec Config { get; set; }
    }
}
