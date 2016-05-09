using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Docker.DotNet.BasicAuth
{
    internal class MaybeSecureString : IDisposable
    {
#if !NETSTANDARD1_3

        public SecureString Value { get; }

        public MaybeSecureString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException(nameof(str));
            }

            var secureStr = new SecureString();

            if (str.Length > 0)
            {
                foreach (char c in str)
                {
                    secureStr.AppendChar(c);
                }
            }

            Value = secureStr;
        }

        public MaybeSecureString(SecureString str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            Value = str.Copy();
        }

        public void Dispose()
        {
            Value.Dispose();
        }

        public MaybeSecureString Copy()
        {
            return new MaybeSecureString(Value.Copy());
        }

        public override string ToString()
        {
            IntPtr unmanagedString = IntPtr.Zero;

            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(Value);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

#else

        public string Value { get; }

        public MaybeSecureString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException(nameof(str));
            }

            Value = str;
        }

        public void Dispose()
        {
        }

        public MaybeSecureString Copy()
        {
            return this;
        }

        public override string ToString()
        {
            return Value;
        }

#endif
    }
}