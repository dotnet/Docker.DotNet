
namespace Docker.DotNet.Models
{
    public class ContainerRenameParameters // (main.ContainerRenameParameters)
    {
        [QueryStringParameter("name", false)]
        public string NewName { get; set; }
    }
}
