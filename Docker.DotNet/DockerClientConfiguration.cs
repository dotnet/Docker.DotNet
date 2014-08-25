using System;

namespace Docker.DotNet
{
	public class DockerClientConfiguration
	{
		public Uri EndpointBaseUri { get; private set; }

		public DockerClientConfiguration (Uri endpoint)
		{
			if (endpoint == null) {
				throw new ArgumentNullException ("endpoint");
			}

			this.EndpointBaseUri = endpoint;
		}

		public DockerClient CreateClient ()
		{
			return new DockerClient (this);
		}
	}
}

