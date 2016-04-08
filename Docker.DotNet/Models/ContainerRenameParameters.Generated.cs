using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerRenameParameters // (main.ContainerRenameParameters)
    {
        [DataMember(Name = "NewName")]
        public string NewName { get; set; }
    }
}
