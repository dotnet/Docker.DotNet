using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class VersionResponse
    {
        [DataMember(Name = "ApiVersion")]
        public string ApiVersion { get; set; }

        [DataMember(Name = "Version")]
        public string Version { get; set; }

        [DataMember(Name = "GitCommit")]
        public string GitCommit { get; set; }

        [DataMember(Name = "GoVersion")]
        public string GoVersion { get; set; }

        public VersionResponse()
        {
        }
    }
}