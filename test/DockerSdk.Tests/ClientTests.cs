using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace DockerSdk.Tests
{
    [Collection("Common")]
    public class ClientTests
    {
        [Fact]
        public async Task ConnectAsync_Defaults_HasCorrectApiVersion()
        {
            using var client = await DockerClient.StartAsync();
            client.ApiVersion.ToString().Should().Be("1.41");
        }

        [Fact]
        public async Task ConnectAsync_Defaults_ReturnsClientObject()
        {
            using var docker = await DockerClient.StartAsync();

            docker.Should().NotBeNull();
        }

        [Fact]
        public async Task ConnectAsync_NoSuchPlace_Throws()
        {
            await Assert.ThrowsAsync<HttpRequestException>(
                () => DockerClient.StartAsync(new Uri("http://localhost:123")));
        }
    }
}
