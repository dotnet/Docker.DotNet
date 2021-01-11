using System;
using System.Collections.Generic;
using Docker.DotNet.Models;
using Xunit;

namespace Docker.DotNet.Tests
{
    public class QueryStringTests
    {
        [Fact]
        public void ServiceListParameters_GenerateIdFilters()
        {
            var p = new ServicesListParameters { Filters = new ServiceFilter { Id = new string[] { "service-id" } } };
            var qs = new QueryString<ServicesListParameters>(p);

            Assert.Equal("filters={\"id\":[\"service-id\"]}", Uri.UnescapeDataString(qs.GetQueryString()));
        }

        [Fact]
        public void ServiceListParameters_GenerateCompositeFilters()
        {
            var p = new ServicesListParameters { Filters = new ServiceFilter { Id = new string[] { "service-id" }, Label = new string[] { "label" } } };
            var qs = new QueryString<ServicesListParameters>(p);

            Assert.Equal("filters={\"id\":[\"service-id\"],\"label\":[\"label\"]}", Uri.UnescapeDataString(qs.GetQueryString()));
        }

        [Fact]
        public void ServicesListParameters_GenerateNullFilters()
        {
            var p = new ServicesListParameters { Filters = new ServiceFilter() };
            var qs = new QueryString<ServicesListParameters>(p);

            Assert.Equal("filters={}", Uri.UnescapeDataString(qs.GetQueryString()));
        }

        [Fact]
        public void ServicesListParameters_GenerateNullModeFilters()
        {
            var p = new ServicesListParameters { Filters = new ServiceFilter() { Mode = new ServiceCreationMode[] { } } };
            var qs = new QueryString<ServicesListParameters>(p);

            Assert.Equal("filters={\"mode\":[]}", Uri.UnescapeDataString(qs.GetQueryString()));
        }
    }
}
