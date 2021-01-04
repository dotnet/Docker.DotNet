using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.ServiceProcess;
using Docker.DotNet.Models;
using Xunit;
using System.IO;
using System.Collections.Generic;

namespace Docker.DotNet.Tests
{
    public class ISystemOperationsTests
    {
        private readonly DockerClient _client;

        public ISystemOperationsTests()
        {
            _client = new DockerClientConfiguration().CreateClient();
        }

        [SupportedOSPlatformsFact(Platform.Windows)]
        public void DockerService_IsRunning()
        {
            var services = ServiceController.GetServices();
            using (var dockerService = services.FirstOrDefault(service => service.ServiceName == "docker" || service.ServiceName == "com.docker.service"))
            {
                Assert.NotNull(dockerService); // docker is not running
                Assert.Equal(ServiceControllerStatus.Running, dockerService.Status);
            }
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
            var progress = new MyProgress()
            {
                _onMessageCalled = (m) => { }
            };

            var cts = new CancellationTokenSource();
            cts.CancelAfter(1000);
            var task = _client.System.MonitorEventsAsync(new EventsParameters(), progress, cts.Token);

            await task;

            // Task will be completed when cancellationToken is cancelled
            Assert.True(task.IsCompleted);
        }

        [Fact]
        public async Task MonitorEventsAsync_NullParameters_Throws()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _client.System.MonitorEventsAsync(null, null));
        }

        [Fact]
        public async Task MonitorEventsAsync_NullProgress_Throws()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _client.System.MonitorEventsAsync(new EventsParameters(), null));
        }

        [Fact]
        public async Task MonitorEventsAsync_Succeeds()
        {
            const string repository = "hello-world";
            var newTag = $"MonitorTests-{Guid.NewGuid().ToString().Substring(1, 10)}";

            var wasProgressCalled = false;

            using var cts = new CancellationTokenSource();
            var progress = new MyProgress()
            {
                _onMessageCalled = (m) =>
                {
                    Assert.NotNull(m);
                    Assert.Equal("tag", m.Status);

                    wasProgressCalled = true;
                }
            };

            var task = Task.Run(() => _client.System.MonitorEventsAsync(new EventsParameters(), progress, cts.Token));
            await _client.Images.TagImageAsync(repository, new ImageTagParameters { RepositoryName = repository, Tag = newTag });

            cts.Cancel();
            await task;

            Assert.True(wasProgressCalled);

            await _client.Images.DeleteImageAsync($"{repository}:{newTag}", new ImageDeleteParameters());
        }

        [Fact]
        public async Task MonitorEventsFiltered_Succeeds()
        {
            const string repository = "hello-world";
            var newTag = $"MonitorTests-{Guid.NewGuid().ToString().Substring(1, 10)}";

            var progressCalledCounter = 0;

            var eventsParams = new EventsParameters()
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
                    }
                }
            };

            using var cts = new CancellationTokenSource();
            var progress = new MyProgress()
            {
                _onMessageCalled = (m) =>
                {
                    progressCalledCounter++;
                }
            };

            var task = Task.Run(() => _client.System.MonitorEventsAsync(eventsParams, progress, cts.Token));

            await _client.Images.TagImageAsync(repository, new ImageTagParameters { RepositoryName = repository, Tag = newTag });
            await _client.Images.DeleteImageAsync($"{repository}:{newTag}", new ImageDeleteParameters());
            var newContainerId = _client.Containers.CreateContainerAsync(new CreateContainerParameters { Image = "hello-world" }).Result.ID;
            await _client.Containers.RemoveContainerAsync(newContainerId, new ContainerRemoveParameters(), cts.Token);

            cts.Cancel();
            await task;

            Assert.Equal(2, progressCalledCounter);
        }

        [Fact]
        public async Task PingAsync_Succeeds()
        {
            await _client.System.PingAsync();
        }

        private class MyProgress : IProgress<Message>
        {
            internal Action<Message> _onMessageCalled;

            void IProgress<Message>.Report(Message value)
            {
                _onMessageCalled(value);
            }
        }
    }
}
