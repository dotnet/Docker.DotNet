using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Docker.DotNet.Models;
using Newtonsoft.Json;
using Xunit;

namespace Docker.DotNet.Tests
{
    public class TestFixture : IDisposable
    {
        // Tests require an image whose containers continue running when created new, and works on both Windows an Linux containers. 
        private const string _imageName = "nats";

        private readonly bool _wasSwarmInitialized = false;

        public TestFixture()
        {
            // Do not wait forever in case it gets stuck
            cts = new CancellationTokenSource(TimeSpan.FromMinutes(5));
            cts.Token.Register(() => throw new TimeoutException("Docker.DotNet test timeout exception"));

            dockerClientConfiguration = new DockerClientConfiguration();
            dockerClient = dockerClientConfiguration.CreateClient();

            // Create image
            dockerClient.Images.CreateImageAsync(
                new ImagesCreateParameters
                {
                    FromImage = _imageName,
                    Tag = "latest"
                },
                null,
                new Progress<JSONMessage>((m) => { Console.WriteLine(JsonConvert.SerializeObject(m)); Debug.WriteLine(JsonConvert.SerializeObject(m)); }),
                cts.Token).GetAwaiter().GetResult();

            // Create local image tag to reuse
            var existingImagesResponse = dockerClient.Images.ListImagesAsync(
               new ImagesListParameters
               {
                   Filters = new Dictionary<string, IDictionary<string, bool>>
                   {
                       ["reference"] = new Dictionary<string, bool>
                       {
                           [_imageName] = true
                       }
                   }
               },
               cts.Token
           ).GetAwaiter().GetResult();

            imageId = existingImagesResponse[0].ID;

            dockerClient.Images.TagImageAsync(
                imageId,
                new ImageTagParameters
                {
                    RepositoryName = repositoryName,
                    Tag = tag
                },
                cts.Token
            ).GetAwaiter().GetResult();

            // Init swarm if not part of one
            try
            {
                var result = dockerClient.Swarm.InitSwarmAsync(new SwarmInitParameters { AdvertiseAddr = "10.10.10.10", ListenAddr = "127.0.0.1" }, default).GetAwaiter().GetResult();
            }
            catch
            {
                Console.WriteLine("Couldn't init a new swarm, node should take part of a existing one");
                _wasSwarmInitialized = true;
            }


        }

        public CancellationTokenSource cts { get; }
        public DockerClient dockerClient { get; }
        public DockerClientConfiguration dockerClientConfiguration { get; }
        public string repositoryName { get; } = Guid.NewGuid().ToString();
        public string tag { get; } = Guid.NewGuid().ToString();
        public string imageId { get; }

        public void Dispose()
        {
            if (_wasSwarmInitialized)
            {
                dockerClient.Swarm.LeaveSwarmAsync(new SwarmLeaveParameters { Force = true }, cts.Token);
            }

            var containerList = dockerClient.Containers.ListContainersAsync(
                new ContainersListParameters
                {
                    Filters = new Dictionary<string, IDictionary<string, bool>>
                    {
                        ["ancestor"] = new Dictionary<string, bool>
                        {
                            [$"{repositoryName}:{tag}"] = true
                        }
                    },
                    All = true,
                },
                cts.Token
                ).GetAwaiter().GetResult();

            foreach (ContainerListResponse container in containerList)
            {
                dockerClient.Containers.RemoveContainerAsync(
                    container.ID,
                    new ContainerRemoveParameters
                    {
                        Force = true
                    },
                    cts.Token
                ).GetAwaiter().GetResult();
            }

            var imageList = dockerClient.Images.ListImagesAsync(
                new ImagesListParameters
                {
                    Filters = new Dictionary<string, IDictionary<string, bool>>
                    {
                        ["reference"] = new Dictionary<string, bool>
                        {
                            [imageId] = true
                        },
                        ["since"] = new Dictionary<string, bool>
                        {
                            [imageId] = true
                        }
                    },
                    All = true
                },
                cts.Token
            ).GetAwaiter().GetResult();

            foreach (ImagesListResponse image in imageList)
            {
                dockerClient.Images.DeleteImageAsync(
                    image.ID,
                    new ImageDeleteParameters { Force = true },
                    cts.Token
                ).GetAwaiter().GetResult();
            }

            dockerClient.Dispose();
            dockerClientConfiguration.Dispose();
            cts.Dispose();
        }
    }

    [CollectionDefinition("Test collection")]
    public class TestsCollection : ICollectionFixture<TestFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
