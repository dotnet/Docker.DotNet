using System;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class JobStatus // (swarm.JobStatus)
    {
        [JsonPropertyName("JobIteration")]
        public Version JobIteration { get; set; }

        [JsonPropertyName("LastExecution")]
        public DateTime LastExecution { get; set; }
    }
}
