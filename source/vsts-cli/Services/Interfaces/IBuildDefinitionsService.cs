using Microsoft.TeamFoundation.Build.WebApi;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vsx.Services
{
    public interface IBuildDefinitionsService
    {
        Task<IList<BuildDefinition>> GetBuildDefinitions(BuildHttpClient buildHttpClient, string project);

        Task<BuildDefinition> GetBuildDefinitionById(BuildHttpClient buildHttpClient, string project, int id);

        Task<IList<BuildDefinition>> GetBuildDefinitionsByTaskId(BuildHttpClient buildHttpClient, string project, string id);

        Task<IList<BuildDefinition>> GetBuildDefinitionsByTaskName(BuildHttpClient buildHttpClient, string project, string name);
    }
}
