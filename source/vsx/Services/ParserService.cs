using System.IO;
using Newtonsoft.Json;

namespace vsx.Services
{
    public class ParserService : IParserService
    {
        public T DeserializeObject<T>(string input) => JsonConvert.DeserializeObject<T>(input);

        public string SerializeDetails<T>(T buildDefinition) => JsonConvert.SerializeObject(buildDefinition, Formatting.Indented);

        public string ParseYamlFileName(string yamlFileName)
        {
            string result = Path.GetFileName(yamlFileName);
            return result;
        }
    }
}
