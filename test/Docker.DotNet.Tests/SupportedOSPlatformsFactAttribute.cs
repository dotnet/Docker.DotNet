using System;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit;

namespace Docker.DotNet.Tests
{
    public enum Platform
    {
        Linux,
        OSX,
        Windows
    }
    
    public sealed class SupportedOSPlatformsFactAttribute : FactAttribute
    {
        private static Platform CurrentPlatform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return Platform.Linux;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return Platform.OSX;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return Platform.Windows;
            throw new PlatformNotSupportedException();
        }

        public SupportedOSPlatformsFactAttribute(params Platform[] supportedPlatforms)
        {
            var currentPlatform = CurrentPlatform();
            var isSupported = supportedPlatforms.Contains(currentPlatform);
            Skip = isSupported ? null : $"Not applicable to {currentPlatform}";
        }
    }
}