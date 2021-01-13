using System;
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

        public ISwarmOperationsTests()
        {
            using var configuration = new DockerClientConfiguration();
            _client = configuration.CreateClient();

            // Init swarm if not part of one
            try
            {
                var result = _client.Swarm.InitSwarmAsync(new SwarmInitParameters { AdvertiseAddr = "10.10.10.10", ListenAddr = "127.0.0.1" }, default).Result;
            }
            catch { }
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

            var services = await _client.Swarm.ListServicesAsync(
                new ServicesListParameters
                {
                    Filters = new ServiceFilter
                    {
                        Name = new string[]
                        {
                            firstServiceName
                        }
                    }
                },
                CancellationToken.None);

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
