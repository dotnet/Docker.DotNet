using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using Xunit;

namespace Docker.DotNet.Tests
{
    public class ISwarmOperationsTests : IDisposable
    {
        private readonly DockerClient _client;
        private bool wasSwarmInitialized = false;

        public ISwarmOperationsTests()
        {
            using var configuration = new DockerClientConfiguration();
            _client = configuration.CreateClient();

            // Init swarm if not part of one
            try
            {
                var result = _client.Swarm.InitSwarmAsync(new SwarmInitParameters { AdvertiseAddr = "10.10.10.10", ListenAddr = "127.0.0.1" }, default).Result;
            }
            catch (Exception ex)
            {
                wasSwarmInitialized = true;
                System.Diagnostics.Debug.WriteLine($"Swarm init: {ex.Message}");
            }
        }

        [Fact]
        public async Task GetFilteredServicesByName_Succeeds()
        {
            var firstServiceName = $"service1-{Guid.NewGuid().ToString().Substring(1, 10)}";
            var firstServiceId = _client.Swarm.CreateServiceAsync(new ServiceCreateParameters
            {
                Service = new ServiceSpec
                {
                    Name = firstServiceName,
                    TaskTemplate = new TaskSpec { ContainerSpec = new ContainerSpec { Image = "hello-world" } }
                }
            }).Result.ID;

            var secondServiceId = _client.Swarm.CreateServiceAsync(new ServiceCreateParameters
            {
                Service = new ServiceSpec
                {
                    Name = $"service2-{Guid.NewGuid().ToString().Substring(1, 10)}",
                    TaskTemplate = new TaskSpec { ContainerSpec = new ContainerSpec { Image = "hello-world" } }
                }
            }).Result.ID;

            var thirdServiceid = _client.Swarm.CreateServiceAsync(new ServiceCreateParameters
            {
                Service = new ServiceSpec
                {
                    Name = $"service3-{Guid.NewGuid().ToString().Substring(1, 10)}",
                    TaskTemplate = new TaskSpec { ContainerSpec = new ContainerSpec { Image = "hello-world" } }
                }
            }).Result.ID;

            var options = new ServicesListParameters { Filters = new ServiceFilter { Name = new string[] { firstServiceName } } };
            var services = await _client.Swarm.ListServicesAsync(options, CancellationToken.None);

            Assert.Single(services);

            await _client.Swarm.RemoveServiceAsync(firstServiceId, default);
            await _client.Swarm.RemoveServiceAsync(secondServiceId, default);
            await _client.Swarm.RemoveServiceAsync(thirdServiceid, default);
        }

        [Fact]
        public async Task GetFilteredServicesById_Succeeds()
        {
            var firstServiceId = _client.Swarm.CreateServiceAsync(new ServiceCreateParameters
            {
                Service = new ServiceSpec
                {
                    Name = $"service1-{Guid.NewGuid().ToString().Substring(1, 10)}",
                    TaskTemplate = new TaskSpec { ContainerSpec = new ContainerSpec { Image = "hello-world" } }
                }
            }).Result.ID;

            var secondServiceId = _client.Swarm.CreateServiceAsync(new ServiceCreateParameters
            {
                Service = new ServiceSpec
                {
                    Name = $"service2-{Guid.NewGuid().ToString().Substring(1, 10)}",
                    TaskTemplate = new TaskSpec { ContainerSpec = new ContainerSpec { Image = "hello-world" } }
                }
            }).Result.ID;

            var thirdServiceid = _client.Swarm.CreateServiceAsync(new ServiceCreateParameters
            {
                Service = new ServiceSpec
                {
                    Name = $"service3-{Guid.NewGuid().ToString().Substring(1, 10)}",
                    TaskTemplate = new TaskSpec { ContainerSpec = new ContainerSpec { Image = "hello-world" } }
                }
            }).Result.ID;

            var services = await _client.Swarm.ListServicesAsync(new ServicesListParameters { Filters = new ServiceFilter { Id = new string[] { firstServiceId } } }, CancellationToken.None);
            Assert.Single(services);

            await _client.Swarm.RemoveServiceAsync(firstServiceId, default);
            await _client.Swarm.RemoveServiceAsync(secondServiceId, default);
            await _client.Swarm.RemoveServiceAsync(thirdServiceid, default);
        }

