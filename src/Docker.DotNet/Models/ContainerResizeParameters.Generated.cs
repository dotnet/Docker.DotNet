
namespace Docker.DotNet.Models
{
    public class ContainerResizeParameters // (main.ContainerResizeParameters)
    {
        [QueryStringParameter("h", false)]
        public long? Height { get; set; }

        [QueryStringParameter("w", false)]
        public long? Width { get; set; }
    }
}
