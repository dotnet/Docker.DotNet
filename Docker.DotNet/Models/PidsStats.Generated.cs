using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class PidsStats // (types.PidsStats)
    {
        [DataMember(Name = "current")]
        public ulong Current { get; set; }
    }
}
