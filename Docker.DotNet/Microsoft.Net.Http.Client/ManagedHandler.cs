using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Net.Http.Client
{
    public class ManagedHandler : HttpMessageHandler
    {
        public ManagedHandler()
        {
            MaxAutomaticRedirects = 20;
            RedirectMode = RedirectMode.NoDowngrade;
        }

        public Uri ProxyAddress
        {
            // TODO: Validate that only an absolute http address is specified. Path, query, and fragment are ignored
            get; set;
        }

        public int MaxAutomaticRedirects { get; set; }

        public RedirectMode RedirectMode { get; set; }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            HttpResponseMessage response = null;
            int redirectCount = 0;
            bool retry;

            do
            {
                retry = false;
                response = await ProcessRequestAsync(request, cancellationToken);
                if (redirectCount < MaxAutomaticRedirects && IsAllowedRedirectResponse(request, response))
                {
                    redirectCount++;
                    retry = true;
                }

            } while (retry);

            return response;
        }

        private bool IsAllowedRedirectResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            // Are redirects enabled?
            if (RedirectMode == RedirectMode.None)
            {
                return false;
            }

            // Status codes 301 and 302
            if (response.StatusCode != HttpStatusCode.Redirect && response.StatusCode != HttpStatusCode.Moved)
            {
                return false;
            }

            Uri location = response.Headers.Location;

            if (location == null)
            {
                return false;
            }

            if (!location.IsAbsoluteUri)
            {
                request.RequestUri = location;
                request.SetPathAndQueryProperty(null);
                request.SetAddressLineProperty(null);
                request.Headers.Authorization = null;
                return true;
            }

            // Check if redirect from https to http is allowed
            if (request.IsHttps() && string.Equals("http", location.Scheme, StringComparison.OrdinalIgnoreCase)
                && RedirectMode == RedirectMode.NoDowngrade)
            {
                return false;
            }

            // Reset fields calculated from the URI.
            request.RequestUri = location;
            request.SetSchemeProperty(null);
            request.Headers.Host = null;
            request.Headers.Authorization = null;
            request.SetHostProperty(null);
            request.SetConnectionHostProperty(null);
            request.SetPortProperty(null);
            request.SetConnectionPortProperty(null);
            request.SetPathAndQueryProperty(null);
            request.SetAddressLineProperty(null);
            return true;
        }

        private async Task<HttpResponseMessage> ProcessRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ProcessUrl(request);
            ProcessHostHeader(request);
            request.Headers.ConnectionClose = true; // TODO: Connection re-use is not supported.

            if (request.Method != HttpMethod.Get)
            {
                throw new NotImplementedException(request.Method.Method); // TODO: POST
            }

            ProxyMode proxyMode = DetermineProxyModeAndAddressLine(request);
            Stream transport = await ConnectAsync(request, cancellationToken);

            if (proxyMode == ProxyMode.Tunnel)
            {
                await TunnelThroughProxyAsync(request, transport, cancellationToken);
            }

            System.Diagnostics.Debug.Assert(!(proxyMode == ProxyMode.Http && request.IsHttps()));

            if (request.IsHttps())
            {
                SslStream sslStream = new SslStream(transport);
                await sslStream.AuthenticateAsClientAsync(request.GetHostProperty());
                transport = sslStream;
            }

            var bufferedReadStream = new BufferedReadStream(transport);
            var connection = new HttpConnection(bufferedReadStream);
            return await connection.SendAsync(request, cancellationToken);
        }

        // Data comes from either the request.RequestUri or from the request.Properties
        private void ProcessUrl(HttpRequestMessage request)
        {
            string scheme = request.GetSchemeProperty();
            if (string.IsNullOrWhiteSpace(scheme))
            {
                if (!request.RequestUri.IsAbsoluteUri)
                {
                    throw new InvalidOperationException("Missing URL Scheme");
                }
                scheme = request.RequestUri.Scheme;
                request.SetSchemeProperty(scheme);
            }
            if (!(request.IsHttp() || request.IsHttps()))
            {
                throw new InvalidOperationException("Only HTTP or HTTPS are supported, not: " + request.RequestUri.Scheme);
            }

            string host = request.GetHostProperty();
            if (string.IsNullOrWhiteSpace(host))
            {
                if (!request.RequestUri.IsAbsoluteUri)
                {
                    throw new InvalidOperationException("Missing URL Scheme");
                }
                host = request.RequestUri.DnsSafeHost;
                request.SetHostProperty(host);
            }
            string connectionHost = request.GetConnectionHostProperty();
            if (string.IsNullOrWhiteSpace(connectionHost))
            {
                request.SetConnectionHostProperty(host);
            }

            int? port = request.GetPortProperty();
            if (!port.HasValue)
            {
                if (!request.RequestUri.IsAbsoluteUri)
                {
                    throw new InvalidOperationException("Missing URL Scheme");
                }
                port = request.RequestUri.Port;
                request.SetPortProperty(port);
            }
            int? connectionPort = request.GetConnectionPortProperty();
            if (!connectionPort.HasValue)
            {
                request.SetConnectionPortProperty(port);
            }

            string pathAndQuery = request.GetPathAndQueryProperty();
            if (string.IsNullOrWhiteSpace(pathAndQuery))
            {
                if (request.RequestUri.IsAbsoluteUri)
                {
                    pathAndQuery = request.RequestUri.PathAndQuery;
                }
                else
                {
                    pathAndQuery = Uri.EscapeUriString(request.RequestUri.ToString());
                }
                request.SetPathAndQueryProperty(pathAndQuery);
            }
        }

        private void ProcessHostHeader(HttpRequestMessage request)
        {
            if (string.IsNullOrWhiteSpace(request.Headers.Host))
            {
                string host = request.GetHostProperty();
                int port = request.GetPortProperty().Value;
                if (host.Contains(':'))
                {
                    // IPv6
                    host = '[' + host + ']';
                }

                request.Headers.Host = host + ":" + port.ToString(CultureInfo.InvariantCulture);
            }
        }

        private ProxyMode DetermineProxyModeAndAddressLine(HttpRequestMessage request)
        {
            string scheme = request.GetSchemeProperty();
            string host = request.GetHostProperty();
            int? port = request.GetPortProperty();
            string pathAndQuery = request.GetPathAndQueryProperty();
            string addressLine = request.GetAddressLineProperty();

            if (string.IsNullOrEmpty(addressLine))
            {
                request.SetAddressLineProperty(pathAndQuery);
            }

            if (ProxyAddress == null)
            {
                return ProxyMode.None;
            }
            if (request.IsHttp())
            {
                if (string.IsNullOrEmpty(addressLine))
                {
                    addressLine = scheme + "://" + host + ":" + port.Value + pathAndQuery;
                    request.SetAddressLineProperty(addressLine);
                }
                request.SetConnectionHostProperty(ProxyAddress.DnsSafeHost);
                request.SetConnectionPortProperty(ProxyAddress.Port);
                return ProxyMode.Http;
            }
            // Tunneling generates a completely seperate request, don't alter the original, just the connection address.
            request.SetConnectionHostProperty(ProxyAddress.DnsSafeHost);
            request.SetConnectionPortProperty(ProxyAddress.Port);
            return ProxyMode.Tunnel;
        }

        private async Task<Stream> ConnectAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            TcpClient client = new TcpClient();
            try
            {
                await client.ConnectAsync(request.GetConnectionHostProperty(), request.GetConnectionPortProperty().Value);
                return client.GetStream();
            }
            catch (SocketException sox)
            {
                ((IDisposable)client).Dispose();
                throw new HttpRequestException("Request failed", sox);
            }
        }

        private async Task TunnelThroughProxyAsync(HttpRequestMessage request, Stream transport, CancellationToken cancellationToken)
        {
            // Send a Connect request:
            // CONNECT server.example.com:80 HTTP / 1.1
            // Host: server.example.com:80
            var connectReqeuest = new HttpRequestMessage();
            connectReqeuest.Headers.ProxyAuthorization = request.Headers.ProxyAuthorization;
            connectReqeuest.Method = new HttpMethod("CONNECT");
            // TODO: IPv6 hosts
            string authority = request.GetHostProperty() + ":" + request.GetPortProperty().Value;
            connectReqeuest.SetAddressLineProperty(authority);
            connectReqeuest.Headers.Host = authority;

            HttpConnection connection = new HttpConnection(new BufferedReadStream(transport));
            HttpResponseMessage connectResponse;
            try
            {
                connectResponse = await connection.SendAsync(connectReqeuest, cancellationToken);
                // TODO:? await connectResponse.Content.LoadIntoBufferAsync(); // Drain any body
                // There's no danger of accidently consuming real response data because the real request hasn't been sent yet.
            }
            catch (Exception ex)
            {
                transport.Dispose();
                throw new HttpRequestException("SSL Tunnel failed to initialize", ex);
            }

            // Listen for a response. Any 2XX is considered success, anything else is considered a failure.
            if ((int)connectResponse.StatusCode < 200 || 300 <= (int)connectResponse.StatusCode)
            {
                transport.Dispose();
                throw new HttpRequestException("Failed to negotiate the proxy tunnel: " + connectResponse.ToString());
            }
        }
    }
}
