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

    public class ServiceFilter : Dictionary<string, string[]>
    {
        public string[] Id
        {
            get => this["id"];
            set => this["id"] = value;
        }
        public string[] Label
        {
            get => this["label"];
            set => this["label"] = value;
        }
        public ServiceCreationMode[] Mode
        {
            get
            {
                var lst = new ServiceCreationMode[this["mode"].Length];
                for(int i = 0; i< this["mode"].Length; i++)
                {
                    lst[i] = (ServiceCreationMode)Enum.Parse(typeof(ServiceCreationMode), this["mode"][i]);
                }
                return lst;   
            }
            set => this["mode"] = value.Select(m => m.ToString()).ToArray();
        }
        public string[] Name
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