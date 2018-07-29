using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vsx.Services
{
    public interface IReleaseDefinitionsService
    {
        Task<IList<ReleaseDefinition>> GetReleaseDefinitions();

        Task<ReleaseDefinition> GetReleaseDefinitionById(string id);
    }
}
