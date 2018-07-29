using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Contracts;
using vsx.Extensions;

namespace vsx.Services
{
    public class ReleaseDefinitionsService : IReleaseDefinitionsService
    {
        private readonly IConnectionService _connectionService;

        public ReleaseDefinitionsService(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task<IList<ReleaseDefinition>> GetReleaseDefinitions()
        {
            var client = await _connectionService.GetReleaseHttpClient();

            return await client.GetReleaseDefinitionsAsync(project: _connectionService.Project, expand: ReleaseDefinitionExpands.None);
        }

        public async Task<ReleaseDefinition> GetReleaseDefinitionById(string rawId)
        {
            var releaseId = rawId.EvaluateToId();
            var releaseDefinitions = await GetReleaseDefinitions();

            return releaseDefinitions.Where(x => x.Id == releaseId).FirstOrDefault();
        }
    }
}
