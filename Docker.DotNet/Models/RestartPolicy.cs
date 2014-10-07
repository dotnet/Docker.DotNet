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

    public class RestartPolicy
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public RestartPolicyKind Name { get; set; }

        public int MaximumRetryCount { get; set; }
    }
}