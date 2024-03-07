
namespace Docker.DotNet.Models
{
    public class ContainerKillParameters // (main.ContainerKillParameters)
    {
        [QueryStringParameter("signal", false)]
        public string Signal { get; set; }
    }
}
