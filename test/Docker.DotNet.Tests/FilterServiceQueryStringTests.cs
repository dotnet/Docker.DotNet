using System;
using Docker.DotNet.Models;
using Xunit;

namespace Docker.DotNet.Tests
{
    public class FilterServiceQueryStringTests
    {
        [Fact]
        public void FilterServiceQueryString_GetQueryString_NullFilters()
        {
            var filter = new FilterServiceQueryString(null);
            Assert.Equal(string.Empty, filter.GetQueryString());
        }
        [Fact]
        public void FilterServiceQueryString_GetQueryString_EmptyFilters()
        {
            var filter = new FilterServiceQueryString(new ServiceFilter());
            Assert.Equal("filters={}", filter.GetQueryString());
        }
        [Fact]
        public void FilterServiceQueryString_GetQueryString_WithIdDefined()
        {
            var filter = new FilterServiceQueryString(new ServiceFilter { Id = "service-id" });
            Assert.Equal("filters={\"id\":{\"service-id\":true}}", filter.GetQueryString());
        }
        [Fact]
        public void FilterServiceQueryString_GetQueryString_WithNameDefined()
        {
            var filter = new FilterServiceQueryString(new ServiceFilter { Name = "service-name" });
            Assert.Equal("filters={\"name\":{\"service-name\":true}}", filter.GetQueryString());
        }
        [Fact]
        public void FilterServiceQueryString_GetQueryString_WithLabelDefined()
        {
            var filter = new FilterServiceQueryString(new ServiceFilter { Label = "service-label" });
            Assert.Equal("filters={\"label\":{\"service-label\":true}}", filter.GetQueryString());
        }
        [Fact]
        public void FilterServiceQueryString_GetQueryString_WithModeDefined()
        {
            var filter = new FilterServiceQueryString(new ServiceFilter { Mode = "replicated" });
            Assert.Equal("filters={\"mode\":{\"replicated\":true}}", filter.GetQueryString());
        }
        [Fact]
        public void FilterServiceQueryString_GetQueryString_InvalidModeDefined()
        {
            Assert.Throws<Exception>(() => new FilterServiceQueryString(new ServiceFilter {Mode = "invalid-mode"}));
        }
        [Fact]
        public void FilterServiceQueryString_GetQueryString_ComposedFilters()
        {
            var filter = new FilterServiceQueryString(new ServiceFilter { Label = "service-label", Mode = "global"});
            Assert.Equal("filters={\"label\":{\"service-label\":true},\"mode\":{\"global\":true}}", filter.GetQueryString());
        }
    }
}