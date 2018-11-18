using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using System.Collections.Generic;
using Xunit;
using System;

namespace Docker.DotNet.Tests
{
    public class ISwarmOperationsTests :IDisposable
    {
        private readonly DockerClient _client;
        private readonly string _testServiceId;
        private static string _testServiceName = "docker-dotnet-test-service";

        public ISwarmOperationsTests()
        {
            _client = new DockerClientConfiguration().CreateClient();

            _testServiceId = _client.Swarm.CreateServiceAsync(new ServiceCreateParameters()
            {

                Service = new ServiceSpec
                {
                    Name = _testServiceName,
                    TaskTemplate = new TaskSpec()
                    {
                        ContainerSpec = new ContainerSpec()
                        {
                            Image = "nginx:latest"
                        }
                    }
                }
            }).Result.ID;
        }

        [Fact]
        public async Task GetServicesAsync_Succeeds()
        {
            var services = await _client.Swarm.ListServicesAsync(cancellationToken: CancellationToken.None);
            Assert.Contains(_testServiceId, services.Select(s => s.ID));
        }
        [Fact]
        public async Task GetFilteredServicesAsync_Succeeds()
        {
            var services = await _client.Swarm.ListServicesAsync(new ServicesListParameters { Filters = new ServiceFilter { Name = new string[] { _testServiceName } } }, CancellationToken.None);
            Assert.Single(services);
        }
        [Fact]
        public async Task CreateServiceAsync_FaultyNetwork_Throws()
        {
            await Assert.ThrowsAsync<DockerApiException>(() => _client.Swarm.CreateServiceAsync(new ServiceCreateParameters()
            {
                
                Service = new ServiceSpec
                {
                    Name = $"{_testServiceName}2",
                    TaskTemplate = new TaskSpec()
                    {
                        ContainerSpec = new ContainerSpec()
                        {
                            Image = "nginx:latest"
                        }
                    }, 
                    Networks = new List<NetworkAttachmentConfig>() { new NetworkAttachmentConfig() { Target = "non-existing-network" } }
                }
            }));
        }

        public void Dispose()
        {
            _client.Swarm.RemoveServiceAsync(_testServiceId, CancellationToken.None);
        }
    }
}