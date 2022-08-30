using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using Newtonsoft.Json;
using Xunit;

namespace Docker.DotNet.Tests
{
    public sealed class TestFixture : IAsyncLifetime, IDisposable
    {
        /// <summary>
        /// The Docker image name.
        /// </summary>
        private const string Name = "nats";
        
        private static readonly Progress<JSONMessage> WriteProgressOutput;

        private bool _hasInitializedSwarm;
        
        static TestFixture()
        {
            WriteProgressOutput = new Progress<JSONMessage>(jsonMessage =>
            {
                var message = JsonConvert.SerializeObject(jsonMessage);
                Console.WriteLine(message);
                Debug.WriteLine(message);
            });
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TestFixture" /> class.
        /// </summary>
        /// <exception cref="TimeoutException">Thrown when tests are not finished within 5 minutes.</exception>
        public TestFixture()
        {
            DockerClientConfiguration = new DockerClientConfiguration();
            DockerClient = DockerClientConfiguration.CreateClient();
            Cts = new CancellationTokenSource(TimeSpan.FromMinutes(5));
            Cts.Token.Register(() => throw new TimeoutException("Docker.DotNet test timeout exception"));
        }

        /// <summary>
        /// Gets the Docker image repository.
        /// </summary>
        public string Repository { get; }
            = Guid.NewGuid().ToString("N");

        /// <summary>
        /// Gets the Docker image tag.
        /// </summary>
        public string Tag { get; }
            = Guid.NewGuid().ToString("N");

        /// <summary>
        /// Gets the Docker client.
        /// </summary>
        public DockerClient DockerClient { get; }

        /// <summary>
        /// Gets the Docker client configuration.
        /// </summary>
        public DockerClientConfiguration DockerClientConfiguration { get; }

        /// <summary>
        /// Gets the cancellation token source.
        /// </summary>
        public CancellationTokenSource Cts { get; }

        /// <summary>
        /// Gets or sets the Docker image.
        /// </summary>
        public ImagesListResponse Image { get; private set; }

        /// <inheritdoc />
        public async Task InitializeAsync()
        {
            // Create image
            await DockerClient.Images.CreateImageAsync(new ImagesCreateParameters { FromImage = Name, Tag = "latest" }, null, WriteProgressOutput, Cts.Token)
                .ConfigureAwait(false);

            // Get images
            var images = await DockerClient.Images.ListImagesAsync(
                    new ImagesListParameters
                    {
                        Filters = new Dictionary<string, IDictionary<string, bool>>
                        {
                            ["reference"] = new Dictionary<string, bool>
                            {
                                [Name] = true
                            }
                        }
                    }, Cts.Token)
                .ConfigureAwait(false);

            // Set image
            Image = images.Single();

            // Tag image
            await DockerClient.Images.TagImageAsync(Image.ID, new ImageTagParameters { RepositoryName = Repository, Tag = Tag }, Cts.Token)
                .ConfigureAwait(false);

            // Init a new swarm, if not part of an existing one
            try
            {
                _ = await DockerClient.Swarm.InitSwarmAsync(new SwarmInitParameters { AdvertiseAddr = "10.10.10.10", ListenAddr = "127.0.0.1" }, Cts.Token)
                    .ConfigureAwait(false);

                _hasInitializedSwarm = true;
            }
            catch
            {
                const string message = "Couldn't init a new swarm, the node should take part of an existing one.";
                Console.WriteLine(message);
                Debug.WriteLine(message);

                _hasInitializedSwarm = false;
            }
        }

        /// <inheritdoc />
        public async Task DisposeAsync()
        {
            if (_hasInitializedSwarm)
            {
                await DockerClient.Swarm.LeaveSwarmAsync(new SwarmLeaveParameters { Force = true }, Cts.Token)
                    .ConfigureAwait(false);
            }

            var containers = await DockerClient.Containers.ListContainersAsync(
                    new ContainersListParameters
                    {
                        Filters = new Dictionary<string, IDictionary<string, bool>>
                        {
                            ["ancestor"] = new Dictionary<string, bool>
                            {
                                [Image.ID] = true
                            }
                        },
                        All = true
                    }, Cts.Token)
                .ConfigureAwait(false);

            var images = await DockerClient.Images.ListImagesAsync(
                    new ImagesListParameters
                    {
                        Filters = new Dictionary<string, IDictionary<string, bool>>
                        {
                            ["reference"] = new Dictionary<string, bool>
                            {
                                [Image.RepoDigests.Single()] = true
                            }
                        },
                        All = true
                    }, Cts.Token)
                .ConfigureAwait(false);

            foreach (var container in containers)
            {
                await DockerClient.Containers.RemoveContainerAsync(container.ID, new ContainerRemoveParameters { Force = true }, Cts.Token)
                    .ConfigureAwait(false);
            }

            foreach (var image in images)
            {
                await DockerClient.Images.DeleteImageAsync(image.ID, new ImageDeleteParameters { Force = true }, Cts.Token)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            DockerClient.Dispose();
            DockerClientConfiguration.Dispose();
            Cts.Dispose();
        }
    }

    [CollectionDefinition(nameof(TestCollection))]
    public sealed class TestCollection : ICollectionFixture<TestFixture>
    {
    }
}