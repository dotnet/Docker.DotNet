using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    public enum RestartPolicyKind
    {
        [EnumMember(Value = "")]
        Undefined,

        [EnumMember(Value = "no")]
        No,

        [EnumMember(Value = "always")]
        Always,

        [EnumMember(Value = "on-failure")]
        OnFailure,

        [EnumMember(Value = "unless-stopped")]
        UnlessStopped
    }

    [DataContract]
    public class RestartPolicy
    {
        [DataMember(Name = "Name")]
        public RestartPolicyKind Name { get; set; }

        [DataMember(Name = "MaximumRetryCount")]
        public int MaximumRetryCount { get; set; }
    }
}