using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Docker.DotNet.Tests
{
    [Collection("Test collection")]
    public class ISystemOperationsTests
    {
        private readonly DockerClient _client;
        private readonly TestOutput _output;
        private readonly string _repositoryName;
        private readonly string _tag;
        private readonly CancellationTokenSource _cts;

        public ISystemOperationsTests(TestFixture testFixture, ITestOutputHelper output)
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(testFixture.cts.Token);
            _cts.Token.Register(() => throw new TimeoutException("ISystemOperationsTest timeout"));
            _cts.CancelAfter(TimeSpan.FromMinutes(5));

            _repositoryName = testFixture.repositoryName;
            _tag = testFixture.tag;

            _client = testFixture.dockerClient;
            _output = new TestOutput(output);
        }

        [Fact]
        public void Docker_IsRunning()
        {
            var dockerProcess = Process.GetProcesses().FirstOrDefault(_ => _.ProcessName.Equals("docker", StringComparison.InvariantCultureIgnoreCase) || _.ProcessName.Equals("dockerd", StringComparison.InvariantCultureIgnoreCase));
            Assert.NotNull(dockerProcess); // docker is not running
        }

        [Fact]
        public async Task GetSystemInfoAsync_Succeeds()
        {
            var info = await _client.System.GetSystemInfoAsync();
            Assert.NotNull(info.Architecture);
        }

        [Fact]
        public async Task GetVersionAsync_Succeeds()
        {
            var version = await _client.System.GetVersionAsync();
            Assert.NotNull(version.APIVersion);
        }

        [Fact]
        public async Task MonitorEventsAsync_EmptyContainersList_CanBeCancelled()
        {
            var progress = new Progress<Message>();

            using var cts = new CancellationTokenSource();
            cts.Cancel();
            await Task.Delay(1);

            await Assert.ThrowsAsync<TaskCanceledException>(() => _client.System.MonitorEventsAsync(new ContainerEventsParameters(), progress, cts.Token));

        }

        [Fact]
        public async Task MonitorEventsAsync_NullParameters_Throws()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _client.System.MonitorEventsAsync(null, null));
        }

        [Fact]
        public async Task MonitorEventsAsync_NullProgress_Throws()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _client.System.MonitorEventsAsync(new ContainerEventsParameters(), null));
        }

        [Fact]
        public async Task MonitorEventsAsync_Succeeds()
        {
            var newTag = $"MonitorTests-{Guid.NewGuid().ToString().Substring(1, 10)}";

            var progressJSONMessage = new Progress<JSONMessage>((m) =>
            {
                // Status could be 'Pulling from...'
                Assert.NotNull(m);
                _output.WriteLine($"MonitorEventsAsync_Succeeds: JSONMessage - {m.ID} - {m.Status} {m.From} - {m.Stream}");
            });

            var wasProgressCalled = false;

            var progressMessage = new Progress<Message>((m) =>
            {
                _output.WriteLine($"MonitorEventsAsync_Succeeds: Message - {m.Action} - {m.Status} {m.From} - {m.Type}");
                wasProgressCalled = true;
                Assert.NotNull(m);
            });

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token);

            var task = _client.System.MonitorEventsAsync(
                new ContainerEventsParameters(),
                progressMessage,
                cts.Token);

            await _client.Images.CreateImageAsync(new ImagesCreateParameters { FromImage = $"{_repositoryName}:{_tag}" }, null, progressJSONMessage, _cts.Token);

            await _client.Images.TagImageAsync($"{_repositoryName}:{_tag}", new ImageTagParameters { RepositoryName = _repositoryName, Tag = newTag }, _cts.Token);

            await _client.Images.DeleteImageAsync(
                name: $"{_repositoryName}:{newTag}",
                new ImageDeleteParameters
                {
                    Force = true
                },
                _cts.Token);

            // Give it some time for output operation to complete before cancelling task
            await Task.Delay(TimeSpan.FromSeconds(1));

            cts.Cancel();

            await Assert.ThrowsAsync<TaskCanceledException>(() => task).ConfigureAwait(false);

            Assert.True(wasProgressCalled);
        }

        [Fact]
        public async Task MonitorEventsAsync_IsCancelled_NoStreamCorruption()
        {
            var rand = new Random();
            var sw = new Stopwatch();

            for (int i = 0; i < 20; ++i)
            {
                try
                {
                    // (1) Create monitor task
                    using var cts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token);

                    string newImageTag = Guid.NewGuid().ToString();

                    var monitorTask = _client.System.MonitorEventsAsync(
                        new ContainerEventsParameters(),
                        new Progress<Message>((value) => _output.WriteLine($"DockerSystemEvent: {JsonConvert.SerializeObject(value)}")),
                        cts.Token);

                    // (2) Wait for some time to make sure we get into blocking IO call
                    await Task.Delay(100);

                    // (3) Invoke another request that will attempt to grab the same buffer
                    var listImagesTask1 = _client.Images.TagImageAsync(
                        $"{_repositoryName}:{_tag}",
                        new ImageTagParameters
                        {
                            RepositoryName = _repositoryName,
                            Tag = newImageTag,
                            Force = true
                        },
                        default);

                    // (4) Wait for a short bit again and cancel the monitor task - if we get lucky, we the list images call will grab the same buffer while
                    sw.Restart();
                    var iterations = rand.Next(15000000);

                    for (int j = 0; j < iterations; j++)
                    {
                        // noop
                    }
                    _output.WriteLine($"Waited for {sw.Elapsed.TotalMilliseconds} ms");

                    cts.Cancel();

                    listImagesTask1.GetAwaiter().GetResult();

                    _client.Images.TagImageAsync(
                        $"{_repositoryName}:{_tag}",
                        new ImageTagParameters
                        {
                            RepositoryName = _repositoryName,
                            Tag = newImageTag,
                            Force = true
                        }
                    ).GetAwaiter().GetResult();

                    monitorTask.GetAwaiter().GetResult();
                }
                catch (TaskCanceledException)
                {
                    // Exceptions other than this causes test to fail
                }
            }
        }

        [Fact]
        public async Task MonitorEventsFiltered_Succeeds()
        {
            string newTag = $"MonitorTests-{Guid.NewGuid().ToString().Substring(1, 10)}";
            string newImageRespositoryName = Guid.NewGuid().ToString();

            await _client.Images.TagImageAsync(
                $"{_repositoryName}:{_tag}",
                new ImageTagParameters
                {
                    RepositoryName = newImageRespositoryName,
                    Tag = newTag
                },
                _cts.Token
            );

            ImageInspectResponse image = await _client.Images.InspectImageAsync(
                $"{newImageRespositoryName}:{newTag}",
                _cts.Token
            );

            var progressCalledCounter = 0;

            var eventsParams = new ContainerEventsParameters()
            {
                Filters = new Dictionary<string, IDictionary<string, bool>>()
                {
                    {
                        "event", new Dictionary<string, bool>()
                        {
                            {
                                "tag", true
                            },
                            {
                                "untag", true
                            }
                        }
                    },
                    {
                        "type", new Dictionary<string, bool>()
                        {
                            {
                                "image", true
                            }
                        }
                    },
                    {
                        "image", new Dictionary<string, bool>()
                        {
                            {
                                image.ID, true
                            }
                        }
                    }
                }
            };

            var progress = new Progress<Message>((m) =>
            {
                progressCalledCounter++;
                Assert.True(m.Status == "tag" || m.Status == "untag");
                _output.WriteLine($"MonitorEventsFiltered_Succeeds: Message received: {m.Action} - {m.Status} {m.From} - {m.Type}");
            });

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token);
            var task = Task.Run(() => _client.System.MonitorEventsAsync(eventsParams, progress, cts.Token));

            await _client.Images.CreateImageAsync(new ImagesCreateParameters { FromImage = $"{_repositoryName}:{_tag}" }, null, new Progress<JSONMessage>());

            await _client.Images.TagImageAsync($"{_repositoryName}:{_tag}", new ImageTagParameters { RepositoryName = _repositoryName, Tag = newTag });
            await _client.Images.DeleteImageAsync($"{_repositoryName}:{newTag}", new ImageDeleteParameters());

            var createContainerResponse = await _client.Containers.CreateContainerAsync(new CreateContainerParameters { Image = $"{_repositoryName}:{_tag}" });
            await _client.Containers.RemoveContainerAsync(createContainerResponse.ID, new ContainerRemoveParameters(), cts.Token);

            await Task.Delay(TimeSpan.FromSeconds(1));
            cts.Cancel();

            await Assert.ThrowsAsync<TaskCanceledException>(() => task);

            Assert.Equal(2, progressCalledCounter);
            Assert.True(task.IsCanceled);
        }

        [Fact]
        public async Task PingAsync_Succeeds()
        {
            await _client.System.PingAsync();
        }

        [Fact]
        public async Task AuthenticateAsync_Succeeds()
        {
            // The blank config goes to Dockerhub, which allows anonymous 
            // reads, so the auth will definitely be accepted. This lets
            // us check whether we correctly retrieve that acceptance.
            var config = new AuthConfig();
            var response = await _client.System.AuthenticateAsync(config);
            Assert.Equal("Login Succeeded", response.Status);
        }
    }
}
