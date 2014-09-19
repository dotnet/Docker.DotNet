using System;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Docker.DotNet
{
    public class BasicAuthCredentials : Credentials
    {
        public SecureString Username { get; private set; }
        public SecureString Password { get; private set; }

        public BasicAuthCredentials(SecureString username, SecureString password)
        {
            this.Username = username;
            this.Password = password;
        }

        public BasicAuthCredentials(string username, SecureString password)
        {
            this.Username = ConvertToSecureString(username);
            this.Password = password;
        }

        public BasicAuthCredentials(SecureString username, string password)
        {
            this.Username = username;
            this.Password = ConvertToSecureString(password);
        }

        public BasicAuthCredentials(string username, string password)
        {
            this.Username = ConvertToSecureString(username);
            this.Password = ConvertToSecureString(password);
        }

        public override HttpClient BuildHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", BuildBasicAuth(this.Username, this.Password));

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
