using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class VersionResponse // (types.Version)
    {
        [DataMember(Name = "Version")]
        public string Version { get; set; }

        [DataMember(Name = "ApiVersion")]
        public string APIVersion { get; set; }

        [DataMember(Name = "GitCommit")]
        public string GitCommit { get; set; }

        [DataMember(Name = "GoVersion")]
        public string GoVersion { get; set; }

        [DataMember(Name = "Os")]
        public string Os { get; set; }

        [DataMember(Name = "Arch")]
        public string Arch { get; set; }

        [DataMember(Name = "KernelVersion")]
        public string KernelVersion { get; set; }

        [DataMember(Name = "Experimental")]
        public bool Experimental { get; set; }

        [DataMember(Name = "BuildTime")]
        public string BuildTime { get; set; }
    }
}
