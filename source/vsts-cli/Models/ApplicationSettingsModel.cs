using System;

namespace vsx.Models
{
    [Serializable()]
    public class ApplicationSettingsModel
    {
        public int CacheExpiration { get; set; } = 3600;

        public LogLevel LogLevel { get; set; }

        public OutputMode OutputMode { get; set; }

        public string OutPutFilePath { get; set; }
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
