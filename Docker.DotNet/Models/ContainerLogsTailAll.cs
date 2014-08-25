using System;

namespace Docker.DotNet.Models
{
	public class ContainerLogsTailAll : IContainerLogsTailMode
	{
		public ContainerLogsTailAll ()
		{
		}

		public string Value {
			get {
				return "all";
			}
		}
	}
}

