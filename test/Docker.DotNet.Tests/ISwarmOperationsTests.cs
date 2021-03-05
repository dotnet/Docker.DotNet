using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using Xunit;

namespace Docker.DotNet.Tests
{
    [Collection("Test collection")]
    public class ISwarmOperationsTests
    {
        private readonly DockerClient _client;
        private readonly string _imageId;

        public ISwarmOperationsTests(TestFixture testFixture)
        {
            _client = testFixture.dockerClient;
            _imageId = testFixture.imageId;
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
                    TaskTemplate = new TaskSpec { ContainerSpec = new ContainerSpec { Image = _imageId } }
                }
            }).Result.ID;

            var secondServiceId = _client.Swarm.CreateServiceAsync(new ServiceCreateParameters
            {
                Service = new ServiceSpec
                {
                    Name = $"service2-{Guid.NewGuid().ToString().Substring(1, 10)}",
                    TaskTemplate = new TaskSpec { ContainerSpec = new ContainerSpec { Image = _imageId } }
                }
            }).Result.ID;

            var thirdServiceid = _client.Swarm.CreateServiceAsync(new ServiceCreateParameters
            {
                Service = new ServiceSpec
                {
                    Name = $"service3-{Guid.NewGuid().ToString().Substring(1, 10)}",
                    TaskTemplate = new TaskSpec { ContainerSpec = new ContainerSpec { Image = _imageId } }
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
                    TaskTemplate = new TaskSpec { ContainerSpec = new ContainerSpec { Image = _imageId } }
                }
            }).Result.ID;

            var secondServiceId = _client.Swarm.CreateServiceAsync(new ServiceCreateParameters
            {
                Service = new ServiceSpec
                {
                    Name = $"service2-{Guid.NewGuid().ToString().Substring(1, 10)}",
                    TaskTemplate = new TaskSpec { ContainerSpec = new ContainerSpec { Image = _imageId } }
                }
            }).Result.ID;

            var thirdServiceid = _client.Swarm.CreateServiceAsync(new ServiceCreateParameters
            {
                Service = new ServiceSpec
                {
                    Name = $"service3-{Guid.NewGuid().ToString().Substring(1, 10)}",
                    TaskTemplate = new TaskSpec { ContainerSpec = new ContainerSpec { Image = _imageId } }
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
                    TaskTemplate = new TaskSpec { ContainerSpec = new ContainerSpec { Image = _imageId } }
                }
            }).Result.ID;

            var secondServiceId = _client.Swarm.CreateServiceAsync(new ServiceCreateParameters
            {
                Service = new ServiceSpec
                {
                    Name = $"service2-{Guid.NewGuid().ToString().Substring(1, 10)}",
                    TaskTemplate = new TaskSpec { ContainerSpec = new ContainerSpec { Image = _imageId } }
                }
            }).Result.ID;

            var thirdServiceid = _client.Swarm.CreateServiceAsync(new ServiceCreateParameters
            {
                Service = new ServiceSpec
                {
                    Name = $"service3-{Guid.NewGuid().ToString().Substring(1, 10)}",
                    TaskTemplate = new TaskSpec { ContainerSpec = new ContainerSpec { Image = _imageId } }
                }
            }).Result.ID;

            var services = await _client.Swarm.ListServicesAsync(cancellationToken: CancellationToken.None);

            Assert.True(services.Count() > initialServiceCount);

            await _client.Swarm.RemoveServiceAsync(firstServiceId, default);
            await _client.Swarm.RemoveServiceAsync(secondServiceId, default);
            await _client.Swarm.RemoveServiceAsync(thirdServiceid, default);
        }
    }
}
