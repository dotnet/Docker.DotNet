using System;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
	[DataContract]
	public class AuthConfig
	{
		[DataMember (Name = "username")]
		public string Username { get; set; }

		[DataMember (Name = "password")]
		public string Password { get; set; }

		[DataMember (Name = "email")]
		public string Email  { get; set; }

		[DataMember (Name = "serveraddress")]
		public string ServerAddress  { get; set; }

		public AuthConfig ()
		{
		}
	}
}

