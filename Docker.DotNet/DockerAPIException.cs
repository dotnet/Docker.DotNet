using System;
using System.Net;

namespace Docker.DotNet
{
	public class DockerAPIException : Exception
	{
		private HttpStatusCode StatusCode { get; set;}

		private string ResponseBody { get; set;}

		public DockerAPIException (HttpStatusCode statusCode, string responseBody)
			: base(string.Format("Docker API responded with status code={0}, response={1}", statusCode, responseBody))
		{
			this.StatusCode = statusCode;
			this.ResponseBody = responseBody;
		}
	}
}

