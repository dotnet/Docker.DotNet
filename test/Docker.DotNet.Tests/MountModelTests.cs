using System.Text.Json;
using Docker.DotNet.Models;
using Xunit;

namespace Docker.DotNet.Tests
{
    public class MountModelTests
    {
        [Fact]
        public void VolumeOptions_Subpath_IsSerialized()
        {
            var mount = new Mount
            {
                VolumeOptions = new VolumeOptions
                {
                    Subpath = "data/logs"
                }
            };

            using var document = JsonDocument.Parse(System.Text.Json.JsonSerializer.Serialize(mount));

            Assert.Equal("data/logs", document.RootElement
                .GetProperty("VolumeOptions")
                .GetProperty("Subpath")
                .GetString());
        }
    }
}
