using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.VisualStudio.Services.WebApi;
using vsx.Extensions;

namespace vsx.Services
{
    public class BuildDefinitionsService : IBuildDefinitionsService
    {
        private readonly IConnectionService _connectionService;

        public BuildDefinitionsService(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task<IList<BuildDefinition>> GetBuildDefinitions()
        {
            var client = await _connectionService.GetBuildHttpClient();

            List<BuildDefinition> buildDefinitions = new List<BuildDefinition>();
            string continuationToken = null;

            do
            {
                IPagedList<BuildDefinition> buildDefinitionsPage = await client.GetFullDefinitionsAsync2(
                    project: _connectionService.Project,
                    continuationToken: continuationToken);

                buildDefinitions.AddRange(buildDefinitionsPage);
                continuationToken = buildDefinitionsPage.ContinuationToken;

            } while (!string.IsNullOrEmpty(continuationToken));

            return buildDefinitions;
        }

        public async Task<BuildDefinition> GetBuildDefinitionById(string rawId)
        {
            var definitionId = rawId.EvaluateToId();

            var definitions = await GetBuildDefinitions();
            return definitions.Where(x => x.Id == definitionId).FirstOrDefault();
        }

        public Task<IList<BuildDefinition>> GetBuildDefinitionsByTaskId(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<BuildDefinition>> GetBuildDefinitionsByTaskName(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
