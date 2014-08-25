using System;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
	[DataContract]
	public class KeyValuePair
	{
		[DataMember(Name="Key")]
		public string Key { get; set; }

		[DataMember(Name="Value")]
		public string Value { get; set; }

		public KeyValuePair ()
		{
		}
	}
}

