namespace Docker.DotNet.Models
{
    public class CreateContainerParameters
    {
        public Config Config { get; set; }

        [QueryStringParameter("name", false)]
        public string ContainerName { get; set; }

        public CreateContainerParameters()
        {
        }
    }
}