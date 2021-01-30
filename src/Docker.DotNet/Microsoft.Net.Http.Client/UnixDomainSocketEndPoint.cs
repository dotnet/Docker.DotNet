// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE-MIT.txt file for more information.

#if NETSTANDARD1_6 || NETSTANDARD2_0

using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Microsoft.Net.Http.Client
{
    internal sealed class UnixDomainSocketEndPoint : EndPoint
    {
        private const AddressFamily EndPointAddressFamily = AddressFamily.Unix;

        private static readonly int NativeAddressSize = NativePathOffset + NativePathLength;
        private static readonly int NativePathLength = 91;
        private static readonly int NativePathOffset = 2;
        private static readonly Encoding PathEncoding = Encoding.UTF8;

        // = offsetof(struct sockaddr_un, sun_path). It's the same on Linux and OSX

        private readonly byte[] _encodedPath;

        // sockaddr_un.sun_path at http://pubs.opengroup.org/onlinepubs/9699919799/basedefs/sys_un.h.html, -1 for terminator
        private readonly string _path;

        public UnixDomainSocketEndPoint(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            _path = path;
            _encodedPath = PathEncoding.GetBytes(_path);

            if (path.Length == 0 || _encodedPath.Length > NativePathLength)
            {
                throw new ArgumentOutOfRangeException(nameof(path), path);
            }
        }

        internal UnixDomainSocketEndPoint(SocketAddress socketAddress)
        {
            if (socketAddress == null)
            {
                throw new ArgumentNullException(nameof(socketAddress));
            }

            if (socketAddress.Family != EndPointAddressFamily ||
                socketAddress.Size > NativeAddressSize)
            {
                throw new ArgumentOutOfRangeException(nameof(socketAddress));
            }

            if (socketAddress.Size > NativePathOffset)
            {
                _encodedPath = new byte[socketAddress.Size - NativePathOffset];
                for (int i = 0; i < _encodedPath.Length; i++)
                {
                    _encodedPath[i] = socketAddress[NativePathOffset + i];
                }

                _path = PathEncoding.GetString(_encodedPath, 0, _encodedPath.Length);
            }
            else
            {
                _encodedPath = Array.Empty<byte>();
                _path = string.Empty;
            }
        }

        public override AddressFamily AddressFamily => EndPointAddressFamily;

        public override EndPoint Create(SocketAddress socketAddress) => new UnixDomainSocketEndPoint(socketAddress);

        public override SocketAddress Serialize()
        {
            var result = new SocketAddress(AddressFamily.Unix, NativeAddressSize);
            Debug.Assert(_encodedPath.Length + NativePathOffset <= result.Size, "Expected path to fit in address");

            for (int index = 0; index < _encodedPath.Length; index++)
            {
                result[NativePathOffset + index] = _encodedPath[index];
            }
            result[NativePathOffset + _encodedPath.Length] = 0; // path must be null-terminated

            return result;
        }

        public override string ToString() => _path;
    }
}

#endif
