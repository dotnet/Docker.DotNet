using System;
using System.Collections.Generic;
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
	public class IVolumeOperationsTests
	{
		private readonly CancellationTokenSource _cts;

		private readonly DockerClient _dockerClient;

		public IVolumeOperationsTests(TestFixture testFixture, ITestOutputHelper outputHelper)
		{
			_dockerClient = testFixture.DockerClient;

			// Do not wait forever in case it gets stuck
			_cts = CancellationTokenSource.CreateLinkedTokenSource(testFixture.Cts.Token);
			_cts.CancelAfter(TimeSpan.FromMinutes(5));
			_cts.Token.Register(() => throw new TimeoutException("VolumeOperationsTests timeout"));
		}

		[Fact]
		public async Task ListAsync_VolumeExists_Succeeds()
		{
			const string volumeName = "docker-dotnet-test-volume";

			await _dockerClient.Volumes.CreateAsync(new VolumesCreateParameters
			{
				Name = volumeName,
			},
			_cts.Token);

			try
			{

				var response = await _dockerClient.Volumes.ListAsync(new VolumesListParameters()
				{
					Filters = new Dictionary<string, IDictionary<string, bool>>(),
				},
				_cts.Token);

				Assert.Contains(volumeName, response.Volumes.Select(volume => volume.Name));

			}
			finally
			{
				await _dockerClient.Volumes.RemoveAsync(volumeName, force: true, _cts.Token);
			}
		}
	}
}
