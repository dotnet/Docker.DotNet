using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class Templating
    {
        [DataMember(Name = "Name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "Options", EmitDefaultValue = false)]
        public IDictionary<string, string> Options { get; set; }
    }
}
