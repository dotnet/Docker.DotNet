using System;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Docker.DotNet
{
    public class BasicAuthCredentials : Credentials
    {
        private readonly SecureString _username;
        private readonly SecureString _password;

        public BasicAuthCredentials(SecureString username, SecureString password)
        {
            _username = username;
            _password = password;
        }

        public BasicAuthCredentials(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException();
            }

            _username = ConvertToSecureString(username);
            _password = ConvertToSecureString(password);
        }

        public override HttpClient BuildHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", BuildBasicAuth(_username, _password));

            return httpClient;
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
