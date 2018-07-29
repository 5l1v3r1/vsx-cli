using McMaster.Extensions.CommandLineUtils;
using System;
using System.Threading.Tasks;
using vsx.Models;
using vsx.Services;

namespace vsx.Commands
{
    public abstract class ExecutorBase
    {
        private readonly IConsole _console;
        private readonly IParserService _parseService;
        private readonly IFileService _fileService;
        private readonly CommandLineApplication _app;

        public ExecutorBase(IConsole console, IParserService parserService, IFileService fileService, CommandLineApplication app)
        {
            _console = console;
            _parseService = parserService;
            _fileService = fileService;
            _app = app;
        }

        [Option]
        public string AccountName { get; set; } = default;

        [Option]
        public string PersonalAccessToken { get; set; } = default;

        [Option]
        public string Project { get; set; }

        internal virtual CredentialsModel UseCredentialsFromOptions() 
            => new CredentialsModel(accountName: AccountName, 
                                    project: Project, 
                                    personalAccessToken: PersonalAccessToken);

        internal virtual int ConnectionError()
        {
            _console.ForegroundColor = ConsoleColor.Red;
            _console.WriteLine("Error during establishing connection.");
            return 0;
        }

        internal virtual async Task<int> GetResults()
        {
            switch (_app.Parent.Name)
            {
                default:
                case Commands.Tasks:
                    return await GetTaskResults();
                case Commands.Builds:
                    return await GetBuildResults();
                case Commands.Releases:
                    return await GetReleaseResults();
                case Commands.TaskGroups:
                    return await GetTaskGroupResults();
            }
        }

        internal virtual int ProcessResults<T>(T results)
        {
            var parsedResults = _parseService.SerializeBuildDetails(results);
            _fileService.SaveToJson(parsedResults);
            _console.WriteLine(parsedResults);

            return 1;
        }

        internal virtual Task<int> GetTaskResults() => Task.FromResult(0);

        internal virtual Task<int> GetBuildResults() => Task.FromResult(0);

        internal virtual Task<int> GetReleaseResults() => Task.FromResult(0);

        internal virtual Task<int> GetTaskGroupResults() => Task.FromResult(0);
    }
}
