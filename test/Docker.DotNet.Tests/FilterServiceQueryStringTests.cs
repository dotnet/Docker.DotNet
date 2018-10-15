using System;
using Docker.DotNet.Models;
using Xunit;

namespace Docker.DotNet.Tests
{
    public class FilterServiceQueryStringTests
    {
        [Fact]
        public void FilterServiceQueryString_GetQueryString_WithIdDefined()
        {
            var filter = new FilterServiceQueryString(new FilterServiceParameters { Id = "service-id" });
            Assert.Equal("filters={\"id\":{\"service-id\":true}}", filter.GetQueryString());
        }
        [Fact]
        public void FilterServiceQueryString_GetQueryString_WithNameDefined()
        {
            var filter = new FilterServiceQueryString(new FilterServiceParameters { Name = "service-name" });
            Assert.Equal("filters={\"name\":{\"service-name\":true}}", filter.GetQueryString());
        }
        [Fact]
        public void FilterServiceQueryString_GetQueryString_WithLabelDefined()
        {
            var filter = new FilterServiceQueryString(new FilterServiceParameters { Label = "service-label" });
            Assert.Equal("filters={\"label\":{\"service-label\":true}}", filter.GetQueryString());
        }
        [Fact]
        public void FilterServiceQueryString_GetQueryString_WithModeDefined()
        {
            var filter = new FilterServiceQueryString(new FilterServiceParameters { Mode = "replicated" });
            Assert.Equal("filters={\"mode\":{\"replicated\":true}}", filter.GetQueryString());
        }
        [Fact]
        public void FilterServiceQueryString_GetQueryString_InvalidModeDefined()
        {
            Assert.Throws<Exception>(() => new FilterServiceQueryString(new FilterServiceParameters {Mode = "invalid-mode"}));
        }
        [Fact]
        public void FilterServiceQueryString_GetQueryString_ComposedFilters()
        {
            var filter = new FilterServiceQueryString(new FilterServiceParameters { Label = "service-label", Mode = "global"});
            Assert.Equal("filters={\"label\":{\"service-label\":true},\"mode\":{\"global\":true}}", filter.GetQueryString());
        }
    }
}