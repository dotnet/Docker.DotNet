using System;
using System.Net.Http;
using System.Security;

namespace Docker.DotNet.BasicAuth
{
    public class BasicAuthCredentials : Credentials
    {
        private readonly BasicAuthHandler _handler;
        private readonly bool _isTls;

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

            _handler = CreateHandler(username, password);

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

            _handler = CreateHandler(ConvertToSecureString(username), ConvertToSecureString(password));

            _isTls = isTls;
        }

        private BasicAuthHandler CreateHandler(SecureString username, SecureString password)
        {
            return new BasicAuthHandler(username, password, new HttpClientHandler());
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