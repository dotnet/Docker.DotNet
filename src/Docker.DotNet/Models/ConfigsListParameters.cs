using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docker.DotNet.Models
{
    public class ConfigsListParameters
    {
        [QueryStringParameter("filters", false, typeof(MapQueryStringConverter))]
        public ConfigFilter Filters { get; set; }
    }

    public class ConfigFilter: Dictionary<string, string>
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

        public string Name
        {
            get => this["name"];
            set => this["name"] = value;
        }

        public string Names
        {
            get => this["names"];
            set => this["names"] = value;
        }
    }
}
