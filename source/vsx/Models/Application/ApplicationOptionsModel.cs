using System.IO;

namespace vsx.Models
{
    public class ApplicationOptionsModel
    {
        public int CacheExpiration { get; set; } = 3600;

        public string DetailLevel { get; set; } = "normal";

        public string LogLevel { get; set; } = "info";

        public string OutputMode { get; set; } = "full";

        public string ResultsFolder { get; set; } = $@"{Directory.GetCurrentDirectory()}\results";

        public string ResultsFileName { get; set; } = "{timestamp}-{resourceType}-results.json";

        public string ConnectionFilePath { get; set; } = $@"{Directory.GetCurrentDirectory()}\connection.txt";

        public string ApplicationSettingsFilePath { get; set; } = $@"{Directory.GetCurrentDirectory()}\appsettings.json";
    }
}
