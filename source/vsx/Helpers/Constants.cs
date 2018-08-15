namespace vsx.Helpers
{
    public static class Commands
    {
        public const string Vsx = "vsx";
        public const string Connect = "connect";
        public const string Disconnect = "disconnect";
        public const string Get = "get";
        public const string Set = "set";
        public const string Builds = "builds";
        public const string Releases = "releases";
        public const string TaskGroups = "taskgroups";
        public const string Tasks = "tasks";
        public const string List = "list";
        public const string Search = "search";
        public const string Details = "details";
    }

    public static class LogLevel
    {
        public const string Error = "error";
        public const string Info = "info";
        public const string Verbose = "verbose";
    }

    public static class DetailLevel
    {
        public const string Minimal = "minimal";
        public const string Normal = "normal";
        public const string Verbose = "verbose";
    }

    public static class OutputMode
    {
        public const string ConsoleOnly = "console";
        public const string FileOnly = "file";
        public const string Full = "full";
    }
}
