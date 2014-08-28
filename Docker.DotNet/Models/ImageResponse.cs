using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImageResponse
    {
        [DataMember(Name = "Id")]
        public string Id { get; set; }

        [DataMember(Name = "Parent")]
        public string Parent { get; set; }

        [DataMember(Name = "Container")]
        public string Container { get; set; }

        [DataMember(Name = "Author")]
        public string Author { get; set; }

        [DataMember(Name = "Comment")]
        public string Comment { get; set; }

        [DataMember(Name = "Os")]
        public string Os { get; set; }

        [DataMember(Name = "Size")]
        public long Size { get; set; }

        [DataMember(Name = "Architecture")]
        public string Architecture { get; set; }

        [DataMember(Name = "Created")]
        public string Created { get; set; }

        [DataMember(Name = "DockerVersion")]
        public string DockerVersion { get; set; }

        [DataMember(Name = "Config")]
        public Config Config { get; set; }

        [DataMember(Name = "ContainerConfig")]
        public Config ContainerConfig { get; set; }

        public ImageResponse()
        {
        }
    }
}