using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ConfigCreateResponse
    {
        [DataMember(Name = "ID", EmitDefaultValue = false)]
        public string Id { get; set; }
    }
}
