using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Docker.DotNet.Models
{
	[DataContract]
	public class NetworkSettings
	{
		[DataMember(Name="IPAddress")]
		public string IPAddress { get; set; }

		[DataMember(Name="Gateway")]
		public string Gateway { get; set; }

		[DataMember(Name="Bridge")]
		public string Bridge { get; set; }

		[DataMember(Name="IPPrefixLen")]
		public int IPPrefixLen { get; set; }

		[DataMember(Name="PortMapping")]
		public IDictionary<string, IDictionary<string, string>> PortMapping { get; set; }

		[DataMember(Name="Ports")]
		public IDictionary<string, IList<PortBinding>> Ports;

		public NetworkSettings ()
		{
		}
	}
}

