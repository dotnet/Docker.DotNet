using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class PluginListResponse
    {
        [DataMember(Name = "Id", EmitDefaultValue = false)]
        public string ID { get; set; }

        [DataMember(Name = "Name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "Enabled", EmitDefaultValue = false)]
        public bool Enabled { get; set; }

        [DataMember(Name = "Settings", EmitDefaultValue = false)]
        public PluginSettings Settings { get; set; }

        [DataMember(Name = "PluginReference", EmitDefaultValue = false)]
        public string PluginReference { get; set; }

        [DataMember(Name = "Config", EmitDefaultValue = false)]
        public PluginConfig Config { get; set; }
    }
}
