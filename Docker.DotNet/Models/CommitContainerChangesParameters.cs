namespace Docker.DotNet.Models
{
    public class CommitContainerChangesParameters
    {
        [QueryStringParameter("container", true)]
        public string ContainerId { get; set; }

        [QueryStringParameter("repo", false)]
        public string Repo { get; set; }

        [QueryStringParameter("tag", false)]
        public string Tag { get; set; }

        [QueryStringParameter("m", false)]
        public string Message { get; set; }

        [QueryStringParameter("author", false)]
        public string Author { get; set; }

        // Sent as request body
        public Config Config { get; set; }

        public CommitContainerChangesParameters()
        {
        }
    }
}