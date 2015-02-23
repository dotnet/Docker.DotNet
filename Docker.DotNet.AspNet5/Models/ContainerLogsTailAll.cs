namespace Docker.DotNet.Models
{
    public class ContainerLogsTailAll : IContainerLogsTailMode
    {
        public string Value
        {
            get { return "all"; }
        }

        public ContainerLogsTailAll()
        {
        }
    }
}