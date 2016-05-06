using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImagesImportParameters // (main.ImageImportParameters)
    {
        [QueryStringParameter("fromSrc", true)]
        public string SourceName { get; set; }

        [QueryStringParameter("repo", false)]
        public string RepositoryName { get; set; }

        [QueryStringParameter("message", false)]
        public string Message { get; set; }

        [QueryStringParameter("tag", false)]
        public string Tag { get; set; }

        [QueryStringParameter("changes", false, typeof(EnumerableQueryStringConverter))]
        public IList<string> Changes { get; set; }
    }
}
