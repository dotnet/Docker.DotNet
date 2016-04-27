using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImagesImportParameters // (types.ImageImportOptions)
    {
        [DataMember(Name = "Source")]
        public object Source { get; set; }

        [QueryStringParameter("fromSrc", false)]
        public string SourceName { get; set; }

        [QueryStringParameter("repo", false)]
        public string RepositoryName { get; set; }

        [QueryStringParameter("message", false)]
        public string Message { get; set; }

        [QueryStringParameter("tag", false)]
        public string Tag { get; set; }

        [QueryStringParameter("changes", false)]
        public IList<string> Changes { get; set; }
    }
}
