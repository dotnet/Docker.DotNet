using System;
using System.Collections.Generic;
using Docker.DotNet.Models;
using Xunit;
using Xunit.Abstractions;

namespace Docker.DotNet.Tests
{
    [Collection(nameof(TestCollection))]
    public class IConfigOperationsTests
    {
        private readonly DockerClientConfiguration _dockerClientConfiguration;
        private readonly DockerClient _dockerClient;
        private readonly TestOutput _output;
        public IConfigOperationsTests(TestFixture testFixture, ITestOutputHelper outputHelper)
        {
            _dockerClientConfiguration = testFixture.DockerClientConfiguration;
            _dockerClient = _dockerClientConfiguration.CreateClient();
            _output = new TestOutput(outputHelper);
        }

        [Fact]
        public async void SwarmConfig_CanCreateAndRead()
        {
            var currentConfigs = await _dockerClient.Configs.ListConfigsAsync();

            _output.WriteLine($"Current Configs: {currentConfigs.Count}");
          
            var testConfigSpec = new SwarmConfigSpec
            {
                Name = $"Config-{Guid.NewGuid().ToString().Substring(1, 10)}",
                Labels = new Dictionary<string, string> { { "key", "value" } },
                Data = new List<byte> { 1, 2, 3, 4, 5 }
            };

            var configParameters = new SwarmCreateConfigParameters
            {
                Config = testConfigSpec
            };

            var createdConfig = await _dockerClient.Configs.CreateConfigAsync(configParameters);
            Assert.NotNull(createdConfig.ID);
            _output.WriteLine($"Config created: {createdConfig.ID}");

            var configs = await _dockerClient.Configs.ListConfigsAsync();
            Assert.Contains(configs, c => c.ID == createdConfig.ID);
            _output.WriteLine($"Current Configs: {configs.Count}");

            var configResponse = await _dockerClient.Configs.InspectConfigAsync(createdConfig.ID);

            Assert.NotNull(configResponse);

            Assert.Equal(configResponse.Spec.Name, testConfigSpec.Name);
            Assert.Equal(configResponse.Spec.Data, testConfigSpec.Data);
            Assert.Equal(configResponse.Spec.Labels, testConfigSpec.Labels);
            Assert.Equal(configResponse.Spec.Templating, testConfigSpec.Templating);


            _output.WriteLine($"Config created is the same.");

            await _dockerClient.Configs.RemoveConfigAsync(createdConfig.ID);
            
            await Assert.ThrowsAsync<Docker.DotNet.DockerApiException>(() => _dockerClient.Configs.InspectConfigAsync(createdConfig.ID));


            
        }
    }
}

