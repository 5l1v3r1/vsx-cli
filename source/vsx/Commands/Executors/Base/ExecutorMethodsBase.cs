using System;
using System.ComponentModel;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using vsx.Extensions;
using vsx.Models;
using vsx.Services;
using VsxCommand = vsx.Helpers.Commands;

namespace vsx.Commands
{
    public abstract class ExecutorMethodsBase : ExecutorOptionsBase
    {
        private readonly IConsole _console;
        private readonly IConnectionService _connectionService;
        private readonly IParserService _parserService;
        private readonly IFileService _fileService;
        private readonly CommandLineApplication _app;

        public ExecutorMethodsBase(
            IConfiguration configuration,
            IConsole console,
            IConnectionService connectionService,
            IParserService parserService,
            IFileService fileService,
            CommandLineApplication app)
            : base(configuration)
        {
            _console = console;
            _connectionService = connectionService;
            _parserService = parserService;
            _fileService = fileService;
            _app = app;
        }

        internal int OnExecute() => _connectionService.Connect(UseCredentialsFromOptions()) ? GetResults().Result : ConnectionError();

        internal ResultsModel GetResultsModel(Guid taskId)
            => new ResultsModel()
            {
                Header = new ResultHeader()
                {
                    Date = DateTimeOffset.UtcNow,
                    TaskId = taskId
                },
            };

        internal async Task<int> GetResults()
        {
            switch (_app.Parent.Name)
            {
                default:
                case VsxCommand.Tasks:
                    return await GetTaskResults();
                case VsxCommand.Builds:
                    return await GetBuildResults();
                case VsxCommand.Releases:
                    return await GetReleaseResults();
                case VsxCommand.TaskGroups:
                    return await GetTaskGroupResults();
            }
        }

        internal virtual T ParsePredicate<T>(string commandArgument)
        {
            if (typeof(T) == typeof(String)) return (T)(object)commandArgument;

            var converter = TypeDescriptor.GetConverter(typeof(T));

            return (T)converter.ConvertFrom(commandArgument);
        }

        internal virtual int ProcessResults<T>(T results)
        {
            // check detail level
            // parse to detail model if necessary

            // check folder/file for specific path

            var parsedResults = _parserService.SerializeDetails(results);

            if (OutputMode != "console")
            {
                _fileService.SaveToJson(parsedResults);
            }

            if (OutputMode != "file")
            {
                _console.WriteLine(parsedResults);
            }

            return 1;
        }

        internal virtual Task<int> GetTaskResults() => Task.FromResult(0);

        internal virtual Task<int> GetBuildResults() => Task.FromResult(0);

        internal virtual Task<int> GetReleaseResults() => Task.FromResult(0);

        internal virtual Task<int> GetTaskGroupResults() => Task.FromResult(0);

        private CredentialsModel UseCredentialsFromOptions()
            => new CredentialsModel(
                accountName: AccountName,
                project: Project,
                personalAccessToken: PersonalAccessToken);

        private int ConnectionError()
        {
            _console.ErrorMessage("Error during establishing connection.");
            return 0;
        }
    }
}
