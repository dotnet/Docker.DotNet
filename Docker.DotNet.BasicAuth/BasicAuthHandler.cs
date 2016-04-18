using System;
using System.Net.Http;
using System.Net.Http.Headers;

using System.Text;
using System.Threading;
using System.Threading.Tasks;

#if !netstandard
using System.Runtime.InteropServices;
using System.Security;
#endif

namespace Docker.DotNet.BasicAuth
{
    internal class BasicAuthHandler : DelegatingHandler
    {
#if !netstandard
        private readonly SecureString _username;
        private readonly SecureString _password;
#else
        private readonly string _username;
        private readonly string _password;
#endif

#if !netstandard
        public BasicAuthHandler(SecureString username, SecureString password, HttpMessageHandler innerHandler) : base(innerHandler)
#else
        public BasicAuthHandler(string username, string password, HttpMessageHandler innerHandler) : base(innerHandler)
#endif
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
#if !netstandard
            string authInfo = string.Format("{0}:{1}", ConvertToUnsecureString(_username), ConvertToUnsecureString(_password));
#else
            var authInfo = $"{_username}:{_password}";
#endif

            return Convert.ToBase64String(Encoding.GetEncoding(authInfo).GetBytes(authInfo));
        }

#if !netstandard
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
#endif
    }
}
