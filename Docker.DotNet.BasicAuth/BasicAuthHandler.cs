using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.DotNet.BasicAuth
{
    internal class BasicAuthHandler : DelegatingHandler
    {
        private readonly MaybeSecureString _username;
        private readonly MaybeSecureString _password;

        public BasicAuthHandler(MaybeSecureString username, MaybeSecureString password, HttpMessageHandler innerHandler) : base(innerHandler)
        {
            if (username.Empty)
            {
                throw new ArgumentException("username");
            }

            if (password.Empty)
            {
                throw new ArgumentException("password");
            }

            _username = username;
            _password = password;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", BuildParameters());

            return base.SendAsync(request, cancellationToken);
        }

        private string BuildParameters()
        {
            var authInfo = $"{_username}:{_password}";
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(authInfo));
        }
    }
}
