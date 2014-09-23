namespace Docker.DotNet.Models
{
    public class TagImageParameters
    {
        [QueryStringParameter("repo", true)]
        public string Repo { get; set; }

        [QueryStringParameter("force", false, typeof (BoolQueryStringConverter))]
        public bool? Force { get; set; }

        public TagImageParameters()
        {
        }
    }
}