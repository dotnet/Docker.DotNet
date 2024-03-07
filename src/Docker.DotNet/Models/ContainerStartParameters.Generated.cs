
namespace Docker.DotNet.Models
{
    public class ContainerStartParameters // (main.ContainerStartParameters)
    {
        [QueryStringParameter("detachKeys", false)]
        public string DetachKeys { get; set; }
    }
}
