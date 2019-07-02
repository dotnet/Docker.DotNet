using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using Xunit;

namespace Docker.DotNet.Tests
{
    public class ISwarmOperationsTests
    {
        private readonly DockerClient _client;

        public ISwarmOperationsTests()
        {
            _client = new DockerClientConfiguration().CreateClient();
        }

        [Fact]
        public async Task GetServicesAsync_Succeeds()
        {
            var services = await _client.Swarm.ListServicesAsync(cancellationToken: CancellationToken.None);
            Assert.Equal(2, services.Count());
        }
        [Fact]
        public async Task GetFilteredServicesAsync_Succeeds()
        {
            var services = await _client.Swarm.ListServicesAsync(new ServicesListParameters { Filters = new ServiceFilter { Id = "pr6264hhb836" } }, CancellationToken.None);
            Assert.Single(services);
        }
    }
}