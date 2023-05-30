using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class WaitExitError // (container.WaitExitError)
    {
        [DataMember(Name = "Message", EmitDefaultValue = false)]
        public string Message { get; set; }
    }
}
