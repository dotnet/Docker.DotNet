using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Docker.DotNet.BasicAuth
{
    internal struct MaybeSecureString : IDisposable
    {
#if !netstandard
        public SecureString Value;

        public MaybeSecureString(string str)
        {
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

        public MaybeSecureString(SecureString value)
        {
            Value = value;
        }

        public void Dispose()
        {
            Value.Dispose();
        }

        public bool Empty => Value == null;

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
        public string Value;

        public MaybeSecureString(string value)
        {
            Value = value;
        }

        public void Dispose()
        {
        }

        public bool Empty => string.IsNullOrEmpty(Value);

        public override string ToString()
        {
            return Value;
        }
#endif
    }
}