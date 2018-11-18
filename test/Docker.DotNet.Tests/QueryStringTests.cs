using System;
using Docker.DotNet.Models;
using Xunit;

namespace Docker.DotNet.Tests
{
    public class QueryStringTests
    {
        [Fact]
        public void ServicesListParameters_GenerateIdFilters()
        {
            var p = new ServicesListParameters { Filters = new ServiceFilter { Id = new string[]{ "service-id" } } };
            var qs = new QueryString<ServicesListParameters>(p);

            Assert.Equal("filters={\"id\":[\"service-id\"]}", Uri.UnescapeDataString(qs.GetQueryString()));
        }
        [Fact]
        public void ServicesListParameters_GenerateCompositeFilters()
        {
            var p = new ServicesListParameters { Filters = new ServiceFilter { Id = new string[] { "service-id" }, Label = new string[] { "label" } } };
            var qs = new QueryString<ServicesListParameters>(p);

            Assert.Equal("filters={\"id\":[\"service-id\"],\"label\":[\"label\"]}", Uri.UnescapeDataString(qs.GetQueryString()));
        }
    }
}