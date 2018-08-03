namespace vsx.Services
{
    public interface IParserService
    {
        string SerializeBuildDetails<T>(T buildDefinition);

        T DeserializeObject<T>(string input);

        string ParseYamlFileName(string yamlFileName);
    }
}
