using Microsoft.TeamFoundation.Build.WebApi;
using Newtonsoft.Json.Linq;

namespace vsx.Services
{
    public interface IParserService
    {
        string SerializeBuildDetails(BuildDefinition buildDefinition);
    }
}
