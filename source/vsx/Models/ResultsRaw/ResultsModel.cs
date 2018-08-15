using System.Collections.Generic;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.DistributedTask.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;

namespace vsx.Models
{
    public class ResultsModel
    {
        public ResultHeader Header { get; set; }

        public IList<TaskGroup> TaskGroups { get; set; }

        public IList<BuildDefinition> BuildDefinitions { get; set; }

        public IList<ReleaseDefinition> ReleaseDefinitions { get; set; }
    }
}
