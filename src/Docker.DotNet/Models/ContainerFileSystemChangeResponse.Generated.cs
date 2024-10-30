using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ContainerFileSystemChangeResponse // (container.FilesystemChange)
    {
        [JsonPropertyName("Kind")]
        public FileSystemChangeKind Kind { get; set; }

        [JsonPropertyName("Path")]
        public string Path { get; set; }
    }
}
