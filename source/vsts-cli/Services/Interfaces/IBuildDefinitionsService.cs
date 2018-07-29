using Microsoft.TeamFoundation.Build.WebApi;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vsx.Services
{
    public interface IBuildDefinitionsService
    {
        Task<IList<BuildDefinition>> GetBuildDefinitions();

        Task<BuildDefinition> GetBuildDefinitionById(string rawId);

        Task<IList<BuildDefinition>> GetBuildDefinitionsByTaskId(string rawId);

        Task<IList<BuildDefinition>> GetBuildDefinitionsByTaskName(string name);
    }
}
