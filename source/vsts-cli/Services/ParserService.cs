using Microsoft.TeamFoundation.Build.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace vsx.Services
{
    public class ParserService : IParserService
    {
        public string SerializeBuildDetails(BuildDefinition buildDefinition) => JsonConvert.SerializeObject(buildDefinition, Formatting.Indented);
    }
}
