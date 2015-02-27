namespace Docker.DotNet.Models
{
    public class PushImageParameters
    {
        [QueryStringParameter("tag", false)]
        public string Tag { get; set; }

        public PushImageParameters()
        {
        }
    }
}