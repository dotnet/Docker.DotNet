using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerFileSystemChangeResponse // (container.FilesystemChange)
    {
        [DataMember(Name = "Kind", EmitDefaultValue = false)]
        public FileSystemChangeKind Kind { get; set; }

        [DataMember(Name = "Path", EmitDefaultValue = false)]
        public string Path { get; set; }
    }
}
