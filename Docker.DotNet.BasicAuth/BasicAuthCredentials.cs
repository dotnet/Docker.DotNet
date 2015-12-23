using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.DotNet.BasicAuth
{
    public class BasicAuthCredentials : Credentials
    {
        private readonly BasicAuthHandler _handler;
        private readonly bool _isTls;

        private class BasicAuthHandler : HttpClientHandler
        {
            private readonly SecureString _username;
            private readonly SecureString _password;

            public BasicAuthHandler(SecureString username, SecureString password)
            {
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
                string authInfo = string.Format("{0}:{1}", ConvertToUnsecureString(_username),
                    ConvertToUnsecureString(_password));

                return Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            }

            private static string ConvertToUnsecureString(SecureString secureString)
            {
                IntPtr unmanagedString = IntPtr.Zero;
                try
                {
                    unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                    return Marshal.PtrToStringUni(unmanagedString);
                }
                finally
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
                }
            }
        }

        public override HttpMessageHandler Handler
        {
            get
            {
                return _handler;
            }
        }

        public BasicAuthCredentials(SecureString username, SecureString password, bool isTls = false)
        {
            if (username == null)
            {
                throw new ArgumentException("username");
            }

            if (password == null)
            {
                throw new ArgumentException("password");
            }

            _handler = new BasicAuthHandler(username, password);

            _isTls = isTls;
        }

        public BasicAuthCredentials(string username, string password, bool isTls = false)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("username");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("password");
            }

            _handler = new BasicAuthHandler(ConvertToSecureString(username), ConvertToSecureString(password));

            _isTls = isTls;
        }

        public override bool IsTlsCredentials()
        {
            return _isTls;
        }

        private static SecureString ConvertToSecureString(string str)
        {
            var secureStr = new SecureString();

            if (str.Length > 0)
            {
                foreach (char c in str)
                {
                    secureStr.AppendChar(c);
                }
            }
            return secureStr;
        }

        public override void Dispose()
        {
            _handler.Dispose();
        }
    }
}