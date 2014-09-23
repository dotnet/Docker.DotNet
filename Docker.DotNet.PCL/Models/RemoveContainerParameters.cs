namespace Docker.DotNet.Models
{
    public class RemoveContainerParameters
    {
        [QueryStringParameter("v", false)]
        public bool? RemoveVolumes { get; set; }

        [QueryStringParameter("force", false)]
        public bool? Force { get; set; }

        public RemoveContainerParameters()
        {
        }
    }
}