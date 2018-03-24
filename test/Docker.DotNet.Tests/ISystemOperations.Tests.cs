using System;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using Xunit;

namespace Docker.DotNet.Tests
{
    public class ISystemOperationsTests
    {
        private readonly DockerClient _client;

        public ISystemOperationsTests()
        {
            _client = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient();
        }

        [Fact]
        public void DockerService_IsRunning()
        {
            using (var sc = new System.ServiceProcess.ServiceController("Docker"))
            {
                Assert.Equal(System.ServiceProcess.ServiceControllerStatus.Running, sc.Status);
            }
        }

        [Fact]
        public async Task GetVersionAsync_Succeeds()
        {
            var version = await _client.System.GetVersionAsync();
            Assert.NotNull(version.APIVersion);
        }

        [Fact]
        public async Task PingAsync_Succeeds()
        {
            await _client.System.PingAsync();
        }

        [Fact]
        public async Task GetSystemInfoAsync_Succeeds()
        {
            var info = await _client.System.GetSystemInfoAsync();
            Assert.NotNull(info.Architecture);
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
            const string repository = "microsoft/nanoserver";
            const string tag = "MonitorTests";

            var wasProgressCalled = false;

            var cts = new CancellationTokenSource();
            var progress = new Progress()
            {
                _onCalled = (m) =>
                {
                    Assert.NotNull(m);
                    Assert.Equal("tag", m.Status);

                    wasProgressCalled = true;
                }
            };

            var task = Task.Run(() => _client.System.MonitorEventsAsync(new ContainerEventsParameters(), progress, cts.Token));
            await _client.Images.TagImageAsync(repository, new ImageTagParameters { RepositoryName = repository, Tag = tag });
            cts.Cancel();
            await task;

            Assert.True(wasProgressCalled);

            await _client.Images.DeleteImageAsync($"{repository}:{tag}", new ImageDeleteParameters());
        }

        class Progress : IProgress<JSONMessage>
        {
            internal Action<JSONMessage> _onCalled;

            void IProgress<JSONMessage>.Report(JSONMessage value)
            {
                _onCalled(value);
            }
        }
    }
}
