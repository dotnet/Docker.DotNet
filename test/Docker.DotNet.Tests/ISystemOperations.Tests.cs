using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Docker.DotNet.Tests
{
    [Collection(nameof(TestCollection))]
    public sealed class ISystemOperationsTests : IDisposable
    {
        private readonly CancellationTokenSource _cts;

        private readonly TestOutput _output;
        private readonly string _repositoryName;
        private readonly string _tag;
        private readonly DockerClient _dockerClient;

        public ISystemOperationsTests(TestFixture testFixture, ITestOutputHelper outputHelper)
        {
            _output = new TestOutput(outputHelper);

            _dockerClient = testFixture.DockerClient;

            // Do not wait forever in case it gets stuck
            _cts = CancellationTokenSource.CreateLinkedTokenSource(testFixture.Cts.Token);
            _cts.CancelAfter(TimeSpan.FromMinutes(5));
            _cts.Token.Register(() => throw new TimeoutException("SystemOperationsTests timeout"));

            _repositoryName = testFixture.Repository;
            _tag = testFixture.Tag;
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
            var info = await _dockerClient.System.GetSystemInfoAsync();
            Assert.NotNull(info.Architecture);
        }

        [Fact]
        public async Task GetVersionAsync_Succeeds()
        {
            var version = await _dockerClient.System.GetVersionAsync();
            Assert.NotNull(version.APIVersion);
        }

        [Fact]
        public async Task MonitorEventsAsync_EmptyContainersList_CanBeCancelled()
        {
            var progress = new Progress<Message>();

            using var cts = new CancellationTokenSource();
            cts.Cancel();
            await Task.Delay(1);

            await Assert.ThrowsAsync<TaskCanceledException>(() => _dockerClient.System.MonitorEventsAsync(new ContainerEventsParameters(), progress, cts.Token));

        }

        [Fact]
        public async Task MonitorEventsAsync_NullParameters_Throws()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _dockerClient.System.MonitorEventsAsync(null, null));
        }

        [Fact]
        public async Task MonitorEventsAsync_NullProgress_Throws()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _dockerClient.System.MonitorEventsAsync(new ContainerEventsParameters(), null));
        }

        [Fact]
        public async Task MonitorEventsAsync_Succeeds()
        {
            var newTag = $"MonitorTests-{Guid.NewGuid().ToString().Substring(1, 10)}";

            var wasProgressCalled = false;

            var progressMessage = new Progress<Message>((m) =>
            {
                _output.WriteLine($"MonitorEventsAsync_Succeeds: Message - {m.Action} - {m.Status} {m.Actor.Attributes["name"]} - {m.Type}");
                wasProgressCalled = true;
                Assert.NotNull(m);
            });

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token);

            var task = _dockerClient.System.MonitorEventsAsync(
                new ContainerEventsParameters(),
                progressMessage,
                cts.Token);

            await _dockerClient.Images.TagImageAsync($"{_repositoryName}:{_tag}", new ImageTagParameters { RepositoryName = _repositoryName, Tag = newTag }, _cts.Token);

            await _dockerClient.Images.DeleteImageAsync(
                name: $"{_repositoryName}:{newTag}",
                new ImageDeleteParameters
                {
                    Force = true
                },
                _cts.Token);

            // Give it some time for output operation to complete before cancelling task
            await Task.Delay(TimeSpan.FromSeconds(1), cts.Token)
                .ConfigureAwait(false);

            cts.Cancel();

            await Assert.ThrowsAsync<TaskCanceledException>(() => task)
                .ConfigureAwait(false);

            Assert.True(wasProgressCalled);
        }

        [Fact]
        public async Task MonitorEventsAsync_IsCancelled_NoStreamCorruption()
        {
            const int iterationCount = 10;

            ICollection<string> events = new List<string>();

            for (var i = 0; i < iterationCount; ++i)
            {
                using var cts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token);

                using var eventsSync = new AutoResetEvent(false);

                var imageTagParameters = new ImageTagParameters();
                imageTagParameters.RepositoryName = _repositoryName;
                imageTagParameters.Tag = Guid.NewGuid().ToString();

                var progress = new Progress<Message>(message => _output.WriteLine($"DockerSystemEvent: {JsonConvert.SerializeObject(message)}"));
                progress.ProgressChanged += (_, message) => events.Add(message.Status);
                progress.ProgressChanged += (_, _) => eventsSync.Set();

                var monitorTask = _dockerClient.System.MonitorEventsAsync(new ContainerEventsParameters(), progress, cts.Token);

                var tagImage = (CancellationToken ct) => _dockerClient.Images.TagImageAsync($"{_repositoryName}:{_tag}", imageTagParameters, ct);

                _ = tagImage.Invoke(default);

                _ = eventsSync.WaitOne(TimeSpan.FromSeconds(1));

                cts.Cancel();

                await tagImage.Invoke(default)
                    .ConfigureAwait(false);

                await Assert.ThrowsAsync<TaskCanceledException>(() => monitorTask)
                    .ConfigureAwait(false);
            }

            Assert.Equal(iterationCount, events.Count);
        }

        [Fact]
        public async Task MonitorEventsFiltered_Succeeds()
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token);

            using var eventsSync = new AutoResetEvent(false);

            ICollection<string> events = new List<string>();

            var sourceImageTag = $"{_repositoryName}:{_tag}";

            var targetImageTag = $"{_repositoryName}:{Guid.NewGuid().ToString()}";

            var image = await _dockerClient.Images.InspectImageAsync(sourceImageTag, _cts.Token)
                .ConfigureAwait(false);

            var imageTagParameters = new ImageTagParameters
            {
                RepositoryName = targetImageTag.Split(':').First(),
                Tag = targetImageTag.Split(':').Last()
            };

            var containerEventsParameters = new ContainerEventsParameters
            {
                Filters = new Dictionary<string, IDictionary<string, bool>>
                {
                    {
                        "event", new Dictionary<string, bool>
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
                        "type", new Dictionary<string, bool>
                        {
                            {
                                "image", true
                            }
                        }
                    },
                    {
                        "image", new Dictionary<string, bool>
                        {
                            {
                                image.ID, true
                            }
                        }
                    }
                }
            };

            var progress = new Progress<Message>(message => _output.WriteLine($"MonitorEventsFiltered_Succeeds: Message received: {message.Action} - {message.Status} {message.Actor.Attributes["name"]} - {message.Type}"));
            progress.ProgressChanged += (_, message) => events.Add(message.Status);
            progress.ProgressChanged += (_, _) => eventsSync.Set();

            var monitorTask = _dockerClient.System.MonitorEventsAsync(containerEventsParameters, progress, cts.Token);

            await _dockerClient.Images.TagImageAsync(sourceImageTag, imageTagParameters, cts.Token)
                .ConfigureAwait(false);

            _ = eventsSync.WaitOne(TimeSpan.FromSeconds(1));

            await _dockerClient.Images.DeleteImageAsync(targetImageTag, new ImageDeleteParameters(), cts.Token)
                .ConfigureAwait(false);

            _ = eventsSync.WaitOne(TimeSpan.FromSeconds(1));

            var createContainerResponse = await _dockerClient.Containers.CreateContainerAsync(new CreateContainerParameters { Image = sourceImageTag }, cts.Token)
                .ConfigureAwait(false);

            await _dockerClient.Containers.RemoveContainerAsync(createContainerResponse.ID, new ContainerRemoveParameters(), cts.Token)
                .ConfigureAwait(false);

            cts.Cancel();

            await Assert.ThrowsAsync<TaskCanceledException>(() => monitorTask)
                .ConfigureAwait(false);

            Assert.True(monitorTask.IsCanceled);
            Assert.Equal(2, events.Count);
            Assert.Contains("tag", events);
            Assert.Contains("untag", events);
        }

        [Fact]
        public async Task PingAsync_Succeeds()
        {
            var exception = await Record.ExceptionAsync(() => _dockerClient.System.PingAsync())
                .ConfigureAwait(false);

            Assert.Null(exception);
        }

        public void Dispose()
        {
            _cts.Dispose();
        }
    }
}