using System;
using System.Collections.Generic;
using System.Linq;

namespace Docker.DotNet.Models
{
    public class ServicesListParameters
    {
        [QueryStringParameter("filters", false, typeof(MapQueryStringConverter))]
        public ServiceFilter Filters { get; set; }
    }

    public class ServiceFilter : Dictionary<string, string>
    {
        public string Id
        {
            get => this["id"];
            set => this["id"] = value;
        }
        public string Label
        {
            get => this["label"];
            set => this["label"] = value;
        }
        /// <summary> "replicated"|"global" </summary>
        public string Mode
        {
            get => this["mode"];
            set
            {
                if (!Enum.TryParse(value, true, out ServiceCreationMode _))
                {
                    var validValues = Enum.GetValues(typeof(ServiceCreationMode)).OfType<ServiceCreationMode>();
                    throw new Exception($"Invalid filter specified. 'Mode' should be on of '{string.Join(", ", validValues.Select(i => i.ToString()))}'");
                }

                this["mode"] = value;
            }
        }
        public string Name
        {
            get => this["name"];
            set => this["name"] = value;
        }

    }

    public enum ServiceCreationMode
    {
        Replicated,
        Global
    }
}