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
    internal class BasicAuthHandler : DelegatingHandler
    {
        private readonly SecureString _username;
        private readonly SecureString _password;

        public BasicAuthHandler(SecureString username, SecureString password, HttpMessageHandler innerHandler) : base(innerHandler)
        {
            if (username == null)
            {
                throw new ArgumentException("username");
            }

            if (password == null)
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
}
