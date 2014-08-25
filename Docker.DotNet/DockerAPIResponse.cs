using System;
using System.Net;

namespace Docker.DotNet
{
	internal class DockerAPIResponse
	{
		public HttpStatusCode StatusCode { get; private set; }

		public string Body { get; private set; }

		public DockerAPIResponse (HttpStatusCode statusCode, string body)
		{
			this.StatusCode = statusCode;
			this.Body = body;
		}
	}
}

