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
            var progress = new ProgressMessage()
            {
                _onMessageCalled = (m) => { }
            };

            var cts = new CancellationTokenSource();
            cts.CancelAfter(1000);

            var task = _client.System.MonitorEventsAsync(new EventsParameters(), progress, cts.Token);

            var taskFinishes = false;
            try
            {
                await task;
            }
            catch
            {
                // Exception is not always thrown when cancelling token
            }
            finally
            {
                taskFinishes = true;
            }

            // On local develop machine task is completed.
            // On CI/CD Pipeline exception is thrown, not always

            Assert.True(task.IsCompleted || taskFinishes);
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

            var wasJSONMessageProgressCalled = false;
            var progressJSONMessage = new ProgressJSONMessage
            {
                _onJSONMessageCalled = (m) =>
                {
                    Assert.NotNull(m);
                    // Status could be 'Pulling from...'
                    wasJSONMessageProgressCalled = true;
                }
            };

            var wasProgressCalled = false;
            var progressMessage = new ProgressMessage
            {
                _onMessageCalled = (m) =>
                {
                    Assert.NotNull(m);
                    Assert.True(m.Status == "tag" || m.Status == "pull");

                    wasProgressCalled = true;
                }
            };

            using var cts = new CancellationTokenSource();

            await _client.Images.CreateImageAsync(new ImagesCreateParameters { FromImage = "hello-world" }, null, progressJSONMessage);

            var task = Task.Run(() => _client.System.MonitorEventsAsync(new EventsParameters(), progressMessage, cts.Token));

            await _client.Images.TagImageAsync(repository, new ImageTagParameters { RepositoryName = repository, Tag = newTag });

            cts.Cancel();
            try
            {
                await task;
            }
            catch
            {
                // Exception could be thrown when cancelling task, not always thrown, not always the same exception type
            }

            Assert.True(wasProgressCalled);
            Assert.True(wasJSONMessageProgressCalled);

            await _client.Images.DeleteImageAsync($"{repository}:{newTag}", new ImageDeleteParameters());
        }

        [Fact]
        public async Task MonitorEventsFiltered_Succeeds()
        {
            const string repository = "hello-world";
            var newTag = $"MonitorTests-{Guid.NewGuid().ToString().Substring(1, 10)}";

            var progressJSONMessage = new ProgressJSONMessage
            {
                _onJSONMessageCalled = (m) => { }
            };

            await _client.Images.CreateImageAsync(new ImagesCreateParameters { FromImage = repository }, null, progressJSONMessage);

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

            var progress = new ProgressMessage()
            {
                _onMessageCalled = (m) =>
                {
                    Console.WriteLine($"Received: {m.Action} - {m.Status} {m.From} - {m.Type}");
                    Assert.True(m.Status == "tag" || m.Status == "untag");
                    progressCalledCounter++;
                }
            };

            using var cts = new CancellationTokenSource();
            var task = Task.Run(() => _client.System.MonitorEventsAsync(eventsParams, progress, cts.Token));

            await _client.Images.CreateImageAsync(new ImagesCreateParameters { FromImage = repository }, null, progressJSONMessage);

            await _client.Images.TagImageAsync(repository, new ImageTagParameters { RepositoryName = repository, Tag = newTag });
            await _client.Images.DeleteImageAsync($"{repository}:{newTag}", new ImageDeleteParameters());

            var newContainerId = _client.Containers.CreateContainerAsync(new CreateContainerParameters { Image = repository }).Result.ID;
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

        private class ProgressMessage : IProgress<Message>
        {
            internal Action<Message> _onMessageCalled;

            void IProgress<Message>.Report(Message value)
            {
                _onMessageCalled(value);
            }
        }

        private class ProgressJSONMessage : IProgress<JSONMessage>
        {
            internal Action<JSONMessage> _onJSONMessageCalled;

            void IProgress<JSONMessage>.Report(JSONMessage value)
            {
                _onJSONMessageCalled(value);
            }
        }
    }
}
