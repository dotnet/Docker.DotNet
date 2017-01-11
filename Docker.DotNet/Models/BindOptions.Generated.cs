using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class BindOptions // (mount.BindOptions)
    {
        [DataMember(Name = "Propagation", EmitDefaultValue = false)]
        public string Propagation { get; set; }
    }
}