        [Fact]
        public async Task GetServices_Succeeds()
        {
            var initialServiceCount = _client.Swarm.ListServicesAsync(cancellationToken: CancellationToken.None).Result.Count();

            var firstServiceId = _client.Swarm.CreateServiceAsync(new ServiceCreateParameters
            {
                Service = new ServiceSpec
                {
                    Name = $"service1-{Guid.NewGuid().ToString().Substring(1, 10)}",
                    TaskTemplate = new TaskSpec { ContainerSpec = new ContainerSpec { Image = "hello-world" } }
                }
            }).Result.ID;

            var secondServiceId = _client.Swarm.CreateServiceAsync(new ServiceCreateParameters
            {
                Service = new ServiceSpec
                {
                    Name = $"service2-{Guid.NewGuid().ToString().Substring(1, 10)}",
                    TaskTemplate = new TaskSpec { ContainerSpec = new ContainerSpec { Image = "hello-world" } }
                }
            }).Result.ID;

            var thirdServiceid = _client.Swarm.CreateServiceAsync(new ServiceCreateParameters
            {
                Service = new ServiceSpec
                {
                    Name = $"service3-{Guid.NewGuid().ToString().Substring(1, 10)}",
                    TaskTemplate = new TaskSpec { ContainerSpec = new ContainerSpec { Image = "hello-world" } }
                }
            }).Result.ID;

            var services = await _client.Swarm.ListServicesAsync(cancellationToken: CancellationToken.None);

            Assert.True(services.Count() > initialServiceCount);

            await _client.Swarm.RemoveServiceAsync(firstServiceId, default);
            await _client.Swarm.RemoveServiceAsync(secondServiceId, default);
            await _client.Swarm.RemoveServiceAsync(thirdServiceid, default);
        }

        [Fact]
        public async Task GetServiceLogs_Succeeds()
        {
            var serviceName = $"service1-{Guid.NewGuid().ToString().Substring(1, 10)}";

            var firstServiceId = _client.Swarm.CreateServiceAsync(new ServiceCreateParameters
            {
                Service = new ServiceSpec
                {
                    Name = serviceName,
                    TaskTemplate = new TaskSpec { ContainerSpec = new ContainerSpec { Image = "hello-world" } }
                }
            }).Result.ID;

            var stream = await _client.Swarm.GetServiceLogsAsync(
                serviceName,
                false,
                new ServiceLogsParameters
                {
                    Follow = true,
                    ShowStdout = true,
                    ShowStderr = true
                }
            );

            var reader = new MultiplexedStreamReader(stream);

            // First line is String.Emtpy

            var line = "";
            for (var i = 0; i < 5; i++)
            {
                line += await reader.ReadLineAsync(default);
            }

            Assert.NotEmpty(line);

            await _client.Swarm.RemoveServiceAsync(serviceName, default);
        }

        [Fact]
        public async Task CreateService_FaultyNetwork_Throws()
        {
            await Assert.ThrowsAsync<DockerSwarmException>(() => _client.Swarm.CreateServiceAsync(new ServiceCreateParameters
            {
                Service = new ServiceSpec
                {
                    Name = "faultyNetworkService",
                    TaskTemplate = new TaskSpec { ContainerSpec = new ContainerSpec { Image = "hello-world" } },
                    Networks = new List<NetworkAttachmentConfig> { new NetworkAttachmentConfig { Target = "non-existing-network" } }
                }
            }));
        }

        public void Dispose()
        {
            //if (!wasSwarmInitialized)
            //{
            //    _client.Swarm.LeaveSwarmAsync(new SwarmLeaveParameters { Force = true });
            //}

            GC.SuppressFinalize(this);
        }
    }
}
