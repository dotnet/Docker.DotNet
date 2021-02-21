using System;
using FluentAssertions;
using Xunit;
using CoreModels = Docker.DotNet.Models;

namespace DockerSdk.Tests
{
    // These tests can be done in parallel with other tests.
    public class ClientUnitTests
    {
        [Fact]
        public void DetermineVersionToUse_DaemonRangeEngulfsSdkRange_ReturnsSdkMax()
        {
            var input = new CoreModels.VersionResponse
            {
                MinAPIVersion = "1.11",
                APIVersion = "1.91",
            };
            var libMin = new Version("1.31");
            var libMax = new Version("1.71");

            var result = DockerClient.DetermineVersionToUse(libMin, input, libMax);

            result.ToString().Should().Be("1.71");
        }

        [Fact]
        public void DetermineVersionToUse_ExactlyOneVersionEach_SameVersion_ReturnsThatVersion()
        {
            var input = new CoreModels.VersionResponse
            {
                MinAPIVersion = "1.23",
                APIVersion = "1.23",
            };
            var libMin = new Version("1.23");
            var libMax = new Version("1.23");

            var result = DockerClient.DetermineVersionToUse(libMin, input, libMax);

            result.ToString().Should().Be("1.23");
        }

        [Fact]
        public void DetermineVersionToUse_NoOverlap_DaemonHigher_Throws()
        {
            var input = new CoreModels.VersionResponse
            {
                MinAPIVersion = "1.71",
                APIVersion = "1.91",
            };
            var libMin = new Version("1.11");
            var libMax = new Version("1.31");

            var ex = Assert.Throws<DockerVersionException>(
                () => DockerClient.DetermineVersionToUse(libMin, input, libMax));

            ex.Message.Should().Be("Version mismatch: The Docker daemon supports API versions v1.71 through v1.91, and the Docker SDK library supports API versions v1.11 through v1.31.");
        }

        [Fact]
        public void DetermineVersionToUse_NoOverlap_SdkHigher_Throws()
        {
            var input = new CoreModels.VersionResponse
            {
                MinAPIVersion = "1.11",
                APIVersion = "1.31",
            };
            var libMin = new Version("1.71");
            var libMax = new Version("1.91");

            var ex = Assert.Throws<DockerVersionException>(
                () => DockerClient.DetermineVersionToUse(libMin, input, libMax));

            ex.Message.Should().Be("Version mismatch: The Docker daemon only supports API versions up to v1.31, and the Docker SDK library only supports API versions down to v1.71.");
        }

        [Fact]
        public void DetermineVersionToUse_Overlap_DaemonHigher_ReturnsSdkMax()
        {
            var input = new CoreModels.VersionResponse
            {
                MinAPIVersion = "1.31",
                APIVersion = "1.91",
            };
            var libMin = new Version("1.11");
            var libMax = new Version("1.71");

            var result = DockerClient.DetermineVersionToUse(libMin, input, libMax);

            result.ToString().Should().Be("1.71");
        }

        [Fact]
        public void DetermineVersionToUse_Overlap_SdkHigher_ReturnsDaemonMax()
        {
            var input = new CoreModels.VersionResponse
            {
                MinAPIVersion = "1.11",
                APIVersion = "1.71",
            };
            var libMin = new Version("1.31");
            var libMax = new Version("1.91");

            var result = DockerClient.DetermineVersionToUse(libMin, input, libMax);

            result.ToString().Should().Be("1.71");
        }

        [Fact]
        public void DetermineVersionToUse_SdkRangeEngulfsDaemonRange_ReturnsDaemonMax()
        {
            var input = new CoreModels.VersionResponse
            {
                MinAPIVersion = "1.31",
                APIVersion = "1.71",
            };
            var libMin = new Version("1.11");
            var libMax = new Version("1.91");

            var result = DockerClient.DetermineVersionToUse(libMin, input, libMax);

            result.ToString().Should().Be("1.71");
        }
    }
}
