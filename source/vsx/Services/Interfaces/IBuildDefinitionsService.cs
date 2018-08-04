using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Build.WebApi;

namespace vsx.Services
{
    public interface IBuildDefinitionsService
    {
        Task<IList<BuildDefinition>> GetBuildDefinitions();

        Task<BuildDefinition> GetBuildDefinitionById(int definitionId);

        Task<BuildDefinition> GetBuildDefinitionByName(string definitionName);

        Task<IList<BuildDefinition>> SearchForTaskInBuildDefinitions(Guid taskId);

        Task<IList<BuildDefinition>> SearchForTaskInBuildDefinitions(Guid[] taskIds);
    }
}
