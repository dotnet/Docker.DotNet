using System;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using Xunit;

namespace Docker.DotNet.Tests
{
	public class IContainerOperationsTests : IDisposable
	{
		private readonly DockerClient _client;

		public IContainerOperationsTests()
		{
			_client = new DockerClientConfiguration().CreateClient();
		}

		[Fact]
		public async Task CreateImageAsync_NonexistantImage_ThrowsDockerImageNotFoundException()
		{
			var parameters = new CreateContainerParameters
			{
				Image = "no-such-image-ytfghbkufhresdhtrjygvb",
			};
			Func<Task> op = async () => await _client.Containers.CreateContainerAsync(parameters);

			await Assert.ThrowsAsync<DockerImageNotFoundException>(op);
		}

		public void Dispose()
		{
			_client.Dispose();
		}
	}
}
