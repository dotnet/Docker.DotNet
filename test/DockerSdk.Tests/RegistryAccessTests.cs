using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace DockerSdk.Tests
{
    [Collection("Common")]
    public class RegistryAccessTests
    {
        private const string BasicAuthRegistry = "localhost:5001";
        private const string OpenRegistry = "localhost:4000";

        [Fact]
        public async Task CheckAuthenticationAsync_AnonymousAccess_ToBasicAuthRegistry_Fails()
        {
            using var client = await DockerClient.StartAsync();
            client.Registries.AddAnonymous(BasicAuthRegistry);

            var result = await client.Registries.CheckAuthenticationAsync(BasicAuthRegistry);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task CheckAuthenticationAsync_AnonymousAccess_ToDockerhub_Suceeds()
        {
            using var client = await DockerClient.StartAsync();

            var result = await client.Registries.CheckAuthenticationAsync("docker.io");

            result.Should().BeTrue();
        }

        [Fact]
        public async Task CheckAuthenticationAsync_AnonymousAccess_ToRecognizedOpenRegistry_Suceeds()
        {
            using var client = await DockerClient.StartAsync();
            client.Registries.AddAnonymous(OpenRegistry);

            var result = await client.Registries.CheckAuthenticationAsync(OpenRegistry);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task CheckAuthenticationAsync_AnonymousAccess_ToUnrecognizedOpenRegistry_Suceeds()
        {
            using var client = await DockerClient.StartAsync();

            var result = await client.Registries.CheckAuthenticationAsync(OpenRegistry);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task CheckAuthenticationAsync_BasicAuth_ToOpenRegistry_Succeeds()
        {
            using var client = await DockerClient.StartAsync();
            client.Registries.AddBasicAuth(OpenRegistry, "donald", "duck");

            var result = await client.Registries.CheckAuthenticationAsync(OpenRegistry);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task CheckAuthenticationAsync_CorrectCredentials_ToBasicAuthRegistry_Fails()
        {
            using var client = await DockerClient.StartAsync();
            client.Registries.AddBasicAuth(BasicAuthRegistry, "testuser", "testpassword");

            var result = await client.Registries.CheckAuthenticationAsync(BasicAuthRegistry);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task CheckAuthenticationAsync_IncorrectCredentials_ToBasicAuthRegistry_Fails()
        {
            using var client = await DockerClient.StartAsync();
            client.Registries.AddBasicAuth(BasicAuthRegistry, "wronguser", "wrongpassword");

            var result = await client.Registries.CheckAuthenticationAsync(BasicAuthRegistry);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task CheckAuthenticationAsync_MadeUpAuthToken_ToBasicAuthRegistry_Fails()
        {
            using var client = await DockerClient.StartAsync();
            client.Registries.AddIdentityToken(BasicAuthRegistry, "faketoken");

            var result = await client.Registries.CheckAuthenticationAsync("localhost:5001");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task CheckAuthenticationAsync_MadeUpAuthToken_ToOpenRegistry_Succeeds()
        {
            using var client = await DockerClient.StartAsync();
            client.Registries.AddIdentityToken(OpenRegistry, "faketoken");

            var result = await client.Registries.CheckAuthenticationAsync(OpenRegistry);

            result.Should().BeTrue();
        }
    }
}
