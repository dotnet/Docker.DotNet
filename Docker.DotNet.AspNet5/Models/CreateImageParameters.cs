namespace Docker.DotNet.Models
{
    public class CreateImageParameters
    {
        [QueryStringParameter("fromImage", false)]
        public string FromImage { get; set; }

        [QueryStringParameter("repo", false)]
        public string Repo { get; set; }

        [QueryStringParameter("tag", false)]
        public string Tag { get; set; }

        [QueryStringParameter("registry", false)]
        public string Registry { get; set; }

        public CreateImageParameters()
        {
        }
    }
}