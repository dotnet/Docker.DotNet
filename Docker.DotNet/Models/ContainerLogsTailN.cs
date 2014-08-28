using System;
using System.Globalization;

namespace Docker.DotNet.Models
{
    public class ContainerLogsTailN : IContainerLogsTailMode
    {
        private int N { get; set; }

        public ContainerLogsTailN(int n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException("n");
            }
            this.N = n;
        }

        public string Value
        {
            get { return N.ToString(CultureInfo.InvariantCulture); }
        }
    }
}