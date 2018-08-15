using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using vsx.Models;
using VsxLogLevel = vsx.Helpers.LogLevel;
using VsxDetailLevel = vsx.Helpers.DetailLevel;
using VsxOutputMode = vsx.Helpers.OutputMode;

namespace vsx.Commands
{
    public abstract class ExecutorOptionsBase
    {
        public ExecutorOptionsBase(IConfiguration configuration)
        {
            configuration
                .GetSection(nameof(ApplicationOptionsModel))
                .Bind(ApplicationOptions);
        }

        public static ApplicationOptionsModel ApplicationOptions { get; set; } = new ApplicationOptionsModel();

        [Option(
            ShortName = "-acc",
            LongName = "--account",
            Description = "A valid VSTS account to connect to.")]
        public string AccountName { get; set; } = default;

        [Option(
            ShortName = "-pat",
            LongName = "--accessToken",
            Description = "A valid VSTS personal access token to use for the connection.")]
        public string PersonalAccessToken { get; set; } = default;

        [Option(
            ShortName = "-p",
            LongName = "--project",
            Description = "A valid VSTS project to work with.")]
        public string Project { get; set; } = default;

        [Option(
            ShortName = "-ll",
            LongName = "--logLevel",
            Description = "Set the log level of the application. Available levels are: error, info (default), verbose")]
        [AllowedValues(
            VsxLogLevel.Error,
            VsxLogLevel.Info,
            VsxLogLevel.Verbose,
            IgnoreCase = true,
            ErrorMessage = "Invalid log level.")]
        public string LogLevel { get; set; } = ApplicationOptions.DetailLevel ?? VsxLogLevel.Info;

        [Option(
            ShortName = "-dl",
            LongName = "--detailLevel",
            Description = "Set the detail level of the results. Available levels are: minimal, normal (default), verbose")]
        [AllowedValues(
            VsxDetailLevel.Minimal,
            VsxDetailLevel.Normal,
            VsxDetailLevel.Verbose,
            IgnoreCase = true,
            ErrorMessage = "Invalid detail level.")]
        public string DetailLevel { get; set; } = ApplicationOptions.DetailLevel ?? VsxDetailLevel.Normal;

        [Option(
            ShortName = "-om",
            LongName = "--outputMode",
            Description = "Set the output. Available levels are: console, file, full (default)")]
        [AllowedValues(
            VsxOutputMode.ConsoleOnly,
            VsxOutputMode.FileOnly,
            VsxOutputMode.Full,
            IgnoreCase = true,
            ErrorMessage = "Invalid output mode.")]
        public string OutputMode { get; set; } = ApplicationOptions.OutputMode ?? VsxOutputMode.Full;

        [Option(
            ShortName = "-sf",
            LongName = "--saveFolder",
            Description = "The path of the results folder. (default folder: {vsx executable}\results)")]
        public string FolderPath { get; set; } = ApplicationOptions.ResultsFolder ?? "normal";

        [Option(
            ShortName = "-fn",
            LongName = "--filename",
            Description = "Specific name for the result file. (default filename: {timestamp}-{resourceType}-results.json)")]
        public string FileName { get; set; } = ApplicationOptions.ResultsFileName ?? "normal";
    }
}
