using System;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Docker.DotNet.BasicAuth
{
    public class BasicAuthCredentials : Credentials
    {
        private readonly SecureString _username;
        private readonly SecureString _password;
        private readonly bool _isTls;

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

            _username = username;
            _password = password;
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

            _username = ConvertToSecureString(username);
            _password = ConvertToSecureString(password);
            _isTls = isTls;
        }

        public override HttpClient BuildHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", BuildBasicAuth(_username, _password));

            return httpClient;
        }

        public override bool IsTlsCredentials()
        {
            return _isTls;
        }

        private static string BuildBasicAuth(SecureString username, SecureString password)
        {
            string authInfo = string.Format("{0}:{1}", ConvertToUnsecureString(username),
                ConvertToUnsecureString(password));

            return string.Format("Basic {0}", Convert.ToBase64String(Encoding.Default.GetBytes(authInfo)));
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