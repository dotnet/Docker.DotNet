using System.Collections.Generic;
using System.Net.Http;
using Org.BouncyCastle.X509;

namespace Docker.DotNet
{
    /// <summary>
    /// This is essentially a shim for the WebRequestHandler found in the "big boy" version
    /// of System.Net.Http.  Inherits from HttpClientHandler and implements a collection of
    /// X509Certificate objects.
    /// </summary>
    public class PortableWebRequestHandler : HttpClientHandler
    {
        /// <summary>
        /// Gets the client certificates associated with the client handler.
        /// </summary>
        /// <value>
        /// The client certificates.
        /// </value>
        public ICollection<X509Certificate> ClientCertificates { get;private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PortableWebRequestHandler"/> class.
        /// </summary>
        public PortableWebRequestHandler() : base()
        {
            ClientCertificates = new List<X509Certificate>();
        }
    }
}
