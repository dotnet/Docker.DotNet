using System;
using System.IO;

namespace Microsoft.Net.Http.Client
{
    /// <summary>
    /// Summary description for ApmStream
    /// </summary>
    public abstract class ApmStream : Stream
    {
#if !NET45
        public abstract IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, Object state);
#endif

#if !NET45
        public abstract int EndRead(IAsyncResult asyncResult);
#endif

#if !NET45
        public abstract IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, Object state);
#endif

#if !NET45
        public abstract void EndWrite(IAsyncResult asyncResult);
#endif
    }
}