using System;
using System.Collections.Generic;
using System.Threading;

namespace Docker.DotNet
{
    public class DockerClientConfiguration : IDisposable
    {
        /// <summary>
        /// Creates a configuration whose endpoint is resolved like the docker CLI:
        /// the <c>DOCKER_HOST</c> environment variable first, then the active Docker
        /// context (<c>DOCKER_CONTEXT</c> or <c>currentContext</c> in
        /// <c>~/.docker/config.json</c>), then the platform default socket.
        /// </summary>
        public DockerClientConfiguration(
            Credentials credentials = null,
            TimeSpan defaultTimeout = default,
            TimeSpan namedPipeConnectTimeout = default,
            IReadOnlyDictionary<string, string> defaultHttpRequestHeaders = null)
            : this(DockerContextResolver.Resolve(), credentials, defaultTimeout, namedPipeConnectTimeout, defaultHttpRequestHeaders)
        {
        }

        /// <summary>
        /// Creates a configuration that targets the named Docker context, reading
        /// its endpoint from <c>~/.docker/contexts/meta/&lt;sha256(name)&gt;/meta.json</c>.
        /// </summary>
        public static DockerClientConfiguration FromContext(
            string contextName,
            Credentials credentials = null,
            TimeSpan defaultTimeout = default,
            TimeSpan namedPipeConnectTimeout = default,
            IReadOnlyDictionary<string, string> defaultHttpRequestHeaders = null)
        {
            return new DockerClientConfiguration(
                DockerContextResolver.ResolveForContext(contextName),
                credentials,
                defaultTimeout,
                namedPipeConnectTimeout,
                defaultHttpRequestHeaders);
        }

        public DockerClientConfiguration(
            Uri endpoint,
            Credentials credentials = null,
            TimeSpan defaultTimeout = default,
            TimeSpan namedPipeConnectTimeout = default,
            IReadOnlyDictionary<string, string> defaultHttpRequestHeaders = null)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            if (defaultTimeout < Timeout.InfiniteTimeSpan)
            {
                throw new ArgumentException("Default timeout must be greater than -1", nameof(defaultTimeout));
            }

            EndpointBaseUri = endpoint;
            Credentials = credentials ?? new AnonymousCredentials();
            DefaultTimeout = TimeSpan.Equals(default, defaultTimeout) ? TimeSpan.FromSeconds(100) : defaultTimeout;
            NamedPipeConnectTimeout = TimeSpan.Equals(default, namedPipeConnectTimeout) ? TimeSpan.FromMilliseconds(100) : namedPipeConnectTimeout;
            DefaultHttpRequestHeaders = defaultHttpRequestHeaders ?? new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the collection of default HTTP request headers.
        /// </summary>
        public IReadOnlyDictionary<string, string> DefaultHttpRequestHeaders { get; }

        public Uri EndpointBaseUri { get; }

        public Credentials Credentials { get; }

        public TimeSpan DefaultTimeout { get; }

        public TimeSpan NamedPipeConnectTimeout { get; }

        public DockerClient CreateClient(Version requestedApiVersion = null)
        {
            return new DockerClient(this, requestedApiVersion);
        }

        public void Dispose()
        {
            Credentials.Dispose();
        }
    }
}