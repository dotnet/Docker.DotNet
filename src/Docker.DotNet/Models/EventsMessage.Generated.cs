using System;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class EventsMessage // (events.Message)
    {
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string ID { get; set; }

        [DataMember(Name = "from", EmitDefaultValue = false)]
        public string From { get; set; }

        [DataMember(Name = "Type", EmitDefaultValue = false)]
        public string Type { get; set; }

        [DataMember(Name = "Action", EmitDefaultValue = false)]
        public string Action { get; set; }

        [DataMember(Name = "Actor", EmitDefaultValue = false)]
        public Actor Actor { get; set; }

        [DataMember(Name = "time", EmitDefaultValue = false)]
        public DateTime Time { get; set; }

        [DataMember(Name = "timeNano", EmitDefaultValue = false)]
        public long TimeNano { get; set; }
    }
}
