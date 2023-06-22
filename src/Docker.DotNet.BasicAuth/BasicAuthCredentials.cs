using System.Net.Http;
using System.Security;

namespace Docker.DotNet.BasicAuth
{
    public class BasicAuthCredentials : Credentials
    {
        private readonly bool _isTls;

        private readonly MaybeSecureString _username;
        private readonly MaybeSecureString _password;

        public override HttpMessageHandler GetHandler(HttpMessageHandler innerHandler)
        {
            return new BasicAuthHandler(_username, _password, innerHandler);
        }

        public BasicAuthCredentials(SecureString username, SecureString password, bool isTls = false)
            : this(new MaybeSecureString(username), new MaybeSecureString(password), isTls)
        {
        }

        public BasicAuthCredentials(string username, string password, bool isTls = false)
            : this(new MaybeSecureString(username), new MaybeSecureString(password), isTls)
        {
        }

        private BasicAuthCredentials(MaybeSecureString username, MaybeSecureString password, bool isTls)
        {
            _isTls = isTls;
            _username = username;
            _password = password;
        }

        public override bool IsTlsCredentials()
        {
            return _isTls;
        }

        public override bool IsSshCredentials()
        {
            return false;
        }

        public override void Dispose()
        {
            _username.Dispose();
            _password.Dispose();
        }
    }
}
