using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class SwarmCreateConfigParameters
    {
        public SwarmCreateConfigParameters()
        {

        }

        [DataMember(Name = "Config", EmitDefaultValue = false)]
        public ConfigSpec Config { get; set; }
    }
}
