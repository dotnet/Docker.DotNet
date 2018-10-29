using System;
using Docker.DotNet.Models;
using Xunit;

namespace Docker.DotNet.Tests
{
    public class ServiceFilterTests
    {
        [Fact]
        public void ServiceFilter_AddInvalidMode()
        {
            var sf = new ServiceFilter();
            Assert.Throws<Exception>(() => sf.Mode = "invalid");
        }

        [Fact]
        public void ServiceFilter_AddValidMode()
        {
            var sf = new ServiceFilter { Mode = "global" };
        }
    }
}