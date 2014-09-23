namespace Docker.DotNet.Models
{
    public class KillContainerParameters
    {
        [QueryStringParameter("signal", false)]
        public string Signal { get; set; }

        public KillContainerParameters()
        {
        }
    }
}