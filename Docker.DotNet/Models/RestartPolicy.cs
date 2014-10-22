using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    public enum RestartPolicyKind
    {
        [EnumMember(Value = "always")]
        Always,

        [EnumMember(Value = "on-failure")]
        OnFailure
    }

    [DataContract] 
    public class RestartPolicy
    {
        [DataMember(Name="Name")]
        public RestartPolicyKind Name { get; set; }
        [DataMember(Name = "MaximumRetryCount")]
        public int MaximumRetryCount { get; set; }
    }
}