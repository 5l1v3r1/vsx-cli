using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;

namespace vsx.Services
{
    public interface IReleaseDefinitionsService
    {
        Task<IList<ReleaseDefinition>> GetReleaseDefinitions();

        Task<ReleaseDefinition> GetReleaseDefinitionById(int definitionId);

        Task<IList<ReleaseDefinition>> SearchForTaskInReleaseDefinitions(Guid taskId);
    }
}
