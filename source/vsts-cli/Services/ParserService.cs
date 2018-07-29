using Newtonsoft.Json;

namespace vsx.Services
{
    public class ParserService : IParserService
    {
        public T DeserializeObject<T>(string input) => JsonConvert.DeserializeObject<T>(input);

        public string SerializeBuildDetails<T>(T buildDefinition) => JsonConvert.SerializeObject(buildDefinition, Formatting.Indented);

    }
}
