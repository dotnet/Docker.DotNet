using System;
using System.Collections.Generic;

namespace Docker.DotNet.Models
{
    public class EventMessage
    {
        public string ID { get; set; }
        public string Status { get; set; }
        public string From { get; set; }

        public string Type { get; set; }
        public string Action { get; set; }
        public Actor Actor { get; set; }

        public DateTime Time { get; set; }
        public long TimeNano { get; set; }
    }


    public class Actor
    {
        public string ID { get; set; }
        public IDictionary<string, string[]> Attributes { get; set; }
    }
}
