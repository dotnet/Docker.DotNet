using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Docker.DotNet.Models
{
    public class AttachContainerParameters
    {
        [QueryStringParameter("stdin",false, typeof(BoolQueryStringConverter))]
        public bool AttachStdIn { get;set; }

        [QueryStringParameter("stdout",false, typeof(BoolQueryStringConverter))]
        public bool AttachStdOut { get;set; }

        [QueryStringParameter("stderr",false, typeof(BoolQueryStringConverter))]
        public bool AttachStdErr { get;set; }

        [QueryStringParameter("logs",false, typeof(BoolQueryStringConverter))]
        public bool IncludeLogs { get;set; }

        [QueryStringParameter("stream",false, typeof(BoolQueryStringConverter))]
        public bool Streaming { get;set; }
    }
}
