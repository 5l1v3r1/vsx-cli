using System;
using System.IO;

namespace vsx.Models
{
    [Serializable()]
    public class ApplicationSettingsModel : IModel
    {
        public int CacheExpiration { get; set; } = 3600;

        public LogLevel LogLevel { get; set; }

        public OutputMode OutputMode { get; set; }

        public string OutPutFilePath { get; set; }

        public virtual string GetSerializedModel() => File.ReadAllText($@"{Directory.GetCurrentDirectory()}\settings.txt");
    }

    public enum OutputMode
    {
        Console,
        File
    }

    public enum LogLevel
    {
        Error,
        Information,
        Verbose
    }
}
