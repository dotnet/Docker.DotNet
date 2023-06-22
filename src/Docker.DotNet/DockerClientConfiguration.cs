using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Net.Http.Client;

namespace Docker.DotNet
{
    public class DockerClientConfiguration : IDisposable
    {
        public DockerClientConfiguration(
            Credentials credentials = null,
            TimeSpan defaultTimeout = default,
            TimeSpan namedPipeConnectTimeout = default,
            IReadOnlyDictionary<string, string> defaultHttpRequestHeaders = null)
            : this(GetLocalDockerEndpoint(), credentials, defaultTimeout, namedPipeConnectTimeout, defaultHttpRequestHeaders)
        {
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

        public (Uri url, ManagedHandler handler) GetHandler()
        {
            if (!Credentials.SupportsScheme(EndpointBaseUri.Scheme))
            {
                throw new Exception($"The provided credentials don't support the {EndpointBaseUri.Scheme} scheme.");
            }

            var uri = EndpointBaseUri;
            ManagedHandler handler;

            switch (EndpointBaseUri.Scheme.ToLowerInvariant())
            {
                case "npipe":
                    var segments = uri.Segments;
                    if (segments.Length != 3 || !segments[1].Equals("pipe/", StringComparison.OrdinalIgnoreCase))
                    {
                        throw new ArgumentException($"{uri} is not a valid npipe URI");
                    }

                    var serverName = uri.Host;
                    if (string.Equals(serverName, "localhost", StringComparison.OrdinalIgnoreCase))
                    {
                        // npipe schemes dont work with npipe://localhost/... and need npipe://./... so fix that for a client here.
                        serverName = ".";
                    }

                    var pipeName = uri.Segments[2];

                    uri = new UriBuilder("http", pipeName).Uri;
                    handler = new ManagedHandler(async (host, port, cancellationToken) =>
                    {
                        var timeout = (int)NamedPipeConnectTimeout.TotalMilliseconds;
                        var stream = new NamedPipeClientStream(serverName, pipeName, PipeDirection.InOut, PipeOptions.Asynchronous);
                        var dockerStream = new DockerPipeStream(stream);

                        await stream.ConnectAsync(timeout, cancellationToken)
                            .ConfigureAwait(false);

                        return dockerStream;
                    });
                    break;

                case "tcp":
                case "http":
                    var builder = new UriBuilder(uri)
                    {
                        Scheme = Credentials.IsTlsCredentials() ? "https" : "http"
                    };
                    uri = builder.Uri;
                    handler = new ManagedHandler();
                    break;

                case "https":
                    handler = new ManagedHandler();
                    break;

                case "unix":
                    var pipeString = uri.LocalPath;
                    handler = new ManagedHandler(async (host, port, cancellationToken) =>
                    {
                        var sock = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified);

                        await sock.ConnectAsync(new Microsoft.Net.Http.Client.UnixDomainSocketEndPoint(pipeString))
                            .ConfigureAwait(false);

                        return sock;
                    });
                    uri = new UriBuilder("http", uri.Segments.Last()).Uri;
                    break;

                default:
                    throw new Exception($"URL scheme {EndpointBaseUri.Scheme} is unsupported by this implementation.");
            }

            return (uri, handler);
        }

        private static Uri GetLocalDockerEndpoint()
        {
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            return isWindows ? new Uri("npipe://./pipe/docker_engine") : new Uri("unix:/var/run/docker.sock");
        }
    }
}
