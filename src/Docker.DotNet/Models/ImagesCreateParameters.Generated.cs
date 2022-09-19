using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImagesCreateParameters // (main.ImagesCreateParameters)
    {
        [QueryStringParameter("fromImage", false)]
        public string FromImage { get; set; }

        [QueryStringParameter("fromSrc", false)]
        public string FromSrc { get; set; }

        [QueryStringParameter("repo", false)]
        public string Repo { get; set; }

        [QueryStringParameter("tag", false)]
        public string Tag { get; set; }

        [QueryStringParameter("message", false)]
        public string Message { get; set; }

        [QueryStringParameter("changes", false, typeof(EnumerableQueryStringConverter))]
        public IList<string> Changes { get; set; }

        [QueryStringParameter("platform", false)]
        public string Platform { get; set; }

        [DataMember(Name = "RegistryAuth", EmitDefaultValue = false)]
        public AuthConfig RegistryAuth { get; set; }
    }
}
