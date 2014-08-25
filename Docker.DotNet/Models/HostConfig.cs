using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
	[DataContract]
	public class HostConfig
	{
		[DataMember(Name="Binds")]
		public IList<string> Binds { get; set; }

		[DataMember(Name="Links")]
		public IList<string> Links { get; set; }

		[DataMember(Name="ContainerIDFile")]
		public string ContainerIdFile { get; set; }

		[DataMember(Name="Privileged")]
		public bool Privileged { get; set; }

		[DataMember(Name="PortBindings")]
		public IDictionary<string, IList<PortBinding>> PortBindings;

		[DataMember(Name="PublishAllPorts")]
		public bool PublishAllPorts { get; set; }

		[DataMember(Name="Dns")]
		public IList<string> Dns { get; set; }

		[DataMember(Name="DnsSearch")]
		public IList<string> DnsSearch { get; set; }

		[DataMember(Name="VolumesFrom")]
		public IList<string> VolumesFrom { get; set; }

		[DataMember(Name="LxcConf")]
		public IList<KeyValuePair> LxcConf { get; set; }

		public HostConfig ()
		{
		}
	}
}

