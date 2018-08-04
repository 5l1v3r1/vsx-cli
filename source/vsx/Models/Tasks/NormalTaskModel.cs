using System;
using System.Collections.Generic;
using Microsoft.TeamFoundation.DistributedTask.WebApi;

namespace vsx.Models
{
    [Serializable()]
    public class NormalTaskModel : MinimalTaskModel
    {
        public string Description { get; set; }

        public string DefinitionType { get; set; }

        public string Category { get; set; }

        public TaskVersion TaskVersion { get; set; }

        public IList<string> Visibility { get; set; }

        public string MinimumAgentVersion { get; set; }
    }
}
