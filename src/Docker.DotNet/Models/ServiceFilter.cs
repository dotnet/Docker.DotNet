using System;
using System.Collections.Generic;

namespace Docker.DotNet.Models
{
    public class ServicesListParameters
    {
        public ServiceFilter Filters { get; set; }
    }

    public class ServiceFilter : Dictionary<string, string>
    {
        public string Id
        {
            get => this["id"];
            set
            {
                if (ContainsKey("id"))
                    this["id"] = value;
                else
                    Add("id", value);
            }
        }
        public string Label
        {
            get => this["label"];
            set
            {
                if (ContainsKey("label"))
                    this["label"] = value;
                else
                    Add("label", value);
            }
        }
        /// <summary> "replicated"|"global" </summary>
        public string Mode
        {
            get => this["mode"];
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value != "replicated" && value != "global")
                    throw new Exception("Invalid filter specified. 'Mode' should be 'global' or 'replicated'");

                if (ContainsKey("mode"))
                    this["mode"] = value;
                else
                    Add("mode", value);
            }
        }
        public string Name
        {
            get => this["name"];
            set
            {
                if (ContainsKey("name"))
                    this["name"] = value;
                else
                    Add("name", value);
            }
        }
    }
}