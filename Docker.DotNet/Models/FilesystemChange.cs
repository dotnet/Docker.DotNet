using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class FilesystemChange
    {
        public enum FilesystemChangeKind
        {
            Modify = 0,
            Add,
            Delete
        }

        [DataMember(Name = "Path")]
        public string Path { get; set; }

        [DataMember(Name = "Kind")]
        public FilesystemChangeKind Kind { get; set; }

        public FilesystemChange()
        {
        }
    }
}