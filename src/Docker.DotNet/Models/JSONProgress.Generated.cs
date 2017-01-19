using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class JSONProgress // (jsonmessage.JSONProgress)
    {
        [DataMember(Name = "current", EmitDefaultValue = false)]
        public long Current { get; set; }

        [DataMember(Name = "total", EmitDefaultValue = false)]
        public long Total { get; set; }

        [DataMember(Name = "start", EmitDefaultValue = false)]
        public long Start { get; set; }
    }
}
