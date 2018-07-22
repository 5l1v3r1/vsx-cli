using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.VisualStudio.Services.WebApi;

namespace vsx.Services
{
    public class BuildDefinitionsService : IBuildDefinitionsService
    {
        public async Task<IList<BuildDefinition>> GetBuildDefinitions(BuildHttpClient buildHttpClient, string project)
        {
            List<BuildDefinition> buildDefinitions = new List<BuildDefinition>();
            string continuationToken = null;

            do
            {
                IPagedList<BuildDefinition> buildDefinitionsPage = await buildHttpClient.GetFullDefinitionsAsync2(
                    project: project,
                    continuationToken: continuationToken);

                buildDefinitions.AddRange(buildDefinitionsPage);
                continuationToken = buildDefinitionsPage.ContinuationToken;

            } while (!string.IsNullOrEmpty(continuationToken));

            return buildDefinitions;
        }

        public Task<IList<BuildDefinition>> GetBuildDefinitionsByTaskId(BuildHttpClient buildHttpClient, string project, string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<BuildDefinition>> GetBuildDefinitionsByTaskName(BuildHttpClient buildHttpClient, string project, string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
