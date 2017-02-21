using System;
using System.Threading;

namespace Docker.DotNet
{
    public class DockerClientConfiguration : IDisposable
    {
        public Uri EndpointBaseUri { get; internal set; }

        public Credentials Credentials { get; internal set; }

        public TimeSpan DefaultTimeout { get; internal set; } = TimeSpan.FromSeconds(100);

        public DockerClientConfiguration(Uri endpoint, Credentials credentials = null,
            TimeSpan defaultTimeout = default(TimeSpan))
        {
            if (endpoint == null)
                throw new ArgumentNullException(nameof(endpoint));


            Credentials = credentials ?? new AnonymousCredentials();
            EndpointBaseUri = endpoint;
            if (defaultTimeout != TimeSpan.Zero)
            {
                if (defaultTimeout < Timeout.InfiniteTimeSpan)
                    // TODO: Should be a resource for localization.
                    // TODO: Is this a good message?
                    throw new ArgumentException("Timeout must be greater than Timeout.Infinite", nameof(defaultTimeout));
                DefaultTimeout = defaultTimeout;
            }
        }

        public DockerClient CreateClient()
        {
            return this.CreateClient(null);
        }

        public DockerClient CreateClient(Version requestedApiVersion)
        {
            return new DockerClient(this, requestedApiVersion);
        }

        public void Dispose()
        {
            Credentials.Dispose();
        }
    }
}