namespace vsx.Services
{
    public interface IParserService
    {
        string SerializeDetails<T>(T buildDefinition);

        T DeserializeObject<T>(string input);

        string ParseYamlFileName(string yamlFileName);
    }
}
