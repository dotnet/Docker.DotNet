using System;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class VersionResponse
    {
        [DataMember(Name = "ApiVersion")]
        public Version ApiVersion { get; set; }

        [DataMember(Name = "Version")]
        public Version Version { get; set; }

        [DataMember(Name = "GitCommit")]
        public string GitCommit { get; set; }

        [DataMember(Name = "GoVersion")]
        public string GoVersion { get; set; }

        public VersionResponse()
        {
        }
    }
}